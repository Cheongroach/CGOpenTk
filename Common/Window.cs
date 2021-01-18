using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using System;
using System.Dynamic;
using System.Text;
namespace MyOpenTk.Common
{
    public class Window : GameWindow
    {
        public Window(int width, int height, string title) :
            base(GameWindowSettings.Default,
                new NativeWindowSettings
                {
                    APIVersion = new Version(3, 3),
                    Size = new OpenTK.Mathematics.Vector2i(width, height),
                    Title = title,
                    Profile = OpenTK.Windowing.Common.ContextProfile.Core
                })
        { }

    }

}
