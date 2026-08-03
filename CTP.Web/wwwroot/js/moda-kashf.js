/* ============================================= */
/* MODA Kashf (Checklists) - JavaScript          */
/* إدارة الكشوفات                                 */
/* ============================================= */
(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        setupToastFromTempData();
    });

    /* ======================================== */
    /* HELPERS                                   */
    /* ======================================== */
    function getUrl(id) {
        var el = document.getElementById(id);
        return el ? el.value : '';
    }

    function showLoading() {
        var el = document.getElementById('loadingOverlay');
        if (el) el.classList.add('show');
    }

    function hideLoading() {
        var el = document.getElementById('loadingOverlay');
        if (el) el.classList.remove('show');
    }

    function showToast(message, type) {
        var toast = document.getElementById('kfToast');
        if (!toast) return;
        toast.className = 'kf-toast kf-toast-' + (type || 'success');
        toast.innerHTML = '<i class="bi ' + (type === 'error' ? 'bi-x-circle-fill' : 'bi-check-circle-fill') + '"></i> ' + message;
        setTimeout(function () { toast.classList.add('show'); }, 50);
        setTimeout(function () { toast.classList.remove('show'); }, 3500);
    }

    function setupToastFromTempData() {
        var s = document.getElementById('tempSuccess');
        var e = document.getElementById('tempError');
        if (s && s.value) showToast(s.value, 'success');
        if (e && e.value) showToast(e.value, 'error');
    }

    /* ======================================== */
    /* DELETE KASHF                               */
    /* ======================================== */
    window.deleteKashf = function (kashfId) {
        Moda.confirmDelete({
            title: 'حذف الكشف',
            text: 'هل أنت متأكد من حذف هذا الكشف؟ لا يمكن التراجع.'
        }).then(function (res) {
            if (!res.isConfirmed) return;

            Moda.loading('جاري الحذف...');
            fetch(getUrl('deleteUrl') + '/' + kashfId, { method: 'POST' })
                .then(function (r) { return r.json(); })
                .then(function (response) {
                    Moda.closeLoading();
                    if (response.success) {
                        Moda.toastSuccess('تم حذف الكشف بنجاح');
                        setTimeout(function () { location.reload(); }, 1000);
                    } else {
                        Moda.error('فشل الحذف', response.message || 'تعذّر حذف الكشف');
                    }
                })
                .catch(function () {
                    Moda.closeLoading();
                    Moda.error('خطأ', 'حدث خطأ أثناء الحذف');
                });
        });
    };

    /* ======================================== */
    /* EXPORT TO PDF                              */
    /* ======================================== */
    window.exportToPdf = function (kashfId) {
        window.location.href = getUrl('exportUrl') + '?id=' + kashfId;
    };

})();
