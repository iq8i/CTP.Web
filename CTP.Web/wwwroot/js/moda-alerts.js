/* ============================================= */
/* MODA Alerts - SweetAlert2 wrapper             */
/* تنبيهات وحوارات نظام مودا الموحدة             */
/* ============================================= */
(function (global) {
    'use strict';

    if (typeof Swal === 'undefined') {
        console.warn('[moda-alerts] SweetAlert2 not loaded — wrapper disabled');
        return;
    }

    // ألوان النظام
    var THEME = {
        primary: '#1a3d2d',
        primaryLight: '#2d5a3f',
        gold: '#C9A84C',
        success: '#16a34a',
        danger: '#dc2626',
        warning: '#d97706',
        info: '#2563eb'
    };

    // إعدادات افتراضية لكل التنبيهات
    var defaults = {
        confirmButtonColor: THEME.primary,
        cancelButtonColor: '#94a3b8',
        denyButtonColor: THEME.danger,
        customClass: {
            popup: 'moda-swal-popup',
            confirmButton: 'moda-swal-confirm',
            cancelButton: 'moda-swal-cancel',
            denyButton: 'moda-swal-deny'
        },
        buttonsStyling: true,
        reverseButtons: true,
        focusConfirm: false
    };

    // أنماط مخصّصة (تُحقن مرة واحدة)
    if (!document.getElementById('moda-swal-styles')) {
        var style = document.createElement('style');
        style.id = 'moda-swal-styles';
        style.textContent = [
            '.moda-swal-popup{font-family:inherit;border-radius:14px!important;direction:rtl;text-align:right;}',
            '.moda-swal-popup .swal2-title{font-weight:700;color:#1e293b;}',
            '.moda-swal-popup .swal2-html-container{color:#475569;font-size:.95rem;}',
            '.moda-swal-confirm{font-weight:600!important;border-radius:10px!important;padding:8px 22px!important;}',
            '.moda-swal-cancel{font-weight:600!important;border-radius:10px!important;padding:8px 22px!important;}',
            '.moda-swal-deny{font-weight:600!important;border-radius:10px!important;padding:8px 22px!important;}',
            '.moda-swal-popup .swal2-actions{gap:8px;}',
            '.swal2-toast.moda-swal-popup{padding:14px 18px!important;}',
            '.swal2-icon.swal2-success{border-color:' + THEME.success + '!important;color:' + THEME.success + '!important;}',
            '.swal2-icon.swal2-error{border-color:' + THEME.danger + '!important;color:' + THEME.danger + '!important;}',
            '.swal2-icon.swal2-warning{border-color:' + THEME.warning + '!important;color:' + THEME.warning + '!important;}',
            '.swal2-icon.swal2-info{border-color:' + THEME.info + '!important;color:' + THEME.info + '!important;}',
            '.swal2-icon.swal2-question{border-color:' + THEME.primary + '!important;color:' + THEME.primary + '!important;}'
        ].join('');
        document.head.appendChild(style);
    }

    // مزج إعدادات مع الافتراضي
    function mix(opts) {
        opts = opts || {};
        var merged = Object.assign({}, defaults, opts);
        if (opts.customClass) {
            merged.customClass = Object.assign({}, defaults.customClass, opts.customClass);
        }
        return merged;
    }

    var Moda = {
        // تأكيد إجراء (نعم/إلغاء) — يُرجع Promise بقيمة isConfirmed
        confirm: function (opts) {
            return Swal.fire(mix(Object.assign({
                icon: 'question',
                title: opts.title || 'تأكيد',
                text: opts.text || 'هل أنت متأكد؟',
                showCancelButton: true,
                confirmButtonText: opts.confirmText || 'نعم، تأكيد',
                cancelButtonText: opts.cancelText || 'إلغاء',
                confirmButtonColor: opts.danger ? THEME.danger : THEME.primary
            }, opts)));
        },

        // تأكيد حذف — لون أحمر
        confirmDelete: function (opts) {
            opts = opts || {};
            return Swal.fire(mix({
                icon: 'warning',
                title: opts.title || 'تأكيد الحذف',
                html: opts.text || 'هل أنت متأكد من الحذف؟<br><small style="color:#dc2626;">هذا الإجراء لا يمكن التراجع عنه.</small>',
                showCancelButton: true,
                confirmButtonText: opts.confirmText || 'نعم، احذف',
                cancelButtonText: opts.cancelText || 'إلغاء',
                confirmButtonColor: THEME.danger
            }));
        },

        // رسالة نجاح
        success: function (title, text) {
            return Swal.fire(mix({
                icon: 'success',
                title: title || 'تمت العملية بنجاح',
                text: text || '',
                confirmButtonText: 'حسناً',
                timer: text ? undefined : 2000,
                timerProgressBar: !text
            }));
        },

        // رسالة خطأ
        error: function (title, text) {
            return Swal.fire(mix({
                icon: 'error',
                title: title || 'حدث خطأ',
                text: text || 'حدث خطأ غير متوقع، حاول مرة أخرى.',
                confirmButtonText: 'حسناً'
            }));
        },

        // تنبيه (warning)
        warning: function (title, text) {
            return Swal.fire(mix({
                icon: 'warning',
                title: title || 'تنبيه',
                text: text || '',
                confirmButtonText: 'حسناً'
            }));
        },

        // معلومة (info)
        info: function (title, text) {
            return Swal.fire(mix({
                icon: 'info',
                title: title || 'معلومة',
                text: text || '',
                confirmButtonText: 'حسناً'
            }));
        },

        // toast (إشعار صغير في الزاوية)
        toast: function (icon, title) {
            var Toast = Swal.mixin({
                toast: true,
                position: 'top-start',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                customClass: { popup: 'moda-swal-popup' }
            });
            return Toast.fire({ icon: icon || 'success', title: title || '' });
        },
        toastSuccess: function (title) { return this.toast('success', title || 'تمت العملية بنجاح'); },
        toastError:   function (title) { return this.toast('error',   title || 'حدث خطأ'); },
        toastWarning: function (title) { return this.toast('warning', title || 'تنبيه'); },
        toastInfo:    function (title) { return this.toast('info',    title || 'معلومة'); },

        // إظهار loading
        loading: function (title) {
            Swal.fire({
                title: title || 'جاري المعالجة...',
                allowOutsideClick: false,
                allowEscapeKey: false,
                didOpen: function () { Swal.showLoading(); },
                customClass: { popup: 'moda-swal-popup' }
            });
        },
        closeLoading: function () { Swal.close(); },

        // عرض أخطاء validation كقائمة
        showErrors: function (errors, title) {
            var list = (errors || []).map(function (e) { return '<li style="margin:4px 0;">' + e + '</li>'; }).join('');
            return Swal.fire(mix({
                icon: 'error',
                title: title || 'يوجد أخطاء في البيانات',
                html: '<ul style="text-align:right;direction:rtl;padding-right:18px;color:#475569;">' + list + '</ul>',
                confirmButtonText: 'حسناً'
            }));
        },

        // التقاط TempData من الصفحة (Success/Error/Warning) وعرضها تلقائياً
        autoFlash: function () {
            var s = document.querySelector('[data-flash-success]');
            if (s) { this.toastSuccess(s.getAttribute('data-flash-success')); s.remove(); }
            var e = document.querySelector('[data-flash-error]');
            if (e) { this.error('خطأ', e.getAttribute('data-flash-error')); e.remove(); }
            var w = document.querySelector('[data-flash-warning]');
            if (w) { this.warning('تنبيه', w.getAttribute('data-flash-warning')); w.remove(); }
            var i = document.querySelector('[data-flash-info]');
            if (i) { this.info('معلومة', i.getAttribute('data-flash-info')); i.remove(); }
        }
    };

    global.Moda = Moda;

    // التشغيل التلقائي عند التحميل
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', function () { Moda.autoFlash(); });
    } else {
        Moda.autoFlash();
    }

})(window);
