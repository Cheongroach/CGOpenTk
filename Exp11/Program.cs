using System;

namespace Exp11
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var window = new MyWindow(500, 500, "Sierpinski Gasket"))
            {
                window.Run();
            }
        }
    }
}
