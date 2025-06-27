document.addEventListener('DOMContentLoaded', () => {
    const addButton = document.getElementById('add-new-task');
    if (!addButton) throw new Error("Кнопка 'add-new-task' не знайдена");
    addButton.addEventListener('click', handleAddTask);

    const exportButton = document.getElementById('export-tasks');
    if (!exportButton) throw new Error("Кнопка 'export-tasks' не знайдена");
    exportButton.addEventListener('click', handleExport);

    const taskContainer = document.getElementById('task-list');
    if (!taskContainer) throw new Error("Елемент 'task-list' не знайдено");

    window.taskListRef = taskContainer;

    attachActionHandlers();
});

function attachActionHandlers() {
    const actions = {
        'insert': handleInsert,
        'delete': handleDelete,
        'move-up': handleMoveUp,
        'move-down': handleMoveDown,
        'edit': handleEdit
    };

    for (const [action, handler] of Object.entries(actions)) {
        document.querySelectorAll(`[data-action="${action}"]`)
            .forEach(btn => btn.onclick = handler);
    }
}

function handleDelete(event) {
    const item = event.target.closest('li');
    if (item) {
        window.taskListRef.removeChild(item);
        attachActionHandlers();
    }
}

function handleInsert(event) {
    const current = event.target.closest('li');
    const content = prompt("Введіть нову задачу:", "Нова задача");
    if (!content?.trim()) return;

    const copy = current.cloneNode(true);
    copy.querySelector('span').innerText = content;
    window.taskListRef.insertBefore(copy, current);
    attachActionHandlers();
}

function handleMoveUp(event) {
    const current = event.target.closest('li');
    const prev = current?.previousElementSibling;
    if (prev) {
        window.taskListRef.insertBefore(current, prev);
    }
}

function handleMoveDown(event) {
    const current = event.target.closest('li');
    const next = current?.nextElementSibling;
    if (next) {
        window.taskListRef.insertBefore(next, current);
    }
}

function handleEdit(event) {
    const item = event.target.closest('li');
    const textEl = item.querySelector('span');
    const updatedText = prompt("Змініть текст:", textEl.innerText);
    if (updatedText?.trim()) {
        textEl.innerText = updatedText;
    }
}

function handleExport() {
    const taskElements = document.querySelectorAll('#task-list li span');
    const output = Array.from(taskElements)
        .map(el => el.innerText)
        .join('\n');

    const resultBox = document.createElement('div');
    resultBox.style.cssText = `
        border: 1px solid #ccc;
        padding: 12px;
        margin: 15px;
        background-color: #f9f9f9;
        white-space: pre-wrap;
        box-shadow: 2px 2px 8px rgba(0,0,0,0.1);
    `;

    resultBox.innerText = output;

    document.querySelectorAll('div[style*="border"]').forEach(el => el.remove());
    document.body.appendChild(resultBox);

    console.log(output);
}

function handleAddTask() {
    const taskContent = prompt("Введіть нову задачу:", "Нова задача");
    if (!taskContent?.trim()) return;

    const baseItem = window.taskListRef.querySelector('li');
    if (!baseItem) throw new Error("Шаблон задачі не знайдено");

    const clone = baseItem.cloneNode(true);
    clone.querySelector('span').innerText = taskContent;
    window.taskListRef.appendChild(clone);

    attachActionHandlers();
}