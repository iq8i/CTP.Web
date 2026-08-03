/* ============================================= */
/* MODA Oracle Viewer - JavaScript               */
/* عارض قاعدة بيانات Oracle                        */
/* ============================================= */
(function () {
    'use strict';

    var currentTable = null;
    var currentPage = 1;
    var pageSize = 50;
    var currentFilter = null;
    var allColumns = [];

    document.addEventListener('DOMContentLoaded', function () {
        loadTables();

        var applyBtn = document.getElementById('applyFilterBtn');
        var clearBtn = document.getElementById('clearFilterBtn');
        if (applyBtn) applyBtn.addEventListener('click', applyFilter);
        if (clearBtn) clearBtn.addEventListener('click', clearFilter);
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

        list.innerHTML = '<div class="cf-loading-state"><div class="cf-spinner"></div><p>\u062C\u0627\u0631\u064A \u0627\u0644\u0627\u062A\u0635\u0627\u0644 \u0628\u0640 Oracle...</p></div>';

        fetch(getUrl('getOracleTablesUrl'))
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
                            items.forEach(function (i) { i.classList.remove('active'); });
                            item.classList.add('active');
                            currentFilter = null;
                            var filterCol = qs('filterColumn');
                            var filterVal = qs('filterValue');
                            var filterMatch = qs('filterMatchType');
                            if (filterCol) filterCol.value = '';
                            if (filterVal) filterVal.value = '';
                            if (filterMatch) filterMatch.value = 'contains';
                            loadTableData(item.getAttribute('data-table'), 1);
                        });
                    });
                } else {
                    showConnectionWarning();
                    list.innerHTML = '<div class="cf-error-alert" style="margin:10px;"><i class="bi bi-exclamation-triangle-fill"></i> ' + (response.message || '\u0641\u0634\u0644 \u0627\u0644\u0627\u062A\u0635\u0627\u0644') + '</div>';
                }
            })
            .catch(function () {
                showConnectionWarning();
                list.innerHTML = '<div class="cf-error-alert" style="margin:10px;"><i class="bi bi-exclamation-triangle-fill"></i> \u0641\u0634\u0644 \u0627\u0644\u0627\u062A\u0635\u0627\u0644 \u0628\u0642\u0627\u0639\u062F\u0629 \u0628\u064A\u0627\u0646\u0627\u062A Oracle</div>';
            });
    }

    function showConnectionWarning() {
        var warn = qs('connectionWarning');
        if (warn) warn.classList.add('show');
    }

    /* ======================================== */
    /* BADGE MAPPING                             */
    /* ======================================== */
    function getBadgeForTable(tableName) {
        if (tableName.includes('PERS') || tableName.includes('DATA_PERS')) return '<span class="cf-table-badge cf-badge-personnel">\u0623\u0641\u0631\u0627\u062F</span>';
        if (tableName.includes('EDU') || tableName.includes('DATA_EDU')) return '<span class="cf-table-badge cf-badge-education">\u062A\u0639\u0644\u064A\u0645</span>';
        if (tableName.includes('PROM') || tableName.includes('DATA_PROM')) return '<span class="cf-table-badge cf-badge-promotion">\u062A\u0631\u0642\u064A\u0627\u062A</span>';
        if (tableName.includes('TOE') || tableName.includes('DATA_TOE')) return '<span class="cf-table-badge cf-badge-unit">\u0648\u062D\u062F\u0627\u062A</span>';
        if (tableName.includes('BIO') || tableName.includes('DATA_BIO')) return '<span class="cf-table-badge cf-badge-bio">\u0633\u064A\u0631\u0629</span>';
        if (tableName.includes('DEPND') || tableName.includes('DATA_DEPND')) return '<span class="cf-table-badge cf-badge-dependent">\u0645\u0639\u0627\u0644\u064A\u0646</span>';
        if (tableName.includes('TRANS') || tableName.includes('DATA_TRANS')) return '<span class="cf-table-badge cf-badge-transfer">\u0646\u0642\u0644</span>';
        if (tableName.includes('ENL') || tableName.includes('DATA_ENL')) return '<span class="cf-table-badge cf-badge-enlistment">\u062A\u062C\u0646\u064A\u062F</span>';
        return '<span class="cf-table-badge cf-badge-default">...</span>';
    }

    /* ======================================== */
    /* LOAD TABLE DATA                           */
    /* ======================================== */
    function loadTableData(tableName, page) {
        currentTable = tableName;
        currentPage = page;

        // Show filter section
        var filterSection = qs('filterSection');
        if (filterSection) filterSection.classList.add('show');

        var container = qs('tableDataContainer');
        container.innerHTML = '<div class="cf-loading-state"><div class="cf-spinner"></div><p>\u062C\u0627\u0631\u064A \u062A\u062D\u0645\u064A\u0644 \u0627\u0644\u0628\u064A\u0627\u0646\u0627\u062A \u0645\u0646 Oracle...</p></div>';

        var url = getUrl('getOracleTableDataUrl') + '?tableName=' + encodeURIComponent(tableName) + '&page=' + page + '&pageSize=' + pageSize;

        if (currentFilter) {
            url += '&filterColumn=' + encodeURIComponent(currentFilter.column);
            url += '&filterValue=' + encodeURIComponent(currentFilter.value);
            url += '&matchType=' + encodeURIComponent(currentFilter.matchType || 'contains');
        }

        fetch(url)
            .then(function (r) { return r.json(); })
            .then(function (response) {
                if (response.success) {
                    // Populate filter column dropdown
                    if (response.columns && response.columns.length > 0) {
                        allColumns = response.columns;
                        var filterSelect = qs('filterColumn');
                        if (filterSelect) {
                            var selectedCol = currentFilter ? currentFilter.column : '';
                            var optHtml = '<option value="">-- \u0627\u062E\u062A\u0631 \u0627\u0644\u0639\u0645\u0648\u062F --</option>';
                            response.columns.forEach(function (col) {
                                optHtml += '<option value="' + col + '"' + (col === selectedCol ? ' selected' : '') + '>' + col + '</option>';
                            });
                            filterSelect.innerHTML = optHtml;
                        }
                    }
                    displayTableData(response);
                } else {
                    container.innerHTML = '<div class="cf-error-alert"><i class="bi bi-exclamation-triangle-fill"></i> ' + (response.message || '\u062E\u0637\u0623') + '</div>';
                }
            })
            .catch(function () {
                container.innerHTML = '<div class="cf-error-alert"><i class="bi bi-exclamation-triangle-fill"></i> \u0641\u0634\u0644 \u062A\u062D\u0645\u064A\u0644 \u0627\u0644\u0628\u064A\u0627\u0646\u0627\u062A \u0645\u0646 Oracle</div>';
            });
    }

    /* ======================================== */
    /* DISPLAY TABLE DATA                        */
    /* ======================================== */
    function displayTableData(response) {
        var titleEl = qs('tableTitle');
        var infoEl = qs('tableInfo');
        if (titleEl) titleEl.innerHTML = '<i class="bi bi-grid-3x3-gap-fill"></i> ' + response.tableName;
        if (infoEl) infoEl.innerHTML = '<span class="cf-source-badge cf-source-oracle"><i class="bi bi-server"></i> Oracle</span><span>' + response.totalRecords.toLocaleString('en') + ' \u0633\u062C\u0644</span>';

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
                } else if (typeof value === 'object') {
                    html += '<td>' + escapeHtml(JSON.stringify(value)) + '</td>';
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

        if (response.currentPage > 1) {
            html += '<button class="cf-page-btn nav-btn" data-page="' + (response.currentPage - 1) + '"><i class="bi bi-chevron-right"></i></button>';
        }

        var maxButtons = 5;
        var startPage = Math.max(1, response.currentPage - Math.floor(maxButtons / 2));
        var endPage = Math.min(response.totalPages, startPage + maxButtons - 1);
        if (endPage - startPage < maxButtons - 1) {
            startPage = Math.max(1, endPage - maxButtons + 1);
        }

        for (var i = startPage; i <= endPage; i++) {
            html += '<button class="cf-page-btn' + (i === response.currentPage ? ' active' : '') + '" data-page="' + i + '">' + i + '</button>';
        }

        if (response.currentPage < response.totalPages) {
            html += '<button class="cf-page-btn nav-btn" data-page="' + (response.currentPage + 1) + '"><i class="bi bi-chevron-left"></i></button>';
        }

        btnsEl.innerHTML = html;

        btnsEl.querySelectorAll('.cf-page-btn').forEach(function (btn) {
            btn.addEventListener('click', function () {
                loadTableData(currentTable, parseInt(btn.getAttribute('data-page')));
            });
        });

        var pagination = qs('paginationContainer');
        if (pagination) pagination.classList.add('show');
    }

    /* ======================================== */
    /* FILTER                                    */
    /* ======================================== */
    function applyFilter() {
        var column = qs('filterColumn') ? qs('filterColumn').value : '';
        var value = qs('filterValue') ? qs('filterValue').value : '';
        var matchType = qs('filterMatchType') ? qs('filterMatchType').value : 'contains';

        if (!column || !value) {
            alert('\u064A\u0631\u062C\u0649 \u0627\u062E\u062A\u064A\u0627\u0631 \u0627\u0644\u0639\u0645\u0648\u062F \u0648\u0625\u062F\u062E\u0627\u0644 \u0627\u0644\u0642\u064A\u0645\u0629');
            return;
        }

        currentFilter = { column: column, value: value, matchType: matchType };
        loadTableData(currentTable, 1);
    }

    function clearFilter() {
        currentFilter = null;
        var filterCol = qs('filterColumn');
        var filterVal = qs('filterValue');
        var filterMatch = qs('filterMatchType');
        if (filterCol) filterCol.value = '';
        if (filterVal) filterVal.value = '';
        if (filterMatch) filterMatch.value = 'contains';
        if (currentTable) loadTableData(currentTable, 1);
    }

    /* ======================================== */
    /* UTILITY                                   */
    /* ======================================== */
    function escapeHtml(text) {
        var d = document.createElement('div');
        d.textContent = text;
        return d.innerHTML;
    }

    // Expose for onclick
    window.oracleViewerLoadTables = loadTables;

})();
