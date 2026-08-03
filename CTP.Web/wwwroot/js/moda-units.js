/* ============================================= */
/* MODA Units Management - JavaScript             */
/* صفحة إدارة الوحدات                              */
/* ============================================= */

(function () {
    'use strict';

    var allUnits = [];
    var filteredUnits = [];
    var currentView = 'grid';
    var currentPage = 1;
    var pageSize = 24;
    var searchDebounceTimer;

    document.addEventListener('DOMContentLoaded', function () {
        // Only init Index page elements if they exist
        var searchInput = document.getElementById('searchInput');
        if (searchInput) {
            loadUnits();
            searchInput.addEventListener('input', function () {
                clearTimeout(searchDebounceTimer);
                searchDebounceTimer = setTimeout(filterUnits, 300);
            });
            document.getElementById('regionFilter').addEventListener('change', filterUnits);
            document.getElementById('branchFilter').addEventListener('change', filterUnits);
        }
    });

    // ---- Tab Switching for Details Page ----
    function switchTab(btn, tabId) {
        // Remove active from all buttons
        var allBtns = document.querySelectorAll('.units-tab-btn');
        for (var i = 0; i < allBtns.length; i++) {
            allBtns[i].classList.remove('active');
        }
        btn.classList.add('active');

        // Hide all panes, show selected
        var allPanes = document.querySelectorAll('.detail-tab-pane');
        for (var j = 0; j < allPanes.length; j++) {
            allPanes[j].classList.remove('active');
        }
        var targetPane = document.getElementById(tabId);
        if (targetPane) {
            targetPane.classList.add('active');
        }
    }

    // Expose functions globally for onclick handlers
    window.loadUnits = loadUnits;
    window.setView = setView;
    window.changePageSize = changePageSize;
    window.goToPage = goToPage;
    window.showUnitDetails = showUnitDetails;
    window.switchTab = switchTab;

    function loadUnits() {
        var container = document.getElementById('unitsContainer');
        container.textContent = '';

        var loadingDiv = document.createElement('div');
        loadingDiv.className = 'loading-state';

        var spinner = document.createElement('div');
        spinner.className = 'loading-spinner';
        loadingDiv.appendChild(spinner);

        var loadingText = document.createElement('p');
        loadingText.textContent = 'جاري تحميل بيانات الوحدات...';
        loadingDiv.appendChild(loadingText);

        container.appendChild(loadingDiv);

        fetch('/api/units/parent-units')
            .then(function (response) { return response.json(); })
            .then(function (units) {
                allUnits = units;
                filteredUnits = units;
                updateStats(units);
                populateFilters(units);
                currentPage = 1;
                displayUnits();
            })
            .catch(function (error) {
                console.error('Error loading units:', error);
                showError(container);
            });
    }

    function showError(container) {
        container.textContent = '';

        var errorDiv = document.createElement('div');
        errorDiv.className = 'error-state';

        var icon = document.createElement('i');
        icon.className = 'bi bi-exclamation-triangle-fill';
        errorDiv.appendChild(icon);

        var heading = document.createElement('h4');
        heading.textContent = 'حدث خطأ أثناء تحميل الوحدات';
        errorDiv.appendChild(heading);

        var para = document.createElement('p');
        para.textContent = 'يرجى المحاولة مرة أخرى';
        errorDiv.appendChild(para);

        var retryBtn = document.createElement('button');
        retryBtn.className = 'units-btn units-btn-primary';
        retryBtn.style.marginTop = '16px';
        retryBtn.onclick = loadUnits;

        var retryIcon = document.createElement('i');
        retryIcon.className = 'bi bi-arrow-clockwise';
        retryBtn.appendChild(retryIcon);
        retryBtn.appendChild(document.createTextNode(' إعادة المحاولة'));

        errorDiv.appendChild(retryBtn);
        container.appendChild(errorDiv);
    }

    function updateStats(units) {
        var totalUnitsEl = document.getElementById('totalUnits');
        var totalPersonnelEl = document.getElementById('totalPersonnel');
        var totalChildEl = document.getElementById('totalChildUnits');
        var totalPosEl = document.getElementById('totalPositions');

        var totalPersonnel = 0;
        var totalChildUnits = 0;
        var totalPositions = 0;

        for (var i = 0; i < units.length; i++) {
            totalPersonnel += units[i].personnelCount || 0;
            totalChildUnits += units[i].childUnitsCount || 0;
            totalPositions += units[i].positionsCount || 0;
        }

        animateCounter(totalUnitsEl, units.length);
        animateCounter(totalPersonnelEl, totalPersonnel);
        animateCounter(totalChildEl, totalChildUnits);
        animateCounter(totalPosEl, totalPositions);
    }

    function animateCounter(el, target) {
        if (!el || target === 0) {
            if (el) el.textContent = '0';
            return;
        }

        var duration = 1000;
        var startTime = null;

        function step(timestamp) {
            if (!startTime) startTime = timestamp;
            var progress = Math.min((timestamp - startTime) / duration, 1);
            // Ease-out cubic
            var eased = 1 - Math.pow(1 - progress, 3);
            var current = Math.floor(eased * target);
            el.textContent = current.toLocaleString('en');

            if (progress < 1) {
                requestAnimationFrame(step);
            } else {
                el.textContent = target.toLocaleString('en');
            }
        }

        requestAnimationFrame(step);
    }

    function populateFilters(units) {
        var regionsSet = {};
        var branchesSet = {};

        for (var i = 0; i < units.length; i++) {
            if (units[i].regionDescription) regionsSet[units[i].regionDescription] = true;
            if (units[i].unitBranchDescription) branchesSet[units[i].unitBranchDescription] = true;
        }

        var regions = Object.keys(regionsSet);
        var branches = Object.keys(branchesSet);

        var regionFilter = document.getElementById('regionFilter');
        var branchFilter = document.getElementById('branchFilter');

        regionFilter.textContent = '';
        branchFilter.textContent = '';

        var defaultRegion = document.createElement('option');
        defaultRegion.value = '';
        defaultRegion.textContent = 'جميع المناطق';
        regionFilter.appendChild(defaultRegion);

        var defaultBranch = document.createElement('option');
        defaultBranch.value = '';
        defaultBranch.textContent = 'جميع الفروع';
        branchFilter.appendChild(defaultBranch);

        regions.forEach(function (region) {
            var option = document.createElement('option');
            option.value = region;
            option.textContent = region;
            regionFilter.appendChild(option);
        });

        branches.forEach(function (branch) {
            var option = document.createElement('option');
            option.value = branch;
            option.textContent = branch;
            branchFilter.appendChild(option);
        });
    }

    function filterUnits() {
        var searchTerm = document.getElementById('searchInput').value.toLowerCase();
        var regionFilter = document.getElementById('regionFilter').value;
        var branchFilter = document.getElementById('branchFilter').value;

        filteredUnits = allUnits.filter(function (unit) {
            var matchesSearch = !searchTerm ||
                (unit.unitName && unit.unitName.toLowerCase().indexOf(searchTerm) !== -1) ||
                (unit.unitLocation && unit.unitLocation.toLowerCase().indexOf(searchTerm) !== -1);
            var matchesRegion = !regionFilter || unit.regionDescription === regionFilter;
            var matchesBranch = !branchFilter || unit.unitBranchDescription === branchFilter;

            return matchesSearch && matchesRegion && matchesBranch;
        });

        currentPage = 1;
        displayUnits();
    }

    function setView(view) {
        currentView = view;
        var btns = document.querySelectorAll('.view-toggle-btn');
        for (var i = 0; i < btns.length; i++) {
            btns[i].classList.toggle('active', btns[i].getAttribute('data-view') === view);
        }

        var container = document.getElementById('unitsContainer');
        container.className = view === 'grid' ? 'units-grid' : 'units-list';
        displayUnits();
    }

    function changePageSize() {
        pageSize = parseInt(document.getElementById('pageSizeSelect').value);
        currentPage = 1;
        displayUnits();
    }

    function goToPage(page) {
        var totalPages = Math.ceil(filteredUnits.length / pageSize);
        if (page < 1 || page > totalPages) return;

        currentPage = page;
        displayUnits();
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }

    function displayUnits() {
        var container = document.getElementById('unitsContainer');
        var paginationContainer = document.getElementById('paginationContainer');

        document.getElementById('resultsCount').textContent = filteredUnits.length.toLocaleString('en');

        if (filteredUnits.length === 0) {
            container.textContent = '';
            paginationContainer.style.display = 'none';

            var emptyDiv = document.createElement('div');
            emptyDiv.className = 'empty-state';

            var iconWrapper = document.createElement('div');
            iconWrapper.className = 'empty-state-icon';
            var icon = document.createElement('i');
            icon.className = 'bi bi-inbox';
            iconWrapper.appendChild(icon);
            emptyDiv.appendChild(iconWrapper);

            var heading = document.createElement('h4');
            heading.textContent = 'لا توجد وحدات';
            emptyDiv.appendChild(heading);

            var para = document.createElement('p');
            para.textContent = 'لم يتم العثور على وحدات تطابق معايير البحث';
            emptyDiv.appendChild(para);

            container.appendChild(emptyDiv);
            return;
        }

        // Calculate pagination
        var totalPages = Math.ceil(filteredUnits.length / pageSize);
        var startIndex = (currentPage - 1) * pageSize;
        var endIndex = Math.min(startIndex + pageSize, filteredUnits.length);
        var pageUnits = filteredUnits.slice(startIndex, endIndex);

        // Clear and render units
        container.textContent = '';

        if (currentView === 'grid') {
            pageUnits.forEach(function (unit, index) {
                var card = createGridCard(unit);
                card.style.animationDelay = (index * 40) + 'ms';
                card.classList.add('units-animate');
                container.appendChild(card);
            });
        } else {
            pageUnits.forEach(function (unit, index) {
                var item = createListItem(unit);
                item.style.animationDelay = (index * 30) + 'ms';
                item.classList.add('units-animate');
                container.appendChild(item);
            });
        }

        // Update pagination
        paginationContainer.style.display = 'flex';
        document.getElementById('paginationInfo').textContent =
            'عرض ' + (startIndex + 1).toLocaleString('en') + '-' + endIndex.toLocaleString('en') + ' من ' + filteredUnits.length.toLocaleString('en');

        renderPagination(totalPages);
    }

    function renderPagination(totalPages) {
        var nav = document.getElementById('paginationNav');
        nav.textContent = '';

        if (totalPages <= 1) return;

        // Previous button
        var prevBtn = document.createElement('button');
        prevBtn.className = 'page-btn';
        prevBtn.disabled = currentPage === 1;
        prevBtn.onclick = function () { goToPage(currentPage - 1); };
        var prevIcon = document.createElement('i');
        prevIcon.className = 'bi bi-chevron-right';
        prevBtn.appendChild(prevIcon);
        nav.appendChild(prevBtn);

        // Page numbers
        var maxVisiblePages = 5;
        var startPage = Math.max(1, currentPage - Math.floor(maxVisiblePages / 2));
        var endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);

        if (endPage - startPage + 1 < maxVisiblePages) {
            startPage = Math.max(1, endPage - maxVisiblePages + 1);
        }

        if (startPage > 1) {
            var firstBtn = document.createElement('button');
            firstBtn.className = 'page-btn';
            firstBtn.textContent = '1';
            firstBtn.onclick = function () { goToPage(1); };
            nav.appendChild(firstBtn);

            if (startPage > 2) {
                var ellipsis = document.createElement('span');
                ellipsis.className = 'page-ellipsis';
                ellipsis.textContent = '...';
                nav.appendChild(ellipsis);
            }
        }

        for (var i = startPage; i <= endPage; i++) {
            (function (pageNum) {
                var pageBtn = document.createElement('button');
                pageBtn.className = 'page-btn' + (pageNum === currentPage ? ' active' : '');
                pageBtn.textContent = pageNum.toLocaleString('en');
                pageBtn.onclick = function () { goToPage(pageNum); };
                nav.appendChild(pageBtn);
            })(i);
        }

        if (endPage < totalPages) {
            if (endPage < totalPages - 1) {
                var ellipsis2 = document.createElement('span');
                ellipsis2.className = 'page-ellipsis';
                ellipsis2.textContent = '...';
                nav.appendChild(ellipsis2);
            }

            var lastBtn = document.createElement('button');
            lastBtn.className = 'page-btn';
            lastBtn.textContent = totalPages.toLocaleString('en');
            lastBtn.onclick = function () { goToPage(totalPages); };
            nav.appendChild(lastBtn);
        }

        // Next button
        var nextBtn = document.createElement('button');
        nextBtn.className = 'page-btn';
        nextBtn.disabled = currentPage === totalPages;
        nextBtn.onclick = function () { goToPage(currentPage + 1); };
        var nextIcon = document.createElement('i');
        nextIcon.className = 'bi bi-chevron-left';
        nextBtn.appendChild(nextIcon);
        nav.appendChild(nextBtn);
    }

    function createGridCard(unit) {
        var card = document.createElement('div');
        card.className = 'unit-card';
        card.onclick = function () { showUnitDetails(unit.unitNumber); };

        // Header
        var header = document.createElement('div');
        header.className = 'unit-card-header';

        var iconBadge = document.createElement('div');
        iconBadge.className = 'unit-icon-badge';
        var buildingIcon = document.createElement('i');
        buildingIcon.className = 'bi bi-building-fill';
        iconBadge.appendChild(buildingIcon);

        var info = document.createElement('div');
        info.className = 'unit-info';

        var name = document.createElement('h3');
        name.className = 'unit-name';
        name.textContent = unit.unitName || 'وحدة بدون اسم';

        var location = document.createElement('div');
        location.className = 'unit-location';
        var locIcon = document.createElement('i');
        locIcon.className = 'bi bi-geo-alt-fill';
        location.appendChild(locIcon);
        location.appendChild(document.createTextNode(' ' + (unit.unitLocation || 'غير محدد')));

        info.appendChild(name);
        info.appendChild(location);
        header.appendChild(iconBadge);
        header.appendChild(info);

        // Body
        var body = document.createElement('div');
        body.className = 'unit-card-body';

        var statsGrid = document.createElement('div');
        statsGrid.className = 'unit-stats-grid';

        // Personnel stat
        statsGrid.appendChild(createStat('personnel', 'bi-people-fill',
            (unit.personnelCount || 0).toLocaleString('en'), 'أفراد'));

        // Children stat
        statsGrid.appendChild(createStat('children', 'bi-diagram-3-fill',
            (unit.childUnitsCount || 0).toLocaleString('en'), 'وحدات فرعية'));

        // Positions stat
        statsGrid.appendChild(createStat('positions', 'bi-briefcase-fill',
            (unit.positionsCount || 0).toLocaleString('en'), 'وظائف'));

        // Region stat
        var regionStat = createStat('region', 'bi-pin-map-fill', null, 'المنطقة', true);
        regionStat.classList.add('region-stat');
        var regionText = regionStat.querySelector('.unit-stat-content');
        regionText.textContent = '';
        var regionValue = document.createElement('div');
        regionValue.className = 'unit-stat-text';
        regionValue.textContent = unit.regionDescription || 'غير محدد';
        var regionLabel = document.createElement('div');
        regionLabel.className = 'unit-stat-label';
        regionLabel.textContent = 'المنطقة';
        regionText.appendChild(regionValue);
        regionText.appendChild(regionLabel);
        statsGrid.appendChild(regionStat);

        body.appendChild(statsGrid);
        card.appendChild(header);
        card.appendChild(body);

        return card;
    }

    function createStat(type, iconClass, value, label, isText) {
        var stat = document.createElement('div');
        stat.className = 'unit-stat';

        var iconDiv = document.createElement('div');
        iconDiv.className = 'unit-stat-icon ' + type;
        var icon = document.createElement('i');
        icon.className = 'bi ' + iconClass;
        iconDiv.appendChild(icon);

        var content = document.createElement('div');
        content.className = 'unit-stat-content';

        if (!isText) {
            var valueDiv = document.createElement('div');
            valueDiv.className = 'unit-stat-value';
            valueDiv.textContent = value;
            var labelDiv = document.createElement('div');
            labelDiv.className = 'unit-stat-label';
            labelDiv.textContent = label;
            content.appendChild(valueDiv);
            content.appendChild(labelDiv);
        }

        stat.appendChild(iconDiv);
        stat.appendChild(content);
        return stat;
    }

    function createListItem(unit) {
        var item = document.createElement('div');
        item.className = 'unit-list-item';
        item.onclick = function () { showUnitDetails(unit.unitNumber); };

        var icon = document.createElement('div');
        icon.className = 'unit-list-icon';
        var buildingIcon = document.createElement('i');
        buildingIcon.className = 'bi bi-building-fill';
        icon.appendChild(buildingIcon);

        var info = document.createElement('div');
        info.className = 'unit-list-info';
        var name = document.createElement('h4');
        name.className = 'unit-list-name';
        name.textContent = unit.unitName || 'وحدة بدون اسم';
        var loc = document.createElement('div');
        loc.className = 'unit-list-location';
        var locIcon = document.createElement('i');
        locIcon.className = 'bi bi-geo-alt-fill';
        loc.appendChild(locIcon);
        loc.appendChild(document.createTextNode(' ' + (unit.unitLocation || 'غير محدد') + ' • ' + (unit.regionDescription || 'غير محدد')));
        info.appendChild(name);
        info.appendChild(loc);

        var stats = document.createElement('div');
        stats.className = 'unit-list-stats';
        stats.appendChild(createListStat((unit.personnelCount || 0).toLocaleString('en'), 'أفراد'));
        stats.appendChild(createListStat((unit.childUnitsCount || 0).toLocaleString('en'), 'فرعية'));
        stats.appendChild(createListStat((unit.positionsCount || 0).toLocaleString('en'), 'وظائف'));

        var action = document.createElement('div');
        action.className = 'unit-list-action';
        var eyeIcon = document.createElement('i');
        eyeIcon.className = 'bi bi-eye-fill';
        action.appendChild(eyeIcon);
        action.appendChild(document.createTextNode(' عرض'));

        item.appendChild(icon);
        item.appendChild(info);
        item.appendChild(stats);
        item.appendChild(action);

        return item;
    }

    function createListStat(value, label) {
        var stat = document.createElement('div');
        stat.className = 'unit-list-stat';
        var valueDiv = document.createElement('div');
        valueDiv.className = 'unit-list-stat-value';
        valueDiv.textContent = value;
        var labelDiv = document.createElement('div');
        labelDiv.className = 'unit-list-stat-label';
        labelDiv.textContent = label;
        stat.appendChild(valueDiv);
        stat.appendChild(labelDiv);
        return stat;
    }

    function showUnitDetails(unitNumber) {
        window.location.href = '/Organization/Units/Details/' + unitNumber;
    }

})();
