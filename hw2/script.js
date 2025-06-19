// 1

const shoppingList = [
  { name: "Milk", quantity: 2, bought: false },
  { name: "Bread", quantity: 1, bought: true },
  { name: "Eggs", quantity: 12, bought: false },
];

function displayShoppingList(list) {
  const notBought = list.filter(item => !item.bought); 
  const bought = list.filter(item => item.bought);     

  console.log("Shopping list:");
  notBought.forEach(item => {
    console.log(`- ${item.name}, quantity: ${item.quantity} (not bought)`);
  });
  bought.forEach(item => {
    console.log(`- ${item.name}, quantity: ${item.quantity} (bought)`);
  });
}

function addPurchase(list, productName, quantity) {
  const item = list.find(i => i.name.toLowerCase() === productName.toLowerCase());
  if (item) {
    item.quantity += quantity;
  } else {
    list.push({ name: productName, quantity, bought: false });
  }
}

function buyProduct(list, productName) {
  const item = list.find(i => i.name.toLowerCase() === productName.toLowerCase());
  if (item) {
    item.bought = true;
  } else {
    console.log(`Product "${productName}" not found in the list.`);
  }
}

// 2

const receipt = [
  { name: "Milk", quantity: 2, pricePerUnit: 1.2 },
  { name: "Bread", quantity: 1, pricePerUnit: 1.0 },
  { name: "Eggs", quantity: 12, pricePerUnit: 0.1 },
];

function displayReceipt(receipt) {
  console.log("Receipt:");
  receipt.forEach(item => {
    console.log(`- ${item.name}, quantity: ${item.quantity}, unit price: $${item.pricePerUnit.toFixed(2)}`);
  });
}

function totalAmount(receipt) {
  return receipt.reduce((sum, item) => sum + item.quantity * item.pricePerUnit, 0);
}

function mostExpensivePurchase(receipt) {
  let maxItem = receipt[0];
  for (const item of receipt) {
    if (item.quantity * item.pricePerUnit > maxItem.quantity * maxItem.pricePerUnit) {
      maxItem = item;
    }
  }
  return maxItem;
}

function averageCostPerItem(receipt) {
  const totalQuantity = receipt.reduce((sum, item) => sum + item.quantity, 0);
  if (totalQuantity === 0) return 0;
  const total = totalAmount(receipt);
  return total / totalQuantity;
}

// 3

const cssStyles = [
  { name: "color", value: "red" },
  { name: "font-size", value: "20px" },
  { name: "text-align", value: "center" },
  { name: "text-decoration", value: "underline" },
];

function writeStyledText(styles, text) {
  const styleString = styles.map(s => `${s.name}: ${s.value}`).join("; ");
  const p = document.createElement("p");
  p.style.cssText = styleString;
  p.textContent = text;
  document.body.appendChild(p);
}

// 4

const auditoriums = [
  { name: "Auditorium 101", seats: 15, faculty: "Mathematics" },
  { name: "Auditorium 102", seats: 20, faculty: "Physics" },
  { name: "Auditorium 103", seats: 12, faculty: "Mathematics" },
];

function displayAuditoriums(list) {
  console.log("Auditoriums:");
  list.forEach(aud => {
    console.log(`- ${aud.name}, seats: ${aud.seats}, faculty: ${aud.faculty}`);
  });
}

function displayAuditoriumsByFaculty(list, facultyName) {
  const filtered = list.filter(aud => aud.faculty.toLowerCase() === facultyName.toLowerCase());
  displayAuditoriums(filtered);
}

function displayAuditoriumsForGroup(list, group) {
  const suitable = list.filter(aud =>
    aud.faculty.toLowerCase() === group.faculty.toLowerCase() &&
    aud.seats >= group.studentsCount
  );
  displayAuditoriums(suitable);
}

function sortAuditoriumsBySeats(list) {
  return [...list].sort((a, b) => a.seats - b.seats);
}

function sortAuditoriumsByName(list) {
  return [...list].sort((a, b) => a.name.localeCompare(b.name));
}

