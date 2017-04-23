using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Series3D1.Managers;
using Series3D1.Entities;
using Series3D1.Components;
using Series3D1.Systems;
using System.Collections.Generic;
using System;
using Series3D1.Components.Other;

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
            Window.Title = "Game";
            // initialize camera start position

            SystemManager.Instance.RegisterSystem("game", new KeyBoardSystem());
            SystemManager.Instance.RegisterSystem("game", new TransformSystem());
            SystemManager.Instance.RegisterSystem("game", new CameraSystem());
            SystemManager.Instance.RegisterSystem("game", new ModelSystem());
            SystemManager.Instance.RegisterSystem("game", new HeightmapSystem());
            SystemManager.Instance.RegisterSystem("game", new StaticObjectSystem());

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

            Entity heightmap = new Entity();
            SceneManager.Instance.AddEntityToScene("game", heightmap);
            ComponentManager.Instance.AddComponentToEntity(heightmap, new TagComponent("heightmap"));
            ComponentManager.Instance.AddComponentToEntity(heightmap, new HeightmapComponent(Content.Load<Texture2D>("US_Canyon"), 
                Content.Load<Texture2D>("mntn_canyon_d"), graphics.GraphicsDevice, 9));
            ComponentManager.Instance.AddComponentToEntity(heightmap, new TransformComponent(new Vector3(-500, -200, -100), Vector3.Zero, Vector3.Zero));

            Entity camera = new Entity();
            SceneManager.Instance.AddEntityToScene("game", camera);
            ComponentManager.Instance.AddComponentToEntity(camera, new TagComponent("camera"));
            ComponentManager.Instance.AddComponentToEntity(camera, new CameraComponent(MathHelper.PiOver4, 1.0f, 1000.0f, 1.33f, new Vector3(-50, 40, 100), Vector3.Zero));
            ComponentManager.Instance.AddComponentToEntity(camera, new TransformComponent(Vector3.Zero, Vector3.Zero, Vector3.Zero));

            Entity player = new Entity();
            SceneManager.Instance.AddEntityToScene("game", player);
            ComponentManager.Instance.AddComponentToEntity(player, new TagComponent("player"));
            ComponentManager.Instance.AddComponentToEntity(player, new ModelComponent(new Model(GraphicsDevice, new List<ModelBone>(), new List<ModelMesh>())));
            ComponentManager.Instance.AddComponentToEntity(player, new TransformComponent(Vector3.Zero, Vector3.Zero, Vector3.One));
            ComponentManager.Instance.AddComponentToEntity(player, new PlayerComponent(Vector3.Zero));
            Dictionary<Keys, IAction> actions = new Dictionary<Keys, IAction>();
            AddPlayerKeys(actions);
            ComponentManager.Instance.AddComponentToEntity(player, new KeyActionsComponent(actions));

            Random ran = new Random();

            for (int i = 0; i < 10; i++)
            {
                Entity house = new Entity();
                SceneManager.Instance.AddEntityToScene("game", house);
                ComponentManager.Instance.AddComponentToEntity(house, new TagComponent("house" + i));
                ComponentManager.Instance.AddComponentToEntity(house, new ModelComponent(Content.Load<Model>("House")));
                TransformComponent htc = new TransformComponent(Vector3.Zero, Vector3.Zero, new Vector3(0.02f, 0.02f, 0.02f));
                ComponentManager.Instance.AddComponentToEntity(house, htc);
            }
            for (int i = 0; i < 20; i++)
            {
                Entity stone = new Entity();
                SceneManager.Instance.AddEntityToScene("game", stone);
                ComponentManager.Instance.AddComponentToEntity(stone, new TagComponent("stone" + i));
                ComponentManager.Instance.AddComponentToEntity(stone, new ModelComponent(Content.Load<Model>("Stone")));
                TransformComponent stc = new TransformComponent(Vector3.Zero, Vector3.Zero, new Vector3(0.02f, 0.02f, 0.02f));
                ComponentManager.Instance.AddComponentToEntity(stone, stc);
            }
            for (int i = 0; i < 30; i++)
            {
                Entity tree = new Entity();
                SceneManager.Instance.AddEntityToScene("game", tree);
                ComponentManager.Instance.AddComponentToEntity(tree, new TagComponent("tree" + i));
                ComponentManager.Instance.AddComponentToEntity(tree, new ModelComponent(Content.Load<Model>("Tree")));
                TransformComponent ttc = new TransformComponent(Vector3.Zero, Vector3.Zero, new Vector3(0.01f, 0.01f, 0.01f));
                ComponentManager.Instance.AddComponentToEntity(tree, ttc);
            }
        }
        void AddPlayerKeys(Dictionary<Keys, IAction> actions)
        {
            PlayerMoves pm = new PlayerMoves();
            actions.Add(Keys.W, pm);
            actions.Add(Keys.A, pm);
            actions.Add(Keys.S, pm);
            actions.Add(Keys.D, pm);
            actions.Add(Keys.Left, pm);
            actions.Add(Keys.Right, pm);
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
