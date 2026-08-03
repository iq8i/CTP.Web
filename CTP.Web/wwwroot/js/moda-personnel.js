/* ============================================= */
/* MODA Personnel Page - JavaScript               */
/* صفحة إدارة الأفراد                              */
/* ============================================= */

(function () {
    'use strict';

    var currentResults = [];
    var searchUrl = ''; // Set from view via data attribute

    $(document).ready(function () {
        // Get search URL from data attribute
        searchUrl = $('#searchForm').data('search-url') || '';

        // Toggle advanced filters
        $('#toggleAdvanced').click(function () {
            $('#advancedFilters').slideToggle(300);
            $(this).toggleClass('active');
            $(this).find('i').toggleClass('bi-sliders bi-chevron-up');
        });

        // Search
        $('#searchBtn').click(performSearch);

        // Clear
        $('#clearBtn').click(function () {
            $('#searchForm')[0].reset();
            $('#resultsCard').hide();
            $('#initialState').show();
            currentResults = [];
        });

        // Exports
        $('#exportExcelBtn').click(exportToExcel);
        $('#exportPdfBtn').click(exportToPdf);
        $('#exportCsvBtn').click(exportToCsv);

        // Enter key
        $('#searchForm input').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                performSearch();
            }
        });
    });

    function performSearch() {
        var serial = $('#searchSerial').val();
        var name = $('#searchName').val();
        var unit = $('#searchUnit').val();
        var rank = $('#searchRank').val();
        var status = $('#searchStatus').val();
        var rankType = $('#searchRankType').val();
        var enlistFrom = $('#searchEnlistFrom').val();
        var enlistTo = $('#searchEnlistTo').val();
        var rankLevel = $('#searchRankLevel').val();
        var limit = $('#searchLimit').val() || 100;

        if (!serial && !name && !unit && !rank && !status && !rankType && !enlistFrom && !enlistTo && !rankLevel) {
            Swal.fire({
                icon: 'warning',
                title: 'تنبيه',
                text: 'الرجاء إدخال معيار بحث واحد على الأقل',
                confirmButtonText: 'حسناً'
            });
            return;
        }

        var btn = $('#searchBtn');
        var originalHtml = btn.html();
        btn.prop('disabled', true).html('<span class="personnel-spinner"></span> جاري البحث...');

        $.ajax({
            url: searchUrl,
            type: 'GET',
            data: {
                serial: serial, name: name, unit: unit, rank: rank,
                status: status, rankType: rankType,
                enlistFrom: enlistFrom, enlistTo: enlistTo,
                rankLevel: rankLevel, limit: limit
            },
            success: function (data) {
                displayResults(data);
            },
            error: function (xhr, status, error) {
                Swal.fire({
                    icon: 'error',
                    title: 'خطأ',
                    text: 'فشل البحث: ' + error,
                    confirmButtonText: 'حسناً'
                });
            },
            complete: function () {
                btn.prop('disabled', false).html(originalHtml);
            }
        });
    }

    function displayResults(data) {
        currentResults = data;
        $('#initialState').hide();
        $('#resultsCard').show();

        if (data.length === 0) {
            $('#noResults').show();
            $('#personnelTable').hide();
            $('#resultCount').text('0 نتيجة');
        } else {
            $('#noResults').hide();
            $('#personnelTable').show();
            $('#resultCount').text(data.length + ' نتيجة');

            var tbody = $('#resultsBody');
            tbody.empty();

            $.each(data, function (index, item) {
                var statusBadge = item.hasPosition
                    ? '<span class="badge-status badge-status-active"><i class="bi bi-check-circle-fill"></i> مداوم</span>'
                    : '<span class="badge-status badge-status-inactive"><i class="bi bi-dash-circle"></i> غير محدد</span>';

                var unitDisplay = item.unitName || 'غير متوفر';

                if (item.unitToNumber && item.assignmentUnitName) {
                    unitDisplay = '<strong>' + (item.unitName || 'غير متوفر') + '</strong><br/>' +
                        '<span class="badge-assignment"><i class="bi bi-arrow-left-right"></i> مكلف في ' + item.assignmentUnitName + '</span>';
                }

                var row =
                    '<tr>' +
                    '<td><span class="badge-serial">' + (item.nbrSerial || '') + '</span></td>' +
                    '<td><strong>' + (item.employeeName || 'غير متوفر') + '</strong></td>' +
                    '<td>' + (item.rankDescription || 'غير متوفر') + '</td>' +
                    '<td>' + unitDisplay + '</td>' +
                    '<td>' + statusBadge + '</td>' +
                    '<td>' +
                    '<div class="personnel-actions">' +
                    '<a href="/Personnel/Details/' + item.nbrSerial + '" class="btn-action btn-action-view" title="عرض"><i class="bi bi-eye-fill"></i></a>' +
                    '<a href="/Personnel/Edit/' + item.nbrSerial + '" class="btn-action btn-action-edit" title="تعديل"><i class="bi bi-pencil-fill"></i></a>' +
                    '<a href="/Personnel/Delete/' + item.nbrSerial + '" class="btn-action btn-action-delete" title="حذف"><i class="bi bi-trash-fill"></i></a>' +
                    '</div>' +
                    '</td>' +
                    '</tr>';
                tbody.append(row);
            });
        }
    }

    function exportToExcel() {
        if (currentResults.length === 0) {
            Swal.fire({ icon: 'warning', title: 'تنبيه', text: 'لا توجد بيانات للتصدير', confirmButtonText: 'حسناً' });
            return;
        }

        var exportData = currentResults.map(function (item) {
            return {
                'الرقم التسلسلي': item.nbrSerial || '',
                'اسم الموظف': item.employeeName || '',
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
        XLSX.utils.book_append_sheet(wb, ws, 'الأفراد');
        XLSX.writeFile(wb, 'Personnel_' + new Date().toISOString().split('T')[0] + '.xlsx');

        Swal.fire({ icon: 'success', title: 'تم التصدير', text: 'تم تصدير البيانات إلى Excel بنجاح', confirmButtonText: 'حسناً', timer: 2000 });
    }

    function exportToPdf() {
        if (currentResults.length === 0) {
            Swal.fire({ icon: 'warning', title: 'تنبيه', text: 'لا توجد بيانات للتصدير', confirmButtonText: 'حسناً' });
            return;
        }

        var jsPDF = window.jspdf.jsPDF;
        var doc = new jsPDF({ orientation: 'landscape', unit: 'mm', format: 'a4' });

        var tableData = currentResults.map(function (item) {
            return [
                item.nbrSerial || '',
                item.employeeName || '',
                item.rankDescription || '',
                item.unitName || '',
                item.statusCode ? 'نشط' : 'غير نشط'
            ];
        });

        doc.text('Personnel Report - تقرير الأفراد', 148, 15, { align: 'center' });

        doc.autoTable({
            head: [['Serial', 'Name', 'Rank', 'Unit', 'Status']],
            body: tableData,
            startY: 25,
            styles: { font: 'helvetica', fontSize: 8 },
            headStyles: { fillColor: [36, 59, 51] }
        });

        doc.save('Personnel_' + new Date().toISOString().split('T')[0] + '.pdf');
        Swal.fire({ icon: 'success', title: 'تم التصدير', text: 'تم تصدير البيانات إلى PDF بنجاح', confirmButtonText: 'حسناً', timer: 2000 });
    }

    function exportToCsv() {
        if (currentResults.length === 0) {
            Swal.fire({ icon: 'warning', title: 'تنبيه', text: 'لا توجد بيانات للتصدير', confirmButtonText: 'حسناً' });
            return;
        }

        var headers = ['الرقم التسلسلي', 'اسم الموظف', 'الرتبة', 'رمز الرتبة', 'نوع الرتبة', 'المستوى', 'رقم الوحدة', 'اسم الوحدة', 'حالة الخدمة', 'تاريخ الالتحاق', 'تاريخ انتهاء الخدمة'];
        var rows = currentResults.map(function (item) {
            return [
                item.nbrSerial || '', item.employeeName || '', item.rankDescription || '',
                item.rankCode || '', item.rankTypeDescription || '', item.rankLevel || '',
                item.unitNumber || '', item.unitName || '',
                item.statusCode ? 'نشط' : 'غير نشط',
                item.enlistmentDate || '', item.enlistmentEndDate || ''
            ];
        });

        var csvContent = '\uFEFF';
        csvContent += headers.join(',') + '\n';
        rows.forEach(function (row) {
            csvContent += row.map(function (cell) { return '"' + cell + '"'; }).join(',') + '\n';
        });

        var blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
        var link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = 'Personnel_' + new Date().toISOString().split('T')[0] + '.csv';
        link.style.visibility = 'hidden';
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);

        Swal.fire({ icon: 'success', title: 'تم التصدير', text: 'تم تصدير البيانات إلى CSV بنجاح', confirmButtonText: 'حسناً', timer: 2000 });
    }

})();
