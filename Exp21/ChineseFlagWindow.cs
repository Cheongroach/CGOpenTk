using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MyOpenTk.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace Exp21
{
    class ChineseFlagWindow : Window
    {
        public ChineseFlagWindow(int size, string title) : base(size, size, title)
        {

        }

        private readonly ChineseFlag flag = new ChineseFlag();
        private Shader shader;
        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;

        private int vbo2, vao2, ebo2;

        private Vector3 red = new Vector3(0xDE / 255f, 0x29 / 255f, 0x10 / 255f);
        private Vector3 yellow = new Vector3(0xFF / 255f, 0xDE / 255f, 0f);
        private static readonly float[] retangleVertices = new float[]
        {
            -1f, 2f / 3, 0f,
            1f, 2f / 3, 0f,
            1f, -2f/3, 0f,
            -1f, -2f/3, 0f,
        };
        private static readonly uint[] rectangleElements = new uint[]
        {
            0, 1, 2,
            0, 3, 2
        };

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");


            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, flag.Verties.Length * sizeof(float), flag.Verties, BufferUsageHint.StaticDraw);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, flag.Indexes.Length * sizeof(uint), flag.Indexes, BufferUsageHint.StaticDraw);


            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);


            vbo2 = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo2);
            GL.BufferData(BufferTarget.ArrayBuffer, retangleVertices.Length * sizeof(float), retangleVertices, BufferUsageHint.StaticDraw);

            ebo2 = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo2);
            GL.BufferData(BufferTarget.ElementArrayBuffer, rectangleElements.Length * sizeof(uint), rectangleElements, BufferUsageHint.StaticDraw);

            vao2 = GL.GenVertexArray();
            GL.BindVertexArray(vao2);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo2);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo2);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.BindVertexArray(vao2);
            shader.SetVector3("uColor", red);
            GL.DrawElements(PrimitiveType.Triangles, rectangleElements.Length, DrawElementsType.UnsignedInt, 0);


            GL.BindVertexArray(vertexArrayObject);
            shader.SetVector3("uColor", yellow);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.DrawElements(PrimitiveType.Triangles, flag.Indexes.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }
    }
}
