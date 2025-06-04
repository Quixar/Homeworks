document.addEventListener('DOMContentLoaded', () => {
    const ball = document.querySelector('.ball');
    const ballSize = 100; 

    document.body.addEventListener('click', (e) => {
        if (e.target.classList.contains('ball')) return;

        let x = e.clientX;
        let y = e.clientY;

        let left = x - ballSize / 2;
        let top = y - ballSize / 2;

        const minLeft = 0;
        const minTop = 0;
        const maxLeft = window.innerWidth - ballSize;
        const maxTop = window.innerHeight - ballSize;

        left = Math.max(minLeft, Math.min(left, maxLeft));
        top = Math.max(minTop, Math.min(top, maxTop));

        ball.style.left = `${left}px`;
        ball.style.top = `${top}px`;
    });
});