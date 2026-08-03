/* ============================================= */
/* MODA Configuration - JavaScript               */
/* إعدادات الاتصال بـ Oracle                       */
/* ============================================= */
(function () {
    'use strict';

    document.addEventListener('DOMContentLoaded', function () {
        var testBtn = document.getElementById('testConnectionBtn');
        var syncBtn = document.getElementById('syncBtn');
        var mockBtn = document.getElementById('generateMockDataBtn');

        if (testBtn) testBtn.addEventListener('click', testConnection);
        if (syncBtn) syncBtn.addEventListener('click', syncDatabase);
        if (mockBtn) mockBtn.addEventListener('click', showMockModal);

        // Mock modal buttons
        var mockConfirm = document.getElementById('mockConfirmBtn');
        var mockCancel = document.getElementById('mockCancelBtn');
        if (mockConfirm) mockConfirm.addEventListener('click', generateMockData);
        if (mockCancel) mockCancel.addEventListener('click', hideMockModal);
    });

    /* ======================================== */
    /* HELPERS                                   */
    /* ======================================== */
    function getUrl(id) {
        var el = document.getElementById(id);
        return el ? el.value : '';
    }

    function getVal(id) {
        var el = document.getElementById(id);
        return el ? el.value : '';
    }

    function showLoading(text) {
        var el = document.getElementById('loadingOverlay');
        var txt = document.getElementById('loadingText');
        if (txt) txt.textContent = text || '\u062C\u0627\u0631\u064A \u0627\u0644\u0645\u0639\u0627\u0644\u062C\u0629...';
        if (el) el.classList.add('show');
    }

    function hideLoading() {
        var el = document.getElementById('loadingOverlay');
        if (el) el.classList.remove('show');
    }

    function showToast(message, type) {
        var toast = document.getElementById('cfToast');
        if (!toast) return;
        toast.className = 'cf-toast cf-toast-' + (type || 'success');
        toast.innerHTML = '<i class="bi ' + (type === 'error' ? 'bi-x-circle-fill' : type === 'info' ? 'bi-info-circle-fill' : 'bi-check-circle-fill') + '"></i> ' + message;
        setTimeout(function () { toast.classList.add('show'); }, 50);
        setTimeout(function () { toast.classList.remove('show'); }, 4000);
    }

    function setBtnLoading(btn, text) {
        if (!btn) return;
        btn._origHTML = btn.innerHTML;
        btn.disabled = true;
        btn.innerHTML = '<span class="cf-spinner" style="width:18px;height:18px;border-width:2px;margin:0;display:inline-block;vertical-align:middle;"></span> ' + text;
    }

    function resetBtn(btn) {
        if (!btn || !btn._origHTML) return;
        btn.disabled = false;
        btn.innerHTML = btn._origHTML;
    }

    /* ======================================== */
    /* TEST CONNECTION                           */
    /* ======================================== */
    function testConnection() {
        var btn = document.getElementById('testConnectionBtn');
        setBtnLoading(btn, '\u062C\u0627\u0631\u064A \u0627\u0644\u0627\u062E\u062A\u0628\u0627\u0631...');

        var config = {
            host: getVal('Host'),
            port: parseInt(getVal('Port')) || 1521,
            sid: getVal('Sid'),
            username: getVal('Username'),
            password: getVal('Password')
        };

        fetch(getUrl('testConnectionUrl'), {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(config)
        })
            .then(function (r) { return r.json(); })
            .then(function (response) {
                resetBtn(btn);
                if (response.success) {
                    showToast('\u0646\u062C\u062D \u0627\u0644\u0627\u062A\u0635\u0627\u0644! ' + (response.message || ''), 'success');
                    if (confirm('\u0646\u062C\u062D \u0627\u0644\u0627\u062A\u0635\u0627\u0644. \u0647\u0644 \u062A\u0631\u064A\u062F \u062D\u0641\u0638 \u0627\u0644\u0625\u0639\u062F\u0627\u062F\u0627\u062A\u061F')) {
                        document.getElementById('configForm').submit();
                    }
                } else {
                    showToast('\u0641\u0634\u0644 \u0627\u0644\u0627\u062A\u0635\u0627\u0644: ' + (response.message || ''), 'error');
                }
            })
            .catch(function () {
                resetBtn(btn);
                showToast('\u062E\u0637\u0623 \u0641\u064A \u0627\u062E\u062A\u0628\u0627\u0631 \u0627\u0644\u0627\u062A\u0635\u0627\u0644', 'error');
            });
    }

    /* ======================================== */
    /* SYNC DATABASE                             */
    /* ======================================== */
    function syncDatabase() {
        if (!confirm('\u0633\u064A\u062A\u0645 \u0645\u0632\u0627\u0645\u0646\u0629 \u062C\u0645\u064A\u0639 \u0627\u0644\u0628\u064A\u0627\u0646\u0627\u062A \u0645\u0646 Oracle \u0625\u0644\u0649 SQLite. \u0647\u0644 \u062A\u0631\u064A\u062F \u0627\u0644\u0645\u062A\u0627\u0628\u0639\u0629\u061F')) return;

        var btn = document.getElementById('syncBtn');
        setBtnLoading(btn, '\u062C\u0627\u0631\u064A \u0627\u0644\u0645\u0632\u0627\u0645\u0646\u0629...');
        showLoading('\u062C\u0627\u0631\u064A \u0645\u0632\u0627\u0645\u0646\u0629 \u0642\u0627\u0639\u062F\u0629 \u0627\u0644\u0628\u064A\u0627\u0646\u0627\u062A...');

        fetch(getUrl('syncUrl'), { method: 'POST' })
            .then(function (r) { return r.json(); })
            .then(function (response) {
                hideLoading();
                resetBtn(btn);
                if (response.success) {
                    showSyncResults(response);
                    showToast('\u0646\u062C\u062D\u062A \u0627\u0644\u0645\u0632\u0627\u0645\u0646\u0629!', 'success');
                } else {
                    showToast('\u0641\u0634\u0644\u062A \u0627\u0644\u0645\u0632\u0627\u0645\u0646\u0629: ' + (response.message || ''), 'error');
                }
            })
            .catch(function () {
                hideLoading();
                resetBtn(btn);
                showToast('\u062E\u0637\u0623 \u0641\u064A \u0627\u0644\u0645\u0632\u0627\u0645\u0646\u0629', 'error');
            });
    }

    function showSyncResults(data) {
        var panel = document.getElementById('syncResultCard');
        var body = document.getElementById('syncResultBody');
        if (!panel || !body) return;

        var html = '';
        html += '<div class="cf-result-row"><span class="cf-result-label">\u0627\u0644\u0623\u0641\u0631\u0627\u062F</span><span class="cf-result-value">' + (data.personnelSynced || 0) + ' \u0633\u062C\u0644</span></div>';
        html += '<div class="cf-result-row"><span class="cf-result-label">\u0627\u0644\u0648\u062D\u062F\u0627\u062A</span><span class="cf-result-value">' + (data.unitsSynced || 0) + ' \u0633\u062C\u0644</span></div>';
        html += '<div class="cf-result-row"><span class="cf-result-label">\u0627\u0644\u062A\u0639\u0644\u064A\u0645</span><span class="cf-result-value">' + (data.educationsSynced || 0) + ' \u0633\u062C\u0644</span></div>';
        html += '<div class="cf-result-row"><span class="cf-result-label">\u0627\u0644\u062A\u0631\u0642\u064A\u0627\u062A</span><span class="cf-result-value">' + (data.promotionsSynced || 0) + ' \u0633\u062C\u0644</span></div>';
        html += '<div class="cf-result-row"><span class="cf-result-label">\u0627\u0644\u0645\u062C\u0645\u0648\u0639</span><span class="cf-result-value">' + (data.totalRecords || 0) + ' \u0633\u062C\u0644</span></div>';
        if (data.duration !== undefined) {
            html += '<div class="cf-result-row"><span class="cf-result-label">\u0627\u0644\u0645\u062F\u0629</span><span class="cf-result-value">' + data.duration.toFixed(2) + ' \u062B\u0627\u0646\u064A\u0629</span></div>';
        }

        body.innerHTML = html;
        panel.classList.add('show');
    }

    /* ======================================== */
    /* MOCK DATA                                 */
    /* ======================================== */
    function showMockModal() {
        var modal = document.getElementById('mockModal');
        if (modal) modal.classList.add('show');
    }

    function hideMockModal() {
        var modal = document.getElementById('mockModal');
        if (modal) modal.classList.remove('show');
    }

    function generateMockData() {
        hideMockModal();

        var personnelCount = parseInt(getVal('personnelCount')) || 5000;
        var unitsCount = parseInt(getVal('unitsCount')) || 500;

        showLoading('\u062C\u0627\u0631\u064A \u062A\u0648\u0644\u064A\u062F \u0627\u0644\u0628\u064A\u0627\u0646\u0627\u062A \u0627\u0644\u062A\u062C\u0631\u064A\u0628\u064A\u0629...');

        fetch(getUrl('generateMockDataUrl'), {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ personnelCount: personnelCount, unitsCount: unitsCount })
        })
            .then(function (r) { return r.json(); })
            .then(function (response) {
                hideLoading();
                if (response.success) {
                    var msg = '\u0627\u0644\u0623\u0641\u0631\u0627\u062F: ' + (response.personnelGenerated || 0) + ' | \u0627\u0644\u0648\u062D\u062F\u0627\u062A: ' + (response.unitsGenerated || 0);
                    showToast('\u0646\u062C\u062D \u0627\u0644\u062A\u0648\u0644\u064A\u062F! ' + msg, 'success');
                    setTimeout(function () { location.reload(); }, 1500);
                } else {
                    showToast('\u0641\u0634\u0644 \u0627\u0644\u062A\u0648\u0644\u064A\u062F: ' + (response.message || ''), 'error');
                }
            })
            .catch(function () {
                hideLoading();
                showToast('\u062E\u0637\u0623 \u0641\u064A \u062A\u0648\u0644\u064A\u062F \u0627\u0644\u0628\u064A\u0627\u0646\u0627\u062A', 'error');
            });
    }

})();
