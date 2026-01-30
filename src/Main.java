void main()
{
    Device[] devices = new Device[3];
    Device d = new Device("device", 100.0);
    Phone p = new Phone("phone", 100.0, 200);
    Laptop l = new Laptop("laptop", 100.0, 300);

    devices[0] = d;
    devices[1] = p;
    devices[2] = l;

    for (Device device : devices)
    {
        device.showInfo();
    }
}