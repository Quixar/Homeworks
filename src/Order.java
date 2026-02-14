import java.util.ArrayList;
import java.util.List;

public class Order {
    private String orderId;
    private List<OrderDetails> items = new ArrayList<>();
    private OrderStatus status;
    private OrderLogger logger;

    public Order(String orderId) {
        this.orderId = orderId;
        this.status = OrderStatus.NEW;
        this.logger = new OrderLogger();
    }

    public static class OrderDetails {
        private String itemName;
        private int quantity;
        private double price;

        public OrderDetails(String itemName, int quantity, double price) {
            this.itemName = itemName;
            this.quantity = quantity;
            this.price = price;
        }

        public double getTotalPrice() {
            return quantity * price;
        }

        @Override
        public String toString() {
            return itemName + " (x" + quantity + ") - " + getTotalPrice() + " USD";
        }
    }

    public class OrderLogger {
        public void logStatusChange(OrderStatus oldStatus, OrderStatus newStatus) {
            System.out.println("[LOG] Order " + orderId + ": status changed from "
                    + oldStatus + " to " + newStatus);
        }
    }

    public void addItem(String name, int qty, double price) {
        if (status.isFinal()) {
            System.out.println("Error");
            return;
        }
        items.add(new OrderDetails(name, qty, price));
    }

    public void setStatus(OrderStatus newStatus) {
        if (this.status.isFinal()) {
            System.out.println("Error: Current status " + this.status + " is final");
            return;
        }
        OrderStatus oldStatus = this.status;
        this.status = newStatus;
        logger.logStatusChange(oldStatus, newStatus);
    }

    public double applyDiscount() {
        double total = items.stream().mapToDouble(OrderDetails::getTotalPrice).sum();

        class DiscountCalculator {
            double calculate(double amount) {
                return amount > 1000 ? amount * 0.10 : 0;
            }
        }

        DiscountCalculator calc = new DiscountCalculator();
        double discount = calc.calculate(total);
        System.out.println("Discount applied: " + discount + " USD");
        return total - discount;
    }

    public void validateOrder() {
        interface Validator {
            boolean isValid();
        }

        Validator orderValidator = new Validator() {
            @Override
            public boolean isValid() {
                if (items.isEmpty()) return false;
                for (OrderDetails item : items) {
                    if (item.itemName == null || item.itemName.isEmpty() || item.quantity <= 0) {
                        return false;
                    }
                }
                return true;
            }
        };

        if (orderValidator.isValid()) {
            System.out.println("Validation successful");
        } else {
            System.out.println("Validation failed");
        }
    }
}