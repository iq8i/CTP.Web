/* ============================================= */
/* MODA Quick Organizer - JavaScript              */
/* منظم الوحدات السريع                             */
/* ============================================= */
(function () {
    'use strict';

    var allUnits = [];
    var parentUnits = [];
    var unassignedUnits = [];
    var selectedUnits = new Set();
    var selectedParentForAssignment = null;
    var assignModal = null;
    var draggedUnitId = null;

    document.addEventListener('DOMContentLoaded', function () {
        var modalEl = document.getElementById('assignModal');
        if (modalEl) {
            assignModal = new bootstrap.Modal(modalEl);
        }

        var searchParents = document.getElementById('searchParents');
        var searchUnassigned = document.getElementById('searchUnassigned');
        var refreshBtn = document.getElementById('refreshBtn');
        var assignBtn = document.getElementById('assignBtn');
        var clearSelectionBtn = document.getElementById('clearSelectionBtn');
        var confirmAssignBtn = document.getElementById('confirmAssignBtn');

        if (searchParents) searchParents.addEventListener('input', filterParents);
        if (searchUnassigned) searchUnassigned.addEventListener('input', filterUnassigned);
        if (refreshBtn) refreshBtn.addEventListener('click', refreshData);
        if (assignBtn) assignBtn.addEventListener('click', showAssignModal);
        if (clearSelectionBtn) clearSelectionBtn.addEventListener('click', clearSelection);
        if (confirmAssignBtn) confirmAssignBtn.addEventListener('click', confirmAssignment);

        loadData();
    });

    function loadData() {
        showLoading('parentsContainer');
        showLoading('unassignedContainer');

        fetch('/api/units')
            .then(function (response) { return response.json(); })
            .then(function (data) {
                allUnits = data;
                categorizeUnits();
                renderParentUnits();
                renderUnassignedUnits();
                updateStats();
            })
            .catch(function (error) {
                console.error('Error loading data:', error);
                alert('فشل تحميل البيانات');
            });
    }

    function showLoading(containerId) {
        var container = document.getElementById(containerId);
        if (!container) return;
        container.innerHTML = '';

        var loadingDiv = document.createElement('div');
        loadingDiv.className = 'qo-loading-state';

        var spinner = document.createElement('div');
        spinner.className = 'qo-loading-spinner';

        var text = document.createElement('p');
        text.textContent = 'جاري التحميل...';

        loadingDiv.appendChild(spinner);
        loadingDiv.appendChild(text);
        container.appendChild(loadingDiv);
    }

    function categorizeUnits() {
        parentUnits = allUnits.filter(function (u) {
            var effectiveParent = u.isHierarchyManuallySet ? u.manualParentUnitNumber : u.parentUnitNumber;
            return !effectiveParent || effectiveParent === 0;
        });

        unassignedUnits = allUnits.filter(function (u) {
            var effectiveParent = u.isHierarchyManuallySet ? u.manualParentUnitNumber : u.parentUnitNumber;
            return effectiveParent && effectiveParent !== 0 && !u.isHierarchyManuallySet;
        });
    }

    function refreshData() {
        loadData();
    }

    function getChildrenOfUnit(parentId) {
        return allUnits.filter(function (u) {
            var effectiveParent = u.isHierarchyManuallySet ? u.manualParentUnitNumber : u.parentUnitNumber;
            return effectiveParent === parentId;
        });
    }

    function renderParentUnits(filteredUnits) {
        var container = document.getElementById('parentsContainer');
        if (!container) return;
        var units = filteredUnits || parentUnits;

        var countEl = document.getElementById('parentCount');
        if (countEl) countEl.textContent = units.length.toLocaleString('en');
        container.innerHTML = '';

        if (units.length === 0) {
            showEmptyState(container, 'لا توجد وحدات رئيسية');
            return;
        }

        units.forEach(function (unit) {
            var children = getChildrenOfUnit(unit.unitNumber);
            var card = createParentCardElement(unit, children);
            container.appendChild(card);
        });
    }

    function createParentCardElement(unit, children) {
        var card = document.createElement('div');
        card.className = 'qo-parent-card';
        card.dataset.parentId = unit.unitNumber;

        card.addEventListener('dragover', function (e) { handleDragOver(e, unit.unitNumber); });
        card.addEventListener('dragleave', handleDragLeave);
        card.addEventListener('drop', function (e) { handleDrop(e, unit.unitNumber); });

        // Header
        var header = document.createElement('div');
        header.className = 'qo-parent-header';
        header.addEventListener('click', function () { toggleParentChildren(unit.unitNumber); });

        var icon = document.createElement('div');
        icon.className = 'qo-parent-icon';
        var iconI = document.createElement('i');
        iconI.className = 'bi bi-building-fill';
        icon.appendChild(iconI);

        var info = document.createElement('div');
        info.className = 'qo-parent-info';

        var name = document.createElement('h4');
        name.className = 'qo-parent-name';
        name.textContent = unit.unitName || 'وحدة ' + unit.unitNumber.toLocaleString('en');

        var meta = document.createElement('div');
        meta.className = 'qo-parent-meta';

        var idSpan = document.createElement('span');
        var hashIcon = document.createElement('i');
        hashIcon.className = 'bi bi-hash';
        idSpan.appendChild(hashIcon);
        idSpan.appendChild(document.createTextNode(' ' + unit.unitNumber.toLocaleString('en')));
        meta.appendChild(idSpan);

        if (unit.unitLocation) {
            var locSpan = document.createElement('span');
            var locIcon = document.createElement('i');
            locIcon.className = 'bi bi-geo-alt';
            locSpan.appendChild(locIcon);
            locSpan.appendChild(document.createTextNode(' ' + unit.unitLocation));
            meta.appendChild(locSpan);
        }

        info.appendChild(name);
        info.appendChild(meta);

        var stats = document.createElement('div');
        stats.className = 'qo-parent-stats';

        var childStat = document.createElement('div');
        childStat.className = 'qo-parent-stat';
        var childIcon = document.createElement('i');
        childIcon.className = 'bi bi-diagram-3';
        var childCount = document.createElement('span');
        childCount.textContent = children.length.toLocaleString('en');
        childStat.appendChild(childIcon);
        childStat.appendChild(document.createTextNode(' '));
        childStat.appendChild(childCount);

        var personnelStat = document.createElement('div');
        personnelStat.className = 'qo-parent-stat';
        var personnelIcon = document.createElement('i');
        personnelIcon.className = 'bi bi-people';
        var personnelCount = document.createElement('span');
        personnelCount.textContent = (unit.personnelCount || 0).toLocaleString('en');
        personnelStat.appendChild(personnelIcon);
        personnelStat.appendChild(document.createTextNode(' '));
        personnelStat.appendChild(personnelCount);

        stats.appendChild(childStat);
        stats.appendChild(personnelStat);

        var toggle = document.createElement('button');
        toggle.className = 'qo-parent-toggle';
        toggle.addEventListener('click', function (e) {
            e.stopPropagation();
            toggleParentChildren(unit.unitNumber);
        });
        var toggleIcon = document.createElement('i');
        toggleIcon.className = 'bi bi-chevron-down';
        toggle.appendChild(toggleIcon);

        header.appendChild(icon);
        header.appendChild(info);
        header.appendChild(stats);
        header.appendChild(toggle);

        // Children container
        var childrenDiv = document.createElement('div');
        childrenDiv.className = 'qo-parent-children';
        childrenDiv.id = 'children-' + unit.unitNumber;

        if (children.length > 0) {
            children.forEach(function (child) {
                var childItem = createChildItemElement(child);
                childrenDiv.appendChild(childItem);
            });
        } else {
            var noChildren = document.createElement('div');
            noChildren.className = 'qo-no-children';
            var noIcon = document.createElement('i');
            noIcon.className = 'bi bi-inbox';
            noChildren.appendChild(noIcon);
            noChildren.appendChild(document.createElement('br'));
            noChildren.appendChild(document.createTextNode('لا توجد وحدات فرعية'));
            childrenDiv.appendChild(noChildren);
        }

        card.appendChild(header);
        card.appendChild(childrenDiv);

        return card;
    }

    function createChildItemElement(child) {
        var item = document.createElement('div');
        item.className = 'qo-child-item' + (child.isHierarchyManuallySet ? ' manual' : '');
        item.dataset.unitId = child.unitNumber;

        var iconDiv = document.createElement('div');
        iconDiv.className = 'qo-child-icon';
        var icon = document.createElement('i');
        icon.className = 'bi bi-building';
        iconDiv.appendChild(icon);

        var nameSpan = document.createElement('span');
        nameSpan.className = 'qo-child-name';
        nameSpan.textContent = child.unitName || 'وحدة ' + child.unitNumber.toLocaleString('en');

        var idSpan = document.createElement('span');
        idSpan.className = 'qo-child-id';
        idSpan.textContent = '#' + child.unitNumber.toLocaleString('en');

        item.appendChild(iconDiv);
        item.appendChild(nameSpan);
        item.appendChild(idSpan);

        if (child.isHierarchyManuallySet) {
            var badge = document.createElement('span');
            badge.className = 'qo-child-badge';
            badge.textContent = 'يدوي';
            item.appendChild(badge);
        }

        var removeBtn = document.createElement('button');
        removeBtn.className = 'qo-child-remove';
        removeBtn.title = 'إزالة من الوحدة الأب';
        removeBtn.addEventListener('click', function (e) { removeFromParent(child.unitNumber, e); });
        var removeIcon = document.createElement('i');
        removeIcon.className = 'bi bi-x-lg';
        removeBtn.appendChild(removeIcon);
        item.appendChild(removeBtn);

        return item;
    }

    function renderUnassignedUnits(filteredUnits) {
        var container = document.getElementById('unassignedContainer');
        if (!container) return;
        var units = filteredUnits || unassignedUnits;

        var countEl = document.getElementById('unassignedCount');
        if (countEl) countEl.textContent = units.length.toLocaleString('en');
        container.innerHTML = '';

        if (units.length === 0) {
            showEmptyState(container, 'جميع الوحدات معينة', 'bi bi-check-circle');
            return;
        }

        units.forEach(function (unit) {
            var item = createUnassignedItemElement(unit);
            container.appendChild(item);
        });
    }

    function createUnassignedItemElement(unit) {
        var item = document.createElement('div');
        item.className = 'qo-unassigned-unit' + (selectedUnits.has(unit.unitNumber) ? ' selected' : '');
        item.dataset.unitId = unit.unitNumber;
        item.draggable = true;

        item.addEventListener('dragstart', function (e) { handleDragStart(e, unit.unitNumber); });
        item.addEventListener('dragend', handleDragEnd);

        var checkbox = document.createElement('input');
        checkbox.type = 'checkbox';
        checkbox.className = 'qo-unassigned-checkbox';
        checkbox.id = 'check-' + unit.unitNumber;
        checkbox.checked = selectedUnits.has(unit.unitNumber);
        checkbox.addEventListener('change', function () { toggleUnitSelection(unit.unitNumber, this.checked); });

        var iconDiv = document.createElement('div');
        iconDiv.className = 'qo-unassigned-icon';
        var icon = document.createElement('i');
        icon.className = 'bi bi-building';
        iconDiv.appendChild(icon);

        var info = document.createElement('div');
        info.className = 'qo-unassigned-info';

        var name = document.createElement('h5');
        name.className = 'qo-unassigned-name';
        name.textContent = unit.unitName || 'وحدة ' + unit.unitNumber.toLocaleString('en');

        var meta = document.createElement('div');
        meta.className = 'qo-unassigned-meta';

        if (unit.unitLocation) {
            var locSpan = document.createElement('span');
            var locIcon = document.createElement('i');
            locIcon.className = 'bi bi-geo-alt';
            locSpan.appendChild(locIcon);
            locSpan.appendChild(document.createTextNode(' ' + unit.unitLocation));
            meta.appendChild(locSpan);
        }

        if (unit.parentUnitNumber) {
            var parentSpan = document.createElement('span');
            var parentIcon = document.createElement('i');
            parentIcon.className = 'bi bi-arrow-up';
            parentSpan.appendChild(parentIcon);
            parentSpan.appendChild(document.createTextNode(' أب: ' + unit.parentUnitNumber.toLocaleString('en')));
            meta.appendChild(parentSpan);
        }

        info.appendChild(name);
        info.appendChild(meta);

        var idSpan = document.createElement('span');
        idSpan.className = 'qo-unassigned-id';
        idSpan.textContent = '#' + unit.unitNumber.toLocaleString('en');

        item.appendChild(checkbox);
        item.appendChild(iconDiv);
        item.appendChild(info);
        item.appendChild(idSpan);

        return item;
    }

    function showEmptyState(container, message, iconClass) {
        var emptyDiv = document.createElement('div');
        emptyDiv.className = 'qo-empty-state';

        var icon = document.createElement('i');
        icon.className = iconClass || 'bi bi-inbox';

        var title = document.createElement('h4');
        title.textContent = message;

        emptyDiv.appendChild(icon);
        emptyDiv.appendChild(title);
        container.appendChild(emptyDiv);
    }

    function toggleParentChildren(parentId) {
        var childrenDiv = document.getElementById('children-' + parentId);
        if (childrenDiv) {
            childrenDiv.classList.toggle('show');
            var card = childrenDiv.closest('.qo-parent-card');
            if (card) {
                var toggleIcon = card.querySelector('.qo-parent-toggle i');
                if (toggleIcon) {
                    toggleIcon.classList.toggle('bi-chevron-down');
                    toggleIcon.classList.toggle('bi-chevron-up');
                }
            }
        }
    }

    function toggleUnitSelection(unitId, checked) {
        var unitElement = document.querySelector('[data-unit-id="' + unitId + '"].qo-unassigned-unit');

        if (checked) {
            selectedUnits.add(unitId);
            if (unitElement) unitElement.classList.add('selected');
        } else {
            selectedUnits.delete(unitId);
            if (unitElement) unitElement.classList.remove('selected');
        }

        updateSelectionUI();
    }

    function clearSelection() {
        selectedUnits.clear();
        var checkboxes = document.querySelectorAll('.qo-unassigned-checkbox');
        for (var i = 0; i < checkboxes.length; i++) checkboxes[i].checked = false;
        var selectedEls = document.querySelectorAll('.qo-unassigned-unit.selected');
        for (var j = 0; j < selectedEls.length; j++) selectedEls[j].classList.remove('selected');
        updateSelectionUI();
    }

    function updateSelectionUI() {
        var bar = document.getElementById('assignmentBar');
        var countEl = document.getElementById('selectedCount');
        if (countEl) countEl.textContent = selectedUnits.size.toLocaleString('en');

        if (bar) {
            if (selectedUnits.size > 0) {
                bar.classList.add('show');
            } else {
                bar.classList.remove('show');
            }
        }
    }

    function handleDragStart(event, unitId) {
        draggedUnitId = unitId;
        event.target.classList.add('dragging');
        event.dataTransfer.effectAllowed = 'move';
    }

    function handleDragEnd(event) {
        event.target.classList.remove('dragging');
        var dropTargets = document.querySelectorAll('.qo-parent-card.drop-target');
        for (var i = 0; i < dropTargets.length; i++) dropTargets[i].classList.remove('drop-target');
    }

    function handleDragOver(event, parentId) {
        event.preventDefault();
        event.dataTransfer.dropEffect = 'move';
        var card = document.querySelector('[data-parent-id="' + parentId + '"]');
        if (card) card.classList.add('drop-target');
    }

    function handleDragLeave(event) {
        var card = event.currentTarget;
        if (card) card.classList.remove('drop-target');
    }

    function handleDrop(event, parentId) {
        event.preventDefault();
        var card = event.currentTarget;
        if (card) card.classList.remove('drop-target');

        if (!draggedUnitId) return;

        var unitsToMove = selectedUnits.size > 0 && selectedUnits.has(draggedUnitId)
            ? Array.from(selectedUnits)
            : [draggedUnitId];

        assignUnitsToParent(unitsToMove, parentId);
        draggedUnitId = null;
    }

    function showAssignModal() {
        var container = document.getElementById('parentSelectList');
        if (!container) return;
        container.innerHTML = '';

        parentUnits.forEach(function (unit) {
            var item = document.createElement('label');
            item.className = 'qo-parent-select-item';
            item.addEventListener('click', function () { selectParentForAssignment(unit.unitNumber); });

            var radio = document.createElement('input');
            radio.type = 'radio';
            radio.name = 'parentSelect';
            radio.value = unit.unitNumber;

            var iconDiv = document.createElement('div');
            iconDiv.className = 'qo-parent-icon';
            var icon = document.createElement('i');
            icon.className = 'bi bi-building-fill';
            iconDiv.appendChild(icon);

            var info = document.createElement('div');
            info.className = 'qo-parent-info';

            var name = document.createElement('h5');
            name.className = 'qo-parent-name';
            name.textContent = unit.unitName || 'وحدة ' + unit.unitNumber.toLocaleString('en');

            var meta = document.createElement('div');
            meta.className = 'qo-parent-meta';
            meta.textContent = '#' + unit.unitNumber.toLocaleString('en') + (unit.unitLocation ? ' • ' + unit.unitLocation : '');

            info.appendChild(name);
            info.appendChild(meta);

            item.appendChild(radio);
            item.appendChild(iconDiv);
            item.appendChild(info);
            container.appendChild(item);
        });

        selectedParentForAssignment = null;
        if (assignModal) assignModal.show();
    }

    function selectParentForAssignment(parentId) {
        selectedParentForAssignment = parentId;
        var items = document.querySelectorAll('.qo-parent-select-item');
        for (var i = 0; i < items.length; i++) items[i].classList.remove('selected');
        var radio = document.querySelector('input[value="' + parentId + '"]');
        if (radio) {
            var parentItem = radio.closest('.qo-parent-select-item');
            if (parentItem) parentItem.classList.add('selected');
        }
    }

    function confirmAssignment() {
        if (!selectedParentForAssignment) {
            alert('يرجى اختيار الوحدة الأب أولاً');
            return;
        }

        if (assignModal) assignModal.hide();
        assignUnitsToParent(Array.from(selectedUnits), selectedParentForAssignment);
    }

    function assignUnitsToParent(unitIds, parentId) {
        if (unitIds.length === 0) return;

        var parentUnit = parentUnits.find(function (p) { return p.unitNumber === parentId; });
        var parentName = parentUnit ? parentUnit.unitName : parentId;

        var confirmed = confirm(
            'سيتم تعيين ' + unitIds.length + ' وحدة إلى الوحدة الأب: ' + parentName +
            '\n\nسيتم حفظ هذا التعيين كتسلسل يدوي ولن يتأثر بالمزامنة من Oracle\n\nهل تريد المتابعة؟'
        );

        if (!confirmed) return;

        var updates = unitIds.map(function (unitNumber) {
            return { unitNumber: unitNumber, newParentUnitNumber: parentId, setManualHierarchy: true };
        });

        fetch('/api/unithierarchyapi/bulk-update', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ updates: updates, validateCircularReferences: true })
        })
            .then(function (response) { return response.json().then(function (data) { return { ok: response.ok, data: data }; }); })
            .then(function (result) {
                if (result.ok && result.data.success) {
                    alert('تم تعيين ' + unitIds.length + ' وحدة بنجاح');
                    clearSelection();
                    loadData();
                } else {
                    throw new Error(result.data.message || 'فشل التعيين');
                }
            })
            .catch(function (error) {
                console.error('Error assigning units:', error);
                alert(error.message || 'فشل تعيين الوحدات');
            });
    }

    function removeFromParent(unitId, event) {
        event.stopPropagation();

        var confirmed = confirm('هل تريد إزالة هذه الوحدة وإعادتها إلى القيمة الأصلية من Oracle؟');
        if (!confirmed) return;

        fetch('/api/unithierarchyapi/reset/' + unitId, { method: 'POST' })
            .then(function (response) { return response.json().then(function (data) { return { ok: response.ok, data: data }; }); })
            .then(function (result) {
                if (result.ok && result.data.success) {
                    alert('تم إعادة التعيين بنجاح');
                    loadData();
                } else {
                    throw new Error(result.data.message || 'فشل إعادة التعيين');
                }
            })
            .catch(function (error) {
                console.error('Error resetting unit:', error);
                alert(error.message || 'فشل إعادة التعيين');
            });
    }

    function filterParents() {
        var input = document.getElementById('searchParents');
        if (!input) return;
        var term = input.value.toLowerCase();
        if (!term) {
            renderParentUnits();
            return;
        }

        var filtered = parentUnits.filter(function (u) {
            return (u.unitName && u.unitName.toLowerCase().includes(term)) ||
                u.unitNumber.toString().includes(term) ||
                (u.unitLocation && u.unitLocation.toLowerCase().includes(term));
        });
        renderParentUnits(filtered);
    }

    function filterUnassigned() {
        var input = document.getElementById('searchUnassigned');
        if (!input) return;
        var term = input.value.toLowerCase();
        if (!term) {
            renderUnassignedUnits();
            return;
        }

        var filtered = unassignedUnits.filter(function (u) {
            return (u.unitName && u.unitName.toLowerCase().includes(term)) ||
                u.unitNumber.toString().includes(term) ||
                (u.unitLocation && u.unitLocation.toLowerCase().includes(term));
        });
        renderUnassignedUnits(filtered);
    }

    function updateStats() {
        var el1 = document.getElementById('totalParents');
        var el2 = document.getElementById('totalUnassigned');
        var el3 = document.getElementById('totalAssigned');
        var el4 = document.getElementById('totalManual');

        if (el1) el1.textContent = parentUnits.length.toLocaleString('en');
        if (el2) el2.textContent = unassignedUnits.length.toLocaleString('en');

        var manualCount = allUnits.filter(function (u) { return u.isHierarchyManuallySet; }).length;
        if (el3) el3.textContent = (allUnits.length - parentUnits.length).toLocaleString('en');
        if (el4) el4.textContent = manualCount.toLocaleString('en');
    }
})();
