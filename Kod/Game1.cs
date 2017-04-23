using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Series3D1.Managers;
using Series3D1.Entities;
using Series3D1.Components;
using Series3D1.Systems;
using System.Collections.Generic;

namespace Series3D1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
         GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Test number uno :) ";
            // initialize camera start position

            SystemManager.Instance.RegisterSystem("game", new KeyBoardSystem());
            SystemManager.Instance.RegisterSystem("game", new TransformSystem());
            SystemManager.Instance.RegisterSystem("game", new CameraSystem());
            SystemManager.Instance.RegisterSystem("game", new ModelSystem());
            SystemManager.Instance.RegisterSystem("game", new HeightmapSystem());

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
            AddEntitiesAndComponents();
            SystemManager.Instance.RunLoadContentSystems();
            //load heightMap and heightMapTexture to create landscape
            base.LoadContent();
        }
        public void AddEntitiesAndComponents()
        {
            SystemManager.Instance.ActiveCategory = "game";
            SceneManager.Instance.ActiveScene = "game";
            CameraComponent camComp = new CameraComponent(MathHelper.PiOver4, 1.0f, 1000.0f, 1.33f, new Vector3(0, 1, 10));
            Entity chopper = new Entity();
            SceneManager.Instance.AddEntityToScene("game", chopper);
            ComponentManager.Instance.AddComponentToEntity(chopper, new TagComponent("chopper"));
            ComponentManager.Instance.AddComponentToEntity(chopper, new ModelComponent(Content.Load<Model>("Chopper")));
            TransformComponent tc = new TransformComponent(Vector3.Zero, Vector3.Zero, new Vector3(1, 1, 1));
            ComponentManager.Instance.AddComponentToEntity(chopper, tc);
            ComponentManager.Instance.AddComponentToEntity(chopper, camComp);
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            SystemManager.Instance.RunUpdateSystems(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SystemManager.Instance.RunDrawSystems(spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
