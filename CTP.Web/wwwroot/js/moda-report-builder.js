/* ============================================= */
/* MODA Report Builder - JavaScript              */
/* منشئ التقارير المخصصة                          */
/* ============================================= */
(function () {
    'use strict';

    var currentFilter = 'all';

    document.addEventListener('DOMContentLoaded', function () {
        setupFilterTabs();
        setupToastFromTempData();
    });

    /* ======================================== */
    /* HELPERS                                   */
    /* ======================================== */
    function getUrl(id) {
        var el = document.getElementById(id);
        return el ? el.value : '';
    }

    function getToken() {
        var el = document.querySelector('input[name="__RequestVerificationToken"]');
        return el ? el.value : '';
    }

    function setupToastFromTempData() {
        var successEl = document.getElementById('tempSuccess');
        var errorEl = document.getElementById('tempError');
        if (successEl && successEl.value) Moda.toastSuccess(successEl.value);
        if (errorEl && errorEl.value) Moda.error('خطأ', errorEl.value);
    }

    /* ======================================== */
    /* FILTER TABS                               */
    /* ======================================== */
    function setupFilterTabs() {
        var buttons = document.querySelectorAll('.rb-filter-btn');
        buttons.forEach(function (btn) {
            btn.addEventListener('click', function () {
                buttons.forEach(function (b) { b.classList.remove('active'); });
                btn.classList.add('active');
                currentFilter = btn.getAttribute('data-filter');
                applyFilter();
            });
        });
    }

    function applyFilter() {
        var cards = document.querySelectorAll('.rb-report-item');
        cards.forEach(function (card) {
            var isFav = card.getAttribute('data-favorite') === 'true';
            var isPub = card.getAttribute('data-public') === 'true';
            var show = true;
            if (currentFilter === 'favorites' && !isFav) show = false;
            if (currentFilter === 'public' && !isPub) show = false;
            if (currentFilter === 'private' && isPub) show = false;
            card.style.display = show ? '' : 'none';
        });
    }

    /* ======================================== */
    /* EXECUTE REPORT                            */
    /* ======================================== */
    var lastResultData = null;
    var lastResultFields = null;
    var lastFieldLabels = {};

    window.executeReport = function (reportId) {
        var loadUrl = getUrl('loadReportUrl');
        var execUrl = getUrl('executeReportUrl');

        Moda.loading('جاري تشغيل التقرير...');

        // 1) تحميل بيانات التقرير
        fetch(loadUrl + '/' + reportId)
            .then(function (r) {
                if (!r.ok) throw new Error('فشل تحميل التقرير');
                return r.json();
            })
            .then(function (report) {
                // 2) تحميل أسماء الحقول العربية لمصدر البيانات
                return fetch('/Reporting/ReportBuilder/GetAvailableFields?dataSource=' + encodeURIComponent(report.dataSource || ''))
                    .then(function (r) { return r.json(); })
                    .then(function (fieldsResp) {
                        lastFieldLabels = {};
                        var data = fieldsResp.data || fieldsResp || [];
                        data.forEach(function (f) { lastFieldLabels[f.name] = f.displayName || f.name; });
                        return report;
                    });
            })
            .then(function (report) {
                // 3) تنفيذ التقرير — body بالشكل الصحيح المتوقّع من الـendpoint
                return fetch(execUrl, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': getToken()
                    },
                    body: JSON.stringify({
                        report: report,
                        pageNumber: 1,
                        pageSize: 1000
                    })
                });
            })
            .then(function (r) {
                if (!r.ok) {
                    return r.text().then(function (txt) {
                        throw new Error('HTTP ' + r.status + ' — ' + (txt || ''));
                    });
                }
                return r.json();
            })
            .then(function (response) {
                Moda.closeLoading();
                if (response.success) {
                    var totalRecords = (response.pagination && response.pagination.totalRecords) || (response.data ? response.data.length : 0);
                    if (!response.data || response.data.length === 0) {
                        Moda.info('لا توجد بيانات', 'لم يُرجع التقرير أي سجلات. تحقّق من الفلاتر أو من توفّر البيانات في القاعدة.');
                        return;
                    }
                    showResultsModal(response.data, response.fields || [], totalRecords);
                } else {
                    var msg = response.message || 'فشل في تنفيذ التقرير';
                    if (response.errors && response.errors.length) {
                        Moda.showErrors(response.errors, msg);
                    } else {
                        Moda.error('فشل التنفيذ', msg);
                    }
                }
            })
            .catch(function (err) {
                Moda.closeLoading();
                Moda.error('خطأ', 'حدث خطأ أثناء تنفيذ التقرير: ' + (err.message || err));
            });
    };

    /* ======================================== */
    /* RESULTS MODAL                             */
    /* ======================================== */
    function escapeHtml(s) {
        if (s === null || s === undefined) return '';
        return String(s).replace(/[&<>"']/g, function (c) {
            return ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' })[c];
        });
    }

    function showResultsModal(data, fields, totalCount) {
        lastResultData = data;
        lastResultFields = fields;

        var overlay = document.getElementById('resultsModal');
        var body = document.getElementById('resultsBody');
        var countEl = document.getElementById('resultsCount');
        if (!overlay || !body) {
            // فالباك في حال غير صفحة Index
            Moda.info('عدد السجلات', 'تم استرجاع ' + totalCount + ' سجلاً.');
            return;
        }

        if (countEl) countEl.textContent = totalCount;

        var html = '<div style="overflow-x:auto;"><table class="rb-results-table"><thead><tr>';
        fields.forEach(function (f) {
            var label = lastFieldLabels[f] || f;
            html += '<th>' + escapeHtml(label) + '</th>';
        });
        html += '</tr></thead><tbody>';

        data.forEach(function (row) {
            html += '<tr>';
            fields.forEach(function (f) {
                var val = row[f];
                html += '<td>' + (val !== null && val !== undefined && val !== '' ? escapeHtml(val) : '-') + '</td>';
            });
            html += '</tr>';
        });

        html += '</tbody></table></div>';
        body.innerHTML = html;
        overlay.classList.add('show');
    }

    window.closeResultsModal = function () {
        var overlay = document.getElementById('resultsModal');
        if (overlay) overlay.classList.remove('show');
    };

    window.exportResultsExcel = function () {
        if (!lastResultData || !lastResultFields || typeof XLSX === 'undefined') {
            Moda.warning('تنبيه', 'لا توجد بيانات للتصدير');
            return;
        }
        // تجهيز البيانات بأسماء أعمدة عربية
        var rows = lastResultData.map(function (row) {
            var arabicRow = {};
            lastResultFields.forEach(function (f) {
                var label = lastFieldLabels[f] || f;
                arabicRow[label] = row[f] !== null && row[f] !== undefined ? row[f] : '';
            });
            return arabicRow;
        });
        var ws = XLSX.utils.json_to_sheet(rows);
        var wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, 'Report');
        XLSX.writeFile(wb, 'Report_' + new Date().toISOString().slice(0, 10) + '.xlsx');
        Moda.toastSuccess('تم تصدير الملف');
    };

    /* ======================================== */
    /* TOGGLE FAVORITE                           */
    /* ======================================== */
    window.toggleFavorite = function (reportId, button) {
        var url = getUrl('toggleFavoriteUrl');

        fetch(url + '/' + reportId, {
            method: 'POST',
            headers: { 'RequestVerificationToken': getToken() }
        })
            .then(function (r) { return r.json(); })
            .then(function (response) {
                if (response.success) {
                    var icon = button.querySelector('i');
                    var card = button.closest('.rb-report-item');
                    var reportCard = button.closest('.rb-report-card');

                    if (response.isFavorite) {
                        icon.className = 'bi bi-star-fill';
                        card.setAttribute('data-favorite', 'true');
                        if (!reportCard.querySelector('.rb-fav-badge')) {
                            var badge = document.createElement('div');
                            badge.className = 'rb-fav-badge';
                            badge.innerHTML = '<i class="bi bi-star-fill"></i>';
                            reportCard.insertBefore(badge, reportCard.firstChild);
                        }
                    } else {
                        icon.className = 'bi bi-star';
                        card.setAttribute('data-favorite', 'false');
                        var badge = reportCard.querySelector('.rb-fav-badge');
                        if (badge) badge.remove();
                    }

                    updateStatNumbers();
                    Moda.toastSuccess(response.isFavorite ? 'تمت الإضافة للمفضلة' : 'تمت الإزالة من المفضلة');
                } else {
                    Moda.error('خطأ', response.message || 'فشل تحديث المفضلة');
                }
            })
            .catch(function () {
                Moda.error('خطأ', 'حدث خطأ في تحديث المفضلة');
            });
    };

    function updateStatNumbers() {
        var cards = document.querySelectorAll('.rb-report-item');
        var total = cards.length;
        var fav = 0, pub = 0, priv = 0;
        cards.forEach(function (c) {
            if (c.getAttribute('data-favorite') === 'true') fav++;
            if (c.getAttribute('data-public') === 'true') pub++;
            else priv++;
        });
        var totalEl = document.getElementById('statTotal');
        var favEl = document.getElementById('statFavorites');
        var pubEl = document.getElementById('statPublic');
        var privEl = document.getElementById('statPrivate');
        if (totalEl) totalEl.textContent = total;
        if (favEl) favEl.textContent = fav;
        if (pubEl) pubEl.textContent = pub;
        if (privEl) privEl.textContent = priv;
    }

    /* ======================================== */
    /* DELETE REPORT                             */
    /* ======================================== */
    window.deleteReport = function (reportId) {
        Moda.confirmDelete({
            title: 'حذف التقرير',
            text: 'هل أنت متأكد من حذف هذا التقرير؟ لا يمكن التراجع.'
        }).then(function (res) {
            if (!res.isConfirmed) return;

            var url = getUrl('deleteReportUrl');
            Moda.loading('جاري الحذف...');

            fetch(url + '/' + reportId, {
                method: 'POST',
                headers: { 'RequestVerificationToken': getToken() }
            })
                .then(function (r) { return r.json(); })
                .then(function (response) {
                    Moda.closeLoading();
                    if (response.success) {
                        Moda.toastSuccess('تم حذف التقرير بنجاح');
                        setTimeout(function () { location.reload(); }, 1000);
                    } else {
                        Moda.error('فشل الحذف', response.message || 'تعذّر حذف التقرير');
                    }
                })
                .catch(function () {
                    Moda.closeLoading();
                    Moda.error('خطأ', 'حدث خطأ أثناء الحذف');
                });
        });
    };

    /* ======================================== */
    /* EXPORT TO PDF                             */
    /* ======================================== */
    window.exportToPdf = function (reportId) {
        var url = getUrl('exportPdfUrl');
        window.open(url + '?reportId=' + reportId, '_blank');
    };

    /* ======================================== */
    /* LOAD (EDIT) REPORT                        */
    /* ======================================== */
    window.loadReport = function (reportId) {
        var url = getUrl('editReportUrl');
        window.location.href = url + '?id=' + reportId;
    };

})();
