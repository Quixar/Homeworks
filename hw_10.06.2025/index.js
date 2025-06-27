document.getElementById('newColorForm').addEventListener('submit', function (e) {
    e.preventDefault();

    const titleInput = document.getElementById('colorTitle');
    const formatSelect = document.getElementById('formatSelect');
    const codeInput = document.getElementById('codeInput');
    const nameWarning = document.getElementById('titleError');
    const codeWarning = document.getElementById('codeError');
    const displayZone = document.getElementById('paletteDisplay');

    const colorName = titleInput.value.trim().toLowerCase();
    const format = formatSelect.value;
    const colorCode = codeInput.value.trim();

    nameWarning.textContent = '';
    codeWarning.textContent = '';

    const existing = Array.from(displayZone.getElementsByClassName('color-item')).map(el => el.dataset.name.toLowerCase());
    if (!/^[a-zA-Zа-яА-ЯіїєІЇЄ]+$/.test(colorName)) {
        nameWarning.textContent = 'Назва має складатися лише з літер';
        return;
    }

    if (existing.includes(colorName)) {
        nameWarning.textContent = 'Такий колір вже додано';
        return;
    }

    let valid = false;
    if (format === 'RGB') {
        const rgbCheck = /^(\d{1,3}),(\d{1,3}),(\d{1,3})$/;
        if (rgbCheck.test(colorCode)) {
            const [r, g, b] = colorCode.split(',').map(Number);
            valid = [r, g, b].every(v => v >= 0 && v <= 255);
        }
    } else if (format === 'RGBA') {
        const rgbaCheck = /^(\d{1,3}),(\d{1,3}),(\d{1,3}),(0(\.\d+)?|1(\.0+)?)$/;
        if (rgbaCheck.test(colorCode)) {
            const [r, g, b, a] = colorCode.split(',').map(Number);
            valid = r >= 0 && r <= 255 && g >= 0 && g <= 255 && b >= 0 && b <= 255 && a >= 0 && a <= 1;
        }
    } else if (format === 'HEX') {
        const hexCheck = /^#([A-Fa-f0-9]{6})$/;
        valid = hexCheck.test(colorCode);
    }

    if (!valid) {
        if (format === 'RGB') {
            codeWarning.textContent = 'Очікується RGB: 0-255,0-255,0-255';
        } else if (format === 'RGBA') {
            codeWarning.textContent = 'Очікується RGBA: 0-255,0-255,0-255,0-1';
        } else {
            codeWarning.textContent = 'HEX повинен мати формат #RRGGBB';
        }
        return;
    }

    const block = document.createElement('div');
    block.className = 'color-item';
    block.dataset.name = colorName;
    block.style.backgroundColor = format === 'HEX' ? colorCode : `${format.toLowerCase()}(${colorCode})`;
    block.innerHTML = `<h5 class="text-white text-shadow">${colorName.toUpperCase()}</h5><p class="text-white small">${format}<br>${colorCode}</p>`;

    displayZone.appendChild(block);
    this.reset();
});