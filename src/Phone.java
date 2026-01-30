public class Phone extends Device
{
 private int cameraResolution;


    public Phone(String brand, double price, int cameraResolution) {
        super(brand, price);
        this.cameraResolution = cameraResolution;
    }

    @Override
    public void showInfo() {
        super.showInfo();
        System.out.println("cameraResolution: " + this.cameraResolution);
    }
}
