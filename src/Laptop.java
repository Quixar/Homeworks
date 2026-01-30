public class Laptop extends Device
{
    private int ram;

    public Laptop(String brand, double price, int ram) {
        super(brand, price);
        this.ram = ram;
    }

    @Override
    public void showInfo() {
        super.showInfo();
        System.out.println("ram: " + this.ram);
    }
}
