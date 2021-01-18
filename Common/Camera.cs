using System;
using OpenTK.Mathematics;

namespace MyOpenTk.Common
{
    public class Camera
    {
        // 摄像机朝向，用Vector3代替，初始为Z轴反方向（以后更新通过计算旋转和仰角得到）
        private Vector3 _front = -Vector3.UnitZ;

        // 摄像机的正上方方向，初始为Y轴正方形（以后更新通过计算旋转和仰角得到）
        private Vector3 _up = Vector3.UnitY;

        // 摄像机的右方向，初始为X轴方向（以后更新通过计算旋转和仰角得到）
        private Vector3 _right = Vector3.UnitX;

        // 绕X轴旋转
        private float _pitch;

        // 绕Y轴旋转
        private float _yaw = -MathHelper.PiOver2; // Without this you would be started rotated 90 degrees right

        // 绕Z轴旋转
        private float _fov = MathHelper.PiOver2;

        public Camera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
        }

        // The position of the camera
        public Vector3 Position { get; set; }

        // This is simply the aspect ratio of the viewport, used for the projection matrix
        public float AspectRatio { private get; set; }

        public Vector3 Front => _front;

        public Vector3 Up => _up;

        public Vector3 Right => _right;

        // We convert from degrees to radians as soon as the property is set to improve performance
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                // We clamp the pitch value between -89 and 89 to prevent the camera from going upside down, and a bunch
                // of weird "bugs" when you are using euler angles for rotation.
                // If you want to read more about this you can try researching a topic called gimbal lock
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        // We convert from degrees to radians as soon as the property is set to improve performance
        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        // The field of view (FOV) is the vertical angle of the camera view, this has been discussed more in depth in a
        // previous tutorial, but in this tutorial you have also learned how we can use this to simulate a zoom feature.
        // We convert from degrees to radians as soon as the property is set to improve performance
        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 45f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        // Get the view matrix using the amazing LookAt function described more in depth on the web tutorials
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + _front, _up);
        }

        // Get the projection matrix using the same method we have used up until this point
        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);
        }

        // This function is going to update the direction vertices using some of the math learned in the web tutorials
        private void UpdateVectors()
        {
            // First the front matrix is calculated using some basic trigonometry
            _front.X = (float)Math.Cos(_pitch) * (float)Math.Cos(_yaw);
            _front.Y = (float)Math.Sin(_pitch);
            _front.Z = (float)Math.Cos(_pitch) * (float)Math.Sin(_yaw);

            // We need to make sure the vectors are all normalized, as otherwise we would get some funky results
            _front = Vector3.Normalize(_front);

            // Calculate both the right and the up vector using cross product
            // Note that we are calculating the right from the global up, this behaviour might
            // not be what you need for all cameras so keep this in mind if you do not want a FPS camera
            _right = Vector3.Cross(_front, Vector3.UnitY).Normalized();
            _up = Vector3.Cross(_right, _front).Normalized();
        }
    }
}
