// Задание 1
class Marker {
    constructor(color, ink) {
        this.color = color;
        this.ink = ink; 
    }

    print(text) {
        let printed = '';
        for (let char of text) {
            if (char !== ' ' && this.ink >= 0.5) {
                printed += char;
                this.ink -= 0.5;
            } else if (char === ' ') {
                printed += char;
            } else {
                break;
            }
        }
        const output = `<span style="color:${this.color}">${printed}</span>`;
        document.getElementById('marker-output').innerHTML = output;
    }
}

class RefillableMarker extends Marker {
    refill(amount) {
        this.ink = Math.min(100, this.ink + amount);
    }
}

const marker = new Marker('blue', 10);
marker.print('Hello World! Маркер тест'); 

const refillable = new RefillableMarker('green', 2);
refillable.print('Refillable marker test!');
refillable.refill(10);
refillable.print(' После заправки!');

// Задание 2
class ExtendedDate extends Date {
    getTextDate() {
        const months = [
            'січня', 'лютого', 'березня', 'квітня', 'травня', 'червня',
            'липня', 'серпня', 'вересня', 'жовтня', 'листопада', 'грудня'
        ];
        return `${this.getDate()} ${months[this.getMonth()]}`;
    }

    isFuture() {
        const now = new Date();
        return this >= new Date(now.getFullYear(), now.getMonth(), now.getDate());
    }

    isLeapYear() {
        const year = this.getFullYear();
        return (year % 4 === 0 && year % 100 !== 0) || (year % 400 === 0);
    }

    getNextDate() {
        const next = new Date(this);
        next.setDate(this.getDate() + 1);
        return next;
    }
}

const extDate = new ExtendedDate(2025, 1, 28); 
document.getElementById('date-output').innerHTML =
    `Текстовая дата: ${extDate.getTextDate()}<br>
    Будущее/настоящее: ${extDate.isFuture()}<br>
    Високосный: ${extDate.isLeapYear()}<br>
    Следующая дата: ${extDate.getNextDate().toLocaleDateString()}`;

// Задание 3
class Employee {
    constructor(name, position, salary) {
        this.name = name;
        this.position = position;
        this.salary = salary;
    }
}

class EmpTable {
    constructor(employees) {
        this.employees = employees;
    }

    getHtml() {
        let html = '<table><tr><th>Имя</th><th>Должность</th><th>Зарплата</th></tr>';
        for (const emp of this.employees) {
            html += `<tr><td>${emp.name}</td><td>${emp.position}</td><td>${emp.salary}</td></tr>`;
        }
        html += '</table>';
        return html;
    }
}

// Демонстрация EmpTable
const employees = [
    new Employee('Иван', 'Кассир', 12000),
    new Employee('Мария', 'Менеджер', 18000),
    new Employee('Петр', 'Охранник', 10000)
];
const empTable = new EmpTable(employees);
document.getElementById('table-output').innerHTML = empTable.getHtml();

// Задание 4
class StyledEmpTable extends EmpTable {
    getStyles() {
        return `<style>
            table { border-collapse: collapse; width: 60%; margin: 10px 0;}
            th, td { border: 1px solid #333; padding: 8px 12px; text-align: left;}
            th { background: #e0e0e0; }
            tr:nth-child(even) { background: #f9f9f9; }
        </style>`;
    }

    getHtml() {
        return this.getStyles() + super.getHtml();
    }
}

const styledTable = new StyledEmpTable(employees);
document.getElementById('styled-table-output').innerHTML = styledTable.getHtml();