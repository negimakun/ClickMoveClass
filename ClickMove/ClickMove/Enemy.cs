using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMove
{
    class EnemyLevel1
    {
        public Camp baseCamp;

        public float enemyMoveTime; //一マス当たりの移動時間
        public Vector2 enemySpawnPos; //スポーン位置
        public Vector2 enemyMovePos; //移動量
        public Vector2 enemyHeadPos; //向かう場所
        public Vector2 enemyLimit; //移動量の限界値
        public Vector2 enemyMasu; //マスの位置
        public Vector2 enemyPos; //ポジション
        public Direction direction;
        private readonly int UIWidth = 300;

        private int eatTime = 3 * 60;

        public readonly int TextureSize = 50;
        public List<Wall> walls;
        public List<Player> players;
        public int targetPlayerNom;
        public int colWallNum;

        int stuff;//満腹いくつ？
        public bool stuffMAXFlag = false; //満足して帰ったらtrue

        bool neerGlassEaterFlag;
        public bool moveEndFlag = false;
        
        public EnemyLevel1(Direction direction, Camp camp, List<Player> players,List<Wall> walls)
        {
            this.direction = direction;
            baseCamp = camp;
            this.players = players;
            this.walls = walls;
        }
        

        public void Initialze()
        {
            stuff = 1;
            neerGlassEaterFlag = false;
            switch (direction)
            {
                case Direction.LEFT:
                    enemySpawnPos = new Vector2((int)(Screen.ScreenWidth / TextureSize) * TextureSize, (int)((Screen.ScreenHeight / 2) / TextureSize) * TextureSize);
                    break;
                case Direction.RIGHT:
                    enemySpawnPos = new Vector2((int)(UIWidth / TextureSize) * TextureSize - TextureSize, (int)((Screen.ScreenHeight / 2) / TextureSize) * TextureSize);
                    break;
                case Direction.BOTTOM:
                    enemySpawnPos = new Vector2((int)(((Screen.ScreenWidth - UIWidth) / 2 + UIWidth) / TextureSize) * TextureSize, -TextureSize);
                    break;
                case Direction.TOP:
                    enemySpawnPos = new Vector2((int)(((Screen.ScreenWidth - UIWidth) / 2 + UIWidth) / TextureSize) * TextureSize, Screen.ScreenHeight);
                    break;
                default:
                    break;
            }
            enemyMovePos = enemySpawnPos;
            enemyPos = enemySpawnPos;
        }

        public void Update()
        {
            int nowCnt = 0;
            foreach (var wall in walls)
            {
                if(!Collision.WallXEnemy(wall, this))
                {
                    if (stuff > 0 && !moveEndFlag)
                    {
                        NeerGlassEater();
                        if (!neerGlassEaterFlag)
                        {
                            MoveToCamp();
                        }
                        else
                        {
                            MoveToGE();
                        }
                    }
                    else if (stuff <= 0 && eatTime > 0)
                    {
                        eatTime--;
                    }
                    else if (stuff <= 0 && eatTime <= 0)
                    {
                        MoveToSpawn();
                    }
                    else if (moveEndFlag)
                    {
                        //ここに草食獣のストックを減らす処理
                        //Camp.～～～～();
                    }
                }
                else
                {
                    colWallNum = nowCnt;
                    WallAvoid();
                }
                nowCnt++;

                if (direction == Direction.TOP)
                {
                    Console.WriteLine(moveEndFlag+ ":" + stuffMAXFlag + ":" + Collision.WallXEnemy(wall, this));
                }
            }

            enemyMasu = new Vector2(enemyPos.X / TextureSize, enemyPos.Y / TextureSize);
        }

        public void WallAvoid()
        {
            //walls[colWallNum]; 当たってる壁
            direction = Collision.WallXEnemyDirection(walls[colWallNum], this);
            Vector2 center = new Vector2(enemyPos.X + TextureSize / 2, enemyPos.Y + TextureSize / 2);
            switch (direction)
            {
                case Direction.RIGHT:
                    break;
                case Direction.LEFT:
                    break;
                case Direction.TOP:
                    break;
                case Direction.BOTTOM:
                    
                    break;
                case Direction.NULL:
                    break;
                default:
                    break;
            }
        }

        public void NeerGlassEater()
        {
            int nowCnt = 0;
            if (players != null)
            {
                foreach (var ge in players)
                {
                    if (Vector2.Distance(enemyMasu, ge.plMasu) <= 4 && !ge.isDeadFlag)
                    {
                        neerGlassEaterFlag = true;
                        enemyHeadPos = ge.rendPos;
                        targetPlayerNom = nowCnt;
                    }
                    else
                    {
                        neerGlassEaterFlag = false;
                    }
                    nowCnt++;
                }
            }
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("wolf", enemyPos);
        }



        public void MoveToCamp()
        {
            //拠点移動
            enemyHeadPos = new Vector2((int)((baseCamp.campPos.X + TextureSize) / TextureSize)/*何マス目か*/ * TextureSize,
                (int)((baseCamp.campPos.Y + TextureSize) / TextureSize) * TextureSize);
            enemyLimit = new Vector2((int)(enemyHeadPos.X - enemyPos.X) / TextureSize,
                (int)(enemyHeadPos.Y - enemyPos.Y) / TextureSize);

            //時間 ＝ フレーム　一マス辺りの時間　移動マス
            if (Math.Abs(enemyLimit.X) < Math.Abs(enemyLimit.Y)) enemyMoveTime = 60 * 1 * Math.Abs(enemyLimit.Y);
            else enemyMoveTime = 60 * 1 * Math.Abs(enemyLimit.X);

            if (enemyPos != enemyHeadPos)
            {
                if (Math.Abs(enemyLimit.X) > Math.Abs((enemyMovePos.X / TextureSize) - (enemyPos.X / TextureSize)))
                {
                    enemyMovePos += new Vector2((enemyHeadPos.X - enemyPos.X)/*何マス離れてるか*/ / (enemyMoveTime/*60f×秒数*/), 0);
                }
                if (Math.Abs(enemyLimit.Y) > Math.Abs((enemyMovePos.Y / TextureSize) - (enemyPos.Y / TextureSize)))
                {
                    enemyMovePos += new Vector2(0, (enemyHeadPos.Y - enemyPos.Y) / (enemyMoveTime/*×秒数*/));
                }

                enemyPos = new Vector2((int)(enemyMovePos.X / TextureSize) * TextureSize,
                    (int)(enemyMovePos.Y / TextureSize) * TextureSize);
            }
            else
            {
                moveEndFlag = true;
            }
        }

        public void MoveToGE()
        {
            enemyLimit = new Vector2((int)(enemyHeadPos.X - enemyPos.X) / TextureSize,
                   (int)(enemyHeadPos.Y - enemyPos.Y) / TextureSize);

            //時間 ＝ フレーム　一マス辺りの時間　移動マス
            if (Math.Abs(enemyLimit.X) < Math.Abs(enemyLimit.Y)) enemyMoveTime = 60 * 1 * Math.Abs(enemyLimit.Y);
            else enemyMoveTime = 60 * 1 * Math.Abs(enemyLimit.X);

            if (enemyPos != enemyHeadPos)
            {
                if (Math.Abs(enemyLimit.X) >= Math.Abs((enemyMovePos.X / TextureSize) - (enemyPos.X / TextureSize)))
                {
                    enemyMovePos += new Vector2((enemyHeadPos.X - enemyPos.X)/*何マス離れてるか*/ / (enemyMoveTime/*60f×秒数*/), 0);
                }
                if (Math.Abs(enemyLimit.Y) >= Math.Abs((enemyMovePos.Y / TextureSize) - (enemyPos.Y / TextureSize)))
                {
                    enemyMovePos += new Vector2(0, (enemyHeadPos.Y - enemyPos.Y) / (enemyMoveTime/*×秒数*/));
                }

                enemyPos = new Vector2((int)(enemyMovePos.X / TextureSize) * TextureSize,
                    (int)(enemyMovePos.Y / TextureSize) * TextureSize);
            }
            else
            {
                if (players != null)
                {
                    players[targetPlayerNom].isDeadFlag = true;
                    stuff -= players[targetPlayerNom].stuff;
                }
            }
        }

        public void MoveToSpawn()
        {
            enemyHeadPos = enemySpawnPos;
            enemyLimit = new Vector2((int)(enemyHeadPos.X - enemyPos.X) / TextureSize,
                  (int)(enemyHeadPos.Y - enemyPos.Y) / TextureSize);

            //時間 ＝ フレーム　一マス辺りの時間　移動マス
            if (Math.Abs(enemyLimit.X) < Math.Abs(enemyLimit.Y)) enemyMoveTime = 60 * 1 * Math.Abs(enemyLimit.Y);
            else enemyMoveTime = 60 * 1 * Math.Abs(enemyLimit.X);

            if (enemyPos != enemyHeadPos)
            {
                if (Math.Abs(enemyLimit.X) > Math.Abs((enemyMovePos.X / TextureSize) - (enemyPos.X / TextureSize)))
                {
                    enemyMovePos += new Vector2((enemyHeadPos.X - enemyPos.X)/*何マス離れてるか*/ / (enemyMoveTime/*60f×秒数*/), 0);
                }
                if (Math.Abs(enemyLimit.Y) > Math.Abs((enemyMovePos.Y / TextureSize) - (enemyPos.Y / TextureSize)))
                {
                    enemyMovePos += new Vector2(0, (enemyHeadPos.Y - enemyPos.Y) / (enemyMoveTime/*×秒数*/));
                }

                enemyPos = new Vector2((int)(enemyMovePos.X / TextureSize) * TextureSize,
                    (int)(enemyMovePos.Y / TextureSize) * TextureSize);
            }
            else
            {
                stuffMAXFlag = true;
            }
        }
    }
}
