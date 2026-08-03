/* ============================================= */
/* MODA Notifications Center - JavaScript        */
/* مركز الإشعارات                                 */
/* ============================================= */
(function () {
    'use strict';

    var allNotifications = [];

    document.addEventListener('DOMContentLoaded', function () {
        loadNotifications();
        loadStats();
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
        var toast = document.getElementById('ntToast');
        if (!toast) return;
        toast.className = 'nt-toast nt-toast-' + (type || 'success');
        toast.innerHTML = '<i class="bi ' + (type === 'error' ? 'bi-x-circle-fill' : 'bi-check-circle-fill') + '"></i> ' + message;
        setTimeout(function () { toast.classList.add('show'); }, 50);
        setTimeout(function () { toast.classList.remove('show'); }, 3500);
    }

    function formatDate(dateString) {
        var date = new Date(dateString);
        var now = new Date();
        var diff = now - date;
        var minutes = Math.floor(diff / 60000);
        var hours = Math.floor(minutes / 60);
        var days = Math.floor(hours / 24);

        if (days > 0) return '\u0645\u0646\u0630 ' + days + ' \u064A\u0648\u0645';
        if (hours > 0) return '\u0645\u0646\u0630 ' + hours + ' \u0633\u0627\u0639\u0629';
        if (minutes > 0) return '\u0645\u0646\u0630 ' + minutes + ' \u062F\u0642\u064A\u0642\u0629';
        return '\u0627\u0644\u0622\u0646';
    }

    /* ======================================== */
    /* LOAD DATA                                 */
    /* ======================================== */
    function loadNotifications() {
        fetch(getUrl('getRecentUrl') + '?count=100')
            .then(function (r) { return r.json(); })
            .then(function (data) {
                allNotifications = data;
                renderNotifications(data);
            })
            .catch(function () {
                setEmpty();
            });
    }

    function loadStats() {
        fetch(getUrl('getStatsUrl'))
            .then(function (r) { return r.json(); })
            .then(function (data) {
                document.getElementById('totalNotifications').textContent = data.total;
                document.getElementById('unreadNotifications').textContent = data.unread;
                document.getElementById('criticalNotifications').textContent = data.critical;
                document.getElementById('highNotifications').textContent = data.high;
            })
            .catch(function () {});
    }

    /* ======================================== */
    /* RENDER                                    */
    /* ======================================== */
    function renderNotifications(notifications) {
        var container = document.getElementById('notificationsList');
        if (!container) return;

        // Update count
        var countEl = document.getElementById('listCount');
        if (countEl) countEl.textContent = notifications.length;

        if (notifications.length === 0) {
            setEmpty();
            return;
        }

        var html = '';
        notifications.forEach(function (n) {
            var priorityClass = n.priority === 'Critical' ? 'critical' : n.priority === 'High' ? 'high' : n.priority === 'Normal' ? 'normal' : 'low';

            var categoryIcon = n.category === 'Retirement' ? 'person-x' : n.category === 'Maintenance' ? 'tools' : n.category === 'Promotion' ? 'arrow-up-circle' : 'bell';

            var priorityText = n.priority === 'Critical' ? '\u062D\u0631\u062C\u0629' : n.priority === 'High' ? '\u0639\u0627\u0644\u064A\u0629' : n.priority === 'Normal' ? '\u0639\u0627\u062F\u064A\u0629' : '\u0645\u0646\u062E\u0641\u0636\u0629';

            var readClass = n.isRead ? ' is-read' : '';

            html += '<div class="nt-item' + readClass + '">';
            html += '<div class="nt-item-priority-bar ' + priorityClass + '"></div>';
            html += '<div class="nt-item-icon ' + priorityClass + '"><i class="bi bi-' + categoryIcon + '"></i></div>';
            html += '<div class="nt-item-body">';
            html += '<div class="nt-item-head">';
            html += '<h5 class="nt-item-title">' + n.title + '</h5>';
            html += '<div class="nt-item-actions">';
            if (!n.isRead) {
                html += '<button class="nt-item-action mark-read" onclick="markAsRead(' + n.notificationId + ')" title="\u062A\u0639\u0644\u064A\u0645 \u0643\u0645\u0642\u0631\u0648\u0621"><i class="bi bi-check"></i></button>';
            }
            html += '<button class="nt-item-action delete" onclick="deleteNotification(' + n.notificationId + ')" title="\u062D\u0630\u0641"><i class="bi bi-trash"></i></button>';
            html += '</div></div>';
            if (n.message) {
                html += '<p class="nt-item-message">' + n.message + '</p>';
            }
            html += '<div class="nt-item-meta">';
            html += '<span class="nt-badge ' + priorityClass + '"><i class="bi bi-circle-fill"></i> ' + priorityText + '</span>';
            if (n.category) {
                html += '<span class="nt-badge category">' + n.category + '</span>';
            }
            html += '<span class="nt-badge time"><i class="bi bi-clock"></i> ' + formatDate(n.createdDate) + '</span>';
            if (n.actionUrl) {
                html += '<a href="' + n.actionUrl + '" class="nt-badge link"><i class="bi bi-box-arrow-up-left"></i> \u0639\u0631\u0636 \u0627\u0644\u062A\u0641\u0627\u0635\u064A\u0644</a>';
            }
            html += '</div></div></div>';
        });

        container.innerHTML = html;
    }

    function setEmpty() {
        var container = document.getElementById('notificationsList');
        if (container) {
            container.innerHTML = '<div class="nt-empty"><div class="nt-empty-icon"><i class="bi bi-bell-slash"></i></div><h3 class="nt-empty-title">\u0644\u0627 \u062A\u0648\u062C\u062F \u0625\u0634\u0639\u0627\u0631\u0627\u062A</h3><p class="nt-empty-desc">\u0644\u0645 \u064A\u062A\u0645 \u0627\u0644\u0639\u062B\u0648\u0631 \u0639\u0644\u0649 \u0625\u0634\u0639\u0627\u0631\u0627\u062A \u0645\u0637\u0627\u0628\u0642\u0629</p></div>';
        }
    }

    /* ======================================== */
    /* FILTER                                    */
    /* ======================================== */
    window.filterNotifications = function (filter, btn) {
        var buttons = document.querySelectorAll('.nt-filter-btn');
        buttons.forEach(function (b) { b.classList.remove('active'); });
        if (btn) btn.classList.add('active');

        var filtered = allNotifications;
        if (filter === 'unread') {
            filtered = allNotifications.filter(function (n) { return !n.isRead; });
        } else if (filter === 'Critical') {
            filtered = allNotifications.filter(function (n) { return n.priority === 'Critical'; });
        } else if (filter !== 'all') {
            filtered = allNotifications.filter(function (n) { return n.category === filter; });
        }

        renderNotifications(filtered);
    };

    /* ======================================== */
    /* ACTIONS                                   */
    /* ======================================== */
    window.markAsRead = function (id) {
        fetch(getUrl('markAsReadUrl') + '/' + id, { method: 'POST' })
            .then(function (r) { return r.json(); })
            .then(function (response) {
                if (response.success) {
                    loadNotifications();
                    loadStats();
                }
            });
    };

    window.markAllAsRead = function () {
        showLoading();
        fetch(getUrl('markAllAsReadUrl'), { method: 'POST' })
            .then(function (r) { return r.json(); })
            .then(function (response) {
                hideLoading();
                if (response.success) {
                    showToast('\u062A\u0645 \u062A\u0639\u0644\u064A\u0645 ' + response.count + ' \u0625\u0634\u0639\u0627\u0631 \u0643\u0645\u0642\u0631\u0648\u0621', 'success');
                    loadNotifications();
                    loadStats();
                }
            })
            .catch(function () {
                hideLoading();
                showToast('\u062D\u062F\u062B \u062E\u0637\u0623', 'error');
            });
    };

    window.deleteNotification = function (id) {
        if (!confirm('\u0647\u0644 \u0623\u0646\u062A \u0645\u062A\u0623\u0643\u062F \u0645\u0646 \u062D\u0630\u0641 \u0647\u0630\u0627 \u0627\u0644\u0625\u0634\u0639\u0627\u0631\u061F')) return;

        fetch(getUrl('deleteUrl') + '/' + id, { method: 'POST' })
            .then(function (r) { return r.json(); })
            .then(function (response) {
                if (response.success) {
                    showToast('\u062A\u0645 \u062D\u0630\u0641 \u0627\u0644\u0625\u0634\u0639\u0627\u0631', 'success');
                    loadNotifications();
                    loadStats();
                }
            });
    };

    window.deleteAllRead = function () {
        if (!confirm('\u0633\u064A\u062A\u0645 \u062D\u0630\u0641 \u062C\u0645\u064A\u0639 \u0627\u0644\u0625\u0634\u0639\u0627\u0631\u0627\u062A \u0627\u0644\u0645\u0642\u0631\u0648\u0621\u0629. \u0647\u0644 \u0623\u0646\u062A \u0645\u062A\u0623\u0643\u062F\u061F')) return;

        showLoading();
        fetch(getUrl('deleteAllUrl'), { method: 'POST' })
            .then(function (r) { return r.json(); })
            .then(function (response) {
                hideLoading();
                if (response.success) {
                    showToast('\u062A\u0645 \u062D\u0630\u0641 ' + response.count + ' \u0625\u0634\u0639\u0627\u0631', 'success');
                    loadNotifications();
                    loadStats();
                }
            })
            .catch(function () {
                hideLoading();
                showToast('\u062D\u062F\u062B \u062E\u0637\u0623 \u0623\u062B\u0646\u0627\u0621 \u0627\u0644\u062D\u0630\u0641', 'error');
            });
    };

    window.generateNotifications = function () {
        showLoading();
        fetch(getUrl('generateUrl'))
            .then(function (r) { return r.json(); })
            .then(function (response) {
                hideLoading();
                if (response.success) {
                    showToast('\u062A\u0645 \u062A\u0648\u0644\u064A\u062F ' + response.notificationsCreated + ' \u0625\u0634\u0639\u0627\u0631 \u062C\u062F\u064A\u062F', 'success');
                    loadNotifications();
                    loadStats();
                }
            })
            .catch(function () {
                hideLoading();
                showToast('\u0641\u0634\u0644 \u0641\u064A \u062A\u0648\u0644\u064A\u062F \u0627\u0644\u0625\u0634\u0639\u0627\u0631\u0627\u062A', 'error');
            });
    };

})();
