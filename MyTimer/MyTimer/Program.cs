using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            WaitableTimer.SetTimer(long Interval);
            Console.ReadKey();
        }
    }

    public class WaitableTimer
    {
        public delegate void TimerSetDelegate();
        public delegate void TimerCompleteDelegate();

        public event TimerSetDelegate OnTimerSet;
        public event TimerCompleteDelegate OnTimerCompleted;

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateWaitableTimer(IntPtr lpTimerAttributes, bool bManualReset, string lpTimerName);

        [DllImport("kernel32.dll")]
        static extern bool SetWaitableTimer(IntPtr hTimer, [In] ref long ft, int lPeriod, TimerCompleteDelegate pfnCompletionRoutine, IntPtr pArgToCompletionRoutine, bool fResume);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern Int32 WaitForSingleObject(IntPtr Handle, uint Wait);

        [DllImport("kernel32.dll")]
        static extern bool CancelWaitableTimer(IntPtr hTimer);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        private IntPtr handle;
        private uint INFINITE = 0xFFFFFFFF;

        public void SetTimer(long Interval)
        {
            // Creating the timer delegate
            //
            TimerCompleteDelegate TimerComplete = new TimerCompleteDelegate(TimerCompleted);
            TimerSetDelegate TimerSet = new TimerSetDelegate(TimerIsSet);

            // Creating the timer
            //
            Console.WriteLine("Creating WaitableTimer");
            handle = CreateWaitableTimer(IntPtr.Zero, true, "WaitableTimer");
            Console.WriteLine("Last Error = " + Marshal.GetLastWin32Error().ToString());

            // Setting up the timer, the long Interval value needs to be negative if
            // you want to set up a delay in milliseconds. ie
            // if Interval = -60000000 the timer will expire in 1 minute. Once expired it runs the
            // TimerComplete delegate
            //
            Console.WriteLine("Setting WaitableTimer");
            SetWaitableTimer(handle, ref Interval, 0, TimerComplete, IntPtr.Zero, true);
            Console.WriteLine("Last Error = " + Marshal.GetLastWin32Error().ToString()); // The error may be 1004 (Invalid flags), this is not critical
            Console.WriteLine("Timer set @ " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

            // Starting a new thread which waits for the WaitableTimer to expire
            //
            Thread t_Wait = new Thread(new ThreadStart(WaitTimer));
            t_Wait.Start();

            // Raising Event Timer Set
            //
            if (OnTimerSet != null)
            {
                OnTimerSet();
            }
        }

        private void WaitTimer()
        {
            // Waiting for the timer to expire
            //
            if (WaitForSingleObject(handle, INFINITE) != 0)
            {
                Console.WriteLine("Last Error = " + Marshal.GetLastWin32Error().ToString());
            }
            else
            {
                Console.WriteLine("Timer expired @ " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            }

            // Closing the timer
            //
            CloseHandle(handle);

            // Raising event Timer Completed
            //
            if (OnTimerCompleted != null)
            {
                OnTimerCompleted();
            }
        }

        private void TimerCompleted()
        {
            // Routine executed once the timer has expired. This is executed independently of the
            // program calling this class implementation of the OnTimerCompleted Event
            //
            Console.WriteLine("Timer is complete in the class");
        }

        private void TimerIsSet()
        {
            Console.WriteLine("Timer is set in the class");
        }
    }
}
