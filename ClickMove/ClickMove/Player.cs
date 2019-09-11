using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMove
{
    class Player
    {
        Renderer renderer;

        Vector2 mouseClickPosition;

        Vector2 mousePosition;
        Vector2 clickPosition;
        Vector2 movePosition;
        Vector2 rendPos;
        Vector2 limit;

        bool clickFlag = false;
        float time;

        readonly int TextureSize = 32;

        public Player()
        {

        }


        public void Update()
        {
            if (Input.IsMouseLButtonDown())
            {
                mouseClickPosition = new Vector2((int)(Input.MousePosition.X / TextureSize)/*何マス目か*/ * TextureSize,
                    (int)(Input.MousePosition.Y / TextureSize) * TextureSize);

                if (rendPos == mouseClickPosition || clickFlag)
                {
                    if (!clickFlag)
                    {
                        clickPosition = new Vector2((int)(Input.MousePosition.X / TextureSize)/*何マス目か*/ * TextureSize,
                            (int)(Input.MousePosition.Y / TextureSize) * TextureSize);
                        movePosition = clickPosition;
                        clickFlag = true;
                    }
                    else
                    {
                        mousePosition = new Vector2((int)(Input.MousePosition.X / TextureSize)/*何マス目か*/ * TextureSize,
                            (int)(Input.MousePosition.Y / TextureSize) * TextureSize);
                        clickFlag = false;
                        limit = new Vector2((int)(mousePosition.X - clickPosition.X) / TextureSize,
                            (int)(mousePosition.Y - clickPosition.Y) / TextureSize);
                        if (limit.X < 0) limit.X++;
                        if (limit.Y < 0) limit.Y++;

                        //時間 ＝ フレーム　一マス辺りの時間　移動マス
                        if (Math.Abs(limit.X) < Math.Abs(limit.Y)) time = 60 / 10 * Math.Abs(limit.Y);
                        else time = 60 / 10 * Math.Abs(limit.X);
                    }
                }
            }

            if (rendPos != mousePosition && !clickFlag)
            {
                if (Math.Abs(limit.X) >= Math.Abs((movePosition.X / TextureSize) - (clickPosition.X / TextureSize)))
                {
                    movePosition += new Vector2((mousePosition.X - clickPosition.X)/*何マス離れてるか*/ / (time/*60f×秒数*/), 0);
                }
                if (Math.Abs(limit.Y) >= Math.Abs((movePosition.Y / TextureSize) - (clickPosition.Y / TextureSize)))
                {
                    movePosition += new Vector2(0, (mousePosition.Y - clickPosition.Y) / (time/*×秒数*/));
                }

                rendPos = new Vector2((int)(movePosition.X / TextureSize) * TextureSize,
                    (int)(movePosition.Y / TextureSize) * TextureSize);
            }
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("backpi", clickPosition);
            renderer.DrawTexture("backpi", mousePosition);
            renderer.DrawTexture("boxor", rendPos);
        }
    }
}
