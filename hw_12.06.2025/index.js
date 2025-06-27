document.addEventListener('DOMContentLoaded', () => {
    const board = document.getElementById('drawingBoard');
    const context = board.getContext('2d');

    let currentTool = 'square';
    let currentColor = 'black';
    const toolSize = 40;
    let drawing = false;
    let startX = 0;
    let startY = 0;

    document.querySelectorAll('.tool-btn').forEach(button => {
        button.addEventListener('click', () => {
            currentTool = button.dataset.tool;
            document.querySelectorAll('.tool-btn').forEach(b => b.classList.remove('active'));
            button.classList.add('active');
        });
    });

    document.querySelectorAll('.color-swatch').forEach(button => {
        button.addEventListener('click', () => {
            currentColor = button.style.backgroundColor;
            document.querySelectorAll('.color-swatch').forEach(b => b.classList.remove('active'));
            button.classList.add('active');
        });
    });

    document.getElementById('resetCanvas').addEventListener('click', () => {
        context.clearRect(0, 0, board.width, board.height);
    });

    board.addEventListener('click', (e) => {
        if (currentTool !== 'pen') {
            const pos = board.getBoundingClientRect();
            const x = e.clientX - pos.left;
            const y = e.clientY - pos.top;
            renderShape(x, y, currentTool, currentColor);
        }
    });

    board.addEventListener('mousedown', (e) => {
        drawing = true;
        const bounds = board.getBoundingClientRect();
        startX = e.clientX - bounds.left;
        startY = e.clientY - bounds.top;

        if (currentTool === 'pen') {
            context.beginPath();
            context.moveTo(startX, startY);
            context.strokeStyle = currentColor;
            context.lineWidth = 2;
        }
    });

    board.addEventListener('mousemove', (e) => {
        if (!drawing) return;

        const bounds = board.getBoundingClientRect();
        const x = e.clientX - bounds.left;
        const y = e.clientY - bounds.top;

        if (currentTool === 'pen') {
            context.lineTo(x, y);
            context.stroke();
        }
    });

    board.addEventListener('mouseup', () => {
        drawing = false;
    });

    board.addEventListener('mouseleave', () => {
        drawing = false;
    });

    function renderShape(x, y, shape, color, x2 = x + toolSize, y2 = y + toolSize) {
        context.fillStyle = color;
        context.beginPath();

        switch (shape) {
            case 'square':
                context.fillRect(x - toolSize / 2, y - toolSize / 2, toolSize, toolSize);
                break;
            case 'circle':
                context.arc(x, y, toolSize / 2, 0, Math.PI * 2);
                context.fill();
                break;
            case 'diamond':
                context.moveTo(x, y - toolSize / 2);
                context.lineTo(x + toolSize / 2, y);
                context.lineTo(x, y + toolSize / 2);
                context.lineTo(x - toolSize / 2, y);
                context.closePath();
                context.fill();
                break;
            case 'triangle':
                context.moveTo(x, y - toolSize / 2);
                context.lineTo(x + toolSize / 2, y + toolSize / 2);
                context.lineTo(x - toolSize / 2, y + toolSize / 2);
                context.closePath();
                context.fill();
                break;
            case 'house':
                context.fillRect(x - toolSize / 2, y - toolSize / 4, toolSize, toolSize / 2);
                context.moveTo(x - toolSize / 2, y - toolSize / 4);
                context.lineTo(x, y - toolSize / 2);
                context.lineTo(x + toolSize / 2, y - toolSize / 4);
                context.closePath();
                context.fill();
                break;
        }
    }
});