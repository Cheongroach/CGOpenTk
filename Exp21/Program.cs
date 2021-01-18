using Exp21;
using System;

namespace MyOpenTk.Exp21
{
    class Program
    {
        static void Main(string[] args)
        {
             using(var window = new ChineseFlagWindow(1200, "中国国旗"))
            {
                window.Run();
            }
        }
    }
}
