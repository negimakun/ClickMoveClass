using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMove
{
    class Wall
    {
        public Rectangle rectangle;

        public Vector2 position;
        public Vector2 scaleSize;

        public readonly int TextureSize = 150;

        public Wall(Vector2 pos,Rectangle rect)
        {
            position = pos;
            rectangle = rect;
        }

        public void Update()
        {
            scaleSize = new Vector2(rectangle.Width / TextureSize,
                rectangle.Height / TextureSize);

            Console.WriteLine((position.X + rectangle.Width) +":" +(position.X * (rectangle.Width / TextureSize)));
        }

        public void Draw(Renderer renderer)
        {
            //renderer.DrawTexture("waku", position, rectangle);
            renderer.DrawTexture("waku", position, null,
                0.0f, Vector2.Zero, scaleSize);
        }
    }
}
