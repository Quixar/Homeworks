public enum OrderStatus {
    NEW(false),
    PROCESSING(false),
    SHIPPED(false),
    DELIVERED(true),
    CANCELLED(true);

    private final boolean isFinal;

    OrderStatus(boolean isFinal) {
        this.isFinal = isFinal;
    }

    public boolean isFinal() {
        return isFinal;
    }
}