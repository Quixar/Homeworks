function getTimeString() {
  const now = new Date();
  return now.toLocaleTimeString('uk-UA', { hour12: false });
}

document.getElementById('start').onclick = async function () {
  const delay = Number(document.getElementById('delay').value);
  const result = document.querySelector('input[name="result"]:checked').value;
  const log = document.getElementById('log');
  log.textContent = `${getTimeString()} виклик`;

  await new Promise(resolve => setTimeout(resolve, delay));

  if (result === 'success') {
    log.textContent += `\n${getTimeString()} завершено успішно`;
  } else {
    log.textContent += `\n${getTimeString()} завершено з помилкою`;
  }
};