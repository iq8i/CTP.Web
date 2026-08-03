/* ============================================= */
/* MODA Dashboard v3 - JavaScript                 */
/* نظام المودا - لوحة المعلومات                    */
/* ============================================= */

(function () {
    'use strict';

    // =========================================
    // 1. Counter Animation
    // =========================================
    function animateCounter(el) {
        var target = parseInt(el.getAttribute('data-count-target')) || 0;
        if (target === 0) {
            el.textContent = '0';
            return;
        }

        var duration = 1200;
        var startTime = null;

        function update(currentTime) {
            if (!startTime) startTime = currentTime;
            var elapsed = currentTime - startTime;
            var progress = Math.min(elapsed / duration, 1);
            // Ease-out cubic
            var eased = 1 - Math.pow(1 - progress, 3);
            var current = Math.round(target * eased);
            el.textContent = current.toLocaleString('en');

            if (progress < 1) {
                requestAnimationFrame(update);
            } else {
                el.textContent = target.toLocaleString('en');
                el.classList.add('dash-count-done');
            }
        }

        requestAnimationFrame(update);
    }

    function initCounters() {
        var counters = document.querySelectorAll('[data-count-target]');
        if (!counters.length) return;

        if ('IntersectionObserver' in window) {
            var observer = new IntersectionObserver(function (entries) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        animateCounter(entry.target);
                        observer.unobserve(entry.target);
                    }
                });
            }, { threshold: 0.3 });

            counters.forEach(function (el) {
                observer.observe(el);
            });
        } else {
            // Fallback: animate all immediately
            counters.forEach(animateCounter);
        }
    }

    // =========================================
    // 2. Progress Bar Animation
    // =========================================
    function initProgressBars() {
        var bars = document.querySelectorAll('.dash-progress-fill[data-progress]');
        bars.forEach(function (bar) {
            var target = bar.getAttribute('data-progress') || '0';
            setTimeout(function () {
                bar.style.width = target + '%';
            }, 500);
        });
    }

    // =========================================
    // 3. Chart.js Configuration & Initialization
    // =========================================
    var modaColors = {
        gold: '#C9A84C',
        goldLight: '#D4B862',
        green: '#243B33',
        greenLight: '#2D4A40',
        greenDark: '#1A2E28',
        success: '#10B981',
        danger: '#EF4444',
        info: '#3B82F6',
        warning: '#F59E0B',
        gray: '#9CA3AF',
        white: '#FFFFFF'
    };

    function configureChartDefaults() {
        if (typeof Chart === 'undefined') return;

        Chart.defaults.font.family = "'Cairo', 'Segoe UI', sans-serif";
        Chart.defaults.font.size = 12;
        Chart.defaults.color = '#6B7280';
        Chart.defaults.plugins.legend.rtl = true;
        Chart.defaults.plugins.legend.labels.usePointStyle = true;
        Chart.defaults.plugins.legend.labels.pointStyleWidth = 10;
        Chart.defaults.plugins.legend.labels.padding = 16;
        Chart.defaults.plugins.tooltip.rtl = true;
        Chart.defaults.plugins.tooltip.textDirection = 'rtl';
        Chart.defaults.plugins.tooltip.backgroundColor = '#1A2E28';
        Chart.defaults.plugins.tooltip.titleFont = { family: "'Cairo', sans-serif", weight: '700', size: 13 };
        Chart.defaults.plugins.tooltip.bodyFont = { family: "'Cairo', sans-serif", size: 12 };
        Chart.defaults.plugins.tooltip.padding = 12;
        Chart.defaults.plugins.tooltip.cornerRadius = 10;
        Chart.defaults.plugins.tooltip.displayColors = true;
        Chart.defaults.plugins.tooltip.boxPadding = 4;
    }

    // Center text plugin for donut charts
    var centerTextPlugin = {
        id: 'centerText',
        afterDraw: function (chart) {
            if (!chart.config.options.plugins.centerText) return;
            var config = chart.config.options.plugins.centerText;
            var ctx = chart.ctx;
            var centerX = (chart.chartArea.left + chart.chartArea.right) / 2;
            var centerY = (chart.chartArea.top + chart.chartArea.bottom) / 2;

            // Number
            ctx.save();
            ctx.textAlign = 'center';
            ctx.textBaseline = 'middle';
            ctx.font = "700 28px 'Cairo', sans-serif";
            ctx.fillStyle = '#1A1D21';
            ctx.fillText(config.number || '', centerX, centerY - 8);

            // Label
            ctx.font = "400 11px 'Cairo', sans-serif";
            ctx.fillStyle = '#9CA3AF';
            ctx.fillText(config.label || '', centerX, centerY + 16);
            ctx.restore();
        }
    };

    // -- Personnel Distribution Donut (Admin) --
    function initPersonnelChart() {
        var canvas = document.getElementById('chartPersonnelDistribution');
        if (!canvas) return;

        var officers = parseInt(canvas.getAttribute('data-officers')) || 0;
        var ncos = parseInt(canvas.getAttribute('data-ncos')) || 0;
        var total = parseInt(canvas.getAttribute('data-total')) || 0;
        var others = Math.max(0, total - officers - ncos);

        new Chart(canvas, {
            type: 'doughnut',
            data: {
                labels: ['الضباط', 'ضباط الصف', 'أخرى'],
                datasets: [{
                    data: [officers, ncos, others],
                    backgroundColor: [modaColors.gold, modaColors.green, modaColors.info],
                    borderWidth: 3,
                    borderColor: modaColors.white,
                    hoverBorderWidth: 0,
                    hoverOffset: 8
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '68%',
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: {
                            padding: 20,
                            font: { size: 13, weight: '600' }
                        }
                    },
                    centerText: {
                        number: total.toLocaleString('en'),
                        label: 'إجمالي الأفراد'
                    }
                }
            },
            plugins: [centerTextPlugin]
        });
    }

    // -- Admin Overview Bar Chart --
    function initAdminOverviewChart() {
        var canvas = document.getElementById('chartAdminOverview');
        if (!canvas) return;

        var activeOfficers = parseInt(canvas.getAttribute('data-active-officers')) || 0;
        var activeNCOs = parseInt(canvas.getAttribute('data-active-ncos')) || 0;
        var sepOfficers = parseInt(canvas.getAttribute('data-separated-officers')) || 0;
        var sepNCOs = parseInt(canvas.getAttribute('data-separated-ncos')) || 0;
        var academy = parseInt(canvas.getAttribute('data-academy')) || 0;

        new Chart(canvas, {
            type: 'bar',
            data: {
                labels: ['النشطين', 'المتقاعدين/المفصولين', 'طلبة الكلية'],
                datasets: [
                    {
                        label: 'الضباط',
                        data: [activeOfficers, sepOfficers, academy],
                        backgroundColor: modaColors.gold,
                        borderRadius: 8,
                        borderSkipped: false,
                        barPercentage: 0.6,
                        categoryPercentage: 0.7
                    },
                    {
                        label: 'ضباط الصف',
                        data: [activeNCOs, sepNCOs, 0],
                        backgroundColor: modaColors.green,
                        borderRadius: 8,
                        borderSkipped: false,
                        barPercentage: 0.6,
                        categoryPercentage: 0.7
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        position: 'top',
                        labels: {
                            font: { size: 13, weight: '600' }
                        }
                    }
                },
                scales: {
                    x: {
                        grid: { display: false },
                        ticks: {
                            font: { size: 12, weight: '600' }
                        }
                    },
                    y: {
                        beginAtZero: true,
                        grid: {
                            color: 'rgba(0,0,0,0.04)',
                            drawBorder: false
                        },
                        ticks: {
                            font: { size: 11 }
                        }
                    }
                }
            }
        });
    }

    // -- Position Status Donut (Officers/NCO/Org) --
    function initPositionChart(canvasId, labelFilled, labelVacant) {
        var canvas = document.getElementById(canvasId);
        if (!canvas) return;

        var filled = parseInt(canvas.getAttribute('data-filled')) || 0;
        var vacant = parseInt(canvas.getAttribute('data-vacant')) || 0;
        var total = filled + vacant;

        labelFilled = labelFilled || 'مشغولة';
        labelVacant = labelVacant || 'شاغرة';

        new Chart(canvas, {
            type: 'doughnut',
            data: {
                labels: [labelFilled, labelVacant],
                datasets: [{
                    data: [filled, vacant],
                    backgroundColor: [modaColors.success, modaColors.danger],
                    borderWidth: 3,
                    borderColor: modaColors.white,
                    hoverBorderWidth: 0,
                    hoverOffset: 8
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '68%',
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: {
                            padding: 20,
                            font: { size: 13, weight: '600' }
                        }
                    },
                    centerText: {
                        number: total.toLocaleString('en'),
                        label: 'إجمالي الوظائف'
                    }
                }
            },
            plugins: [centerTextPlugin]
        });
    }

    // -- Rank Distribution Horizontal Bar (NCO) --
    function initRankChart() {
        var canvas = document.getElementById('chartRankDistribution');
        if (!canvas) return;

        var dataEl = document.getElementById('ncoRankData');
        if (!dataEl) return;

        var rankData;
        try {
            rankData = JSON.parse(dataEl.textContent);
        } catch (e) {
            return;
        }

        if (!rankData || !rankData.length) return;

        var labels = rankData.map(function (r) { return r.rank || r.Rank || ''; });
        var values = rankData.map(function (r) { return r.count || r.Count || 0; });

        // Generate gradient colors from gold to green
        var colors = values.map(function (_, i) {
            var ratio = i / Math.max(values.length - 1, 1);
            var r = Math.round(201 + (36 - 201) * ratio);
            var g = Math.round(168 + (59 - 168) * ratio);
            var b = Math.round(76 + (51 - 76) * ratio);
            return 'rgb(' + r + ',' + g + ',' + b + ')';
        });

        new Chart(canvas, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    data: values,
                    backgroundColor: colors,
                    borderRadius: 6,
                    borderSkipped: false,
                    barPercentage: 0.7
                }]
            },
            options: {
                indexAxis: 'y',
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { display: false }
                },
                scales: {
                    x: {
                        beginAtZero: true,
                        grid: {
                            color: 'rgba(0,0,0,0.04)',
                            drawBorder: false
                        },
                        ticks: { font: { size: 11 } }
                    },
                    y: {
                        grid: { display: false },
                        ticks: {
                            font: { size: 12, weight: '600' },
                            mirror: false
                        }
                    }
                }
            }
        });
    }

    // =========================================
    // 4. Initialize Everything on DOM Ready
    // =========================================
    document.addEventListener('DOMContentLoaded', function () {
        // Counters
        initCounters();

        // Progress bars
        initProgressBars();

        // Charts
        configureChartDefaults();
        initPersonnelChart();
        initAdminOverviewChart();
        initPositionChart('chartPositionStatus');
        initPositionChart('chartNCOPositionStatus');
        initPositionChart('chartOrgPositionStatus');
        initRankChart();
    });

})();
