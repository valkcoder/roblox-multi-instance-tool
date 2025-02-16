using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace MultiInstanceEnabler
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        private const string MutexName = "ROBLOX_singletonMutex";
        private const string AppName = "ValksMultiInstanceMutex";
        private IntPtr _mutex;
        private readonly Mutex _appMutex;

        public MainForm()
        {
            bool createdNew;
            _appMutex = new Mutex(true, AppName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("You may only have one active instance of the multi instance tool!");
                Application.Exit();
                return;
            }

            InitializeComponent();
        }

        private void EnableBtn_Click(object sender, EventArgs e)
        {
            if (_mutex != IntPtr.Zero) return;
            _mutex = CreateMutex(IntPtr.Zero, true, MutexName);
            MessageBox.Show("Successfully created mutex");
        }

        private void DisableBtn_Click(object sender, EventArgs e)
        {
            if (_mutex == IntPtr.Zero) return;
            CloseHandle(_mutex);
            _mutex = IntPtr.Zero;
            MessageBox.Show("Successfully closed mutex");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_mutex == IntPtr.Zero) return;
            CloseHandle(_mutex);
            _mutex = IntPtr.Zero;
        }
    }
}
