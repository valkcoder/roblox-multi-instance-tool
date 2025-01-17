using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
class Program
{
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr OpenMutex(uint dwDesiredAccess, bool bInheritHandle, string lpName);
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CloseHandle(IntPtr hObject);
    const uint MUTEX_ALL_ACCESS = 0x1F0001;
    static void Main(string[] args)
    {
        string mutexName = "ROBLOX_singletonEvent";
        string startMsg = @"
                  _ _   _       _           _                                            _     _           
                 | | | (_)     (_)         | |                                          | |   | |          
  _ __ ___  _   _| | |_ _       _ _ __  ___| |_ __ _ _ __   ___ ___      ___ _ __   __ _| |__ | | ___ _ __ 
 | '_ ` _ \| | | | | __| |     | | '_ \/ __| __/ _` | '_ \ / __/ _ \    / _ \ '_ \ / _` | '_ \| |/ _ \ '__|
 | | | | | | |_| | | |_| |     | | | | \__ \ || (_| | | | | (_|  __/   |  __/ | | | (_| | |_) | |  __/ |   
 |_| |_| |_|\__,_|_|\__|_|     |_|_| |_|___/\__\__,_|_| |_|\___\___|    \___|_| |_|\__,_|_.__/|_|\___|_|   
                                                                                                           
                                                                                                           ";
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine(startMsg);
        Console.ResetColor();
        // Check if Roblox is running
        if (IsRobloxRunning())
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("roblox is currently running, u wanna close all processes of it? (it won't work if roblox is already opened) (y/n)");
            Console.ResetColor();
            string response = Console.ReadLine()?.Trim().ToLower();
            if (response == "y")
            {
                CloseRobloxProcesses();
            }
            else if (response == "n")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("exiting..");
                Console.ResetColor();
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("invalid input, i'll assume that means yes");
                Console.ResetColor();
                CloseRobloxProcesses();
            }
        }
        IntPtr handle = OpenMutex(MUTEX_ALL_ACCESS, false, mutexName);
        if (handle != IntPtr.Zero)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("invalid mutex (i actually don't know what causes this tbh)");
            Console.ResetColor();
            CloseHandle(handle);
            return;
        }
        try
        {
            // Create the Mutex
            using (Mutex mutex = new Mutex(true, mutexName, out bool isNew))
            {
                if (isNew)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("enabled");
                    Console.ResetColor();
                    while (true)
                    {
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("mutex already exists (program is already running)");
                    Console.ResetColor();
                    Console.ReadLine();
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("access denied (try running the file as admin)");
            Console.ResetColor();
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            Console.ReadLine();
        }
        Console.ReadLine();
    }
    static bool IsRobloxRunning()
    {
        Process[] robloxProcesses = Process.GetProcessesByName("RobloxPlayerBeta");
        return robloxProcesses.Length > 0;
    }
    static void CloseRobloxProcesses()
    {
        Process[] robloxProcesses = Process.GetProcessesByName("RobloxPlayerBeta");
        foreach (var process in robloxProcesses)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"closing process: {process.ProcessName} (process id: {process.Id})");
                Console.ResetColor();
                process.Kill();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"failed to close process {process.ProcessName} (process id: {process.Id}): {ex.Message}");
                Console.ResetColor();
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("all processes closed");
        Console.ResetColor();
    }
}