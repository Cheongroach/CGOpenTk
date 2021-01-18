using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOpenTk.Common;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Platform.Windows;

namespace Exp11
{
    class MyWindow : Window
    {
        int _vbo;
        int _vao;
        Shader _shader;
        const int count = 100000;
        public MyWindow(int width, int height, string name) : base(width, height, name)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);  // 背景色
            _vao = GL.GenVertexArray();             // VertexArrayObject，用来绑定点数组
            GL.BindVertexArray(_vao);               // 下面的操作将自动保存在_vao上下文中

            Random random = new();

            Vector2[] triVertices = new Vector2[]   // 三角形三个点
            {
                new Vector2(-0.5f, -0.5f),
                new Vector2(0.5f,      0.5f),
                new Vector2(0.5f,  -0.5f),
            };

            Vector2 p = new Vector2(-0.2f, 0.1f);   // 初始点
            List<float> drawPixels = new();         // 暂存的点
            for (int i = 0; i < count; i++)
            {
                drawPixels.AddRange(new float[] { p.X, p.Y });
                int next = random.Next(0, 3);
                p = (p + triVertices[next]) / 2;
            }
            float[] pixels = drawPixels.ToArray();

            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, pixels.Length * sizeof(float), pixels, BufferUsageHint.StaticDraw);

            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            int position = _shader.GetAttribLocation("position");
            GL.VertexAttribPointer(position, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(position);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.Points, 0, count);

            SwapBuffers();
        }

    }
}
