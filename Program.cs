using System;
using System.Security.Cryptography;

abstract class Storage
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; } 

    public Storage(string name, string manufacturer, string model, int quantity, double price) 
    {
        Name = name;
        Manufacturer = manufacturer;
        Model = model;
        Quantity = quantity;
        Price = price;
    }

    public abstract void Print();
    public abstract void LoadFile();
    public abstract void SaveFile();

    public override string ToString()
    {
        return "Name: " + Name + "Manufacturer: " + Manufacturer + "Model: " + Model + "Quantity: " + Quantity + "Price: " + Price;
    }
}

class Flash : Storage
{
    public int Capacity { get; set; }
    public int UsbSpeed { get; set; }

    public Flash(string name, string manufacturer, string model, int quantity, double price, int capacity, int usbSpeed) 
    : base(name, manufacturer, model, quantity, price)
    {
        Capacity = capacity;
        UsbSpeed = usbSpeed;
    }

    public override void Print()
    {
        Console.WriteLine("Name: " + Name + ", Manufacturer: " + Manufacturer + ", Model: " + Model + ", Capacity: " + Capacity + "GB, USB Speed: " + UsbSpeed + "MB/s, Quantity: " + Quantity + ", Price: " + Price);
    }


    public override void LoadFile()
    {
        System.Console.WriteLine("Loading flash data from file...");
    }

    public override void SaveFile()
    {
        System.Console.WriteLine("Saving flash data to file...");
    }

    public override string ToString()
    {
        return base.ToString() + "Capacity: " +Capacity + "USB Speed: " + UsbSpeed;
    }
}

class HDD : Storage
{
    public int DiskSize{ get; set; }
    public int UsbSpeed{ get; set; }

    public HDD(string name, string manufacturer, string model, int quantity, double price, int diskSize, int usbSpeed) 
    : base(name, manufacturer, model, quantity, price)
    {
        DiskSize = diskSize;
        UsbSpeed = usbSpeed;
    }

    public override void Print()
    {
        // Исправление: Используем конкатенацию строк с оператором +
        Console.WriteLine("Name: " + Name + ", Manufacturer: " + Manufacturer + ", Model: " + Model + ", Disk Size: " + DiskSize + "GB, USB Speed: " + UsbSpeed + "MB/s, Quantity: " + Quantity + ", Price: " + Price);
    } 

    public override void LoadFile()
    {
        System.Console.WriteLine("Loading HDD data from file...");
    }

    public override void SaveFile()
    {
        System.Console.WriteLine("Saving HDD data to file...");
    }

    public override string ToString()
    {
        return base.ToString() + "Disk Size: " + DiskSize + "USB Speed: " + UsbSpeed;
    }
}

class DVD : Storage
{
    public int ReadSpeed { get; set; }
    public int WriteSpeed { get; set; }

    public DVD(string name, string manufacturer, string model, int quantity, double price, int readSpeed, int writeSpeed) 
    : base(name, manufacturer, model, quantity, price)
    {
        ReadSpeed = readSpeed;
        WriteSpeed = writeSpeed;
    }

    public override void Print()
    {
        // Исправление: Используем конкатенацию строк с оператором +
        Console.WriteLine("Name: " + Name + ", Manufacturer: " + Manufacturer + ", Model: " + Model + ", Read Speed: " + ReadSpeed + "x, Write Speed: " + WriteSpeed + "x, Quantity: " + Quantity + ", Price: " + Price);
    } 

    public override void LoadFile()
    {
        System.Console.WriteLine("Loading DVD data from file...");
    }

    public override void SaveFile()
    {
        System.Console.WriteLine("Saving DVD data to file...");
    }
    
    public override string ToString()
    {
        return base.ToString() + "Read Speed: " + ReadSpeed + "Write Speed: " + WriteSpeed;
    }
}

class Program
{
    static List<Storage> storages= new List<Storage>();

    static void PrintStorages()
    {
        foreach(var storage in storages)
        {
            storage.Print();
        }
    }

    static void SearchByName(string name)
    {
        foreach (var storage in storages)
        {
            if (storage.Name == name)
            {
                storage.Print();
            }
        }
    }

    static void RemoveByModel(string name)
    {
        storages.RemoveAll(storage => storage.Model == name);
    }

    static void AddStorage(Storage storage)
    {
        storages.Add(storage);
    }

    static void Main(string[] args)
    {
        Console.Clear();
        Flash flash = new Flash("USB Flash Drive", "SanDisk", "Ultra", 10, 1200, 64, 100);

        DVD dvd = new DVD("DVD-R", "Sony", "Premium", 50, 25, 16, 8);

        HDD hdd = new HDD("External HDD", "Seagate", "Expansion", 5, 4500, 2000, 120);

        AddStorage(flash);
        AddStorage(hdd);
        AddStorage(dvd);

        PrintStorages();

        SearchByName("USB Flash Drive");

        RemoveByModel("Expansion");

        PrintStorages();
    }
}