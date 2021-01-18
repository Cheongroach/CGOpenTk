using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Linq;

namespace Exp21
{
    public class ChineseFlag
    {
        public Star[] Stars { get; private set; }

        public ChineseFlag()
        {
            Vector2 center = new Vector2(-10, 5);
            Vector2 top = new Vector2(-10, 8);
            float[] coordinates =
            {
                -5, 8,
                -3, 6,
                -3, 3,
                -5, 1,
            };

            List<Star> stars = new List<Star>();
            stars.Add(new Star(center, top));
            for (int i = 0, j = 1; j < coordinates.Length; i += 2, j += 2)
            {
                stars.Add(Star.CreateByFacing(new Vector2(coordinates[i], coordinates[j]), center, 1));
            }
            Stars = stars.ToArray();

            uint[] b =
                {
                    0, 5, 9,
                    1, 5, 6,
                    2,6,7,
                    3, 7, 8,
                    4, 8, 9,
                    5, 6, 7,
                    5, 7, 8,
                    5, 8, 9
                };
            List<uint> rnt = new List<uint>();
            for (int i = 0; i < 5; i++)
                rnt.AddRange(b.Select(x => (uint)(x + i * 10)));
            Indexes = rnt.ToArray();

            var rnt2 = new List<float>();
            for (int i = 0; i < 5; i++)
            {
                rnt2.AddRange(Stars[i].Verties.SelectMany(v => new float[] { v.X / 15f, v.Y / 15f, 0f, }));
            }

            Verties = rnt2.ToArray();
        }
        public uint[] Indexes { get; private set; }
        public float[] Verties { get; private set; }
    }

}
