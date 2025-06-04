document.addEventListener('DOMContentLoaded', () => {
    const lights = document.querySelectorAll('.light');
    const btn = document.getElementById('switchBtn');
    let current = 0;

    btn.addEventListener('click', () => {
        lights[current].classList.remove('active');
        current = (current + 1) % lights.length;
        lights[current].classList.add('active');
    });
});