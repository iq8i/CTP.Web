/* ============================================= */
/* MODA Analytics & Trends - JavaScript          */
/* التحليلات والاتجاهات                            */
/* ============================================= */
(function () {
    'use strict';

    var charts = {};
    var chartColors = [
        '#1A3D2D', '#10B981', '#3B82F6', '#F59E0B', '#EF4444',
        '#8B5CF6', '#EC4899', '#06B6D4', '#84CC16', '#F97316'
    ];

    document.addEventListener('DOMContentLoaded', function () {
        loadAllData();
        setupTimeFilter();
        setupExport();
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
    /* TIME FILTER                               */
    /* ======================================== */
    function setupTimeFilter() {
        var buttons = document.querySelectorAll('[data-range]');
        buttons.forEach(function (btn) {
            btn.addEventListener('click', function () {
                buttons.forEach(function (b) { b.classList.remove('active'); });
                btn.classList.add('active');

                var range = btn.getAttribute('data-range');
                var years = null;
                if (range === '1year') years = 1;
                else if (range === '3years') years = 3;
                else if (range === '5years') years = 5;

                reloadData(years);
            });
        });
    }

    /* ======================================== */
    /* EXPORT                                    */
    /* ======================================== */
    function setupExport() {
        var btn = document.getElementById('exportChartsBtn');
        if (!btn) return;
        btn.addEventListener('click', function () {
            window.print();
        });
    }

    /* ======================================== */
    /* DATA LOADING                              */
    /* ======================================== */
    function getUrl(id) {
        var el = document.getElementById(id);
        return el ? el.value : '';
    }

    function loadAllData() {
        reloadData(null);
    }

    function reloadData(years) {
        showLoading();

        // Destroy existing charts
        Object.keys(charts).forEach(function (key) {
            if (charts[key]) charts[key].destroy();
        });
        charts = {};

        var yearParam = years ? '?years=' + years : '';

        var urls = {
            summary: getUrl('summaryUrl'),
            promotions: getUrl('promotionTrendsUrl') + yearParam,
            enlistments: getUrl('enlistmentTrendsUrl') + yearParam,
            trainings: getUrl('trainingTrendsUrl') + yearParam,
            awards: getUrl('awardsTrendsUrl') + yearParam,
            rankDist: getUrl('rankDistUrl'),
            deployment: getUrl('deploymentUrl'),
            education: getUrl('educationUrl')
        };

        var pending = 8;
        var summaryData = null;
        var trainingTotal = 0;
        var awardsTotal = 0;

        function checkDone() {
            pending--;
            if (pending === 0) {
                // Update summary numbers
                if (summaryData) {
                    animateNumber('totalPersonnel', summaryData.totalPersonnel);
                    animateNumber('totalPromotions', summaryData.totalPromotions);
                }
                animateNumber('totalTrainings', trainingTotal);
                animateNumber('totalAwards', awardsTotal);
                hideLoading();
            }
        }

        // Summary
        fetch(urls.summary)
            .then(function (r) { return r.json(); })
            .then(function (data) { summaryData = data; })
            .catch(function () {})
            .finally(checkDone);

        // Promotion trends
        fetch(urls.promotions)
            .then(function (r) { return r.json(); })
            .then(function (data) { createLineChart('promotionTrendChart', data, '#10B981'); })
            .catch(function () {})
            .finally(checkDone);

        // Enlistment trends
        fetch(urls.enlistments)
            .then(function (r) { return r.json(); })
            .then(function (data) { createLineChart('enlistmentTrendChart', data, '#3B82F6'); })
            .catch(function () {})
            .finally(checkDone);

        // Training trends
        fetch(urls.trainings)
            .then(function (r) { return r.json(); })
            .then(function (data) {
                trainingTotal = data.reduce(function (sum, d) { return sum + d.count; }, 0);
                createBarChart('trainingTrendChart', data, '#F59E0B');
            })
            .catch(function () {})
            .finally(checkDone);

        // Awards trends
        fetch(urls.awards)
            .then(function (r) { return r.json(); })
            .then(function (data) {
                awardsTotal = data.reduce(function (sum, d) { return sum + d.count; }, 0);
                createBarChart('awardsTrendChart', data, '#EF4444');
            })
            .catch(function () {})
            .finally(checkDone);

        // Rank distribution
        fetch(urls.rankDist)
            .then(function (r) { return r.json(); })
            .then(function (data) { createDoughnutChart('rankDistributionChart', data, 'rankType'); })
            .catch(function () {})
            .finally(checkDone);

        // Deployment stats
        fetch(urls.deployment)
            .then(function (r) { return r.json(); })
            .then(function (data) { createPieChart('deploymentStatsChart', data, 'type'); })
            .catch(function () {})
            .finally(checkDone);

        // Education levels
        fetch(urls.education)
            .then(function (r) { return r.json(); })
            .then(function (data) { createHorizontalBarChart('educationLevelChart', data, '#06B6D4'); })
            .catch(function () {})
            .finally(checkDone);
    }

    /* ======================================== */
    /* ANIMATE NUMBER                            */
    /* ======================================== */
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

    /* ======================================== */
    /* CHART CREATORS                            */
    /* ======================================== */
    function createLineChart(canvasId, data, color) {
        var ctx = document.getElementById(canvasId);
        if (!ctx) return;

        charts[canvasId] = new Chart(ctx, {
            type: 'line',
            data: {
                labels: data.map(function (d) { return d.year; }),
                datasets: [{
                    label: '',
                    data: data.map(function (d) { return d.count; }),
                    borderColor: color,
                    backgroundColor: hexToRgba(color, 0.1),
                    tension: 0.4,
                    fill: true,
                    borderWidth: 3,
                    pointBackgroundColor: color,
                    pointRadius: 4,
                    pointHoverRadius: 6
                }]
            },
            options: baseOptions(false)
        });
    }

    function createBarChart(canvasId, data, color) {
        var ctx = document.getElementById(canvasId);
        if (!ctx) return;

        charts[canvasId] = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: data.map(function (d) { return d.year; }),
                datasets: [{
                    label: '',
                    data: data.map(function (d) { return d.count; }),
                    backgroundColor: color,
                    borderRadius: 8
                }]
            },
            options: baseOptions(false)
        });
    }

    function createDoughnutChart(canvasId, data, labelKey) {
        var ctx = document.getElementById(canvasId);
        if (!ctx) return;

        charts[canvasId] = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: data.map(function (d) { return d[labelKey]; }),
                datasets: [{
                    data: data.map(function (d) { return d.count; }),
                    backgroundColor: chartColors
                }]
            },
            options: baseOptions(true)
        });
    }

    function createPieChart(canvasId, data, labelKey) {
        var ctx = document.getElementById(canvasId);
        if (!ctx) return;

        charts[canvasId] = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: data.map(function (d) { return d[labelKey]; }),
                datasets: [{
                    data: data.map(function (d) { return d.count; }),
                    backgroundColor: chartColors
                }]
            },
            options: baseOptions(true)
        });
    }

    function createHorizontalBarChart(canvasId, data, color) {
        var ctx = document.getElementById(canvasId);
        if (!ctx) return;

        charts[canvasId] = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: data.map(function (d) { return d.level; }),
                datasets: [{
                    label: '',
                    data: data.map(function (d) { return d.count; }),
                    backgroundColor: color,
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
    }

    function baseOptions(showLegend) {
        var opts = {
            responsive: true,
            maintainAspectRatio: false,
            animation: false,
            plugins: {
                legend: { display: !!showLegend }
            }
        };
        if (showLegend) opts.plugins.legend.position = 'right';
        if (!showLegend) opts.scales = { y: { beginAtZero: true } };
        return opts;
    }

    function hexToRgba(hex, alpha) {
        var r = parseInt(hex.slice(1, 3), 16);
        var g = parseInt(hex.slice(3, 5), 16);
        var b = parseInt(hex.slice(5, 7), 16);
        return 'rgba(' + r + ',' + g + ',' + b + ',' + alpha + ')';
    }

})();
