using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MoteurParticule
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState kbState, oldKbState;
        MouseState mouseState;

        MoteurParticule moteurParticule2;
        List<Texture2D> Textures2;

        enum Status { 
            EnCours,
            EnPause
        }

        Status statusJeu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //this.graphics.IsFullScreen = true;
            this.graphics.PreferredBackBufferHeight = 600;
            this.graphics.PreferredBackBufferWidth = 1000;
            statusJeu = Status.EnCours;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here


            Textures2 = new List<Texture2D>();


            //Textures2.Add(Content.Load<Texture2D>("circle"));
            //Textures2.Add(Content.Load<Texture2D>("diamond"));
            //Textures2.Add(Content.Load<Texture2D>("star"));

            Textures2.Add(Content.Load<Texture2D>("blue"));
            Textures2.Add(Content.Load<Texture2D>("green"));
            Textures2.Add(Content.Load<Texture2D>("red"));

            moteurParticule2 = new MoteurParticule(Textures2, new Vector2(350, 350), new Vector2(0.00f, -0.0f), new Vector2(0, 0.0f), 0, true);
            moteurParticule2.setAngle((float)(- 0 * Math.PI / 3), (float)(- 6 * Math.PI/3));
            moteurParticule2.setVitesse(1f, 2.5f, -0.001f);
            moteurParticule2.setSize(5, 10, 0.000f);
            moteurParticule2.setTTL(40, 60, 0);

            moteurParticule2.variationVent = new Vector2(0.00f, 0);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            //moteurParticule1.Update();
            oldKbState = kbState;
            kbState = Keyboard.GetState();

            mouseState = Mouse.GetState();

            if (kbState.IsKeyDown(Keys.Space) && oldKbState.IsKeyUp(Keys.Space))
            {
                if (statusJeu == Status.EnCours)
                    statusJeu = Status.EnPause;
                else
                    statusJeu = Status.EnCours;
            }

            if (statusJeu == Status.EnCours)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    moteurParticule2.GenererParticule(30);

                moteurParticule2.Update(new Vector2(mouseState.X, mouseState.Y));
            }

            /*if (moteurParticule2.Vent.X < -0.3)
                moteurParticule2.variationVent.X = 0.008f;
            else if (moteurParticule2.Vent.X > 0.3)
                moteurParticule2.variationVent.X = -0.008f;*/

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            moteurParticule2.Draw(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
