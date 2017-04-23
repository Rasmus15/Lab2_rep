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
    class CameraSystem : IUpdate
    {
        public CameraSystem()
        {

        }

        public void Update(GameTime gametime)
        {
            
            foreach(Entity ent in ComponentManager.Instance.GetAllEntitiesWithCertainComp<CameraComponent>())
            {
                CameraComponent camComp = ComponentManager.Instance.GetEntityComponent<CameraComponent>(ent);
                TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);
                if (ComponentManager.Instance.CheckEntityHasComponent<ModelComponent>(ent))
                {
                    camComp.View = Matrix.CreateLookAt(transComp.Position + camComp.CameraOffset, transComp.Position, Vector3.Up);
                    camComp.Proj = Matrix.CreatePerspectiveFieldOfView(camComp.FOV, camComp.Ratio, camComp.NearPlane, camComp.FarPlane);
                }
            }
        }

        public int Order()
        {
            return 0;
        }
    }
}
