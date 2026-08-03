/* ============================================= */
/* MODA Users - JavaScript                       */
/* إدارة المستخدمين                                */
/* ============================================= */
(function () {
    'use strict';

    function getUrl(id) {
        var el = document.getElementById(id);
        return el ? el.value : '';
    }

    /* ======================================== */
    /* TOGGLE USER STATUS                        */
    /* ======================================== */
    window.toggleStatus = function (userId) {
        Moda.confirm({
            title: 'تغيير الحالة',
            text: 'هل تريد تغيير حالة هذا المستخدم؟',
            confirmText: 'نعم، غيّر الحالة'
        }).then(function (r) {
            if (!r.isConfirmed) return;

            Moda.loading('جاري التحديث...');
            fetch(getUrl('toggleStatusUrl'), {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: 'id=' + userId
            })
                .then(function (r) { return r.json(); })
                .then(function (response) {
                    Moda.closeLoading();
                    if (response.success) {
                        Moda.toastSuccess('تم تغيير حالة المستخدم');
                        setTimeout(function () { location.reload(); }, 800);
                    } else {
                        Moda.error('فشل التحديث', response.message || 'تعذّر تغيير الحالة');
                    }
                })
                .catch(function () {
                    Moda.closeLoading();
                    Moda.error('خطأ', 'حدث خطأ أثناء التحديث');
                });
        });
    };

    /* ======================================== */
    /* DELETE USER                               */
    /* ======================================== */
    window.deleteUser = function (userId) {
        Moda.confirmDelete({
            title: 'حذف المستخدم',
            text: 'سيتم حذف المستخدم نهائياً مع جميع أدواره المرتبطة. هل أنت متأكد؟'
        }).then(function (r) {
            if (!r.isConfirmed) return;

            Moda.loading('جاري الحذف...');
            fetch(getUrl('deleteUrl'), {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: 'id=' + userId
            })
                .then(function (r) { return r.json(); })
                .then(function (response) {
                    Moda.closeLoading();
                    if (response.success) {
                        Moda.toastSuccess('تم حذف المستخدم بنجاح');
                        setTimeout(function () { location.reload(); }, 800);
                    } else {
                        Moda.error('فشل الحذف', response.message || 'تعذّر حذف المستخدم');
                    }
                })
                .catch(function () {
                    Moda.closeLoading();
                    Moda.error('خطأ', 'حدث خطأ أثناء الحذف');
                });
        });
    };

})();
