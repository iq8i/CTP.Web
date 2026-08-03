/* ============================================= */
/* MODA Report Builder Create - JavaScript       */
/* إنشاء تقرير مخصص                              */
/* ============================================= */
(function () {
    'use strict';

    var availableFieldsData = [];
    var selectedFields = [];
    var filterCount = 0;
    var sortableInstance = null;

    document.addEventListener('DOMContentLoaded', function () {
        initSortable();
        setupDataSourceChange();
        checkEditMode();
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

    function showLoading() {
        var el = document.getElementById('loadingOverlay');
        if (el) el.classList.add('show');
    }

    function hideLoading() {
        var el = document.getElementById('loadingOverlay');
        if (el) el.classList.remove('show');
    }

    // Toast wrapper — يستخدم Moda إن توفّر، وإلا يستخدم rc-toast الأصلي
    function showToast(message, type) {
        if (typeof Moda !== 'undefined') {
            if (type === 'error') Moda.toastError(message);
            else if (type === 'warning') Moda.toastWarning(message);
            else if (type === 'info') Moda.toastInfo(message);
            else Moda.toastSuccess(message);
            return;
        }
        var toast = document.getElementById('rcToast');
        if (!toast) return;
        toast.className = 'rc-toast rc-toast-' + (type || 'success');
        toast.innerHTML = '<i class="bi ' + (type === 'error' ? 'bi-x-circle-fill' : type === 'warning' ? 'bi-exclamation-triangle-fill' : 'bi-check-circle-fill') + '"></i> ' + message;
        setTimeout(function () { toast.classList.add('show'); }, 50);
        setTimeout(function () { toast.classList.remove('show'); }, 3500);
    }

    /* ======================================== */
    /* SORTABLE INIT                             */
    /* ======================================== */
    function initSortable() {
        var el = document.getElementById('selectedFieldsList');
        if (!el || typeof Sortable === 'undefined') return;

        sortableInstance = new Sortable(el, {
            animation: 150,
            handle: '.rc-drag-handle',
            onEnd: function () {
                updateFieldOrder();
            }
        });
    }

    function updateFieldOrder() {
        selectedFields = [];
        var items = document.querySelectorAll('#selectedFieldsList .rc-selected-field');
        items.forEach(function (item) {
            selectedFields.push(item.getAttribute('data-field'));
        });
        // Re-render to update numbers
        renderSelectedFields();
        updateSortOptions();
    }

    /* ======================================== */
    /* DATA SOURCE                               */
    /* ======================================== */
    function setupDataSourceChange() {
        var select = document.getElementById('dataSource');
        if (!select) return;
        select.addEventListener('change', function () {
            loadAvailableFields();
        });
    }

    function loadAvailableFields() {
        var dataSource = document.getElementById('dataSource').value;
        if (!dataSource) return;

        var url = getUrl('fieldsUrl');
        fetch(url + '?dataSource=' + dataSource)
            .then(function (r) { return r.json(); })
            .then(function (response) {
                availableFieldsData = response.data || response;
                if (availableFieldsData && availableFieldsData.length > 0) {
                    renderAvailableFields();
                    updateSortOptions();
                } else {
                    var container = document.getElementById('availableFields');
                    if (container) {
                        container.innerHTML = '<div class="rc-fields-placeholder"><p>لا توجد حقول متاحة</p></div>';
                    }
                }
            })
            .catch(function () {
                showToast('فشل في تحميل الحقول المتاحة', 'error');
            });
    }

    /* ======================================== */
    /* AVAILABLE FIELDS RENDER                   */
    /* ======================================== */
    function renderAvailableFields() {
        var container = document.getElementById('availableFields');
        if (!container) return;

        var html = '';
        availableFieldsData.forEach(function (field) {
            var isSelected = selectedFields.indexOf(field.name) > -1;
            var typeLabel = field.typeLabel || field.type;
            var techName = field.englishName || field.name;
            html += '<div class="rc-field-item' + (isSelected ? ' selected' : '') + '" data-field="' + field.name + '" title="الاسم التقني: ' + techName + '" onclick="toggleField(\'' + field.name + '\')">';
            html += '<i class="bi ' + (isSelected ? 'bi-check-circle-fill' : 'bi-circle') + ' rc-field-check"></i>';
            html += '<div>';
            html += '<div class="rc-field-name">' + field.displayName + '</div>';
            html += '<div class="rc-field-type">' + typeLabel + '</div>';
            html += '</div>';
            html += '</div>';
        });
        container.innerHTML = html;
    }

    /* ======================================== */
    /* TOGGLE / REMOVE FIELD                     */
    /* ======================================== */
    window.toggleField = function (fieldName) {
        var index = selectedFields.indexOf(fieldName);
        if (index > -1) {
            selectedFields.splice(index, 1);
        } else {
            selectedFields.push(fieldName);
        }
        renderSelectedFields();
        renderAvailableFields();
        updateSortOptions();
    };

    window.removeField = function (fieldName) {
        var index = selectedFields.indexOf(fieldName);
        if (index > -1) {
            selectedFields.splice(index, 1);
        }
        renderSelectedFields();
        renderAvailableFields();
        updateSortOptions();
    };

    /* ======================================== */
    /* SELECTED FIELDS RENDER                    */
    /* ======================================== */
    function renderSelectedFields() {
        var container = document.getElementById('selectedFieldsList');
        var emptyEl = document.getElementById('builderEmpty');
        var zone = document.getElementById('builderZone');
        if (!container) return;

        if (selectedFields.length === 0) {
            container.innerHTML = '';
            if (emptyEl) emptyEl.style.display = '';
            if (zone) zone.classList.remove('has-fields');
            return;
        }

        if (emptyEl) emptyEl.style.display = 'none';
        if (zone) zone.classList.add('has-fields');

        var html = '';
        selectedFields.forEach(function (fieldName, index) {
            var field = availableFieldsData.find(function (f) { return f.name === fieldName; });
            if (!field) return;
            var typeLabel = field.typeLabel || field.type;
            var techName = field.englishName || field.name;
            html += '<div class="rc-selected-field" data-field="' + fieldName + '" title="الاسم التقني: ' + techName + '">';
            html += '<div class="rc-drag-handle"><i class="bi bi-grip-vertical"></i></div>';
            html += '<div class="rc-field-num">' + (index + 1) + '</div>';
            html += '<div class="rc-field-info">';
            html += '<div class="rc-field-info-name">' + field.displayName + '</div>';
            html += '<div class="rc-field-info-type">' + typeLabel + '</div>';
            html += '</div>';
            html += '<button type="button" class="rc-remove-btn" onclick="removeField(\'' + fieldName + '\')">';
            html += '<i class="bi bi-x-lg"></i>';
            html += '</button>';
            html += '</div>';
        });
        container.innerHTML = html;
    }

    /* ======================================== */
    /* SORT OPTIONS                              */
    /* ======================================== */
    function updateSortOptions() {
        var sortSelect = document.getElementById('sortField');
        if (!sortSelect) return;

        var html = '<option value="">بدون ترتيب</option>';
        selectedFields.forEach(function (fieldName) {
            var field = availableFieldsData.find(function (f) { return f.name === fieldName; });
            if (field) {
                html += '<option value="' + fieldName + '">' + field.displayName + '</option>';
            }
        });
        sortSelect.innerHTML = html;
    }

    /* ======================================== */
    /* FILTERS                                   */
    /* ======================================== */
    window.addFilter = function () {
        filterCount++;
        var container = document.getElementById('filtersList');
        if (!container) return;

        var fieldsOptions = '';
        availableFieldsData.forEach(function (f) {
            fieldsOptions += '<option value="' + f.name + '">' + f.displayName + '</option>';
        });

        var html = '<div class="rc-filter-item" id="filter' + filterCount + '">';
        html += '<div class="rc-filter-head">';
        html += '<span class="rc-filter-title">فلتر ' + filterCount + '</span>';
        html += '<button type="button" class="rc-filter-remove" onclick="removeFilter(' + filterCount + ')"><i class="bi bi-x-lg"></i></button>';
        html += '</div>';
        html += '<select class="rc-select filter-field"><option value="">اختر الحقل</option>' + fieldsOptions + '</select>';
        html += '<select class="rc-select filter-operator">';
        html += '<option value="equals">يساوي</option>';
        html += '<option value="contains">يحتوي على</option>';
        html += '<option value="startsWith">يبدأ بـ</option>';
        html += '<option value="endsWith">ينتهي بـ</option>';
        html += '<option value="greaterThan">أكبر من</option>';
        html += '<option value="lessThan">أقل من</option>';
        html += '</select>';
        html += '<input type="text" class="rc-input filter-value" placeholder="القيمة">';
        html += '</div>';

        container.insertAdjacentHTML('beforeend', html);
    };

    window.removeFilter = function (id) {
        var el = document.getElementById('filter' + id);
        if (el) el.remove();
    };

    /* ======================================== */
    /* COLLECT FILTERS / SORTING                 */
    /* ======================================== */
    function collectFilters() {
        var filters = [];
        var items = document.querySelectorAll('.rc-filter-item');
        items.forEach(function (item) {
            var field = item.querySelector('.filter-field').value;
            var operator = item.querySelector('.filter-operator').value;
            var value = item.querySelector('.filter-value').value;
            if (field && value) {
                filters.push({ field: field, operator: operator, value: value });
            }
        });
        return filters;
    }

    function collectSorting() {
        var sortField = document.getElementById('sortField');
        var sortDir = document.getElementById('sortDirection');
        if (!sortField || !sortField.value) return null;
        return {
            field: sortField.value,
            direction: sortDir ? sortDir.value : 'asc'
        };
    }

    /* ======================================== */
    /* SAVE REPORT                               */
    /* ======================================== */
    window.saveReport = function () {
        var name = document.getElementById('reportName').value;
        var dataSource = document.getElementById('dataSource').value;

        if (!name) { showToast('يرجى إدخال اسم التقرير', 'warning'); return; }
        if (!dataSource) { showToast('يرجى اختيار مصدر البيانات', 'warning'); return; }
        if (selectedFields.length === 0) { showToast('يرجى اختيار حقل واحد على الأقل', 'warning'); return; }

        var reportId = parseInt(document.getElementById('reportId').value || '0');
        var report = {
            reportId: reportId,
            reportName: name,
            description: document.getElementById('reportDescription').value,
            dataSource: dataSource,
            selectedFields: JSON.stringify(selectedFields),
            filters: JSON.stringify(collectFilters()),
            sorting: JSON.stringify(collectSorting()),
            reportType: document.getElementById('reportType').value,
            isPublic: document.getElementById('isPublic').checked,
            reportTitle: document.getElementById('reportTitle').value,
            pdfHeader: document.getElementById('pdfHeader').value,
            pdfFooter: document.getElementById('pdfFooter').value,
            includePageNumbers: document.getElementById('includePageNumbers').checked,
            includeGenerationDate: document.getElementById('includeGenerationDate').checked
        };

        var url = reportId > 0 ? getUrl('updateUrl') : getUrl('saveUrl');
        showLoading();

        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': getToken()
            },
            body: JSON.stringify(report)
        })
        .then(function (r) {
            if (!r.ok) return r.text().then(function (t) { throw new Error('HTTP ' + r.status + ' — ' + (t || '')); });
            return r.json();
        })
        .then(function (response) {
            hideLoading();
            if (response.success) {
                Moda.toastSuccess('تم حفظ التقرير بنجاح');
                setTimeout(function () {
                    window.location.href = getUrl('indexUrl');
                }, 1200);
            } else {
                if (response.errors && response.errors.length) {
                    Moda.showErrors(response.errors, response.message || 'فشل في حفظ التقرير');
                } else {
                    Moda.error('فشل الحفظ', response.message || 'تعذّر حفظ التقرير');
                }
            }
        })
        .catch(function (err) {
            hideLoading();
            Moda.error('خطأ', 'حدث خطأ أثناء الحفظ: ' + (err.message || err));
        });
    };

    /* ======================================== */
    /* PREVIEW REPORT                            */
    /* ======================================== */
    window.previewReport = function () {
        var dataSource = document.getElementById('dataSource').value;
        if (!dataSource) { Moda.warning('تنبيه', 'يرجى اختيار مصدر البيانات'); return; }
        if (selectedFields.length === 0) { Moda.warning('تنبيه', 'يرجى اختيار حقل واحد على الأقل'); return; }

        var request = {
            report: {
                dataSource: dataSource,
                selectedFields: JSON.stringify(selectedFields),
                reportName: document.getElementById('reportName').value || 'معاينة التقرير',
                filters: JSON.stringify(collectFilters()),
                sorting: JSON.stringify(collectSorting())
            },
            pageNumber: 1,
            pageSize: 100
        };

        Moda.loading('جاري تنفيذ التقرير...');

        fetch(getUrl('executeUrl'), {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': getToken()
            },
            body: JSON.stringify(request)
        })
        .then(function (r) {
            if (!r.ok) return r.text().then(function (t) { throw new Error('HTTP ' + r.status + ' — ' + (t || '')); });
            return r.json();
        })
        .then(function (response) {
            Moda.closeLoading();
            if (response.success) {
                if (!response.data || response.data.length === 0) {
                    Moda.info('لا توجد بيانات',
                        'لم يُرجع التقرير أي سجلات. تحقّق من الفلاتر أو من توفّر البيانات في الجدول المختار.');
                    return;
                }
                showPreviewModal(response.data, response.fields, response.pagination);
            } else {
                if (response.errors && response.errors.length) {
                    Moda.showErrors(response.errors, response.message || 'إعدادات التقرير غير صالحة');
                } else {
                    Moda.error('فشل التنفيذ', response.message || 'تعذّر تنفيذ التقرير');
                }
            }
        })
        .catch(function (err) {
            Moda.closeLoading();
            Moda.error('خطأ', 'حدث خطأ أثناء المعاينة: ' + (err.message || err));
        });
    };

    /* ======================================== */
    /* PREVIEW MODAL                             */
    /* ======================================== */
    function showPreviewModal(data, fields, pagination) {
        var overlay = document.getElementById('previewModal');
        var body = document.getElementById('previewBody');
        if (!overlay || !body) return;

        var html = '<div style="overflow-x:auto"><table class="rc-results-table"><thead><tr>';
        fields.forEach(function (f) {
            var fieldData = availableFieldsData.find(function (fd) { return fd.name === f; });
            html += '<th>' + (fieldData ? fieldData.displayName : f) + '</th>';
        });
        html += '</tr></thead><tbody>';

        data.forEach(function (row) {
            html += '<tr>';
            fields.forEach(function (f) {
                html += '<td>' + (row[f] != null ? row[f] : '') + '</td>';
            });
            html += '</tr>';
        });
        html += '</tbody></table></div>';

        if (pagination) {
            html += '<div class="rc-pagination-info">';
            html += '<strong>النتائج:</strong> صفحة ' + pagination.pageNumber + ' من ' + pagination.totalPages;
            html += ' | إجمالي السجلات: ' + pagination.totalRecords;
            html += '</div>';
        }

        body.innerHTML = html;
        overlay.classList.add('show');
    }

    window.closePreviewModal = function () {
        var overlay = document.getElementById('previewModal');
        if (overlay) overlay.classList.remove('show');
    };

    /* ======================================== */
    /* LOAD EXISTING REPORT (EDIT MODE)          */
    /* ======================================== */
    function checkEditMode() {
        var params = new URLSearchParams(window.location.search);
        var reportId = params.get('id');
        if (reportId) {
            loadExistingReport(reportId);
        }
    }

    function loadExistingReport(reportId) {
        showLoading();
        fetch(getUrl('loadUrl') + '/' + reportId)
            .then(function (r) { return r.json(); })
            .then(function (report) {
                document.getElementById('reportId').value = report.reportId;
                document.getElementById('reportName').value = report.reportName || '';
                document.getElementById('reportDescription').value = report.description || '';
                document.getElementById('dataSource').value = report.dataSource || '';
                document.getElementById('reportType').value = report.reportType || 'Table';
                document.getElementById('isPublic').checked = !!report.isPublic;

                if (report.reportTitle) document.getElementById('reportTitle').value = report.reportTitle;
                if (report.pdfHeader) document.getElementById('pdfHeader').value = report.pdfHeader;
                if (report.pdfFooter) document.getElementById('pdfFooter').value = report.pdfFooter;
                if (report.includePageNumbers !== undefined) document.getElementById('includePageNumbers').checked = report.includePageNumbers;
                if (report.includeGenerationDate !== undefined) document.getElementById('includeGenerationDate').checked = report.includeGenerationDate;

                selectedFields = report.selectedFields ? JSON.parse(report.selectedFields) : [];

                // Update header title
                var titleEl = document.querySelector('.rc-header-title h1');
                if (titleEl) titleEl.textContent = 'تعديل التقرير';

                // Load fields then render
                var dsUrl = getUrl('fieldsUrl');
                return fetch(dsUrl + '?dataSource=' + report.dataSource)
                    .then(function (r) { return r.json(); })
                    .then(function (response) {
                        availableFieldsData = response.data || response;
                        renderAvailableFields();
                        renderSelectedFields();
                        updateSortOptions();

                        // Restore sorting
                        if (report.sorting) {
                            try {
                                var sorting = JSON.parse(report.sorting);
                                if (sorting && sorting.field) {
                                    setTimeout(function () {
                                        document.getElementById('sortField').value = sorting.field;
                                        document.getElementById('sortDirection').value = sorting.direction || 'asc';
                                    }, 100);
                                }
                            } catch (e) {}
                        }

                        // Restore filters
                        if (report.filters) {
                            try {
                                var filters = JSON.parse(report.filters);
                                filters.forEach(function (f) {
                                    addFilter();
                                    var lastFilter = document.querySelector('.rc-filter-item:last-child');
                                    if (lastFilter) {
                                        lastFilter.querySelector('.filter-field').value = f.field;
                                        lastFilter.querySelector('.filter-operator').value = f.operator;
                                        lastFilter.querySelector('.filter-value').value = f.value;
                                    }
                                });
                            } catch (e) {}
                        }

                        hideLoading();
                    });
            })
            .catch(function () {
                hideLoading();
                showToast('فشل في تحميل التقرير', 'error');
            });
    }

})();
