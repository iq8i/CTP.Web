/* ============================================= */
/* MODA Reports & Statistics - JavaScript        */
/* التقارير والإحصائيات                            */
/* ============================================= */
(function () {
    'use strict';

    var rankChart = null;
    var unitChart = null;
    var regionChart = null;
    var branchChart = null;
    var chartsInitialized = false;

    var chartColors = [
        '#1A3D2D', '#10B981', '#3B82F6', '#F59E0B', '#EF4444',
        '#8B5CF6', '#EC4899', '#06B6D4', '#84CC16', '#F97316',
        '#6366F1', '#14B8A6', '#A855F7', '#EAB308', '#22C55E',
        '#0EA5E9', '#C9A84C', '#DC2626', '#7C3AED', '#059669'
    ];

    document.addEventListener('DOMContentLoaded', function () {
        initializeCharts();
        setupRefresh();
        setupPrint();
    });

    /* ======================================== */
    /* LOADING                                   */
    /* ======================================== */
    function showLoading() {
        var el = document.getElementById('loadingOverlay');
        if (el) el.classList.add('show');
    }

    function hideLoading() {
        var el = document.getElementById('loadingOverlay');
        if (el) el.classList.remove('show');
    }

    /* ======================================== */
    /* INITIALIZE CHARTS                         */
    /* ======================================== */
    function initializeCharts() {
        if (chartsInitialized) return;
        chartsInitialized = true;
        showLoading();

        loadSummary();
        loadRankChart();
        loadUnitChart();
        loadRegionChart();
        loadBranchChart();
    }

    function loadSummary() {
        var url = document.getElementById('summaryUrl');
        if (!url) return;

        fetch(url.value)
            .then(function (r) { return r.json(); })
            .then(function (data) {
                animateNumber('totalPersonnel', data.totalPersonnel);
                animateNumber('activePersonnel', data.activePersonnel);
                animateNumber('totalUnits', data.totalUnits);
                animateNumber('totalPromotions', data.totalPromotions);
            })
            .catch(function () {
                console.error('Failed to load summary');
            });
    }

    function animateNumber(id, target) {
        var el = document.getElementById(id);
        if (!el || isNaN(target)) return;

        var duration = 1200;
        var startTime = null;

        function step(timestamp) {
            if (!startTime) startTime = timestamp;
            var progress = Math.min((timestamp - startTime) / duration, 1);
            var eased = 1 - Math.pow(1 - progress, 3);
            el.textContent = Math.floor(eased * target).toLocaleString('en');
            if (progress < 1) {
                requestAnimationFrame(step);
            } else {
                el.textContent = target.toLocaleString('en');
            }
        }
        requestAnimationFrame(step);
    }

    function loadRankChart() {
        var url = document.getElementById('rankChartUrl');
        if (!url) return;

        fetch(url.value)
            .then(function (r) { return r.json(); })
            .then(function (data) {
                var ctx = document.getElementById('rankChart');
                if (!ctx) return;

                rankChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: data.map(function (d) { return d.rank; }),
                        datasets: [{
                            label: 'عدد الأفراد',
                            data: data.map(function (d) { return d.count; }),
                            backgroundColor: chartColors[0],
                            borderRadius: 8
                        }]
                    },
                    options: chartOptions(false, false)
                });
            })
            .catch(function () { console.error('Failed to load rank chart'); })
            .finally(hideLoading);
    }

    function loadUnitChart() {
        var url = document.getElementById('unitChartUrl');
        if (!url) return;

        fetch(url.value)
            .then(function (r) { return r.json(); })
            .then(function (data) {
                var ctx = document.getElementById('unitChart');
                if (!ctx) return;

                unitChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: data.map(function (d) { return d.unit; }),
                        datasets: [{
                            data: data.map(function (d) { return d.count; }),
                            backgroundColor: chartColors
                        }]
                    },
                    options: chartOptions(true, true)
                });
            })
            .catch(function () { console.error('Failed to load unit chart'); });
    }

    function loadRegionChart() {
        var url = document.getElementById('regionChartUrl');
        if (!url) return;

        fetch(url.value)
            .then(function (r) { return r.json(); })
            .then(function (data) {
                var ctx = document.getElementById('regionChart');
                if (!ctx) return;

                regionChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: data.map(function (d) { return d.region; }),
                        datasets: [{
                            label: 'عدد الوحدات',
                            data: data.map(function (d) { return d.count; }),
                            backgroundColor: chartColors[4],
                            borderRadius: 8
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        animation: false,
                        indexAxis: 'y',
                        plugins: { legend: { display: false } },
                        scales: { x: { beginAtZero: true } }
                    }
                });
            })
            .catch(function () { console.error('Failed to load region chart'); });
    }

    function loadBranchChart() {
        var url = document.getElementById('branchChartUrl');
        if (!url) return;

        fetch(url.value)
            .then(function (r) { return r.json(); })
            .then(function (data) {
                var ctx = document.getElementById('branchChart');
                if (!ctx) return;

                branchChart = new Chart(ctx, {
                    type: 'pie',
                    data: {
                        labels: data.map(function (d) { return d.branch; }),
                        datasets: [{
                            data: data.map(function (d) { return d.count; }),
                            backgroundColor: chartColors
                        }]
                    },
                    options: chartOptions(true, true)
                });
            })
            .catch(function () { console.error('Failed to load branch chart'); });
    }

    function chartOptions(showLegend, legendRight) {
        var opts = {
            responsive: true,
            maintainAspectRatio: false,
            animation: false,
            plugins: {
                legend: { display: !!showLegend }
            }
        };

        if (showLegend && legendRight) {
            opts.plugins.legend.position = 'right';
        }

        if (!showLegend) {
            opts.scales = { y: { beginAtZero: true } };
        }

        return opts;
    }

    /* ======================================== */
    /* REFRESH                                   */
    /* ======================================== */
    function setupRefresh() {
        var btn = document.getElementById('refreshDataBtn');
        if (!btn) return;

        btn.addEventListener('click', function () {
            if (rankChart) rankChart.destroy();
            if (unitChart) unitChart.destroy();
            if (regionChart) regionChart.destroy();
            if (branchChart) branchChart.destroy();

            chartsInitialized = false;
            initializeCharts();
        });
    }

    /* ======================================== */
    /* PRINT                                     */
    /* ======================================== */
    function setupPrint() {
        var btn = document.getElementById('printReportBtn');
        if (!btn) return;
        btn.addEventListener('click', function () { window.print(); });
    }

    /* ======================================== */
    /* EXPORT                                    */
    /* ======================================== */
    window.exportChart = function (reportType) {
        var url = document.getElementById('exportUrl');
        if (!url) return;
        window.location.href = url.value + '?reportType=' + reportType + '&format=csv';
    };

})();
