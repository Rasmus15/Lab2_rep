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

namespace Series3D1.Systems
{
    class ModelSystem : IDraw
    {
        //public void LoadContent()
        //{
        //    Entity modEntity = ComponentManager.Instance.GetEntityWithTag("chopper", SceneManager.Instance.GetActiveSceneEntities());
        //    ModelComponent modComp = ComponentManager.Instance.GetEntityComponent<ModelComponent>(modEntity);
        //    foreach (ModelMesh mm in modComp.Model.Meshes)
        //    {
        //        foreach (Effect e in mm.Effects)
        //        {
        //            IEffectLights ieLight = e as IEffectLights;
        //            if (ieLight != null)
        //            {
        //                ieLight.EnableDefaultLighting();
        //            }

        //        }
        //    }
        //}

        private void DrawModelViaMeshes(TransformComponent transcomp, ModelComponent mComp, Matrix proj, Matrix view)
        {
            foreach (ModelMesh mesh in mComp.Model.Meshes)
            {
                foreach (BasicEffect e in mesh.Effects)
                {
                    HeightmapComponent hc = ComponentManager.Instance.GetEntityComponent<HeightmapComponent>(ComponentManager.Instance.GetEntityWithTag("heightmap", SceneManager.Instance.GetActiveSceneEntities()));
                    e.World = mComp.MeshMatrices[mesh.ParentBone.Index] * transcomp.CalcMatrix * hc.World;
                    e.Projection = proj;
                    e.View = view;
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

        private Matrix GetParentTransform(Model m, ModelBone mb)
        {
            return (mb == m.Root) ? mb.Transform :
                mb.Transform * GetParentTransform(m, mb.Parent);
        }

        private void DrawModel(Model m, float radius, Matrix proj, Matrix view)
        {
            m.Draw(Matrix.CreateScale(1.0f / radius), view, proj);
        }

        private float GetMaxMeshRadius(Model m)
        {
            float radius = 0.0f;
            foreach (ModelMesh mesh in m.Meshes)
            {
                if (mesh.BoundingSphere.Radius > radius)
                {
                    radius = mesh.BoundingSphere.Radius;
                }
            }
            return radius;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            List<Entity> entities = ComponentManager.Instance.GetAllEntitiesWithCertainComp<ModelComponent>();
            foreach (Entity ent in entities)
            {
                ModelComponent modelComp = ComponentManager.Instance.GetEntityComponent<ModelComponent>(ent);
                CameraComponent camComp = ComponentManager.Instance.GetEntityComponent<CameraComponent>(ent);
                TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);
                modelComp.Model.CopyAbsoluteBoneTransformsTo(modelComp.MeshMatrices);
                DrawModelViaMeshes(transComp, modelComp, camComp.Proj, camComp.View);
            }
            spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);   
        }

        public int Order()
        {
            return 2;
        }
    }
}
