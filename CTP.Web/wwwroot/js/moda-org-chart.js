/* ============================================= */
/* MODA Organization Chart - JavaScript           */
/* الهيكل التنظيمي - عرض شجري عمودي حديث          */
/* ============================================= */
(function () {
    'use strict';

    var hierarchyData = [];
    var flatNodes = {};
    var totalUnits = 0;
    var totalPersonnel = 0;
    var maxDepth = 0;

    document.addEventListener('DOMContentLoaded', function () {
        var expandAllBtn = document.getElementById('expandAllBtn');
        var collapseAllBtn = document.getElementById('collapseAllBtn');
        var searchInput = document.getElementById('ocSearchInput');
        var detailClose = document.getElementById('ocDetailClose');
        var detailOverlay = document.getElementById('ocDetailOverlay');

        if (expandAllBtn) expandAllBtn.addEventListener('click', expandAll);
        if (collapseAllBtn) collapseAllBtn.addEventListener('click', collapseAll);
        if (searchInput) searchInput.addEventListener('input', debounce(handleSearch, 250));
        if (detailClose) detailClose.addEventListener('click', closeDetail);
        if (detailOverlay) detailOverlay.addEventListener('click', closeDetail);

        loadOrganizationChart();
    });

    function debounce(fn, delay) {
        var timer;
        return function () {
            var args = arguments;
            var ctx = this;
            clearTimeout(timer);
            timer = setTimeout(function () { fn.apply(ctx, args); }, delay);
        };
    }

    /* ======================================== */
    /* DATA LOADING                              */
    /* ======================================== */
    function loadOrganizationChart() {
        var treeBody = document.getElementById('ocTreeBody');
        if (!treeBody) return;

        treeBody.innerHTML = '<div class="oc-loading"><div class="oc-loading-spinner"></div><p>جاري تحميل الهيكل التنظيمي...</p></div>';

        fetch('/Organization/Organization/GetHierarchy')
            .then(function (r) { return r.json(); })
            .then(function (data) {
                hierarchyData = data;

                if (!data || data.length === 0) {
                    treeBody.innerHTML = '<div class="oc-empty"><i class="bi bi-inbox"></i><h4>لا توجد وحدات لعرضها</h4></div>';
                    return;
                }

                totalUnits = 0;
                totalPersonnel = 0;
                maxDepth = 0;
                flatNodes = {};
                data.forEach(function (n) { indexNode(n, null, 0); });

                updateStats(data.length);
                renderTree(data);
            })
            .catch(function (err) {
                console.error('Error loading org chart:', err);
                treeBody.innerHTML = '<div class="oc-empty oc-error"><i class="bi bi-exclamation-triangle"></i><h4>فشل تحميل الهيكل التنظيمي</h4></div>';
            });
    }

    function indexNode(node, parentId, depth) {
        totalUnits++;
        totalPersonnel += (node.personnelCount || 0);
        if (depth + 1 > maxDepth) maxDepth = depth + 1;

        flatNodes[String(node.id)] = {
            data: node,
            parentId: parentId,
            depth: depth
        };

        if (node.children && node.children.length > 0) {
            node.children.forEach(function (child) {
                indexNode(child, node.id, depth + 1);
            });
        }
    }

    /* ======================================== */
    /* STATS                                     */
    /* ======================================== */
    function updateStats(rootCount) {
        animateEl('ocTotalUnits', totalUnits);
        animateEl('ocRootUnits', rootCount);
        animateEl('ocTotalPersonnel', totalPersonnel);
        animateEl('ocMaxDepth', maxDepth);
    }

    function animateEl(id, target) {
        var el = document.getElementById(id);
        if (!el) return;
        var duration = 800;
        var startTime = null;

        function step(ts) {
            if (!startTime) startTime = ts;
            var p = Math.min((ts - startTime) / duration, 1);
            var eased = 1 - Math.pow(1 - p, 3);
            el.textContent = Math.round(target * eased).toLocaleString('en');
            if (p < 1) requestAnimationFrame(step);
        }
        requestAnimationFrame(step);
    }

    /* ======================================== */
    /* TREE RENDERING                            */
    /* ======================================== */
    function renderTree(data) {
        var treeBody = document.getElementById('ocTreeBody');
        if (!treeBody) return;

        var html = '<ul class="oc-tree-list">';
        data.forEach(function (node) {
            html += renderNodeHTML(node, 0);
        });
        html += '</ul>';
        treeBody.innerHTML = html;

        attachNodeEvents();

        // Auto-collapse level 2+ for cleaner initial view
        var items = treeBody.querySelectorAll('.oc-tree-item[data-depth="1"]');
        for (var i = 0; i < items.length; i++) {
            if (items[i].querySelector('.oc-tree-children')) {
                items[i].classList.add('collapsed');
            }
        }
    }

    function renderNodeHTML(node, depth) {
        var hasChildren = node.children && node.children.length > 0;
        var childCount = hasChildren ? node.children.length : 0;
        var nodeClass = 'oc-node';
        if (depth === 0) nodeClass += ' oc-root';
        else if (depth === 1) nodeClass += ' oc-level-1';

        var html = '<li class="oc-tree-item" data-id="' + (node.id || '') + '" data-depth="' + depth + '">';

        // Node card
        html += '<div class="' + nodeClass + '" data-id="' + (node.id || '') + '">';

        // Toggle
        if (hasChildren) {
            html += '<button class="oc-node-toggle" type="button"><i class="bi bi-chevron-down"></i></button>';
        } else {
            html += '<div class="oc-node-toggle-placeholder"></div>';
        }

        // Icon
        html += '<div class="oc-node-icon-wrap"><i class="bi bi-building-fill"></i></div>';

        // Info
        html += '<div class="oc-node-info">';
        html += '<div class="oc-node-name">' + escapeHtml(node.name || 'غير متوفر') + '</div>';
        if (node.id) {
            html += '<div class="oc-node-id">#' + formatNumber(node.id) + '</div>';
        }

        if (node.location || node.branch) {
            html += '<div class="oc-node-meta">';
            if (node.location) {
                html += '<span><i class="bi bi-geo-alt-fill"></i> ' + escapeHtml(node.location) + '</span>';
            }
            if (node.branch) {
                html += '<span><i class="bi bi-diagram-3-fill"></i> ' + escapeHtml(node.branch) + '</span>';
            }
            html += '</div>';
        }
        html += '</div>';

        // Badges
        html += '<div class="oc-node-badges">';
        html += '<span class="oc-badge personnel"><i class="bi bi-people-fill"></i> ' + (node.personnelCount || 0).toLocaleString('en') + '</span>';
        if (childCount > 0) {
            html += '<span class="oc-badge children-count"><i class="bi bi-diagram-3"></i> ' + childCount.toLocaleString('en') + '</span>';
        }
        html += '</div>';

        html += '</div>'; // end oc-node

        // Children
        if (hasChildren) {
            html += '<ul class="oc-tree-children">';
            node.children.forEach(function (child) {
                html += renderNodeHTML(child, depth + 1);
            });
            html += '</ul>';
        }

        html += '</li>';
        return html;
    }

    function attachNodeEvents() {
        // Toggle expand/collapse
        var toggles = document.querySelectorAll('.oc-node-toggle');
        for (var i = 0; i < toggles.length; i++) {
            toggles[i].addEventListener('click', function (e) {
                e.stopPropagation();
                var treeItem = this.closest('.oc-tree-item');
                if (treeItem) treeItem.classList.toggle('collapsed');
            });
        }

        // Node click - show detail
        var nodes = document.querySelectorAll('.oc-node');
        for (var j = 0; j < nodes.length; j++) {
            nodes[j].addEventListener('click', function (e) {
                if (e.target.closest('.oc-node-toggle')) return;
                var nodeId = this.dataset.id;
                showDetail(nodeId);

                // Visual selection
                var allNodes = document.querySelectorAll('.oc-node');
                for (var k = 0; k < allNodes.length; k++) allNodes[k].classList.remove('selected');
                this.classList.add('selected');
            });
        }
    }

    /* ======================================== */
    /* DETAIL PANEL                              */
    /* ======================================== */
    function showDetail(nodeId) {
        var entry = flatNodes[String(nodeId)];
        if (!entry) return;
        var node = entry.data;

        var panel = document.getElementById('ocDetailPanel');
        var overlay = document.getElementById('ocDetailOverlay');
        if (!panel) return;

        // Header
        var nameEl = document.getElementById('ocDetailUnitName');
        var idEl = document.getElementById('ocDetailUnitId');
        if (nameEl) nameEl.textContent = node.name || 'غير متوفر';
        if (idEl) idEl.textContent = '#' + formatNumber(node.id);

        // Info rows
        var infoBody = document.getElementById('ocDetailInfoBody');
        if (infoBody) {
            var rows = '';
            rows += detailRow('bi-geo-alt-fill', 'الموقع', node.location || '--');
            rows += detailRow('bi-diagram-3-fill', 'الفرع', node.branch || '--');
            rows += detailRow('bi-info-circle-fill', 'الحالة', node.status || '--');
            rows += detailRow('bi-people-fill', 'عدد الأفراد', (node.personnelCount || 0).toLocaleString('en'));
            rows += detailRow('bi-layers-fill', 'المستوى', (entry.depth + 1).toLocaleString('en'));

            // Parent info
            var parentName = '--';
            if (entry.parentId && flatNodes[String(entry.parentId)]) {
                parentName = flatNodes[String(entry.parentId)].data.name || '--';
            }
            rows += detailRow('bi-arrow-up-circle-fill', 'الوحدة الأب', parentName);

            infoBody.innerHTML = rows;
        }

        // Children list
        var childrenSection = document.getElementById('ocDetailChildrenSection');
        var childrenList = document.getElementById('ocDetailChildrenList');
        if (childrenSection && childrenList) {
            if (node.children && node.children.length > 0) {
                childrenSection.style.display = 'block';
                var chtml = '';
                node.children.forEach(function (child) {
                    chtml += '<li class="oc-detail-child" data-id="' + child.id + '">';
                    chtml += '<i class="bi bi-building"></i>';
                    chtml += '<span>' + escapeHtml(child.name || 'وحدة ' + child.id) + '</span>';
                    chtml += '<small>#' + formatNumber(child.id) + '</small>';
                    chtml += '</li>';
                });
                childrenList.innerHTML = chtml;

                // Click child to navigate
                var childEls = childrenList.querySelectorAll('.oc-detail-child');
                for (var i = 0; i < childEls.length; i++) {
                    childEls[i].addEventListener('click', function () {
                        var cid = this.dataset.id;
                        // Expand parent path and scroll to node
                        expandPathToNode(cid);
                        showDetail(cid);
                        // Select the node visually
                        var allNodes = document.querySelectorAll('.oc-node');
                        for (var k = 0; k < allNodes.length; k++) allNodes[k].classList.remove('selected');
                        var targetNode = document.querySelector('.oc-node[data-id="' + cid + '"]');
                        if (targetNode) {
                            targetNode.classList.add('selected');
                            targetNode.scrollIntoView({ behavior: 'smooth', block: 'center' });
                        }
                    });
                }
            } else {
                childrenSection.style.display = 'none';
            }
        }

        // Link
        var linkEl = document.getElementById('ocDetailLink');
        if (linkEl) linkEl.href = '/Organization/Units/Details/' + nodeId;

        panel.classList.add('show');
        if (overlay) overlay.classList.add('show');
    }

    function closeDetail() {
        var panel = document.getElementById('ocDetailPanel');
        var overlay = document.getElementById('ocDetailOverlay');
        if (panel) panel.classList.remove('show');
        if (overlay) overlay.classList.remove('show');

        var allNodes = document.querySelectorAll('.oc-node.selected');
        for (var i = 0; i < allNodes.length; i++) allNodes[i].classList.remove('selected');
    }

    function detailRow(icon, label, value) {
        return '<div class="oc-detail-row">' +
            '<span class="oc-detail-label"><i class="bi ' + icon + '"></i> ' + label + '</span>' +
            '<span class="oc-detail-value">' + escapeHtml(String(value)) + '</span>' +
            '</div>';
    }

    /* ======================================== */
    /* EXPAND / COLLAPSE                         */
    /* ======================================== */
    function expandAll() {
        var items = document.querySelectorAll('.oc-tree-item.collapsed');
        for (var i = 0; i < items.length; i++) items[i].classList.remove('collapsed');
    }

    function collapseAll() {
        var items = document.querySelectorAll('.oc-tree-item');
        for (var i = 0; i < items.length; i++) {
            if (items[i].querySelector('.oc-tree-children')) {
                items[i].classList.add('collapsed');
            }
        }
    }

    function expandPathToNode(nodeId) {
        // Build path from root to this node
        var path = [];
        var current = String(nodeId);
        while (flatNodes[current]) {
            path.unshift(current);
            current = flatNodes[current].parentId ? String(flatNodes[current].parentId) : null;
            if (!current) break;
        }

        // Expand each ancestor
        for (var i = 0; i < path.length; i++) {
            var treeItem = document.querySelector('.oc-tree-item[data-id="' + path[i] + '"]');
            if (treeItem) treeItem.classList.remove('collapsed');
        }
    }

    /* ======================================== */
    /* SEARCH                                    */
    /* ======================================== */
    function handleSearch() {
        var input = document.getElementById('ocSearchInput');
        if (!input) return;
        var term = input.value.toLowerCase().trim();

        var resultsBar = document.getElementById('ocSearchResults');
        var allNodes = document.querySelectorAll('.oc-node');

        // Clear highlights
        for (var i = 0; i < allNodes.length; i++) allNodes[i].classList.remove('highlighted');

        if (!term) {
            if (resultsBar) resultsBar.classList.remove('show');
            return;
        }

        // Find matches
        var matchIds = [];
        var keys = Object.keys(flatNodes);
        for (var j = 0; j < keys.length; j++) {
            var n = flatNodes[keys[j]].data;
            var name = (n.name || '').toLowerCase();
            var id = String(n.id || '');
            var loc = (n.location || '').toLowerCase();
            var branch = (n.branch || '').toLowerCase();
            if (name.includes(term) || id.includes(term) || loc.includes(term) || branch.includes(term)) {
                matchIds.push(keys[j]);
            }
        }

        // Show results count
        if (resultsBar) {
            resultsBar.textContent = 'تم العثور على ' + matchIds.length.toLocaleString('en') + ' نتيجة';
            resultsBar.classList.add('show');
        }

        if (matchIds.length === 0) return;

        // Expand paths and highlight
        for (var k = 0; k < matchIds.length; k++) {
            expandPathToNode(matchIds[k]);
            var nodeEl = document.querySelector('.oc-node[data-id="' + matchIds[k] + '"]');
            if (nodeEl) nodeEl.classList.add('highlighted');
        }

        // Scroll to first match
        var firstMatch = document.querySelector('.oc-node[data-id="' + matchIds[0] + '"]');
        if (firstMatch) {
            firstMatch.scrollIntoView({ behavior: 'smooth', block: 'center' });
        }
    }

    /* ======================================== */
    /* UTILITIES                                 */
    /* ======================================== */
    function escapeHtml(text) {
        var div = document.createElement('div');
        div.appendChild(document.createTextNode(text));
        return div.innerHTML;
    }

    function formatNumber(num) {
        return String(num).replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    }

})();
