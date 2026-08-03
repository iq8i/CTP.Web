/* ============================================= */
/* MODA Unassigned Officers - JavaScript         */
/* الضباط الغير معينين                             */
/* ============================================= */
(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        animateCounters();
        setupImportModal();
        showToastMessages();
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

        counters.forEach(function (el) { observer.observe(el); });
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
    /* IMPORT MODAL                              */
    /* ======================================== */
    function setupImportModal() {
        var importBtn = document.getElementById('importBtn');
        var overlay = document.getElementById('importOverlay');
        var cancelBtn = document.getElementById('importCancel');
        var confirmBtn = document.getElementById('importConfirm');

        if (!importBtn || !overlay) return;

        importBtn.addEventListener('click', function () {
            overlay.classList.add('show');
        });

        if (cancelBtn) {
            cancelBtn.addEventListener('click', function () {
                overlay.classList.remove('show');
            });
        }

        overlay.addEventListener('click', function (e) {
            if (e.target === overlay) overlay.classList.remove('show');
        });

        if (confirmBtn) {
            confirmBtn.addEventListener('click', function () {
                var fileInput = document.getElementById('excelFile');
                if (!fileInput || !fileInput.files[0]) {
                    alert('الرجاء اختيار ملف');
                    return;
                }

                var importUrl = document.getElementById('importUrl');
                if (!importUrl) return;

                var formData = new FormData();
                formData.append('file', fileInput.files[0]);

                confirmBtn.disabled = true;
                confirmBtn.textContent = 'جاري الاستيراد...';

                fetch(importUrl.value, {
                    method: 'POST',
                    body: formData
                })
                .then(function (r) { return r.json(); })
                .then(function (data) {
                    overlay.classList.remove('show');
                    if (data.success) {
                        showToast('success', data.message || 'تم الاستيراد بنجاح');
                        setTimeout(function () { location.reload(); }, 1500);
                    } else {
                        showToast('error', data.message || 'فشل الاستيراد');
                    }
                })
                .catch(function () {
                    overlay.classList.remove('show');
                    showToast('error', 'حدث خطأ أثناء الاستيراد');
                })
                .finally(function () {
                    confirmBtn.disabled = false;
                    confirmBtn.textContent = 'استيراد';
                });
            });
        }
    }

    /* ======================================== */
    /* DOWNLOAD TEMPLATE                         */
    /* ======================================== */
    window.uaDownloadTemplate = function () {
        var url = document.getElementById('templateUrl');
        if (url) window.location.href = url.value;
    };

    /* ======================================== */
    /* TOAST MESSAGES                             */
    /* ======================================== */
    function showToastMessages() {
        var successMsg = document.getElementById('toastSuccess');
        var errorMsg = document.getElementById('toastError');

        if (successMsg && successMsg.value) {
            showToast('success', successMsg.value);
        }
        if (errorMsg && errorMsg.value) {
            showToast('error', errorMsg.value);
        }
    }

    function showToast(type, message) {
        var toast = document.getElementById('uaToast');
        if (!toast) return;

        toast.className = 'ua-toast ' + type + ' show';
        toast.textContent = message;

        setTimeout(function () {
            toast.classList.remove('show');
        }, 3500);
    }

})();
