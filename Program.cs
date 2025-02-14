using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern IntPtr OpenEvent(uint dwDesiredAccess, bool bInheritHandle, string lpName);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);

    const uint EVENT_MODIFY_STATE = 0x0002;
    const uint SYNCHRONIZE = 0x00100000;

    private static IntPtr _handle = IntPtr.Zero;

    static void Main(string[] args)
    {
        string eventName = "ROBLOX_singletonEvent";
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

        if (IsRobloxRunning())
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("roblox is running, opening singletonEvent..");
            Console.ResetColor();

            _handle = OpenEvent(EVENT_MODIFY_STATE | SYNCHRONIZE, false, eventName);
            if (_handle != IntPtr.Zero)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("running");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("failed to open mutex");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("roblox isn't running, creating mutex..");
            Console.ResetColor();

            _handle = CreateMutex(IntPtr.Zero, true, eventName);
            if (_handle != IntPtr.Zero)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("running");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("failed to create mutex");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }
        }

        while (true)
        {
            Thread.Sleep(1000);
        }
    }

    static bool IsRobloxRunning()
    {
        Console.WriteLine("is roblox open rn?");

        string response = Console.ReadLine()?.Trim().ToLower();
        bool isRobloxOpen = response == "y";

        return isRobloxOpen;
    }
}