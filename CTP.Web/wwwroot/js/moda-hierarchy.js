/* ============================================= */
/* MODA Unit Hierarchy - JavaScript               */
/* صفحة التسلسل الهرمي للوحدات                     */
/* ============================================= */

(function () {
    'use strict';

    var treeData = [];
    var currentDraggedNode = null;
    var contextMenuTarget = null;
    var selectedUnits = new Set();

    document.addEventListener('DOMContentLoaded', function () {
        loadTree();
        initializeEventListeners();
        initializeMultiSelectListeners();
    });

    // ==========================================
    // TREE LOADING & RENDERING
    // ==========================================

    function loadTree() {
        showLoading(true);
        fetch('/api/unithierarchyapi/tree/roots')
            .then(function (response) {
                if (!response.ok) throw new Error('Failed to load');
                return response.json();
            })
            .then(function (data) {
                treeData = data;
                renderTree();
                updateStatistics();
            })
            .catch(function (error) {
                console.error('Error loading tree:', error);
                var container = document.getElementById('treeContent');
                container.innerHTML = '<div class="h-empty-state"><i class="bi bi-exclamation-circle"></i><p>فشل تحميل التسلسل الهرمي</p></div>';
            })
            .finally(function () {
                showLoading(false);
            });
    }

    function renderTree() {
        var container = document.getElementById('treeContent');

        if (!treeData || treeData.length === 0) {
            container.innerHTML = '<div class="h-empty-state"><i class="bi bi-inbox"></i><p>لا توجد وحدات للعرض</p></div>';
            return;
        }

        var html = '<ul class="h-tree">';
        for (var i = 0; i < treeData.length; i++) {
            html += renderNode(treeData[i]);
        }
        html += '</ul>';
        container.innerHTML = html;
        initializeDragAndDrop();
    }

    function renderNode(node) {
        var hasChildren = node.hasChildren || (node.children && node.children.length > 0);
        var manualClass = node.isManualHierarchy ? ' manual-hierarchy' : '';
        var childrenHtml = '';

        if (hasChildren) {
            childrenHtml = '<ul class="h-tree-children" id="children-' + node.id + '">';
            if (node.children) {
                for (var i = 0; i < node.children.length; i++) {
                    childrenHtml += renderNode(node.children[i]);
                }
            }
            childrenHtml += '</ul>';
        }

        var toggleHtml = hasChildren
            ? '<button class="h-tree-toggle collapsed" onclick="toggleNode(' + node.id + ')"></button>'
            : '<span style="width:24px;display:inline-block;"></span>';

        var locationHtml = node.location
            ? '<div class="h-node-location"><i class="bi bi-geo-alt"></i> ' + node.location + '</div>'
            : '';

        var badges = '';
        if (node.personnelCount > 0) {
            badges += '<span class="h-badge personnel"><i class="bi bi-people"></i> ' + node.personnelCount.toLocaleString('en') + '</span>';
        }
        if (node.childrenCount > 0) {
            badges += '<span class="h-badge children"><i class="bi bi-diagram-3"></i> ' + node.childrenCount.toLocaleString('en') + '</span>';
        }
        if (node.positionsCount > 0) {
            badges += '<span class="h-badge positions"><i class="bi bi-briefcase"></i> ' + node.positionsCount.toLocaleString('en') + '</span>';
        }
        if (node.isManualHierarchy) {
            badges += '<span class="h-badge manual"><i class="bi bi-pencil"></i></span>';
        }

        return '<li>' +
            '<div class="h-tree-node' + manualClass + '" data-unit-id="' + node.id + '" draggable="true" oncontextmenu="showContextMenu(event, ' + node.id + '); return false;">' +
                '<div class="h-node-header">' +
                    '<input type="checkbox" class="h-node-checkbox" data-unit-id="' + node.id + '" onchange="handleCheckboxChange(' + node.id + ', this.checked)" onclick="event.stopPropagation()">' +
                    '<div class="h-node-info">' +
                        toggleHtml +
                        '<i class="bi ' + (node.icon || 'bi-building') + ' h-node-icon"></i>' +
                        '<div>' +
                            '<div class="h-node-text">' + node.text + ' <span class="h-node-id">(' + node.id + ')</span></div>' +
                            locationHtml +
                        '</div>' +
                    '</div>' +
                    '<div class="h-node-badges">' + badges + '</div>' +
                    '<div class="h-node-actions">' +
                        '<button class="h-node-action-btn" onclick="viewUnitDetails(' + node.id + ')" title="عرض التفاصيل"><i class="bi bi-eye"></i></button>' +
                    '</div>' +
                '</div>' +
                childrenHtml +
            '</div>' +
        '</li>';
    }

    // ==========================================
    // TREE NODE TOGGLE & LAZY LOADING
    // ==========================================

    function toggleNode(unitId) {
        var childrenContainer = document.getElementById('children-' + unitId);
        var nodeEl = document.querySelector('[data-unit-id="' + unitId + '"]');
        var toggleBtn = nodeEl ? nodeEl.querySelector('.h-tree-toggle') : null;

        if (!childrenContainer) return;

        if (childrenContainer.classList.contains('show')) {
            childrenContainer.classList.remove('show');
            if (toggleBtn) {
                toggleBtn.classList.remove('expanded');
                toggleBtn.classList.add('collapsed');
            }
        } else {
            if (!childrenContainer.hasChildNodes() || childrenContainer.children.length === 0) {
                loadChildren(unitId, childrenContainer);
            }
            childrenContainer.classList.add('show');
            if (toggleBtn) {
                toggleBtn.classList.remove('collapsed');
                toggleBtn.classList.add('expanded');
            }
        }
    }

    function loadChildren(parentId, container) {
        fetch('/api/unithierarchyapi/tree/children/' + parentId)
            .then(function (response) {
                if (!response.ok) throw new Error('Failed');
                return response.json();
            })
            .then(function (children) {
                var html = '';
                for (var i = 0; i < children.length; i++) {
                    html += renderNode(children[i]);
                }
                container.innerHTML = html;
                initializeDragAndDrop();
            })
            .catch(function () {
                container.innerHTML = '<li><span style="color:#EF4444;font-size:12px;">فشل تحميل الوحدات الفرعية</span></li>';
            });
    }

    // ==========================================
    // DRAG AND DROP
    // ==========================================

    function initializeDragAndDrop() {
        var nodes = document.querySelectorAll('.h-tree-node');
        for (var i = 0; i < nodes.length; i++) {
            nodes[i].addEventListener('dragstart', handleDragStart);
            nodes[i].addEventListener('dragover', handleDragOver);
            nodes[i].addEventListener('drop', handleDrop);
            nodes[i].addEventListener('dragend', handleDragEnd);
            nodes[i].addEventListener('dragleave', handleDragLeave);
        }
    }

    function handleDragStart(e) {
        currentDraggedNode = parseInt(this.dataset.unitId);
        this.classList.add('dragging');
        e.dataTransfer.effectAllowed = 'move';
        e.dataTransfer.setData('text/plain', this.dataset.unitId);
    }

    function handleDragOver(e) {
        e.preventDefault();
        var targetId = parseInt(this.dataset.unitId);
        if (targetId === currentDraggedNode) {
            this.classList.add('drag-invalid');
            e.dataTransfer.dropEffect = 'none';
            return false;
        }
        this.classList.add('drag-over');
        e.dataTransfer.dropEffect = 'move';
        return false;
    }

    function handleDragLeave() {
        this.classList.remove('drag-over', 'drag-invalid');
    }

    function handleDrop(e) {
        e.stopPropagation();
        this.classList.remove('drag-over', 'drag-invalid');
        var targetId = parseInt(this.dataset.unitId);
        if (targetId === currentDraggedNode) return false;

        Moda.confirm({
            title: 'تأكيد النقل',
            text: 'هل تريد نقل الوحدة ' + currentDraggedNode + ' لتكون تحت الوحدة ' + targetId + '؟',
            confirmText: 'نعم، انقل'
        }).then(function (r) {
            if (r.isConfirmed) updateUnitParent(currentDraggedNode, targetId);
        });
        return false;
    }

    function handleDragEnd() {
        this.classList.remove('dragging');
        var overs = document.querySelectorAll('.drag-over, .drag-invalid');
        for (var i = 0; i < overs.length; i++) {
            overs[i].classList.remove('drag-over', 'drag-invalid');
        }
    }

    // ==========================================
    // API OPERATIONS
    // ==========================================

    function updateUnitParent(unitId, newParentId) {
        showLoading(true);
        fetch('/api/unithierarchyapi/update', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                unitNumber: unitId,
                newParentUnitNumber: newParentId,
                setManualHierarchy: true
            })
        })
        .then(function (response) { return response.json(); })
        .then(function (result) {
            if (result.success) {
                Moda.toastSuccess('تم تحديث التسلسل الهرمي');
                loadTree();
            } else {
                Moda.error('فشل التحديث', result.message || 'تعذّر تحديث الوحدة الأم');
            }
        })
        .catch(function (error) {
            console.error('Error:', error);
            Moda.error('خطأ', 'فشل تحديث التسلسل الهرمي');
        })
        .finally(function () {
            showLoading(false);
        });
    }

    function deleteUnit(unitId) {
        Moda.confirmDelete({
            title: 'حذف الوحدة',
            text: 'هل أنت متأكد من حذف هذه الوحدة؟ لا يمكن التراجع.'
        }).then(function (r) {
            if (!r.isConfirmed) return;

            showLoading(true);
            fetch('/api/unithierarchyapi/delete/' + unitId, { method: 'DELETE' })
                .then(function (response) { return response.json(); })
                .then(function (result) {
                    if (result.success) {
                        Moda.toastSuccess('تم حذف الوحدة');
                        loadTree();
                    } else {
                        Moda.error('فشل الحذف', result.message || 'تعذّر حذف الوحدة');
                    }
                })
                .catch(function () { Moda.error('خطأ', 'فشل حذف الوحدة'); })
                .finally(function () { showLoading(false); });
        });
    }

    function resetToOracle(unitId) {
        Moda.confirm({
            title: 'إعادة التعيين',
            text: 'هل تريد إعادة تعيين التسلسل الهرمي إلى قيم Oracle؟',
            confirmText: 'نعم، أعِد التعيين'
        }).then(function (r) {
            if (!r.isConfirmed) return;

            showLoading(true);
            fetch('/api/unithierarchyapi/reset/' + unitId, { method: 'POST' })
                .then(function (response) { return response.json(); })
                .then(function (result) {
                    if (result.success) {
                        Moda.toastSuccess('تم إعادة التعيين');
                        loadTree();
                    } else {
                        Moda.error('فشل العملية', result.message || 'تعذّر إعادة التعيين');
                    }
                })
                .catch(function () { Moda.error('خطأ', 'فشل إعادة التعيين'); })
                .finally(function () { showLoading(false); });
        });
    }

    function removeParent(unitId) {
        Moda.confirm({
            title: 'إزالة الوحدة الأم',
            text: 'هل تريد إزالة الوحدة الأم؟',
            confirmText: 'نعم، أزل'
        }).then(function (r) {
            if (r.isConfirmed) updateUnitParent(unitId, null);
        });
    }

    // ==========================================
    // CONTEXT MENU
    // ==========================================

    function showContextMenu(event, unitId) {
        event.preventDefault();
        contextMenuTarget = unitId;
        var menu = document.getElementById('contextMenu');
        menu.style.left = event.pageX + 'px';
        menu.style.top = event.pageY + 'px';
        menu.classList.add('show');
        setTimeout(function () {
            document.addEventListener('click', closeContextMenu);
        }, 100);
    }

    function closeContextMenu() {
        document.getElementById('contextMenu').classList.remove('show');
        document.removeEventListener('click', closeContextMenu);
    }

    function handleContextMenuAction() {
        var action = this.dataset.action;
        closeContextMenu();

        switch (action) {
            case 'view':
                viewUnitDetails(contextMenuTarget);
                break;
            case 'edit':
                window.location.href = '/Organization/Units/Edit/' + contextMenuTarget;
                break;
            case 'delete':
                deleteUnit(contextMenuTarget);
                break;
            case 'add-child':
                window.location.href = '/Organization/Units/Create?parentId=' + contextMenuTarget;
                break;
            case 'remove-parent':
                removeParent(contextMenuTarget);
                break;
            case 'reset':
                resetToOracle(contextMenuTarget);
                break;
            case 'expand-all':
                expandNodeRecursively(contextMenuTarget);
                break;
            case 'collapse-all':
                collapseNodeRecursively(contextMenuTarget);
                break;
        }
    }

    // ==========================================
    // SEARCH
    // ==========================================

    function handleSearch() {
        var searchTerm = document.getElementById('searchInput').value.toLowerCase();
        var nodes = document.querySelectorAll('.h-tree-node');
        var clearBtn = document.getElementById('clearSearch');

        clearBtn.classList.toggle('show', searchTerm.length > 0);

        if (!searchTerm) {
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].classList.remove('highlight');
                nodes[i].style.display = '';
            }
            return;
        }

        for (var j = 0; j < nodes.length; j++) {
            var textEl = nodes[j].querySelector('.h-node-text');
            if (!textEl) continue;
            var text = textEl.textContent.toLowerCase();

            if (text.indexOf(searchTerm) !== -1) {
                nodes[j].classList.add('highlight');
                nodes[j].style.display = '';
                // Expand parents
                var parent = nodes[j].closest('.h-tree-children');
                while (parent) {
                    parent.classList.add('show');
                    var toggle = parent.previousElementSibling ? parent.previousElementSibling.querySelector('.h-tree-toggle') : null;
                    if (!toggle) {
                        var parentNode = parent.closest('.h-tree-node');
                        if (parentNode) toggle = parentNode.querySelector('.h-tree-toggle');
                    }
                    if (toggle) {
                        toggle.classList.remove('collapsed');
                        toggle.classList.add('expanded');
                    }
                    var li = parent.closest('li');
                    parent = li ? li.closest('.h-tree-children') : null;
                }
            } else {
                nodes[j].classList.remove('highlight');
                nodes[j].style.display = 'none';
            }
        }
    }

    // ==========================================
    // EXPAND / COLLAPSE
    // ==========================================

    function expandAll() {
        var children = document.querySelectorAll('.h-tree-children');
        for (var i = 0; i < children.length; i++) children[i].classList.add('show');
        var toggles = document.querySelectorAll('.h-tree-toggle');
        for (var j = 0; j < toggles.length; j++) {
            toggles[j].classList.remove('collapsed');
            toggles[j].classList.add('expanded');
        }
    }

    function collapseAll() {
        var children = document.querySelectorAll('.h-tree-children');
        for (var i = 0; i < children.length; i++) children[i].classList.remove('show');
        var toggles = document.querySelectorAll('.h-tree-toggle');
        for (var j = 0; j < toggles.length; j++) {
            toggles[j].classList.remove('expanded');
            toggles[j].classList.add('collapsed');
        }
    }

    function expandNodeRecursively(unitId) {
        var node = document.querySelector('[data-unit-id="' + unitId + '"]');
        if (!node) return;
        var children = node.querySelector('.h-tree-children');
        if (children) {
            children.classList.add('show');
            var toggle = node.querySelector('.h-tree-toggle');
            if (toggle) { toggle.classList.remove('collapsed'); toggle.classList.add('expanded'); }
            var allChildren = children.querySelectorAll('.h-tree-children');
            for (var i = 0; i < allChildren.length; i++) allChildren[i].classList.add('show');
            var allToggles = children.querySelectorAll('.h-tree-toggle');
            for (var j = 0; j < allToggles.length; j++) { allToggles[j].classList.remove('collapsed'); allToggles[j].classList.add('expanded'); }
        }
    }

    function collapseNodeRecursively(unitId) {
        var node = document.querySelector('[data-unit-id="' + unitId + '"]');
        if (!node) return;
        var children = node.querySelector('.h-tree-children');
        if (children) {
            children.classList.remove('show');
            var toggle = node.querySelector('.h-tree-toggle');
            if (toggle) { toggle.classList.remove('expanded'); toggle.classList.add('collapsed'); }
            var allChildren = children.querySelectorAll('.h-tree-children');
            for (var i = 0; i < allChildren.length; i++) allChildren[i].classList.remove('show');
            var allToggles = children.querySelectorAll('.h-tree-toggle');
            for (var j = 0; j < allToggles.length; j++) { allToggles[j].classList.remove('expanded'); allToggles[j].classList.add('collapsed'); }
        }
    }

    // ==========================================
    // EXCEL EXPORT / IMPORT
    // ==========================================

    function exportToExcel() {
        showLoading(true);
        fetch('/api/unithierarchyapi/export')
            .then(function (response) {
                if (!response.ok) throw new Error('Failed');
                return response.blob();
            })
            .then(function (blob) {
                var url = window.URL.createObjectURL(blob);
                var a = document.createElement('a');
                a.href = url;
                a.download = 'Unit_Hierarchy_' + new Date().toISOString().split('T')[0] + '.xlsx';
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
                window.URL.revokeObjectURL(url);
            })
            .catch(function () { Moda.error('خطأ', 'فشل تصدير البيانات'); })
            .finally(function () { showLoading(false); });
    }

    var pendingImportFile = null;

    function handleFileImport(e) {
        var file = e.target.files[0];
        if (!file) return;
        pendingImportFile = file;

        showLoading(true);
        var formData = new FormData();
        formData.append('file', file);

        fetch('/api/unithierarchyapi/import', { method: 'POST', body: formData })
            .then(function (response) { return response.json(); })
            .then(function (result) {
                showLoading(false);
                showImportPreview(result);
            })
            .catch(function () {
                showLoading(false);
                Moda.error('خطأ', 'فشل معاينة البيانات');
            })
            .finally(function () {
                e.target.value = '';
            });
    }

    function showImportPreview(result) {
        document.getElementById('previewTotalRows').textContent = result.totalRows || 0;
        document.getElementById('previewSuccessRows').textContent = result.successfulRows || 0;
        document.getElementById('previewFailedRows').textContent = result.failedRows || 0;
        document.getElementById('changesCount').textContent = (result.changes && result.changes.length) || 0;
        document.getElementById('errorsCount').textContent = (result.errors && result.errors.length) || 0;

        var changesBody = document.getElementById('previewChangesBody');
        changesBody.innerHTML = '';
        if (result.changes && result.changes.length > 0) {
            for (var i = 0; i < result.changes.length; i++) {
                var c = result.changes[i];
                var typeClass = '', typeText = '';
                if (c.changeType === 'Added') { typeClass = 'h-badge children'; typeText = 'إضافة'; }
                else if (c.changeType === 'Removed') { typeClass = 'h-badge manual'; typeText = 'إزالة'; }
                else { typeClass = 'h-badge positions'; typeText = 'تغيير'; }

                changesBody.innerHTML += '<tr><td>' + c.unitNumber + '</td><td>' + c.unitName + '</td>' +
                    '<td>' + (c.oldParentNumber ? c.oldParentNumber + ' - ' + (c.oldParentName || '') : '-') + '</td>' +
                    '<td>' + (c.newParentNumber ? c.newParentNumber + ' - ' + (c.newParentName || '') : '-') + '</td>' +
                    '<td><span class="' + typeClass + '">' + typeText + '</span></td></tr>';
            }
        } else {
            changesBody.innerHTML = '<tr><td colspan="5" style="text-align:center;color:#94A3B8;">لا توجد تغييرات</td></tr>';
        }

        var errorsSection = document.getElementById('previewErrorsSection');
        var errorsBody = document.getElementById('previewErrorsBody');
        errorsBody.innerHTML = '';
        if (result.errors && result.errors.length > 0) {
            errorsSection.style.display = 'block';
            for (var j = 0; j < result.errors.length; j++) {
                var err = result.errors[j];
                errorsBody.innerHTML += '<tr><td>' + err.rowNumber + '</td><td>' + (err.unitNumber || '-') + '</td>' +
                    '<td><span class="h-badge manual">' + err.errorType + '</span></td><td>' + err.errorMessage + '</td></tr>';
            }
        } else {
            errorsSection.style.display = 'none';
        }

        var confirmBtn = document.getElementById('confirmImportBtn');
        if (result.failedRows > 0 && result.successfulRows === 0) {
            confirmBtn.disabled = true;
            confirmBtn.innerHTML = '<i class="bi bi-x-lg"></i> لا يمكن الاستيراد';
        } else {
            confirmBtn.disabled = false;
            confirmBtn.innerHTML = '<i class="bi bi-check-lg"></i> تأكيد الاستيراد';
        }

        var modal = new bootstrap.Modal(document.getElementById('importPreviewModal'));
        modal.show();
    }

    // ==========================================
    // MULTI-SELECT
    // ==========================================

    function handleCheckboxChange(unitId, checked) {
        var node = document.querySelector('[data-unit-id="' + unitId + '"]');
        if (checked) {
            selectedUnits.add(unitId);
            if (node) node.classList.add('selected');
        } else {
            selectedUnits.delete(unitId);
            if (node) node.classList.remove('selected');
        }
        updateBulkActionsBar();
    }

    function selectAllUnits() {
        var checkboxes = document.querySelectorAll('.h-node-checkbox');
        for (var i = 0; i < checkboxes.length; i++) {
            checkboxes[i].checked = true;
            var uid = parseInt(checkboxes[i].dataset.unitId);
            selectedUnits.add(uid);
            var node = document.querySelector('[data-unit-id="' + uid + '"]');
            if (node) node.classList.add('selected');
        }
        updateBulkActionsBar();
    }

    function deselectAllUnits() {
        selectedUnits.clear();
        var checkboxes = document.querySelectorAll('.h-node-checkbox');
        for (var i = 0; i < checkboxes.length; i++) checkboxes[i].checked = false;
        var selectedNodes = document.querySelectorAll('.h-tree-node.selected');
        for (var j = 0; j < selectedNodes.length; j++) selectedNodes[j].classList.remove('selected');
        updateBulkActionsBar();
    }

    function updateBulkActionsBar() {
        var bar = document.getElementById('bulkActionsBar');
        document.getElementById('selectedCount').textContent = selectedUnits.size;
        if (selectedUnits.size > 0) {
            bar.classList.add('show');
        } else {
            bar.classList.remove('show');
        }
    }

    function bulkMoveUnits() {
        if (selectedUnits.size === 0) return;
        var newParent = prompt('أدخل رقم الوحدة الأب الجديدة (اتركه فارغاً لجعلها وحدات جذرية):');
        if (newParent === null) return;
        var parentId = newParent ? parseInt(newParent) : null;

        showLoading(true);
        var updates = [];
        selectedUnits.forEach(function (uid) {
            updates.push({ unitNumber: uid, newParentUnitNumber: parentId, setManualHierarchy: true });
        });

        fetch('/api/unithierarchyapi/bulk-update', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ updates: updates, validateCircularReferences: true })
        })
        .then(function (response) { return response.json(); })
        .then(function (result) {
            if (result.success) {
                Moda.toastSuccess('تم نقل الوحدات بنجاح');
                deselectAllUnits();
                loadTree();
            } else {
                Moda.error('فشل النقل', result.message || 'تعذّر نقل الوحدات');
            }
        })
        .catch(function () { Moda.error('خطأ', 'فشل نقل الوحدات'); })
        .finally(function () { showLoading(false); });
    }

    function bulkResetUnits() {
        if (selectedUnits.size === 0) return;
        Moda.confirm({
            title: 'إعادة تعيين متعددة',
            text: 'هل تريد إعادة تعيين ' + selectedUnits.size + ' وحدة إلى قيم Oracle؟',
            confirmText: 'نعم، أعِد التعيين'
        }).then(function (r) {
            if (!r.isConfirmed) return;

            showLoading(true);
            var promises = [];
            selectedUnits.forEach(function (uid) {
                promises.push(
                    fetch('/api/unithierarchyapi/reset/' + uid, { method: 'POST' })
                        .then(function (r) { return r.json(); })
                        .catch(function () { return { success: false }; })
                );
            });

            Promise.all(promises).then(function (results) {
                var success = 0, errors = 0;
                for (var i = 0; i < results.length; i++) {
                    if (results[i].success) success++; else errors++;
                }
                if (errors === 0) {
                    Moda.success('تمت العملية', 'نجحت العملية لـ ' + success + ' وحدة');
                } else {
                    Moda.warning('انتهت العملية', 'نجح: ' + success + ' — فشل: ' + errors);
                }
                deselectAllUnits();
                loadTree();
            }).finally(function () { showLoading(false); });
        });
    }

    // ==========================================
    // STATISTICS
    // ==========================================

    function updateStatistics() {
        var totalUnits = 0, rootUnits = treeData.length, manualHierarchy = 0, totalPersonnel = 0;

        function countNodes(nodes) {
            for (var i = 0; i < nodes.length; i++) {
                totalUnits++;
                if (nodes[i].isManualHierarchy) manualHierarchy++;
                totalPersonnel += nodes[i].personnelCount || 0;
                if (nodes[i].children && nodes[i].children.length > 0) {
                    countNodes(nodes[i].children);
                }
            }
        }
        countNodes(treeData);

        animateStat('totalUnitsCount', totalUnits);
        animateStat('rootUnitsCount', rootUnits);
        animateStat('manualHierarchyCount', manualHierarchy);
        animateStat('totalPersonnelCount', totalPersonnel);
    }

    function animateStat(elementId, target) {
        var el = document.getElementById(elementId);
        if (!el) return;
        var start = 0;
        var duration = 800;
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

    // ==========================================
    // UTILITIES
    // ==========================================

    function showLoading(show) {
        var overlay = document.getElementById('loadingOverlay');
        if (show) { overlay.classList.add('show'); }
        else { overlay.classList.remove('show'); }
    }

    function viewUnitDetails(unitId) {
        window.location.href = '/Organization/Units/Details/' + unitId;
    }

    // ==========================================
    // EVENT LISTENERS INIT
    // ==========================================

    function initializeEventListeners() {
        document.getElementById('searchInput').addEventListener('input', handleSearch);
        document.getElementById('clearSearch').addEventListener('click', function () {
            document.getElementById('searchInput').value = '';
            handleSearch();
        });
        document.getElementById('expandAllBtn').addEventListener('click', expandAll);
        document.getElementById('collapseAllBtn').addEventListener('click', collapseAll);
        document.getElementById('exportBtn').addEventListener('click', exportToExcel);
        document.getElementById('importBtn').addEventListener('click', function () {
            document.getElementById('fileInput').click();
        });
        document.getElementById('refreshBtn').addEventListener('click', loadTree);
        document.getElementById('fileInput').addEventListener('change', handleFileImport);

        var contextItems = document.querySelectorAll('.h-context-item');
        for (var i = 0; i < contextItems.length; i++) {
            contextItems[i].addEventListener('click', handleContextMenuAction);
        }

        document.getElementById('confirmImportBtn').addEventListener('click', function () {
            var modal = bootstrap.Modal.getInstance(document.getElementById('importPreviewModal'));
            modal.hide();
            loadTree();
            pendingImportFile = null;
        });
    }

    function initializeMultiSelectListeners() {
        document.getElementById('selectAllBtn').addEventListener('click', selectAllUnits);
        document.getElementById('deselectAllBtn').addEventListener('click', deselectAllUnits);
        document.getElementById('bulkMoveBtn').addEventListener('click', bulkMoveUnits);
        document.getElementById('bulkResetBtn').addEventListener('click', bulkResetUnits);
        document.getElementById('bulkClearBtn').addEventListener('click', deselectAllUnits);
    }

    // Expose globally for onclick handlers in rendered HTML
    window.toggleNode = toggleNode;
    window.showContextMenu = showContextMenu;
    window.handleCheckboxChange = handleCheckboxChange;
    window.viewUnitDetails = viewUnitDetails;

})();
