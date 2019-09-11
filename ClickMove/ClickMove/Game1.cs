// このファイルで必要なライブラリのnamespaceを指定
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

/// <summary>
/// プロジェクト名がnamespaceとなります
/// </summary>
namespace ClickMove
{
    /// <summary>
    /// ゲームの基盤となるメインのクラス
    /// 親クラスはXNA.FrameworkのGameクラス
    /// </summary>
    public class Game1 : Game
    {
        // フィールド（このクラスの情報を記述）
        private GraphicsDeviceManager graphicsDeviceManager;//グラフィックスデバイスを管理するオブジェクト
        private SpriteBatch spriteBatch;//画像をスクリーン上に描画するためのオブジェクト

        Renderer renderer;

        Vector2 mousePosition;
        Vector2 clickPosition;
        Vector2 movePosition;
        Vector2 rendPos;
        Vector2 limit;

        bool clickFlag = false;
        float time;

        Player player;
        
        /// <summary>
        /// コンストラクタ
        /// （new で実体生成された際、一番最初に一回呼び出される）
        /// </summary>
        public Game1()
        {
            //グラフィックスデバイス管理者の実体生成
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            //コンテンツデータ（リソースデータ）のルートフォルダは"Contentに設定
            Content.RootDirectory = "Content";

            IsMouseVisible = true;


            graphicsDeviceManager.PreferredBackBufferWidth = Screen.ScreenWidth;
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.ScreenHeight;
        }

        /// <summary>
        /// 初期化処理（起動時、コンストラクタの後に1度だけ呼ばれる）
        /// </summary>
        protected override void Initialize()
        {
            // この下にロジックを記述
            player = new Player();
            // この上にロジックを記述
            base.Initialize();// 親クラスの初期化処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// コンテンツデータ（リソースデータ）の読み込み処理
        /// （起動時、１度だけ呼ばれる）
        /// </summary>
        protected override void LoadContent()
        {
            // 画像を描画するために、スプライトバッチオブジェクトの実体生成
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // この下にロジックを記述
            renderer = new Renderer(Content, GraphicsDevice);
            renderer.LoadContent("backpi");
            renderer.LoadContent("boxor");
            renderer.LoadContent("ice");
            renderer.LoadContent("wallgr");
            renderer.LoadContent("clearor");
            renderer.LoadContent("clearpu");

            // この上にロジックを記述
        }

        /// <summary>
        /// コンテンツの解放処理
        /// （コンテンツ管理者以外で読み込んだコンテンツデータを解放）
        /// </summary>
        protected override void UnloadContent()
        {
            // この下にロジックを記述


            // この上にロジックを記述
        }

        /// <summary>
        /// 更新処理
        /// （1/60秒の１フレーム分の更新内容を記述。音再生はここで行う）
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Update(GameTime gameTime)
        {
            // ゲーム終了処理（ゲームパッドのBackボタンかキーボードのエスケープボタンが押されたら終了）
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                 (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                Exit();
            }

            // この下に更新ロジックを記述

            Input.Update();
            player.Update();
            {
                //if (Input.IsMouseLButtonDown())
                //{
                //    if (!clickFlag)
                //    {
                //        clickPosition = new Vector2((int)(Input.MousePosition.X / 32)/*何マス目か*/ * 32,(int)(Input.MousePosition.Y / 32) * 32);
                //        movePosition = clickPosition;
                //        clickFlag = true;
                //    }
                //    else
                //    {
                //        mousePosition = new Vector2((int)(Input.MousePosition.X / 32)/*何マス目か*/ * 32, (int)(Input.MousePosition.Y / 32) * 32);
                //        clickFlag = false;
                //        limit = new Vector2((int)(mousePosition.X - clickPosition.X) / 32, (int)(mousePosition.Y - clickPosition.Y) / 32);
                //        if (limit.X < 0) limit.X++;
                //        if (limit.Y < 0) limit.Y++;
                //        if (Math.Abs(limit.X) < Math.Abs(limit.Y)) time = 60 / 10 * Math.Abs(limit.Y);
                //        else time = 60 / 10 * Math.Abs(limit.X);
                //    }
                //}

                //if (rendPos != mousePosition && !clickFlag)
                //{
                //    if (Math.Abs(limit.X) >= Math.Abs((movePosition.X / 32) - (clickPosition.X / 32)))
                //    {
                //        //時間 ＝ フレーム　一マス辺りの時間　移動マス
                //        //time = 60 /2 * Math.Abs(limit.X);
                //        movePosition += new Vector2((mousePosition.X - clickPosition.X)/*何マス離れてるか*/ / (time/*60f×秒数*/), 0);
                //    }
                //    if (Math.Abs(limit.Y) >= Math.Abs((movePosition.Y / 32) - (clickPosition.Y / 32)))
                //    {
                //        //時間 ＝ フレーム　一マス辺りの時間　移動マス
                //        //time = 60 /2 * Math.Abs(limit.Y);
                //        movePosition += new Vector2(0, (mousePosition.Y - clickPosition.Y) / (time/*×秒数*/));
                //    }

                //    rendPos = new Vector2((int)(movePosition.X / 32) * 32, (int)(movePosition.Y / 32) * 32);
                //}
            }
            
            // この上にロジックを記述
            base.Update(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Draw(GameTime gameTime)
        {
            // 画面クリア時の色を設定
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // この下に描画ロジックを記述
            renderer.Begin();

            for (int i = 0; i < Screen.ScreenWidth / 32+32; i++)
            {
                for (int j = 0; j < Screen.ScreenHeight / 32+32; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            renderer.DrawTexture("wallgr", new Vector2(i * 32, j * 32));
                        }
                        else
                        {
                            renderer.DrawTexture("ice", new Vector2(i * 32, j * 32));
                        }
                    }
                    else
                    {
                        if (j % 2 == 1)
                        {
                            renderer.DrawTexture("wallgr", new Vector2(i * 32, j * 32));
                        }
                        else
                        {
                            renderer.DrawTexture("ice", new Vector2(i * 32, j * 32));
                        }
                    }
                }
            }
            player.Draw(renderer);
            //renderer.DrawTexture("backpi", clickPosition);
            //renderer.DrawTexture("backpi", mousePosition);
            //renderer.DrawTexture("boxor", rendPos);

            if (!Input.IsMouseLButton())
            {
                renderer.DrawTexture("clearor", new Vector2((int)(Input.MousePosition.X / 32) * 32, (int)(Input.MousePosition.Y / 32) * 32));
            }
            else
            {
                renderer.DrawTexture("clearpu", new Vector2((int)(Input.MousePosition.X / 32) * 32, (int)(Input.MousePosition.Y / 32) * 32));
            }
            renderer.End();

            //この上にロジックを記述
            base.Draw(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }
    }
}
