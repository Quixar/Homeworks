void main()
{
    Order order = new Order("asdf");

    order.addItem("asdf", 2, 100.0);
    order.addItem("asdf", 2, 200.0);

    order.validateOrder();

    order.setStatus(OrderStatus.PROCESSING);
    order.setStatus(OrderStatus.SHIPPED);
    order.setStatus(OrderStatus.DELIVERED);
    order.setStatus(OrderStatus.CANCELLED);
}