using System.Runtime.InteropServices;

internal class Program
{
    [DllImport("user32.dll")]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, int type);
    private static void Main(string[] args)
    {
        MessageBox(IntPtr.Zero, "Yevhenii", "Name", 0);
        MessageBox(IntPtr.Zero, "Martynov", "Surname", 0);
        MessageBox(IntPtr.Zero, "18", "Age", 0);
        MessageBox(IntPtr.Zero, "Student", "Employment", 0);
    }
}