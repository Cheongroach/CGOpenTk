using OpenTK.Mathematics;
using static System.Math;
namespace Exp21
{
    public struct Star
    {
        public Vector2[] Verties { get; set; }
        public Star(Vector2 center, Vector2 top)
        {
            Verties = new Vector2[10];
            Verties[0] = top;
            for (int i = 1; i <= 4; i++)
            {
                Verties[i] = Matrix2.CreateRotation((float)(PI * i * 0.4)) * (top - center) + center;
            }
            for (int i = 5; i < 10; i++)
            {
                Verties[i] = center + (Matrix2.CreateRotation((float)(PI * 0.2)) * (Verties[i - 5] - center)) * (float)(Sin(PI / 10) / Sin(PI * 7 / 10));
            }
        }

        public static Star CreateByCoordinates(float cx, float cy, float tx, float ty)
        {
            return new Star(new Vector2(cx, cy), new Vector2(tx, ty));
        }


        public static Star CreateByFacing(Vector2 center, Vector2 facing, float radius)
        {
            var top = (facing - center).Normalized() * radius + center;
            return new Star(center, top);
        }
    }

}
