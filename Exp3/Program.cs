using System;

namespace Exp3
{
    class Program
    {
        static void Main(string[] args)
        {
            using var window =new LightWindow(1024, 768, "光照投影");
            window.Run();
        }
    }
}
