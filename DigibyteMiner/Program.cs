using OneMiner.Core;
using OneMiner.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace OneMiner
{
    static public class WinApi
    {

        [DllImportAttribute("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32")]
        public static extern bool SendMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

    }
    static class Program
    {

        static Mutex mutex = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //Bring only a single instance
                bool onlyInstance = false;
                mutex = new Mutex(true, "UniqueApplicationName", out onlyInstance);
                if (!onlyInstance)
                {
                    IntPtr handle = WinApi.FindWindow(null, "OneMiner - 1 Click Miner for Ethereum, ZCash");
                    if (handle != IntPtr.Zero)
                        WinApi.PostMessage(handle, 3000, IntPtr.Zero, IntPtr.Zero);
                    return;
                }
            }
            catch (Exception e)
            {
            }
            
            IView view = Factory.Instance.ViewObject;
            view.InitializeView();

            Factory.Instance.CoreObject.LoadDBData();
            view.StartView();
            
        }
    }
}
