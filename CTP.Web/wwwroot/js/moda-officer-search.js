/* ============================================= */
/* MODA Officer Search - JavaScript              */
/* البحث عن الضباط                                */
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
        var table = document.getElementById('officersTable');
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
        var baseUrl = detailsUrl ? detailsUrl.value : '';

        data.forEach(function (item) {
            var tr = document.createElement('tr');

            var statusHTML = item.statusCode
                ? '<span class="os-status-badge active"><i class="bi bi-check-circle-fill"></i> نشط</span>'
                : '<span class="os-status-badge inactive"><i class="bi bi-dash-circle-fill"></i> غير محدد</span>';

            var link = baseUrl ? baseUrl + '/' + (item.nbrSerial || '') : '#';

            tr.innerHTML =
                '<td><span class="os-serial-badge">' + (item.nbrSerial || '') + '</span></td>' +
                '<td><strong>' + (item.employeeName || 'غير متوفر') + '</strong></td>' +
                '<td>' + (item.rankDescription || 'غير متوفر') + '</td>' +
                '<td>' + (item.unitName || 'غير متوفر') + '</td>' +
                '<td>' + statusHTML + '</td>' +
                '<td><a href="' + link + '" class="os-view-btn"><i class="bi bi-eye-fill"></i> عرض</a></td>';

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
                'اسم الضابط': item.employeeName || '',
                'الرتبة': item.rankDescription || '',
                'رمز الرتبة': item.rankCode || '',
                'نوع الرتبة': item.rankTypeDescription || '',
                'المستوى': item.rankLevel || '',
                'رقم الوحدة': item.unitNumber || '',
                'اسم الوحدة': item.unitName || '',
                'حالة الخدمة': item.statusCode ? 'نشط' : 'غير نشط',
                'تاريخ الالتحاق': item.enlistmentDate || '',
                'تاريخ انتهاء الخدمة': item.enlistmentEndDate || ''
            };
        });

        var ws = XLSX.utils.json_to_sheet(exportData);
        var wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, 'الضباط');

        var fileName = 'Officers_' + new Date().toISOString().split('T')[0] + '.xlsx';
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
                item.statusCode ? 'نشط' : 'غير نشط'
            ];
        });

        doc.text('Officers Report', 148, 15, { align: 'center' });

        doc.autoTable({
            head: [['Serial', 'Name', 'Rank', 'Unit', 'Status']],
            body: tableData,
            startY: 25,
            styles: { font: 'helvetica', fontSize: 8 },
            headStyles: { fillColor: [26, 61, 45] }
        });

        var fileName = 'Officers_' + new Date().toISOString().split('T')[0] + '.pdf';
        doc.save(fileName);
    }

    function exportToCsv() {
        if (currentResults.length === 0) {
            alert('لا توجد بيانات للتصدير');
            return;
        }

        var headers = ['الرقم التسلسلي', 'اسم الضابط', 'الرتبة', 'رمز الرتبة', 'نوع الرتبة', 'المستوى', 'رقم الوحدة', 'اسم الوحدة', 'حالة الخدمة', 'تاريخ الالتحاق', 'تاريخ انتهاء الخدمة'];

        var rows = currentResults.map(function (item) {
            return [
                item.nbrSerial || '',
                item.employeeName || '',
                item.rankDescription || '',
                item.rankCode || '',
                item.rankTypeDescription || '',
                item.rankLevel || '',
                item.unitNumber || '',
                item.unitName || '',
                item.statusCode ? 'نشط' : 'غير نشط',
                item.enlistmentDate || '',
                item.enlistmentEndDate || ''
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
        link.setAttribute('download', 'Officers_' + new Date().toISOString().split('T')[0] + '.csv');
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
