﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series3D1.Components;
using Series3D1.Managers;
using Series3D1.Entities;

namespace Series3D1.Systems
{
    class HeightmapSystem : ILoadContent, IDraw
    {

        //method for getting out textures
        private void SetEffects(HeightmapComponent hmComp)
        {
            foreach(Entity ent in ComponentManager.Instance.GetAllEntitiesWithCertainComp<CameraComponent>())
            {
                CameraComponent camComp = ComponentManager.Instance.GetEntityComponent<CameraComponent>(ent);
                hmComp.Effect.View = camComp.View;
                hmComp.Effect.Projection = camComp.Proj;
            }
            hmComp.Effect.World = hmComp.World;
            hmComp.Effect.FogEnabled = false;

            //ändra till false om man vill se trianglarna
            hmComp.Effect.TextureEnabled = true;
            //// draw those triangles
            //RasterizerState state = new RasterizerState();
            //state.FillMode = FillMode.WireFrame;
            //graphicsDevice.RasterizerState = state;

        }

        private void SetIndices(HeightmapComponent hmComp)
        {
            // amount of triangles
            hmComp.Indices = new int[6 * (hmComp.Width - 1) * (hmComp.Height - 1)];
            int number = 0;
            // collect data for corners
            for (int y = 0; y < hmComp.Height - 1; y++)
                for (int x = 0; x < hmComp.Width - 1; x++)
                {
                    // create double triangles
                    hmComp.Indices[number] = x + (y + 1) * hmComp.Width;      // up left
                    hmComp.Indices[number + 1] = x + y * hmComp.Width + 1;        // down right
                    hmComp.Indices[number + 2] = x + y * hmComp.Width;            // down left
                    hmComp.Indices[number + 3] = x + (y + 1) * hmComp.Width;      // up left
                    hmComp.Indices[number + 4] = x + (y + 1) * hmComp.Width + 1;  // up right
                    hmComp.Indices[number + 5] = x + y * hmComp.Width + 1;        // down right
                    number += 6;
                }
        }

        private void SetVertices(HeightmapComponent hmComp)
        {
            hmComp.Vertices = new VertexPositionTexture[hmComp.Width * hmComp.Height];
            Vector2 texturePosition;
            for (int x = 0; x < hmComp.Width; x++)
            {
                for (int y = 0; y < hmComp.Height; y++)
                {
                    texturePosition = new Vector2((float)x /25.5f, (float)y / 25.5f);
                    hmComp.Vertices[x + y * hmComp.Width] = new VertexPositionTexture(new Vector3(x, hmComp.heightMapData[x, y], -y), texturePosition);
                }

                // graphicsDevice.VertexDeclaration = new VertexDeclaration(graphicsDevice, VertexPositionTexture.VertexElements);
            }
        }
        public void SetHeights(HeightmapComponent hmComp)
        {
            Color[] greyValues = new Color[hmComp.Width * hmComp.Height];
            hmComp.HeightMap.GetData(greyValues);
            hmComp.heightMapData = new float[hmComp.Width, hmComp.Height];

            for (int x = 0; x < hmComp.Width; x++)
            {
                for (int y = 0; y < hmComp.Height; y++)
                {
                    hmComp.heightMapData[x, y] = greyValues[x + y * hmComp.Width].G / 3.1f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Entity hmEntity = ComponentManager.Instance.GetEntityWithTag("heightmap", SceneManager.Instance.GetActiveSceneEntities());
            HeightmapComponent hmComp = ComponentManager.Instance.GetEntityComponent<HeightmapComponent>(hmEntity);

            hmComp.Effect.CurrentTechnique.Passes[0].Apply();
            SetEffects(hmComp);
            foreach (EffectPass pass in hmComp.Effect.CurrentTechnique.Passes)
            {

                //pass.Begin();
                pass.Apply();
                spriteBatch.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, hmComp.Vertices, 0, hmComp.Vertices.Length, hmComp.Indices, 0, hmComp.Indices.Length / 3);


                // pass.End();
            }
        }

        public void LoadContent()
        {
            Entity hmEntity = ComponentManager.Instance.GetEntityWithTag("heightmap", SceneManager.Instance.GetActiveSceneEntities());
            HeightmapComponent hmComp = ComponentManager.Instance.GetEntityComponent<HeightmapComponent>(hmEntity);
            TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(hmEntity);
            hmComp.World = Matrix.CreateTranslation(transComp.Position);
            SetHeights(hmComp);
            SetVertices(hmComp);
            SetIndices(hmComp);
        }

        public int Order()
        {
            return 1;
        }
    }
}
