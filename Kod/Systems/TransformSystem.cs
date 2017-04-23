using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Series3D1.Managers;
using Series3D1.Components;
using Series3D1.Entities;

namespace Series3D1.Systems
{
    class TransformSystem : IUpdate
    {
        public int Order()
        {
            throw new NotImplementedException();
        }

        void IUpdate.Update(GameTime gametime)
        {
            List<Entity> entities = SceneManager.Instance.GetActiveSceneEntities();
            foreach (Entity ent in ComponentManager.Instance.GetAllEntitiesWithCertainComp<TransformComponent>())
            {
                TransformComponent transComp = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);

                Quaternion rot = Quaternion.CreateFromYawPitchRoll(transComp.Rotation.Y, transComp.Rotation.X, transComp.Rotation.Z);
                transComp.QRot *= rot;
                transComp.CalcMatrix = Matrix.CreateScale(transComp.Scaling) * Matrix.CreateFromQuaternion(transComp.QRot) * Matrix.CreateTranslation(transComp.Position);
            }
        }
    }
}
