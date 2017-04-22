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
                transComp.QuaternionMatrix *= rot;
                CheckByTwoPi(rot.X);
                CheckByTwoPi(rot.Y);
                CheckByTwoPi(rot.Z);
                transComp.CalcMatrix = Matrix.CreateScale(transComp.Scaling) * Matrix.CreateFromQuaternion(rot) * Matrix.CreateTranslation(transComp.Position);
            }
            foreach(Entity ent in ComponentManager.Instance.GetAllEntitiesWithCertainComp<ModelComponent>())
            {
                ModelComponent mc = ComponentManager.Instance.GetEntityComponent<ModelComponent>(ent);
                mc.Model.Bones[0].Transform = Matrix.CreateTranslation(mc.Model.Bones[0].Transform.Translation);
                //rotera stora propellern
                mc.Model.Bones[1].Transform = Matrix.CreateTranslation(mc.Model.Bones[1].Transform.Translation) * Matrix.CreateRotationY(mc.Model.Bones[1].Transform.Rotation.Y -0.6f);
                //lilla
                mc.Model.Bones[3].Transform = Matrix.CreateFromYawPitchRoll(5f, 0, 0) * mc.Model.Bones[3].Transform;
            }

        }
        private void CheckByTwoPi(float axis)
        {
            if (MathHelper.TwoPi < axis)
            {
                axis -= MathHelper.TwoPi;
            }
            else if (-MathHelper.TwoPi > axis)
            {
                axis += MathHelper.TwoPi;
            }
        }
    }
}
