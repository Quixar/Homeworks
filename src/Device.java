public class Device
{
    public Device(String brand, double price) {
        this.brand = brand;
        this.price = price;
    }

    public void showInfo()
    {
        System.out.println("Brand: " + this.brand);
        System.out.println("Price: " + this.price);
    }

    protected String brand;
    protected double price;

    public String getBrand() {
        return brand;
    }

    public void setBrand(String brand) {
        this.brand = brand;
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        this.price = price;
    }
}
