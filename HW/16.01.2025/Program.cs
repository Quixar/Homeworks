using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            string app = "terminal";
            Process process = new Process();
            
            process.StartInfo.FileName = "open";
            process.StartInfo.Arguments = $"-a {app}";

            process.Start();

            process.WaitForExit();

            int exitCode = process.ExitCode;
            System.Console.WriteLine($"Process has been ended. Exit code {exitCode}");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error {ex.Message}");
        }

    }
}