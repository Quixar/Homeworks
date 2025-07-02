document.addEventListener('DOMContentLoaded', () => {
  const canvas = document.getElementById('canvasGame');
  const ctx = canvas.getContext('2d');

  const screenEnd = document.getElementById('screenEnd');
  const btnPlay = document.getElementById('btnPlay');
  const btnRetry = document.getElementById('btnRetry');
  const btnPause = document.getElementById('btnPause');

  const pointCounter = document.getElementById('pointCounter');
  const lifeCounter = document.getElementById('lifeCounter');
  const scoreFinal = document.getElementById('scoreFinal');

  canvas.width = 800;
  canvas.height = 600;

  let points = 0;
  let lives = 3;
  let active = false;
  let paused = false;
  let animation;

  const platform = {
    w: 100,
    h: 15,
    x: canvas.width / 2 - 50,
    y: canvas.height - 25,
    velocity: 8,
    dx: 0
  };

  const ball = {
    radius: 10,
    x: canvas.width / 2,
    y: canvas.height - 60,
    velocity: 4,
    dx: 3 * (Math.random() > 0.5 ? 1 : -1),
    dy: -3
  };

  const rows = 5;
  const cols = 9;
  const brickW = 75;
  const brickH = 20;
  const space = 15;
  const offsetX = 35;
  const offsetY = 60;

  const grid = [];
  for (let r = 0; r < rows; r++) {
    grid[r] = [];
    for (let c = 0; c < cols; c++) {
      grid[r][c] = { x: 0, y: 0, hit: true, color: randomColor() };
    }
  }

  function randomColor() {
    const palette = ['#FF5252', '#FF4081', '#E040FB', '#7C4DFF', '#536DFE', '#448AFF', '#40C4FF', '#18FFFF', '#64FFDA', '#69F0AE', '#B2FF59', '#EEFF41', '#FFFF00', '#FFD740', '#FFAB40', '#FF6E40'];
    return palette[Math.floor(Math.random() * palette.length)];
  }

  function renderPaddle() {
    ctx.fillStyle = '#0d6efd';
    ctx.fillRect(platform.x, platform.y, platform.w, platform.h);
  }

  function renderBall() {
    ctx.beginPath();
    ctx.arc(ball.x, ball.y, ball.radius, 0, Math.PI * 2);
    ctx.fillStyle = '#dc3545';
    ctx.fill();
    ctx.closePath();
  }

  function renderBricks() {
    for (let r = 0; r < rows; r++) {
      for (let c = 0; c < cols; c++) {
        const b = grid[r][c];
        if (b.hit) {
          const bx = c * (brickW + space) + offsetX;
          const by = r * (brickH + space) + offsetY;
          b.x = bx;
          b.y = by;

          ctx.fillStyle = b.color;
          ctx.fillRect(bx, by, brickW, brickH);
        }
      }
    }
  }

  function movePaddle() {
    platform.x += platform.dx;
    platform.x = Math.max(0, Math.min(platform.x, canvas.width - platform.w));
  }

  function moveBall() {
    ball.x += ball.dx;
    ball.y += ball.dy;

    if (ball.x - ball.radius < 0 || ball.x + ball.radius > canvas.width) ball.dx *= -1;
    if (ball.y - ball.radius < 0) ball.dy *= -1;

    if (
      ball.y + ball.radius > platform.y &&
      ball.x > platform.x &&
      ball.x < platform.x + platform.w
    ) {
      ball.dy = -ball.velocity;
      const offset = (ball.x - (platform.x + platform.w / 2)) / (platform.w / 2);
      ball.dx = offset * ball.velocity;
    }

    if (ball.y + ball.radius > canvas.height) {
      lives--;
      lifeCounter.textContent = lives;
      if (lives <= 0) endGame();
      else resetBall();
    }
  }

  function checkCollisions() {
    for (let r = 0; r < rows; r++) {
      for (let c = 0; c < cols; c++) {
        const b = grid[r][c];
        if (b.hit) {
          if (
            ball.x > b.x &&
            ball.x < b.x + brickW &&
            ball.y > b.y &&
            ball.y < b.y + brickH
          ) {
            ball.dy *= -1;
            b.hit = false;
            points += 10;
            pointCounter.textContent = points;
            if (isWin()) winGame();
          }
        }
      }
    }
  }

  function isWin() {
    return grid.every(row => row.every(b => !b.hit));
  }

  function resetBall() {
    ball.x = canvas.width / 2;
    ball.y = canvas.height - 60;
    ball.dx = 3 * (Math.random() > 0.5 ? 1 : -1);
    ball.dy = -3;
  }

  function endGame() {
    active = false;
    cancelAnimationFrame(animation);
    screenEnd.classList.remove('d-none');
    scoreFinal.textContent = `Результат: ${points}`;
  }

  function winGame() {
    alert(`Молодець! Ви набрали ${points} балів!`);
    restartSession();
  }

  function restartSession() {
    points = 0;
    lives = 3;
    pointCounter.textContent = points;
    lifeCounter.textContent = lives;
    resetBall();
    platform.x = canvas.width / 2 - platform.w / 2;

    for (let r = 0; r < rows; r++) {
      for (let c = 0; c < cols; c++) {
        grid[r][c].hit = true;
        grid[r][c].color = randomColor();
      }
    }
  }

  function toggleGamePause() {
    paused = !paused;
    btnPause.textContent = paused ? 'Продовжити' : 'Зупинити';
    if (!paused && active) animate();
  }

  function animate() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    renderBricks();
    renderPaddle();
    renderBall();
    movePaddle();
    moveBall();
    checkCollisions();

    if (active && !paused) animation = requestAnimationFrame(animate);
  }

  function launchGame() {
    screenEnd.classList.add('d-none');
    btnPause.classList.remove('d-none');
    active = true;
    paused = false;
    restartSession();
    animate();
  }

  function keyPressed(e) {
    if (e.key === 'ArrowRight') platform.dx = platform.velocity;
    if (e.key === 'ArrowLeft') platform.dx = -platform.velocity;
    if (e.key === ' ' && active) toggleGamePause();
  }

  function keyReleased(e) {
    if (e.key === 'ArrowRight' || e.key === 'ArrowLeft') platform.dx = 0;
  }

  document.addEventListener('keydown', keyPressed);
  document.addEventListener('keyup', keyReleased);
  btnPlay.addEventListener('click', launchGame);
  btnRetry.addEventListener('click', launchGame);
  btnPause.addEventListener('click', toggleGamePause);
});