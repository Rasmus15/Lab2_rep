using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Series3D1.Managers;
using Series3D1.Components;
using Series3D1.Entities;
using Microsoft.Xna.Framework.Input;

namespace Series3D1.Systems
{
    class ModelSystem : IDraw
    {
        private Matrix GetParentTransform(ModelComponent m, ModelBone mb)
        {
            return (mb == m.Model.Root) ? mb.Transform :
                mb.Transform * GetParentTransform(m, mb.Parent);
        }

        private float GetMaxMeshRadius(ModelComponent m)
        {
            float radius = 0.0f;
            foreach (ModelMesh mesh in m.Model.Meshes)
            {
                if (mesh.BoundingSphere.Radius > radius)
                {
                    radius = mesh.BoundingSphere.Radius;
                }
            }
            return radius;
        }
        public void DrawModel(GameTime gameTime)
        {
            CameraComponent camComp = ComponentManager.Instance.GetEntityComponent<CameraComponent>(ComponentManager.Instance.GetEntityWithTag("camera", SceneManager.Instance.GetActiveSceneEntities()));
            foreach(Entity ent in ComponentManager.Instance.GetAllEntitiesWithCertainComp<ModelComponent>())
            {
                ModelComponent modComp = ComponentManager.Instance.GetEntityComponent<ModelComponent>(ent);
                TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);
                Matrix[] chopperTransforms = new Matrix[modComp.Model.Bones.Count];
                modComp.Model.CopyAbsoluteBoneTransformsTo(chopperTransforms);
                float radius = GetMaxMeshRadius(modComp);
                foreach (ModelMesh mesh in modComp.Model.Meshes)
                {
                    foreach (BasicEffect e in mesh.Effects)
                    {
                        e.World = GetParentTransform(modComp, mesh.ParentBone) * transComp.CalcMatrix;

                        e.View = camComp.View;
                        e.Projection = camComp.Proj;

                        e.EnableDefaultLighting();
                        e.PreferPerPixelLighting = true;
                        foreach (EffectPass pass in e.CurrentTechnique.Passes)
                        {
                            pass.Apply();
                            mesh.Draw();
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            DrawModel(gameTime);
        }

        public int Order()
        {
            return 2;
        }
    }
}
