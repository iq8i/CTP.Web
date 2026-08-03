/**
 * SweetAlert2 Helper Functions for KAADA Exam System
 * Provides unified alert/confirm/toast functions across the system
 */

// ─── Toast Notifications (auto-dismiss) ──────────────────────────

function showToastSuccess(message) {
    Swal.fire({
        icon: 'success',
        title: message,
        toast: true,
        position: 'top',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: function(toast) {
            toast.addEventListener('mouseenter', Swal.stopTimer);
            toast.addEventListener('mouseleave', Swal.resumeTimer);
        }
    });
}

function showToastError(message) {
    Swal.fire({
        icon: 'error',
        title: message,
        toast: true,
        position: 'top',
        showConfirmButton: false,
        timer: 4000,
        timerProgressBar: true
    });
}

function showToastWarning(message) {
    Swal.fire({
        icon: 'warning',
        title: message,
        toast: true,
        position: 'top',
        showConfirmButton: false,
        timer: 3500,
        timerProgressBar: true
    });
}

function showToastInfo(message) {
    Swal.fire({
        icon: 'info',
        title: message,
        toast: true,
        position: 'top',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true
    });
}

// ─── Alert Dialogs ───────────────────────────────────────────────

function showSuccess(message, title) {
    return Swal.fire({
        icon: 'success',
        title: title || 'تم بنجاح',
        text: message,
        confirmButtonText: 'حسناً',
        confirmButtonColor: '#006838'
    });
}

function showError(message, title) {
    return Swal.fire({
        icon: 'error',
        title: title || 'خطأ',
        text: message,
        confirmButtonText: 'حسناً',
        confirmButtonColor: '#dc3545'
    });
}

function showWarning(message, title) {
    return Swal.fire({
        icon: 'warning',
        title: title || 'تنبيه',
        text: message,
        confirmButtonText: 'حسناً',
        confirmButtonColor: '#f39c12'
    });
}

function showInfo(message, title) {
    return Swal.fire({
        icon: 'info',
        title: title || 'معلومة',
        text: message,
        confirmButtonText: 'حسناً',
        confirmButtonColor: '#006838'
    });
}

// ─── Confirmation Dialogs ────────────────────────────────────────

function confirmAction(message, title, callback) {
    Swal.fire({
        icon: 'question',
        title: title || 'تأكيد',
        text: message,
        showCancelButton: true,
        confirmButtonText: 'نعم',
        cancelButtonText: 'إلغاء',
        confirmButtonColor: '#006838',
        cancelButtonColor: '#6c757d',
        reverseButtons: true
    }).then(function(result) {
        if (result.isConfirmed && callback) {
            callback();
        }
    });
}

function confirmDelete(message, callback) {
    Swal.fire({
        icon: 'warning',
        title: 'تأكيد الحذف',
        text: message || 'هل أنت متأكد من الحذف؟ لا يمكن التراجع عن هذا الإجراء.',
        showCancelButton: true,
        confirmButtonText: 'نعم، احذف',
        cancelButtonText: 'إلغاء',
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        reverseButtons: true
    }).then(function(result) {
        if (result.isConfirmed && callback) {
            callback();
        }
    });
}

function confirmDanger(message, title, confirmText, callback) {
    Swal.fire({
        icon: 'warning',
        title: title || 'تحذير',
        html: message,
        showCancelButton: true,
        confirmButtonText: confirmText || 'نعم، متأكد',
        cancelButtonText: 'إلغاء',
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        reverseButtons: true
    }).then(function(result) {
        if (result.isConfirmed && callback) {
            callback();
        }
    });
}

// ─── Form Submission Helpers ─────────────────────────────────────

function confirmSubmitForm(formElement, message, title) {
    Swal.fire({
        icon: 'question',
        title: title || 'تأكيد',
        text: message,
        showCancelButton: true,
        confirmButtonText: 'نعم',
        cancelButtonText: 'إلغاء',
        confirmButtonColor: '#006838',
        cancelButtonColor: '#6c757d',
        reverseButtons: true
    }).then(function(result) {
        if (result.isConfirmed) {
            formElement.submit();
        }
    });
    return false;
}

function confirmDeleteForm(formElement, message) {
    Swal.fire({
        icon: 'warning',
        title: 'تأكيد الحذف',
        text: message || 'هل أنت متأكد؟ لا يمكن التراجع عن هذا الإجراء.',
        showCancelButton: true,
        confirmButtonText: 'نعم، احذف',
        cancelButtonText: 'إلغاء',
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        reverseButtons: true
    }).then(function(result) {
        if (result.isConfirmed) {
            formElement.submit();
        }
    });
    return false;
}

// ─── AJAX Helpers ────────────────────────────────────────────────

function fetchWithAlert(url, options, successMsg, errorMsg) {
    return fetch(url, options)
        .then(function(response) { return response.json(); })
        .then(function(data) {
            if (data.success) {
                showToastSuccess(successMsg || data.message || 'تمت العملية بنجاح');
            } else {
                showToastError(errorMsg || data.message || 'حدث خطأ');
            }
            return data;
        })
        .catch(function(error) {
            console.error('Error:', error);
            showToastError(errorMsg || 'حدث خطأ في الاتصال');
            throw error;
        });
}

// ─── TempData Auto-Display ───────────────────────────────────────
// رسائل العمليات (إنشاء/تعديل/حذف) تظهر كنافذة وسطية كبيرة موحّدة لكل النظام — وليست toast صغيراً أعلى الصفحة.

function showCenteredSuccess(message) {
    Swal.fire({
        icon: 'success',
        title: 'تم',
        text: message,
        confirmButtonText: 'حسناً',
        confirmButtonColor: '#006838',
        timer: 3000,
        timerProgressBar: true
    });
}

function showCenteredError(message) {
    Swal.fire({
        icon: 'error',
        title: 'خطأ',
        text: message,
        confirmButtonText: 'حسناً',
        confirmButtonColor: '#dc3545'
    });
}

document.addEventListener('DOMContentLoaded', function() {
    var successEl = document.getElementById('tempdata-success');
    var errorEl = document.getElementById('tempdata-error');
    var infoEl = document.getElementById('tempdata-info');

    if (successEl && successEl.textContent.trim()) {
        showCenteredSuccess(successEl.textContent.trim());
    }
    if (errorEl && errorEl.textContent.trim()) {
        showCenteredError(errorEl.textContent.trim());
    }
    if (infoEl && infoEl.textContent.trim()) {
        Swal.fire({ icon: 'info', title: 'معلومة', text: infoEl.textContent.trim(), confirmButtonText: 'حسناً', confirmButtonColor: '#006838', width: '30rem' });
    }
});
