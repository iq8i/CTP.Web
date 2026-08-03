/* ============================================= */
/* MODA Positions - JavaScript                    */
/* إدارة الوظائف                                   */
/* ============================================= */
(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        restoreFilters();
        setupQuickSearch();
        setupRankCategoryChange();
    });

    /* ======================================== */
    /* FILTER RESTORATION                        */
    /* ======================================== */
    function restoreFilters() {
        restoreSelect('status');
        restoreSelectByData('rankCategory');
        restoreSelectByData('unitNumber');
        restoreSelectByData('rankDescription');

        // Trigger rank loading if category is pre-selected
        var rankCat = document.getElementById('rankCategory');
        if (rankCat && rankCat.value) {
            loadRanksByCategory(rankCat.value);
        }
    }

    function restoreSelect(id) {
        var el = document.getElementById(id);
        if (!el) return;
        var val = el.dataset.selected;
        if (val) el.value = val;
    }

    function restoreSelectByData(id) {
        var el = document.getElementById(id);
        if (!el) return;
        var val = el.dataset.selected;
        if (val) el.value = val;
    }

    /* ======================================== */
    /* QUICK SEARCH (in-table)                   */
    /* ======================================== */
    function setupQuickSearch() {
        var searchInput = document.getElementById('searchInput');
        if (!searchInput) return;

        searchInput.addEventListener('input', function () {
            var term = this.value.toLowerCase();
            var table = document.getElementById('positionsTable');
            if (!table) return;

            var tbody = table.querySelector('tbody');
            if (!tbody) return;

            var rows = tbody.querySelectorAll('tr');
            for (var i = 0; i < rows.length; i++) {
                var text = rows[i].textContent.toLowerCase();
                rows[i].style.display = text.indexOf(term) > -1 ? '' : 'none';
            }
        });
    }

    /* ======================================== */
    /* RANK CATEGORY CHANGE                      */
    /* ======================================== */
    function setupRankCategoryChange() {
        var rankCat = document.getElementById('rankCategory');
        if (!rankCat) return;

        rankCat.addEventListener('change', function () {
            loadRanksByCategory(this.value);
        });
    }

    function loadRanksByCategory(category) {
        var rankSelect = document.getElementById('rankDescription');
        if (!rankSelect) return;

        var currentVal = rankSelect.value;

        // Clear options except first
        while (rankSelect.options.length > 1) {
            rankSelect.remove(1);
        }

        if (!category) {
            rankSelect.value = '';
            return;
        }

        var url = rankSelect.dataset.url;
        if (!url) return;

        fetch(url + '?rankCategory=' + encodeURIComponent(category))
            .then(function (r) { return r.json(); })
            .then(function (ranks) {
                ranks.forEach(function (rank) {
                    var option = document.createElement('option');
                    option.value = rank;
                    option.textContent = rank;
                    rankSelect.appendChild(option);
                });

                if (currentVal && ranks.indexOf(currentVal) !== -1) {
                    rankSelect.value = currentVal;
                }
            })
            .catch(function (err) {
                console.error('Error loading ranks:', err);
            });
    }

    /* ======================================== */
    /* EXPORT TO EXCEL                           */
    /* ======================================== */
    window.exportToExcel = function () {
        var fields = ['search', 'employeeName', 'status', 'rankCategory', 'unitNumber', 'rankDescription'];
        var exportUrl = document.getElementById('exportUrl');
        if (!exportUrl) return;

        var form = document.createElement('form');
        form.method = 'GET';
        form.action = exportUrl.value;

        fields.forEach(function (name) {
            var el = document.getElementById(name);
            if (el && el.value) {
                var input = document.createElement('input');
                input.type = 'hidden';
                input.name = name;
                input.value = el.value;
                form.appendChild(input);
            }
        });

        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);
    };

})();
