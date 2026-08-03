/* ============================================= */
/* MODA Officer List Pages - Shared JavaScript   */
/* صفحات القوائم المشتركة (متقاعدون/منقولون)     */
/* ============================================= */
(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        animateCounters();
        restoreSelects();
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
    /* RESTORE SELECTS                           */
    /* ======================================== */
    function restoreSelects() {
        var selects = document.querySelectorAll('select[data-selected]');
        selects.forEach(function (el) {
            var val = el.dataset.selected;
            if (val) el.value = val;
        });
    }

    /* ======================================== */
    /* TOAST MESSAGES                             */
    /* ======================================== */
    function showToastMessages() {
        var successEl = document.getElementById('olToastSuccess');
        var errorEl = document.getElementById('olToastError');

        if (successEl && successEl.value) showToast('success', successEl.value);
        if (errorEl && errorEl.value) showToast('error', errorEl.value);
    }

    function showToast(type, message) {
        var toast = document.getElementById('olToast');
        if (!toast) return;
        toast.className = 'ol-toast ' + type + ' show';
        toast.textContent = message;
        setTimeout(function () { toast.classList.remove('show'); }, 3500);
    }

    /* ======================================== */
    /* TRANSFERS - Mark Returned                 */
    /* ======================================== */
    window.olMarkReturned = function (id) {
        Moda.confirm({
            title: 'تأكيد العودة للخدمة',
            text: 'هل عاد هذا الضابط للخدمة؟',
            confirmText: 'نعم، عاد',
            confirmButtonColor: '#16a34a'
        }).then(function (res) {
            if (!res.isConfirmed) return;
            var url = document.getElementById('markReturnedUrl');
            if (!url) return;

            fetch(url.value, {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: 'id=' + encodeURIComponent(id)
            }).then(function () {
                Moda.toastSuccess('تم تسجيل عودة الضابط');
                setTimeout(function () { location.reload(); }, 800);
            }).catch(function () {
                Moda.error('خطأ', 'حدث خطأ أثناء التحديث');
            });
        });
    };

    /* ======================================== */
    /* TRANSFERS - Delete                        */
    /* ======================================== */
    window.olDeleteTransfer = function (id) {
        Moda.confirmDelete({
            title: 'حذف سجل النقل',
            text: 'سيتم حذف هذا السجل نهائياً. هل تريد المتابعة؟'
        }).then(function (res) {
            if (!res.isConfirmed) return;
            var url = document.getElementById('deleteUrl');
            if (!url) return;

            fetch(url.value, {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: 'id=' + encodeURIComponent(id)
            }).then(function () {
                Moda.toastSuccess('تم حذف السجل');
                setTimeout(function () { location.reload(); }, 800);
            }).catch(function () {
                Moda.error('خطأ', 'حدث خطأ أثناء الحذف');
            });
        });
    };

})();
