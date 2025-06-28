window.addEventListener('hashchange', () => {
    selectPage();});

document.addEventListener('DOMContentLoaded', () => {
    selectPage();
});

function selectPage() {
    const route = window.location.hash.split('/');
    switch(route[0]) {
        case '':
        case '#home': homePage(); break;
        case '#rate': ratePage(route[1]); break;
        default: notFoundPage;
    }
}

function m(val) {
    return val < 10 ? `0${val}` : val;
} 

function ratePage(cc) {
    if(typeof cc == 'undefined') {
        cc = 'USD';
    }
    const date1 = new Date();
    const date2 = new Date(date1.getTime() - 604800000);  // 604800000 - 7 днів у мілісекундах
    const d1 = `${date1.getFullYear()}${m(date1.getMonth()+1)}${m(date1.getDate())}`;
    const d2 = `${date2.getFullYear()}${m(date2.getMonth()+1)}${m(date2.getDate())}`;
    console.log(d1, d2);
    let url = `https://bank.gov.ua/NBU_Exchange/exchange_site?start=${d2}&end=${d1}&valcode=${cc.toLowerCase()}&sort=exchangedate&order=desc&json`;
    url = 'https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?date=20200302&json';
    // url = 'https://bank.gov.ua/NBU_Exchange/exchange_site?start=20250621&end=20250628&valcode=usd&sort=exchangedate&order=desc&json';
    fetch(url)
    .then(r => r.json())
    .then(j => {
        let html = JSON.stringify(j);
        // for(let r of j) {
        //     html += `<p>exchangedate: ${r.exchangedate}, rate: ${r.rate}</p>`;
        // }
        document.getElementById('main-block').innerHTML = html;
    });    
}

function notFoundPage() {
    document.getElementById('main-block').innerHTML = "Not found";
}

function homePage() {
    let dateStr = localStorage.getItem('rate-date');
    let showDate, apiDate;
    if (dateStr) {
        apiDate = dateStr.replace(/-/g, '');
        const [y, mth, d] = dateStr.split('-');
        showDate = `${d}.${mth}.${y}`;
    } else {
        const now = new Date();
        apiDate = `${now.getFullYear()}${m(now.getMonth()+1)}${m(now.getDate())}`;
        dateStr = `${now.getFullYear()}-${m(now.getMonth()+1)}-${m(now.getDate())}`;
        showDate = `${m(now.getDate())}.${m(now.getMonth()+1)}.${now.getFullYear()}`;
    }
    const url = `https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?date=${apiDate}&json`;
    fetch(url)
    .then(r => r.json())
    .then(j => {
        let table = `<input type='date' id='rate-date' value='${dateStr}'/><br/>
        <h4>Курси валют на ${showDate}</h4>
        <table class="table table-success table-striped"><thead><tr><th>Код</th><th>Назва</th><th>Курс (₴)</th></tr></thead><tbody>`;
        for(let r of j) {
            table += `<tr data-cc="${r.cc}"><td>${r.cc}</td><td>${r.txt}</td><td>${r.rate}</td></tr>`;
        }
        table += "</tbody></table>";
        const mainBlock = document.getElementById('main-block');
        mainBlock.innerHTML = table;
        for(let e of mainBlock.querySelectorAll('[data-cc]')) {
            e.onclick = rateClick;
        }
        mainBlock.querySelector('#rate-date').onchange = rateDateChange;
    });
}

function rateDateChange(e) {
    localStorage.setItem('rate-date', e.target.value);
    homePage();
}

function rateClick(e) {
    const cc = e.target.closest('[data-cc]').getAttribute('data-cc');
    console.log(cc);
    window.location.hash = "#rate/" + cc;
}