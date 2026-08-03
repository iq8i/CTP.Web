/* ============================================= */
/* MODA Database Viewer - JavaScript             */
/* عارض قاعدة البيانات المحلية                     */
/* ============================================= */
(function () {
    'use strict';

    var currentTable = null;
    var currentPage = 1;
    var pageSize = 50;

    document.addEventListener('DOMContentLoaded', function () {
        loadTables();
    });

    /* ======================================== */
    /* HELPERS                                   */
    /* ======================================== */
    function getUrl(id) {
        var el = document.getElementById(id);
        return el ? el.value : '';
    }

    function qs(id) { return document.getElementById(id); }

    /* ======================================== */
    /* LOAD TABLES                               */
    /* ======================================== */
    function loadTables() {
        var list = qs('tablesList');
        if (!list) return;

        list.innerHTML = '<div class="cf-loading-state"><div class="cf-spinner"></div><p>\u062C\u0627\u0631\u064A \u0627\u0644\u062A\u062D\u0645\u064A\u0644...</p></div>';

        fetch(getUrl('getTablesUrl'))
            .then(function (r) { return r.json(); })
            .then(function (response) {
                if (response.success) {
                    if (response.tables.length === 0) {
                        list.innerHTML = '<div class="cf-empty-state" style="padding:30px 10px;"><i class="bi bi-inbox"></i><p>\u0644\u0627 \u062A\u0648\u062C\u062F \u062C\u062F\u0627\u0648\u0644</p></div>';
                        return;
                    }
                    var html = '';
                    response.tables.forEach(function (table) {
                        var badge = getBadgeForTable(table);
                        html += '<a href="javascript:void(0)" class="cf-table-item" data-table="' + table + '">';
                        html += '<span class="cf-table-item-name"><i class="bi bi-table"></i><span>' + table + '</span></span>';
                        html += badge;
                        html += '</a>';
                    });
                    list.innerHTML = html;

                    // Bind click events
                    var items = list.querySelectorAll('.cf-table-item');
                    items.forEach(function (item) {
                        item.addEventListener('click', function () {
                            // Remove active from all
                            items.forEach(function (i) { i.classList.remove('active'); });
                            item.classList.add('active');
                            loadTableData(item.getAttribute('data-table'), 1);
                        });
                    });
                } else {
                    list.innerHTML = '<div class="cf-error-alert" style="margin:10px;"><i class="bi bi-exclamation-triangle-fill"></i> ' + (response.message || '\u062E\u0637\u0623') + '</div>';
                }
            })
            .catch(function () {
                list.innerHTML = '<div class="cf-error-alert" style="margin:10px;"><i class="bi bi-exclamation-triangle-fill"></i> \u0641\u0634\u0644 \u062A\u062D\u0645\u064A\u0644 \u0627\u0644\u062C\u062F\u0627\u0648\u0644</div>';
            });
    }

    /* ======================================== */
    /* BADGE MAPPING                             */
    /* ======================================== */
    function getBadgeForTable(tableName) {
        if (tableName.includes('Personnel') || tableName === 'DATA_PERS') return '<span class="cf-table-badge cf-badge-personnel">\u0623\u0641\u0631\u0627\u062F</span>';
        if (tableName.includes('Biography') || tableName === 'Biographies') return '<span class="cf-table-badge cf-badge-bio">\u0633\u064A\u0631\u0629</span>';
        if (tableName.includes('Identity') || tableName === 'Identities') return '<span class="cf-table-badge cf-badge-identity">\u0647\u0648\u064A\u0627\u062A</span>';
        if (tableName.includes('Dependent')) return '<span class="cf-table-badge cf-badge-dependent">\u0645\u0639\u0627\u0644\u064A\u0646</span>';
        if (tableName.includes('Education')) return '<span class="cf-table-badge cf-badge-education">\u062A\u0639\u0644\u064A\u0645</span>';
        if (tableName.includes('Promotion')) return '<span class="cf-table-badge cf-badge-promotion">\u062A\u0631\u0642\u064A\u0627\u062A</span>';
        if (tableName.includes('Enlistment')) return '<span class="cf-table-badge cf-badge-enlistment">\u062A\u062C\u0646\u064A\u062F</span>';
        if (tableName.includes('Discharge')) return '<span class="cf-table-badge cf-badge-discharge">\u062A\u0633\u0631\u064A\u062D</span>';
        if (tableName.includes('Transfer')) return '<span class="cf-table-badge cf-badge-transfer">\u0646\u0642\u0644</span>';
        if (tableName.includes('Unit') || tableName.includes('TOE1')) return '<span class="cf-table-badge cf-badge-unit">\u0648\u062D\u062F\u0627\u062A</span>';
        if (tableName.includes('Section') || tableName.includes('TOE2')) return '<span class="cf-table-badge cf-badge-section">\u0623\u0642\u0633\u0627\u0645</span>';
        if (tableName.includes('Position') || tableName.includes('TOE3')) return '<span class="cf-table-badge cf-badge-position">\u0645\u0646\u0627\u0635\u0628</span>';
        if (tableName === 'Users') return '<span class="cf-table-badge cf-badge-users">\u0645\u0633\u062A\u062E\u062F\u0645\u064A\u0646</span>';
        if (tableName === 'DbConfigurations') return '<span class="cf-table-badge cf-badge-config">\u0625\u0639\u062F\u0627\u062F\u0627\u062A</span>';
        if (tableName === 'AuditLogs') return '<span class="cf-table-badge cf-badge-audit">\u0633\u062C\u0644\u0627\u062A</span>';
        if (tableName === 'Notifications') return '<span class="cf-table-badge cf-badge-notification">\u0625\u0634\u0639\u0627\u0631\u0627\u062A</span>';
        return '<span class="cf-table-badge cf-badge-default">\u062C\u062F\u0648\u0644</span>';
    }

    /* ======================================== */
    /* LOAD TABLE DATA                           */
    /* ======================================== */
    function loadTableData(tableName, page) {
        currentTable = tableName;
        currentPage = page;

        var refreshBtn = qs('refreshTableBtn');
        if (refreshBtn) refreshBtn.style.display = '';

        var container = qs('tableDataContainer');
        container.innerHTML = '<div class="cf-loading-state"><div class="cf-spinner"></div><p>\u062C\u0627\u0631\u064A \u062A\u062D\u0645\u064A\u0644 \u0627\u0644\u0628\u064A\u0627\u0646\u0627\u062A...</p></div>';

        var url = getUrl('getTableDataUrl') + '?tableName=' + encodeURIComponent(tableName) + '&page=' + page + '&pageSize=' + pageSize;

        fetch(url)
            .then(function (r) { return r.json(); })
            .then(function (response) {
                if (response.success) {
                    displayTableData(response);
                } else {
                    container.innerHTML = '<div class="cf-error-alert"><i class="bi bi-exclamation-triangle-fill"></i> ' + (response.message || '\u062E\u0637\u0623') + '</div>';
                }
            })
            .catch(function () {
                container.innerHTML = '<div class="cf-error-alert"><i class="bi bi-exclamation-triangle-fill"></i> \u0641\u0634\u0644 \u062A\u062D\u0645\u064A\u0644 \u0627\u0644\u0628\u064A\u0627\u0646\u0627\u062A</div>';
            });
    }

    /* ======================================== */
    /* DISPLAY TABLE DATA                        */
    /* ======================================== */
    function displayTableData(response) {
        var titleEl = qs('tableTitle');
        var infoEl = qs('tableInfo');
        if (titleEl) titleEl.innerHTML = '<i class="bi bi-grid-3x3-gap-fill"></i> ' + response.tableName;
        if (infoEl) infoEl.innerHTML = '<span class="cf-source-badge cf-source-sqlite"><i class="bi bi-database-fill"></i> SQLite</span><span>' + response.totalRecords.toLocaleString('en') + ' \u0633\u062C\u0644</span>';

        var container = qs('tableDataContainer');

        if (response.data.length === 0) {
            container.innerHTML = '<div class="cf-empty-state"><i class="bi bi-inbox"></i><h4>\u0644\u0627 \u062A\u0648\u062C\u062F \u0628\u064A\u0627\u0646\u0627\u062A</h4><p>\u0647\u0630\u0627 \u0627\u0644\u062C\u062F\u0648\u0644 \u0641\u0627\u0631\u063A</p></div>';
            var pagination = qs('paginationContainer');
            if (pagination) pagination.classList.remove('show');
            return;
        }

        var html = '<div class="cf-data-table-wrap"><table class="cf-data-table"><thead><tr>';
        response.columns.forEach(function (col) {
            html += '<th>' + col + '</th>';
        });
        html += '</tr></thead><tbody>';

        response.data.forEach(function (row) {
            html += '<tr>';
            response.columns.forEach(function (col) {
                var value = row[col];
                if (value === null || value === undefined) {
                    html += '<td><span class="cf-null-value">NULL</span></td>';
                } else {
                    html += '<td>' + escapeHtml(value.toString()) + '</td>';
                }
            });
            html += '</tr>';
        });

        html += '</tbody></table></div>';
        container.innerHTML = html;

        if (response.totalPages > 1) {
            updatePagination(response);
        } else {
            var pagination = qs('paginationContainer');
            if (pagination) pagination.classList.remove('show');
        }
    }

    /* ======================================== */
    /* PAGINATION                                */
    /* ======================================== */
    function updatePagination(response) {
        var start = (response.currentPage - 1) * response.pageSize + 1;
        var end = Math.min(response.currentPage * response.pageSize, response.totalRecords);

        var infoEl = qs('recordsInfo');
        if (infoEl) infoEl.textContent = '\u0639\u0631\u0636 ' + start.toLocaleString('en') + ' - ' + end.toLocaleString('en') + ' \u0645\u0646 ' + response.totalRecords.toLocaleString('en');

        var btnsEl = qs('paginationButtons');
        if (!btnsEl) return;

        var html = '';

        // Previous
        if (response.currentPage > 1) {
            html += '<button class="cf-page-btn nav-btn" data-page="' + (response.currentPage - 1) + '"><i class="bi bi-chevron-right"></i></button>';
        }

        // Page numbers
        var maxButtons = 5;
        var startPage = Math.max(1, response.currentPage - Math.floor(maxButtons / 2));
        var endPage = Math.min(response.totalPages, startPage + maxButtons - 1);
        if (endPage - startPage < maxButtons - 1) {
            startPage = Math.max(1, endPage - maxButtons + 1);
        }

        for (var i = startPage; i <= endPage; i++) {
            html += '<button class="cf-page-btn' + (i === response.currentPage ? ' active' : '') + '" data-page="' + i + '">' + i + '</button>';
        }

        // Next
        if (response.currentPage < response.totalPages) {
            html += '<button class="cf-page-btn nav-btn" data-page="' + (response.currentPage + 1) + '"><i class="bi bi-chevron-left"></i></button>';
        }

        btnsEl.innerHTML = html;

        // Bind page button clicks
        btnsEl.querySelectorAll('.cf-page-btn').forEach(function (btn) {
            btn.addEventListener('click', function () {
                loadTableData(currentTable, parseInt(btn.getAttribute('data-page')));
            });
        });

        var pagination = qs('paginationContainer');
        if (pagination) pagination.classList.add('show');
    }

    /* ======================================== */
    /* UTILITY                                   */
    /* ======================================== */
    function escapeHtml(text) {
        var d = document.createElement('div');
        d.textContent = text;
        return d.innerHTML;
    }

    function refreshCurrentTable() {
        if (currentTable) {
            loadTableData(currentTable, currentPage);
        }
    }

    // Expose for onclick
    window.refreshCurrentTable = refreshCurrentTable;
    window.dbViewerLoadTables = loadTables;

})();
