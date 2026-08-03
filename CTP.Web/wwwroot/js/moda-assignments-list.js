/* ============================================= */
/* MODA Assignments List - JavaScript            */
/* قائمة المكلفين والملحقين                       */
/* ============================================= */
(function () {
    'use strict';

    var currentResults = [];

    document.addEventListener('DOMContentLoaded', function () {
        setupSearch();
        setupClear();
        setupExports();
        setupEnterKey();
    });

    /* ======================================== */
    /* SEARCH                                    */
    /* ======================================== */
    function setupSearch() {
        var btn = document.getElementById('searchBtn');
        if (!btn) return;
        btn.addEventListener('click', performSearch);
    }

    function performSearch() {
        var serial = getValue('searchSerial');
        var name = getValue('searchName');
        var unit = getValue('searchUnit');
        var rank = getValue('searchRank');

        if (!serial && !name && !unit && !rank) {
            alert('الرجاء إدخال معيار بحث واحد على الأقل');
            return;
        }

        var btn = document.getElementById('searchBtn');
        if (!btn) return;

        var originalHTML = btn.innerHTML;
        btn.disabled = true;
        btn.innerHTML = '<span class="os-spinner"></span> جاري البحث...';

        var searchUrl = document.getElementById('searchUrl');
        if (!searchUrl) return;

        var params = new URLSearchParams();
        if (serial) params.append('serial', serial);
        if (name) params.append('name', name);
        if (unit) params.append('unit', unit);
        if (rank) params.append('rank', rank);

        fetch(searchUrl.value + '?' + params.toString())
            .then(function (r) {
                if (!r.ok) throw new Error('فشل البحث');
                return r.json();
            })
            .then(function (data) {
                displayResults(data);
            })
            .catch(function (err) {
                alert('خطأ: ' + err.message);
            })
            .finally(function () {
                btn.disabled = false;
                btn.innerHTML = originalHTML;
            });
    }

    /* ======================================== */
    /* DISPLAY RESULTS                           */
    /* ======================================== */
    function displayResults(data) {
        currentResults = data;

        var initial = document.getElementById('initialState');
        var results = document.getElementById('resultsCard');
        var noResults = document.getElementById('noResults');
        var table = document.getElementById('assignedTable');
        var count = document.getElementById('resultCount');
        var tbody = document.getElementById('resultsBody');

        if (initial) initial.style.display = 'none';
        if (results) results.classList.add('show');

        if (!data || data.length === 0) {
            if (noResults) noResults.classList.add('show');
            if (table) table.style.display = 'none';
            if (count) count.textContent = '0 نتيجة';
            return;
        }

        if (noResults) noResults.classList.remove('show');
        if (table) table.style.display = '';
        if (count) count.textContent = data.length.toLocaleString('en') + ' نتيجة';

        if (!tbody) return;
        tbody.innerHTML = '';

        var detailsUrl = document.getElementById('detailsUrl');
        var editUrl = document.getElementById('editUrl');
        var baseDetailsUrl = detailsUrl ? detailsUrl.value : '';
        var baseEditUrl = editUrl ? editUrl.value : '';

        data.forEach(function (item) {
            var tr = document.createElement('tr');

            var statusHTML = item.statusCode == 1
                ? '<span class="os-status-badge active"><i class="bi bi-check-circle-fill"></i> نشط</span>'
                : '<span class="os-status-badge inactive"><i class="bi bi-dash-circle-fill"></i> غير نشط</span>';

            var unitTo = item.unitToNumber ? 'مكلف في ' + item.unitToNumber : '-';
            var detailLink = baseDetailsUrl ? baseDetailsUrl + '/' + (item.nbrSerial || '') : '#';
            var editLink = baseEditUrl ? baseEditUrl + '/' + (item.nbrSerial || '') : '#';

            tr.innerHTML =
                '<td><span class="os-serial-badge">' + (item.nbrSerial || '') + '</span></td>' +
                '<td><strong>' + (item.employeeName || '-') + '</strong></td>' +
                '<td>' + (item.rankDescription || '-') + '</td>' +
                '<td>' + (item.unitName || '-') + '</td>' +
                '<td>' + unitTo + '</td>' +
                '<td>' + statusHTML + '</td>' +
                '<td>' +
                    '<a href="' + detailLink + '" class="os-view-btn"><i class="bi bi-eye-fill"></i> عرض</a> ' +
                    '<a href="' + editLink + '" class="os-view-btn"><i class="bi bi-pencil-fill"></i> تعديل</a>' +
                '</td>';

            tbody.appendChild(tr);
        });
    }

    /* ======================================== */
    /* CLEAR                                     */
    /* ======================================== */
    function setupClear() {
        var btn = document.getElementById('clearBtn');
        if (!btn) return;

        btn.addEventListener('click', function () {
            var form = document.getElementById('searchForm');
            if (form) form.reset();

            var results = document.getElementById('resultsCard');
            if (results) results.classList.remove('show');

            var initial = document.getElementById('initialState');
            if (initial) initial.style.display = '';

            currentResults = [];
        });
    }

    /* ======================================== */
    /* ENTER KEY                                 */
    /* ======================================== */
    function setupEnterKey() {
        var form = document.getElementById('searchForm');
        if (!form) return;

        var inputs = form.querySelectorAll('input');
        for (var i = 0; i < inputs.length; i++) {
            inputs[i].addEventListener('keypress', function (e) {
                if (e.which === 13 || e.keyCode === 13) {
                    e.preventDefault();
                    performSearch();
                }
            });
        }
    }

    /* ======================================== */
    /* EXPORTS                                   */
    /* ======================================== */
    function setupExports() {
        var excelBtn = document.getElementById('exportExcelBtn');
        var pdfBtn = document.getElementById('exportPdfBtn');
        var csvBtn = document.getElementById('exportCsvBtn');

        if (excelBtn) excelBtn.addEventListener('click', exportToExcel);
        if (pdfBtn) pdfBtn.addEventListener('click', exportToPdf);
        if (csvBtn) csvBtn.addEventListener('click', exportToCsv);
    }

    function exportToExcel() {
        if (currentResults.length === 0) {
            alert('لا توجد بيانات للتصدير');
            return;
        }

        var exportData = currentResults.map(function (item) {
            return {
                'الرقم التسلسلي': item.nbrSerial || '',
                'اسم الموظف': item.employeeName || '',
                'الرتبة': item.rankDescription || '',
                'الوحدة الأصلية': item.unitName || '',
                'الوحدة المكلف بها': item.unitToNumber || '',
                'الوضع': item.statusCode == 1 ? 'نشط' : 'غير نشط'
            };
        });

        var ws = XLSX.utils.json_to_sheet(exportData);
        var wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, 'المكلفون');

        var fileName = 'Assigned_' + new Date().toISOString().split('T')[0] + '.xlsx';
        XLSX.writeFile(wb, fileName);
    }

    function exportToPdf() {
        if (currentResults.length === 0) {
            alert('لا توجد بيانات للتصدير');
            return;
        }

        var jsPDF = window.jspdf.jsPDF;
        var doc = new jsPDF({
            orientation: 'landscape',
            unit: 'mm',
            format: 'a4'
        });

        var tableData = currentResults.map(function (item) {
            return [
                item.nbrSerial || '',
                item.employeeName || '',
                item.rankDescription || '',
                item.unitName || '',
                item.unitToNumber || '',
                item.statusCode == 1 ? 'نشط' : 'غير نشط'
            ];
        });

        doc.text('Assigned Personnel Report', 148, 15, { align: 'center' });

        doc.autoTable({
            head: [['Serial', 'Name', 'Rank', 'Original Unit', 'Assigned Unit', 'Status']],
            body: tableData,
            startY: 25,
            styles: { font: 'helvetica', fontSize: 8 },
            headStyles: { fillColor: [26, 61, 45] }
        });

        var fileName = 'Assigned_' + new Date().toISOString().split('T')[0] + '.pdf';
        doc.save(fileName);
    }

    function exportToCsv() {
        if (currentResults.length === 0) {
            alert('لا توجد بيانات للتصدير');
            return;
        }

        var headers = ['الرقم التسلسلي', 'اسم الموظف', 'الرتبة', 'الوحدة الأصلية', 'الوحدة المكلف بها', 'الوضع'];

        var rows = currentResults.map(function (item) {
            return [
                item.nbrSerial || '',
                item.employeeName || '',
                item.rankDescription || '',
                item.unitName || '',
                item.unitToNumber || '',
                item.statusCode == 1 ? 'نشط' : 'غير نشط'
            ];
        });

        var csvContent = '\uFEFF';
        csvContent += headers.join(',') + '\n';
        rows.forEach(function (row) {
            csvContent += row.map(function (cell) { return '"' + cell + '"'; }).join(',') + '\n';
        });

        var blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
        var link = document.createElement('a');
        var url = URL.createObjectURL(blob);

        link.setAttribute('href', url);
        link.setAttribute('download', 'Assigned_' + new Date().toISOString().split('T')[0] + '.csv');
        link.style.visibility = 'hidden';

        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }

    /* ======================================== */
    /* HELPERS                                   */
    /* ======================================== */
    function getValue(id) {
        var el = document.getElementById(id);
        return el ? el.value.trim() : '';
    }

})();
