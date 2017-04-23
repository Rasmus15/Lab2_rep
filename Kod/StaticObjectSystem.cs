using Series3D1.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Series3D1.Managers;
using Series3D1.Entities;
using Series3D1.Components;
using System.Text.RegularExpressions;

namespace Series3D1
{
    class StaticObjectSystem : ISystem, ILoadContent
    {
        public void LoadContent()
        {
            Random ran = new Random();
            HeightmapComponent hc = ComponentManager.Instance.GetEntityComponent<HeightmapComponent>
                (ComponentManager.Instance.GetEntityWithTag("heightmap", SceneManager.Instance.GetActiveSceneEntities()));

            foreach(Entity ent in ComponentManager.Instance.GetAllEntitiesWithCertainComp<ModelComponent>())
            {  
                TagComponent tagc = ComponentManager.Instance.GetEntityComponent<TagComponent>(ent);
                if (tagc.ID.Contains("tree") || tagc.ID.Contains("house") || tagc.ID.Contains("stone"))
                {
                    ModelComponent mc = ComponentManager.Instance.GetEntityComponent<ModelComponent>(ent);
                    TransformComponent tc = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);
                    tc.Position = Vector3.Transform(hc.Vertices[ran.Next(hc.Vertices.Length)].Position, hc.World);
                }
            }
        }

        public int Order()
        {
            return 5;
        }
    }
}
