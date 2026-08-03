/* ============================================= */
/* MODA Predictive Analytics - JavaScript        */
/* مركز التحليلات التنبؤية                        */
/* ============================================= */
(function () {
    'use strict';

    var retirementData = [];
    var retirementChart = null;
    var serviceYearsChart = null;

    document.addEventListener('DOMContentLoaded', function () {
        updateLiveTime();
        setInterval(updateLiveTime, 1000);

        loadRetirementPredictions();
        loadPromotionForecast();
        loadPersonnelGaps();
        loadServiceYearsAnalysis();
        loadAttritionRisk();
    });

    /* ======================================== */
    /* HELPERS                                   */
    /* ======================================== */
    function getUrl(id) {
        var el = document.getElementById(id);
        return el ? el.value : '';
    }

    function updateLiveTime() {
        var el = document.getElementById('liveTime');
        if (!el) return;
        var now = new Date();
        el.textContent = now.toLocaleTimeString('ar-SA', { hour: '2-digit', minute: '2-digit', second: '2-digit' });
    }

    function setLoading(id) {
        var el = document.getElementById(id);
        if (el) {
            el.innerHTML = '<div class="pa-loading"><div class="pa-spinner"></div><span class="pa-loading-text">جاري تحميل البيانات...</span></div>';
        }
    }

    function setEmpty(id, text) {
        var el = document.getElementById(id);
        if (el) {
            el.innerHTML = '<div class="pa-empty"><i class="bi bi-inbox"></i><div class="pa-empty-text">' + (text || 'لا توجد بيانات') + '</div></div>';
        }
    }

    function setError(id) {
        var el = document.getElementById(id);
        if (el) {
            el.innerHTML = '<div class="pa-empty"><i class="bi bi-exclamation-circle"></i><div class="pa-empty-text">فشل في تحميل البيانات</div></div>';
        }
    }

    /* ======================================== */
    /* RETIREMENT PREDICTIONS                    */
    /* ======================================== */
    function loadRetirementPredictions() {
        fetch(getUrl('retirementUrl'))
            .then(function (r) { return r.json(); })
            .then(function (data) {
                retirementData = data;
                var urgent = data.filter(function (r) { return r.yearsUntilRetirement <= 2; }).length;

                document.getElementById('upcomingRetirements').textContent = urgent;

                setTimeout(function () {
                    var bar = document.getElementById('retirementBar');
                    if (bar) bar.style.width = Math.min(urgent * 5, 100) + '%';
                }, 300);

                // Build chart
                var yearCounts = {};
                data.forEach(function (r) {
                    yearCounts[r.retirementYear] = (yearCounts[r.retirementYear] || 0) + 1;
                });

                var years = Object.keys(yearCounts).sort();
                var counts = years.map(function (y) { return yearCounts[y]; });

                if (retirementChart) retirementChart.destroy();

                var ctx = document.getElementById('retirementChart');
                if (ctx) {
                    retirementChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: years,
                            datasets: [{
                                label: 'عدد المتقاعدين المتوقع',
                                data: counts,
                                backgroundColor: 'rgba(239, 68, 68, 0.7)',
                                borderColor: '#EF4444',
                                borderWidth: 2,
                                borderRadius: 8
                            }]
                        },
                        options: {
                            responsive: true,
                            maintainAspectRatio: false,
                            animation: false,
                            plugins: { legend: { display: false } },
                            scales: {
                                y: { beginAtZero: true, ticks: { stepSize: 1 } },
                                x: { grid: { display: false } }
                            }
                        }
                    });
                }

                // Show table with high priority
                filterRetirements('high');
                var highBtn = document.querySelector('.pa-filter-btn.danger');
                if (highBtn) highBtn.classList.add('active');
            })
            .catch(function () {
                document.getElementById('upcomingRetirements').textContent = '0';
                setError('retirementsBody');
            });
    }

    /* ======================================== */
    /* FILTER RETIREMENTS                        */
    /* ======================================== */
    window.filterRetirements = function (priority, btn) {
        var buttons = document.querySelectorAll('.pa-retirement-filters .pa-filter-btn');
        buttons.forEach(function (b) { b.classList.remove('active'); });
        if (btn) btn.classList.add('active');

        var filtered = priority === 'all' ? retirementData : retirementData.filter(function (r) { return r.priority === priority; });
        var detailsUrl = getUrl('detailsUrl');

        var html = '';
        filtered.forEach(function (r) {
            var badgeClass = r.priority === 'high' ? 'danger' : r.priority === 'medium' ? 'warning' : 'info';
            var priorityText = r.priority === 'high' ? 'عاجل' : r.priority === 'medium' ? 'متوسط' : 'طويل';
            var icon = r.priority === 'high' ? 'exclamation-circle-fill' : r.priority === 'medium' ? 'clock-fill' : 'hourglass-split';

            html += '<tr>';
            html += '<td><a href="' + detailsUrl + '/' + r.serialNumber + '">' + r.serialNumber + '</a></td>';
            html += '<td>' + (r.fullName || 'غير محدد') + '</td>';
            html += '<td>' + (r.rank || 'غير محدد') + '</td>';
            html += '<td>' + r.serviceYears + ' سنة</td>';
            html += '<td><strong>' + r.yearsUntilRetirement + '</strong> سنة</td>';
            html += '<td>' + r.retirementYear + '</td>';
            html += '<td><span class="pa-badge ' + badgeClass + '"><i class="bi bi-' + icon + '"></i> ' + priorityText + '</span></td>';
            html += '</tr>';
        });

        var body = document.getElementById('retirementsBody');
        if (body) {
            body.innerHTML = html || '<tr><td colspan="7"><div class="pa-empty"><i class="bi bi-inbox"></i><div class="pa-empty-text">لا توجد بيانات للعرض</div></div></td></tr>';
        }
    };

    /* ======================================== */
    /* PROMOTION FORECAST                        */
    /* ======================================== */
    function loadPromotionForecast() {
        fetch(getUrl('promotionUrl'))
            .then(function (r) { return r.json(); })
            .then(function (data) {
                document.getElementById('eligiblePromotions').textContent = data.eligible;

                setTimeout(function () {
                    var bar = document.getElementById('promotionBar');
                    if (bar) bar.style.width = Math.min(data.eligible * 3, 100) + '%';
                }, 500);

                var detailsUrl = getUrl('detailsUrl');
                var html = '';
                var personnel = data.personnel || [];
                personnel.slice(0, 20).forEach(function (p) {
                    var statusBadge = p.eligible
                        ? '<span class="pa-badge success"><i class="bi bi-check-circle-fill"></i> مؤهل الآن</span>'
                        : '<span class="pa-badge info"><i class="bi bi-hourglass-split"></i> بعد ' + p.yearsUntilPromotion + ' سنة</span>';

                    html += '<tr>';
                    html += '<td><a href="' + detailsUrl + '/' + p.serialNumber + '">' + p.serialNumber + '</a></td>';
                    html += '<td>' + (p.fullName || 'غير محدد') + '</td>';
                    html += '<td>' + (p.currentRank || 'غير محدد') + '</td>';
                    html += '<td>' + p.serviceYears + ' سنة</td>';
                    html += '<td>' + statusBadge + '</td>';
                    html += '<td>' + p.estimatedPromotionYear + '</td>';
                    html += '</tr>';
                });

                var body = document.getElementById('promotionsBody');
                if (body) {
                    body.innerHTML = html || '<tr><td colspan="6"><div class="pa-empty"><i class="bi bi-inbox"></i><div class="pa-empty-text">لا توجد بيانات</div></div></td></tr>';
                }
            })
            .catch(function () {
                document.getElementById('eligiblePromotions').textContent = '0';
                setError('promotionsBody');
            });
    }

    /* ======================================== */
    /* PERSONNEL GAPS                            */
    /* ======================================== */
    function loadPersonnelGaps() {
        fetch(getUrl('gapsUrl'))
            .then(function (r) { return r.json(); })
            .then(function (data) {
                document.getElementById('unitsWithGaps').textContent = data.unitsWithGaps;

                setTimeout(function () {
                    var bar = document.getElementById('gapsBar');
                    if (bar) bar.style.width = Math.min(data.unitsWithGaps * 10, 100) + '%';
                }, 700);

                var container = document.getElementById('gapsContainer');
                if (!container) return;

                var gaps = data.unitGaps || [];
                if (gaps.length === 0) {
                    container.innerHTML = '<div class="pa-empty"><i class="bi bi-check-circle"></i><div class="pa-empty-text">لا توجد فجوات وظيفية</div></div>';
                    return;
                }

                var html = '';
                gaps.forEach(function (gap) {
                    var sevClass = gap.severity === 'critical' ? 'critical' : 'moderate';
                    var sevText = gap.severity === 'critical' ? 'حرج' : 'متوسط';
                    var gapValClass = gap.severity === 'critical' ? 'gap-danger' : 'gap-warning';

                    html += '<div class="pa-gap-card ' + sevClass + '">';
                    html += '<div class="pa-gap-head">';
                    html += '<div>';
                    html += '<h5 class="pa-gap-name">' + gap.unitName + '</h5>';
                    html += '<div class="pa-gap-id"><i class="bi bi-hash"></i> ' + gap.unitNumber + '</div>';
                    html += '</div>';
                    html += '<span class="pa-gap-severity ' + sevClass + '">' + sevText + '</span>';
                    html += '</div>';
                    html += '<div class="pa-gap-stats">';
                    html += '<div class="pa-gap-stat"><div class="pa-gap-stat-label">القوة الحالية</div><div class="pa-gap-stat-value">' + gap.currentStrength + '</div></div>';
                    html += '<div class="pa-gap-stat"><div class="pa-gap-stat-label">الفجوة</div><div class="pa-gap-stat-value ' + gapValClass + '">-' + gap.gap + '</div></div>';
                    html += '</div>';
                    html += '</div>';
                });

                container.innerHTML = html;
            })
            .catch(function () {
                document.getElementById('unitsWithGaps').textContent = '0';
                setError('gapsContainer');
            });
    }

    /* ======================================== */
    /* SERVICE YEARS ANALYSIS                    */
    /* ======================================== */
    function loadServiceYearsAnalysis() {
        fetch(getUrl('serviceYearsUrl'))
            .then(function (r) { return r.json(); })
            .then(function (data) {
                var labels = data.map(function (d) { return d.category + ' سنة'; });
                var counts = data.map(function (d) { return d.count; });

                if (serviceYearsChart) serviceYearsChart.destroy();

                var ctx = document.getElementById('serviceYearsChart');
                if (!ctx) return;

                serviceYearsChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: labels,
                        datasets: [{
                            data: counts,
                            backgroundColor: [
                                'rgba(59,130,246,0.85)',
                                'rgba(16,185,129,0.85)',
                                'rgba(245,158,11,0.85)',
                                'rgba(239,68,68,0.85)',
                                'rgba(139,92,246,0.85)',
                                'rgba(236,72,153,0.85)'
                            ],
                            borderColor: '#ffffff',
                            borderWidth: 3
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        cutout: '55%',
                        animation: false,
                        plugins: {
                            legend: {
                                position: 'bottom',
                                rtl: true,
                                labels: {
                                    padding: 16,
                                    usePointStyle: true,
                                    pointStyle: 'circle',
                                    font: { size: 12, weight: '500' }
                                }
                            }
                        }
                    }
                });
            })
            .catch(function () {});
    }

    /* ======================================== */
    /* ATTRITION RISK                            */
    /* ======================================== */
    function loadAttritionRisk() {
        fetch(getUrl('attritionUrl'))
            .then(function (r) { return r.json(); })
            .then(function (data) {
                document.getElementById('highRiskPersonnel').textContent = data.highRisk;

                setTimeout(function () {
                    var bar = document.getElementById('riskBar');
                    if (bar) bar.style.width = Math.min(data.highRisk * 5, 100) + '%';
                }, 900);
            })
            .catch(function () {
                document.getElementById('highRiskPersonnel').textContent = '0';
            });
    }

})();
