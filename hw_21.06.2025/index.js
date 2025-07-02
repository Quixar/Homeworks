document.addEventListener('DOMContentLoaded', () => {
    const currencyList = [
        { symbol: "$", name: "Dollar" },
        { symbol: "€", name: "Euro" },
        { symbol: "£", name: "Pound" },
        { symbol: "¥", name: "Yen" },
        { symbol: "₴", name: "Hryvnia" },
        { symbol: "₹", name: "Rupee" },
        { symbol: "₿", name: "Bitcoin" }
    ];

    const palette = ['tomato', 'seagreen', 'khaki', 'skyblue', 'mediumpurple', 'coral', 'hotpink', 'mediumturquoise'];
    const dragArea = document.getElementById('drag-area');
    const zoneA = document.getElementById('zone-a');
    const zoneB = document.getElementById('zone-b');
    const resultBox = document.getElementById('result-output');

    currencyList.forEach((el, i) => {
        const node = generateCurrencyBlock(el, palette[i % palette.length]);
        dragArea.insertBefore(node, zoneA);
    });

    arrangeItems(dragArea);
    setDragListeners();
    displayResults();

    function generateCurrencyBlock(data, color) {
        const block = document.createElement('div');
        block.className = `token ${color}`;
        block.textContent = data.symbol;
        block.dataset.name = data.name;
        block.dataset.symbol = data.symbol;
        return block;
    }

    function arrangeItems(container) {
        const blocks = Array.from(container.querySelectorAll('.token:not(.ghost)'));
        const fullWidth = container.offsetWidth;
        const blockSize = blocks[0]?.offsetWidth + 10 || 60;
        const perRow = Math.floor(fullWidth / blockSize);

        let row = 0, col = 0;
        blocks.forEach((el, idx) => {
            if (idx > 0 && idx % perRow === 0) {
                row++;
                col = 0;
            }
            el.style.left = `${10 + col * blockSize}px`;
            el.style.top = `${10 + row * blockSize}px`;
            col++;
        });
    }

    function setDragListeners() {
        document.querySelectorAll('.token').forEach(el => {
            el.onmousedown = startDrag;
        });

        document.onmousemove = dragMove;
        document.onmouseup = stopDrag;
        window.isDragging = false;
    }

    function startDrag(e) {
        if (!e.target.classList.contains('token')) return;
        e.preventDefault();

        window.originContainer = e.target.parentElement;
        window.itemToMove = e.target;

        const rect = e.target.getBoundingClientRect();
        window.offsetX = e.pageX - rect.left;
        window.offsetY = e.pageY - rect.top;
        window.isDragging = true;
    }

    function dragMove(e) {
        if (!window.isDragging) return;
        e.preventDefault();

        if (!window.dragShadow) {
            window.dragShadow = window.itemToMove.cloneNode(true);
            window.dragShadow.classList.add('ghost');
            window.dragShadow.style.opacity = 0.6;
            window.dragShadow.style.cursor = 'grabbing';
            document.body.appendChild(window.dragShadow);
        }

        window.dragShadow.style.left = `${e.pageX - window.offsetX}px`;
        window.dragShadow.style.top = `${e.pageY - window.offsetY}px`;
    }

    function stopDrag(e) {
        if (!window.isDragging) return;
        window.isDragging = false;

        if (window.dragShadow) {
            window.dragShadow.remove();
            window.dragShadow = null;
        }

        const boxA = zoneA.getBoundingClientRect();
        const boxB = zoneB.getBoundingClientRect();

        let targetZone = null;

        if (isInside(e, boxA)) {
            targetZone = zoneA;
        } else if (isInside(e, boxB)) {
            targetZone = zoneB;
        }

        if (targetZone) {
            targetZone.appendChild(window.itemToMove);
            arrangeItems(zoneA);
            arrangeItems(zoneB);
        } else {
            arrangeItems(window.itemToMove.parentElement);
        }

        displayResults();
    }

    function isInside(event, rect) {
        return (
            event.pageX >= rect.left &&
            event.pageX <= rect.right &&
            event.pageY >= rect.top &&
            event.pageY <= rect.bottom
        );
    }

    function displayResults() {
        const left = Array.from(zoneA.querySelectorAll('.token')).map(el => el.dataset.name);
        const right = Array.from(zoneB.querySelectorAll('.token')).map(el => el.dataset.name);
        const remaining = Array.from(dragArea.querySelectorAll('.token')).length;

        if (remaining === 0) {
            resultBox.innerHTML = `
                <div class="alert alert-success">
                    <strong>Підсумок:</strong><br>
                    [лівий блок: ${left.join(', ') || 'порожньо'}]<br>
                    [правий блок: ${right.join(', ') || 'порожньо'}]
                </div>
            `;
        } else {
            resultBox.innerHTML = `
                <div class="alert alert-warning"><strong>Зона A:</strong> ${left.join(', ') || 'порожньо'}</div>
                <div class="alert alert-info"><strong>Зона B:</strong> ${right.join(', ') || 'порожньо'}</div>
            `;
        }
    }
});