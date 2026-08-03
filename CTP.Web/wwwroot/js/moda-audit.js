/* ============================================= */
/* MODA Audit Log - JavaScript                   */
/* سجل التدقيق والمراجعة                          */
/* ============================================= */
(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        loadStats();
        setupFilters();
    });

    /* ======================================== */
    /* HELPERS                                   */
    /* ======================================== */
    function getUrl(id) {
        var el = document.getElementById(id);
        return el ? el.value : '';
    }

    function showToast(message, type) {
        var toast = document.getElementById('alToast');
        if (!toast) return;
        toast.className = 'al-toast al-toast-' + (type || 'success');
        toast.innerHTML = '<i class="bi ' + (type === 'error' ? 'bi-x-circle-fill' : 'bi-check-circle-fill') + '"></i> ' + message;
        setTimeout(function () { toast.classList.add('show'); }, 50);
        setTimeout(function () { toast.classList.remove('show'); }, 3500);
    }

    /* ======================================== */
    /* LOAD STATS                                */
    /* ======================================== */
    function loadStats() {
        fetch(getUrl('getStatsUrl'))
            .then(function (r) { return r.json(); })
            .then(function (data) {
                document.getElementById('totalLogs').textContent = data.total.toLocaleString();
                document.getElementById('todayLogs').textContent = data.today.toLocaleString();
                document.getElementById('weekLogs').textContent = data.thisWeek.toLocaleString();
                document.getElementById('monthLogs').textContent = data.thisMonth.toLocaleString();

                // Top actions
                var topEl = document.getElementById('topActions');
                if (topEl && data.byAction) {
                    var html = '';
                    data.byAction.forEach(function (item) {
                        html += '<div class="al-action-stat">';
                        html += '<span class="al-action-stat-name">' + (item.action || '\u063A\u064A\u0631 \u0645\u062D\u062F\u062F') + '</span>';
                        html += '<span class="al-action-stat-count">' + item.count + '</span>';
                        html += '</div>';
                    });
                    topEl.innerHTML = html;
                }
            })
            .catch(function () {});
    }

    /* ======================================== */
    /* FILTERS                                   */
    /* ======================================== */
    function setupFilters() {
        var actionFilter = document.getElementById('actionFilter');
        var entityFilter = document.getElementById('entityFilter');
        var dateRangeFilter = document.getElementById('dateRangeFilter');
        var searchInput = document.getElementById('searchInput');
        var clearBtn = document.getElementById('clearFilters');

        if (actionFilter) actionFilter.addEventListener('change', filterLogs);
        if (entityFilter) entityFilter.addEventListener('change', filterLogs);
        if (dateRangeFilter) dateRangeFilter.addEventListener('change', filterLogs);
        if (searchInput) searchInput.addEventListener('keyup', filterLogs);

        if (clearBtn) {
            clearBtn.addEventListener('click', function () {
                if (actionFilter) actionFilter.value = 'all';
                if (entityFilter) entityFilter.value = 'all';
                if (dateRangeFilter) dateRangeFilter.value = 'all';
                if (searchInput) searchInput.value = '';
                filterLogs();
            });
        }

        // Export
        var exportBtn = document.getElementById('exportLogsBtn');
        if (exportBtn) {
            exportBtn.addEventListener('click', exportLogs);
        }
    }

    function filterLogs() {
        var action = document.getElementById('actionFilter').value;
        var entity = document.getElementById('entityFilter').value;
        var dateRange = document.getElementById('dateRangeFilter').value;
        var search = document.getElementById('searchInput').value.toLowerCase();

        var today = new Date();
        today.setHours(0, 0, 0, 0);

        var visibleCount = 0;
        var items = document.querySelectorAll('.al-log-item');

        items.forEach(function (item) {
            var itemAction = item.getAttribute('data-action');
            var itemEntity = item.getAttribute('data-entity');
            var itemDate = new Date(item.getAttribute('data-date'));
            var itemText = item.textContent.toLowerCase();

            var actionMatch = action === 'all' || itemAction === action;
            var entityMatch = entity === 'all' || itemEntity === entity;
            var searchMatch = search === '' || itemText.indexOf(search) !== -1;

            var dateMatch = true;
            if (dateRange === 'today') {
                dateMatch = itemDate.getTime() === today.getTime();
            } else if (dateRange === 'week') {
                var weekAgo = new Date(today);
                weekAgo.setDate(weekAgo.getDate() - 7);
                dateMatch = itemDate >= weekAgo;
            } else if (dateRange === 'month') {
                var monthAgo = new Date(today);
                monthAgo.setDate(monthAgo.getDate() - 30);
                dateMatch = itemDate >= monthAgo;
            }

            if (actionMatch && entityMatch && dateMatch && searchMatch) {
                item.style.display = '';
                visibleCount++;
            } else {
                item.style.display = 'none';
            }
        });

        var countEl = document.getElementById('resultCount');
        if (countEl) countEl.textContent = visibleCount + ' \u0633\u062C\u0644';
    }

    /* ======================================== */
    /* EXPORT                                    */
    /* ======================================== */
    function exportLogs() {
        if (typeof XLSX === 'undefined') {
            showToast('\u0645\u0643\u062A\u0628\u0629 \u0627\u0644\u062A\u0635\u062F\u064A\u0631 \u063A\u064A\u0631 \u0645\u062A\u0648\u0641\u0631\u0629', 'error');
            return;
        }

        var logs = [];
        var items = document.querySelectorAll('.al-log-item');
        items.forEach(function (item) {
            if (item.style.display === 'none') return;

            var metaSpans = item.querySelectorAll('.al-log-meta span');
            var detailsEl = item.querySelector('.al-log-details');

            logs.push({
                '\u0627\u0644\u0639\u0645\u0644\u064A\u0629': item.getAttribute('data-action'),
                '\u0627\u0644\u0643\u064A\u0627\u0646': item.getAttribute('data-entity'),
                '\u0627\u0644\u062A\u0627\u0631\u064A\u062E': item.getAttribute('data-date'),
                '\u0627\u0644\u062A\u0641\u0627\u0635\u064A\u0644': detailsEl ? detailsEl.textContent.trim() : '',
                '\u0627\u0644\u0645\u0633\u062A\u062E\u062F\u0645': metaSpans.length > 0 ? metaSpans[0].textContent.trim() : ''
            });
        });

        var ws = XLSX.utils.json_to_sheet(logs);
        var wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, '\u0633\u062C\u0644 \u0627\u0644\u062A\u062F\u0642\u064A\u0642');

        var fileName = 'AuditLog_' + new Date().toISOString().split('T')[0] + '.xlsx';
        XLSX.writeFile(wb, fileName);

        showToast('\u062A\u0645 \u062A\u0635\u062F\u064A\u0631 \u0633\u062C\u0644 \u0627\u0644\u062A\u062F\u0642\u064A\u0642 \u0628\u0646\u062C\u0627\u062D', 'success');
    }

})();
