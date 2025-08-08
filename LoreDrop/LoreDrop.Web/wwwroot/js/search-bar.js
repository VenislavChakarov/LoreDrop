
document.addEventListener('DOMContentLoaded', function() {
    const dataEl = document.getElementById('series-data');
    if (!dataEl) {
        console.error('❌ series-data <script> tag not found');
        return;
    }

    let seriesList;
    try {
        seriesList = JSON.parse(dataEl.textContent);
    } catch (err) {
        console.error('❌ Error parsing JSON from #series-data:', err);
        return;
    }

    const input       = document.getElementById('filter-input');
    const suggestions = document.getElementById('suggestions');
    const searchBtn   = document.getElementById('filter-btn');

    if (!input || !suggestions || !searchBtn) {
        console.error('❌ Missing #filter-input, #suggestions, or #filter-btn');
        return;
    }

    // start hidden
    suggestions.style.display = 'none';
    console.log(`✅ Autocomplete init: ${seriesList.length} items, suggestions UL =`, suggestions);

    // grab real hrefs off the cards
    function getSeriesUrl(id) {
        const card = document.querySelector(`.series-card[data-id="${id}"]`);
        const link = card?.querySelector('a.card-link');
        return link ? link.href : null;
    }

    function showSuggestions(matches) {
        suggestions.innerHTML = '';

        if (!matches || matches.length === 0) {
            suggestions.style.display = 'none';
            return;
        }

        matches.forEach(series => {
            const item = document.createElement('li');
            item.classList.add('suggestion-item');
            
            const safeImage = series.ImageUrl || '/images/placeholder.jpg';

            item.innerHTML = `
            <img src="${safeImage}" alt="${series.Title}" onerror="this.onerror=null;this.src='/images/placeholder.jpg'" />
            <span>${series.Title}</span>
        `;

            item.addEventListener('click', () => {
                // Prefer the real card link if it's present in the page
                const url = getSeriesUrl(series.Id) || `/Details/Details?id=${series.Id}`;
                window.location.href = url;
            });

            suggestions.appendChild(item);
        });

        suggestions.style.display = 'block';
    }


    function findMatches(term) {
        return term
            ? seriesList.filter(s => s.Title.toLowerCase().includes(term)).slice(0, 10)
            : [];
    }

    function doSearch() {
        const term    = input.value.trim().toLowerCase();
        const matches = findMatches(term);
        if (matches.length) {
            const url = getSeriesUrl(matches[0].Id);
            console.log('🔎 doSearch → first match url =', url);
            if (url) window.location.href = url;
        } else {
            suggestions.innerHTML = '';
            suggestions.style.display = 'none';
        }
    }

    input.addEventListener('input',  () => {
        const term = input.value.trim().toLowerCase();
        console.log('🔍 onInput:', term);
        showSuggestions(findMatches(term));
    });
    input.addEventListener('focus',  () => showSuggestions(findMatches(input.value.trim().toLowerCase())));
    input.addEventListener('blur',   () => setTimeout(() => suggestions.style.display = 'none', 200));
    input.addEventListener('keydown', e => { if (e.key==='Enter') { e.preventDefault(); doSearch(); } });
    searchBtn.addEventListener('click', e => { e.preventDefault(); doSearch(); });
});
