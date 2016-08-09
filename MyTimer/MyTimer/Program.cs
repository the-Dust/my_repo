using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using Microsoft.DirectX;
using Microsoft.DirectX.AudioVideoPlayback;

class Program
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr CreateWaitableTimer(IntPtr lpTimerAttributes,
    bool bManualReset, string lpTimerName);

    [DllImport("kernel32.dll")]
    public static extern bool SetWaitableTimer(IntPtr hTimer, [In] ref long
    pDueTime, int lPeriod, IntPtr pfnCompletionRoutine, IntPtr
    lpArgToCompletionRoutine, bool fResume);

    [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
    public static extern Int32 WaitForSingleObject(IntPtr handle, uint
    milliseconds);

    static IntPtr handle;
static void Main()
{
    /* long duetime = -90000000; 
    Console.WriteLine("Все, ложитесь спать хозяин! Я Вас утром разбужу!");
    handle = CreateWaitableTimer(IntPtr.Zero, true, "MyWaitabletimer");
    SetWaitableTimer(handle, ref duetime, 0, IntPtr.Zero, IntPtr.Zero, true);
    uint INFINITE = 0xFFFFFFFF;
    int ret = WaitForSingleObject(handle, INFINITE);
    System.Diagnostics.Process.Start("C:\\Program Files\\Windows Media Player\\wmplayer.exe");*/
    Audio song = new Audio("D:\\ROCK\\STEVE VAI\\1995 - Alien Love Secrets\07 - Tender Surrender.mp3");
    song.Play();
}
}