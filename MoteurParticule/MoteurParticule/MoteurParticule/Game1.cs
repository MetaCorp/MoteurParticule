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

        MoteurParticule moteurParticule1;
        List<Texture2D> Textures1;

        MoteurParticule moteurParticule2;
        List<Texture2D> Textures2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //this.graphics.IsFullScreen = true;

            
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

            Textures1 = new List<Texture2D>();

            Textures1.Add(Content.Load<Texture2D>("circle"));
            Textures1.Add(Content.Load<Texture2D>("diamond"));
            Textures1.Add(Content.Load<Texture2D>("star"));

            moteurParticule1 = new MoteurParticule(Textures1, new Vector2(350, 300), new Vector2(0.001f, 0));
            moteurParticule1.setAngle((float)(-Math.PI / 3), (float)(-Math.PI / 3));
            moteurParticule1.setVitesse(1f, 2f, 0.0f);
            moteurParticule1.setSize(10, 20, -0.0f);


            Textures2 = new List<Texture2D>();

            for (int i = 0; i < 6; i++)
                Textures2.Add(Content.Load<Texture2D>("red"));
            Textures2.Add(Content.Load<Texture2D>("green"));

            moteurParticule2 = new MoteurParticule(Textures2, new Vector2(350, 300), new Vector2(0.01f, 0.0f));
            moteurParticule2.setAngle((float)(-2 *Math.PI / 3), (float)(-1 * Math.PI / 10));
            moteurParticule2.setVitesse(1f, 3f, -0.1f);
            moteurParticule2.setSize(3, 8, 0.5f);
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
            moteurParticule1.Update();
            moteurParticule2.Update();

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


            spriteBatch.Begin();

            //moteurParticule1.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
