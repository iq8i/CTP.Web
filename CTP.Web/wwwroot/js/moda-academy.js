/* ============================================= */
/* MODA Air Defense Academy - JavaScript         */
/* طلبة كلية الدفاع الجوي                         */
/* ============================================= */
(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        animateCounters();
        setupQuickSearch();
        restoreSelect();
        animateProgressBars();
    });

    /* ======================================== */
    /* ANIMATED COUNTERS                         */
    /* ======================================== */
    function animateCounters() {
        var counters = document.querySelectorAll('[data-count-target]');
        if (!counters.length) return;

        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    startCounting(entry.target);
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.3 });

        counters.forEach(function (el) {
            observer.observe(el);
        });
    }

    function startCounting(el) {
        var target = parseInt(el.getAttribute('data-count-target'), 10);
        if (isNaN(target)) return;

        var duration = 1200;
        var startTime = null;

        function step(timestamp) {
            if (!startTime) startTime = timestamp;
            var progress = Math.min((timestamp - startTime) / duration, 1);
            var eased = 1 - Math.pow(1 - progress, 3);
            var current = Math.floor(eased * target);

            el.textContent = current.toLocaleString('en');

            if (progress < 1) {
                requestAnimationFrame(step);
            } else {
                el.textContent = target.toLocaleString('en');
            }
        }

        requestAnimationFrame(step);
    }

    /* ======================================== */
    /* QUICK SEARCH (in-table)                   */
    /* ======================================== */
    function setupQuickSearch() {
        var input = document.getElementById('acQuickSearch');
        if (!input) return;

        input.addEventListener('input', function () {
            var term = this.value.toLowerCase();
            var table = document.getElementById('studentsTable');
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
    /* RESTORE SELECT                            */
    /* ======================================== */
    function restoreSelect() {
        var el = document.getElementById('personnelType');
        if (!el) return;
        var val = el.dataset.selected;
        if (val) el.value = val;
    }

    /* ======================================== */
    /* PROGRESS BARS                             */
    /* ======================================== */
    function animateProgressBars() {
        var bars = document.querySelectorAll('[data-bar-width]');
        if (!bars.length) return;

        setTimeout(function () {
            bars.forEach(function (bar) {
                var w = bar.getAttribute('data-bar-width');
                if (w) bar.style.width = w + '%';
            });
        }, 400);
    }

})();
