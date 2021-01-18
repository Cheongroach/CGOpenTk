using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using MyOpenTk.Common;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System.Windows.Input;
using OpenTK.Windowing;
namespace Exp3
{
    public class LightWindow : Window
    {
        // 正方体6个面，每个面由两个三角形构成，每个三角形通过一行定义
        private readonly float[] _vertices =
        {
            // 位置               法向量               材质uv坐标
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };

        private readonly Vector3[] _cubePositions =
        {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(2.0f, 5.0f, -15.0f),
            new Vector3(-1.5f, -2.2f, -2.5f),
            new Vector3(-3.8f, -2.0f, -12.3f),
            new Vector3(2.4f, -0.4f, -3.5f),
            new Vector3(-1.7f, 3.0f, -7.5f),
            new Vector3(1.3f, -2.0f, -2.5f),
            new Vector3(1.5f, 2.0f, -2.5f),
            new Vector3(1.5f, 0.2f, -1.5f),
            new Vector3(-1.3f, 1.0f, -1.5f)
        };

        //
        private readonly Vector3[] _pointLightPositions =
        {
            new Vector3(0.7f, 0.2f, 2.0f),
            new Vector3(2.3f, -3.3f, -4.0f),
            new Vector3(-4.0f, 2.0f, -12.0f),
            new Vector3(0.0f, 0.0f, -3.0f)
        };


        private int _vertexBufferObject;

        private int _vaoModel;

        private int _vaoLamp;

        private Shader _lampShader;

        private Shader _lightingShader;

        private Texture _diffuseMap;

        private Texture _specularMap;

        private Camera _camera;

        private bool _firstMove = true;

        private Vector2 _lastPos;

        private DateTimeOffset beginTime;

        public LightWindow(int width, int height, string title) : base(width, height, title)
        {}

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);         // 开启深度消隐

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.DynamicDraw);

            _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");
            _lampShader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _diffuseMap = new Texture("Resources/container2.png");
            _specularMap = new Texture("Resources/container2_specular.png");

            _vaoModel = GL.GenVertexArray();
            GL.BindVertexArray(_vaoModel);      // 多边形的顶点编号

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            var positionLocation = _lightingShader.GetAttribLocation("aPos");       // 获取aPos对应的输入handle，放到光照渲染器上边
            GL.EnableVertexAttribArray(positionLocation);                           // 激活handle
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);    // 每8个的前3个，导入到aPos

            var normalLocation = _lightingShader.GetAttribLocation("aNormal");      // 类似，将3, 4, 5作为一个Vector3导入到aNormal
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords"); // 将6, 7作为一个Vector2导入到aTexCoords（材质坐标）
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));


            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            CursorVisible = false;
            beginTime = DateTimeOffset.Now;
            CursorGrabbed = true;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);          // 清除深度
            GL.BindVertexArray(_vaoModel);          // 切换至_vaoModel上下文
            _diffuseMap.Use();                      // 使用货箱材质
            _specularMap.Use(TextureUnit.Texture1);
            _lightingShader.Use();                  // 使用光照着色器

            _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            _lightingShader.SetVector3("viewPos", _camera.Position);

            _lightingShader.SetInt("material.diffuse", 0);
            _lightingShader.SetInt("material.specular", 1);
            _lightingShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            _lightingShader.SetFloat("material.shininess", 32.0f); 

            // 设置考虑点光源造成的光照
            for (int i = 0; i < _pointLightPositions.Length; i++)
            {
                _lightingShader.SetVector3($"pointLights[{i}].position", _pointLightPositions[i]);          // 光源方向
                _lightingShader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));  // 环境光
                _lightingShader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));     // 漫反射
                _lightingShader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));    // 镜面反射
                _lightingShader.SetFloat($"pointLights[{i}].constant", 1.0f);           // 需要配置光照一次项、二次项和常数项
                _lightingShader.SetFloat($"pointLights[{i}].linear", 0.09f);            // 用以计算光照衰减情况
                _lightingShader.SetFloat($"pointLights[{i}].quadratic", 0.032f);
            }

            // 相机正前方加入一聚光灯
            _lightingShader.SetVector3("spotLight.position", _camera.Position);
            _lightingShader.SetVector3("spotLight.direction", _camera.Front);
            _lightingShader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            _lightingShader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            _lightingShader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            _lightingShader.SetFloat("spotLight.constant", 1.0f);                                   // 同样要计算光衰减
            _lightingShader.SetFloat("spotLight.linear", 0.09f);
            _lightingShader.SetFloat("spotLight.quadratic", 0.032f);
            _lightingShader.SetFloat("spotLight.cutOff", (float)Math.Cos(MathHelper.DegreesToRadians(12.5f)));
            _lightingShader.SetFloat("spotLight.outerCutOff", (float)Math.Cos(MathHelper.DegreesToRadians(12.5f)));


            // （太阳）平行光
            _lightingShader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            _lightingShader.SetVector3("dirLight.ambient", new Vector3(0.5f, 0.0f, 0.5f));
            _lightingShader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            _lightingShader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));

            // 渲染物体
            for (int i = 0; i < _cubePositions.Length; i++)
            {
                // 对于每个cube，计算它的model transportation matrix后输入到光照shader上。
                DateTimeOffset now = DateTimeOffset.Now;
                float seconds = (float)(now - beginTime).TotalSeconds;
                Matrix4 model = Matrix4.Identity;
                model *= Matrix4.CreateFromAxisAngle(new Vector3(0.3f, 0.3f, 0.5f), seconds * 2f);      // 自己旋转
                float theta = seconds * 4 + i * 10;                                                     // 放大缩小
                model *= Matrix4.CreateScale((float)(1 + 0.2 * Math.Sin(theta)));

                model *= Matrix4.CreateTranslation(_cubePositions[i]);      // 作平移变换
                float angle = 20.0f * i;            // 旋转角度
                model *= Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.3f, 0.5f), angle);
                _lightingShader.SetMatrix4("model", model);         // model矩阵

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }


            GL.BindVertexArray(_vaoModel);      // 结束vao

            _lampShader.Use();

            _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            // We use a loop to draw all the lights at the proper position
            for (int i = 0; i < _pointLightPositions.Length; i++)
            {
                Matrix4 lampMatrix = Matrix4.Identity;
                lampMatrix *= Matrix4.CreateScale(0.2f);
                lampMatrix *= Matrix4.CreateTranslation(_pointLightPositions[i]);

                _lampShader.SetMatrix4("model", lampMatrix);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (!IsFocused) return;


            if (IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
            {
                DestroyWindow();
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
            }
            if (IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time; // Backwards
            }
            if (IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time; // Left
            }
            if (IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time; // Right
            }
            if (IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time; // Up
            }
            if (IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time; // Down
            }

            if (_firstMove)
            {
                _firstMove = false;
            } else
            {
                _camera.Yaw += MouseState.Delta.X * sensitivity;
                _camera.Pitch += - MouseState.Delta.Y * sensitivity;
            }
        }


        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            _camera.Fov -= e.OffsetX;
            base.OnMouseWheel(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float)Size.Y;
            base.OnResize(e);
        }

        protected override void OnClosed()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vaoModel);
            GL.DeleteVertexArray(_vaoLamp);

            GL.DeleteProgram(_lampShader.Handle);
            GL.DeleteProgram(_lightingShader.Handle);
        }

    }
}
