using System;
using System.Runtime.InteropServices;
internal class Program
{
    // [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    // // public static extern int MessageBox(IntPtr hWnd, string text, string caption, int type);
    // public static extern bool GetComputerName([Out] char[] lpBuffer, ref uint nSize);
    // static void Main()
    // {
    //     // MessageBox(IntPtr.Zero, "Hello world", "Greeting", 0);
    //     // IntPtr consoleWidnow = GetConsoleWindow();

    //     // if(consoleWidnow != IntPtr.Zero)
    //     // {
    //     //     System.Console.WriteLine($"Дескриптор окна консоли {consoleWidnow}");
    //     // }
    //     // else
    //     // {
    //     //     System.Console.WriteLine("Не удалось получить");
    //     // }

    //     const int bufferSize = 256;
    //     char[] buffer = new char[bufferSize];
    //     uint size = (uint)bufferSize;

    //     if (GetComputerName(buffer, ref size))
    //     {
    //         string computerName = new string(buffer, 0, size);
    //         System.Console.WriteLine($"Имя компьютера {computerName}");
    //     }
    //     else
    //     {
    //         System.Console.WriteLine("Не удалось");
    //     }
    // }

    // [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    // public static extern bool Beep(int hz, int duration);
    // static void Main()
    // {
    //     try
    //     {
    //         Beep(432, 10000);
    //     }
    //     catch (Exception e)
    //     {
    //         if (!Beep(1000, 1000))
    //         {
    //             int errorCode = Marshal.GetLastWin32Error();
    //             throw new InvalidOperationException($"Не вдалося подати звуковий сигнал з кодом помилки {errorCode}.");
    //         }
    //     }
    // }

    // [DllImport("kernel32.dll")]
    // private static extern void Sleep(uint ms);

    // static void Main()
    // {
    //     System.Console.WriteLine("Ожидание");
    //     Sleep(1000);
    //     System.Console.WriteLine("Выполнено");
    // }

    [DllImport("kernel32.dll")]
    private static extern uint GetTickCount();
    static void Main()
    {
        uint ms = GetTickCount();
        for (int i = 0; i < 1000000; i++)
        {
            ms = GetTickCount();
        }
        System.Console.WriteLine($"Tick count: {ms}");
    }
}