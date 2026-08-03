/* ============================================= */
/* MODA App - Global JavaScript                  */
/* ============================================= */

(function () {
    'use strict';

    // ---- Sidebar Toggle (Mobile) ----
    const sidebar = document.querySelector('.moda-sidebar');
    const overlay = document.querySelector('.moda-sidebar-overlay');
    const toggleBtn = document.querySelector('.moda-sidebar-toggle');

    function openSidebar() {
        if (sidebar) sidebar.classList.add('active');
        if (overlay) overlay.classList.add('active');
        document.body.style.overflow = 'hidden';
    }

    function closeSidebar() {
        if (sidebar) sidebar.classList.remove('active');
        if (overlay) overlay.classList.remove('active');
        document.body.style.overflow = '';
    }

    if (toggleBtn) {
        toggleBtn.addEventListener('click', function () {
            if (sidebar && sidebar.classList.contains('active')) {
                closeSidebar();
            } else {
                openSidebar();
            }
        });
    }

    if (overlay) {
        overlay.addEventListener('click', closeSidebar);
    }

    // Close sidebar on Escape key
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') {
            closeSidebar();
        }
    });

    // ---- Card Animation on Scroll ----
    function animateOnScroll() {
        var cards = document.querySelectorAll('.moda-stat-card, .moda-card, .moda-section-card');
        cards.forEach(function (card, index) {
            var rect = card.getBoundingClientRect();
            if (rect.top < window.innerHeight - 50) {
                card.style.opacity = '1';
                card.style.transform = 'translateY(0)';
            }
        });
    }

    // Initial animation setup
    document.addEventListener('DOMContentLoaded', function () {
        var cards = document.querySelectorAll('.moda-stat-card, .moda-card, .moda-section-card');
        cards.forEach(function (card, index) {
            card.style.opacity = '0';
            card.style.transform = 'translateY(15px)';
            card.style.transition = 'opacity 0.4s ease ' + (index * 0.06) + 's, transform 0.4s ease ' + (index * 0.06) + 's';
        });

        // Trigger initial animation
        setTimeout(animateOnScroll, 50);
    });

    window.addEventListener('scroll', animateOnScroll);

    // ---- Active Menu Item Highlight ----
    document.addEventListener('DOMContentLoaded', function () {
        var currentPath = window.location.pathname.toLowerCase();
        var menuItems = document.querySelectorAll('.moda-menu-item');

        menuItems.forEach(function (item) {
            var href = item.getAttribute('href');
            if (href && currentPath === href.toLowerCase()) {
                item.classList.add('active');
            }
        });
    });

    // ---- Tooltip Init (Bootstrap) ----
    document.addEventListener('DOMContentLoaded', function () {
        var tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
        tooltipTriggerList.forEach(function (el) {
            new bootstrap.Tooltip(el);
        });
    });

})();
