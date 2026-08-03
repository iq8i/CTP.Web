/* ============================================= */
/* MODA Officer Affairs Dashboard - JavaScript   */
/* لوحة معلومات شؤون الضباط                       */
/* ============================================= */
(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        animateCounters();
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
        var start = 0;
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
    /* PROGRESS BARS                             */
    /* ======================================== */
    function animateProgressBars() {
        var bars = document.querySelectorAll('[data-bar-width]');
        if (!bars.length) return;

        setTimeout(function () {
            bars.forEach(function (bar) {
                var width = bar.getAttribute('data-bar-width');
                if (width) bar.style.width = width + '%';
            });
        }, 400);
    }

})();
