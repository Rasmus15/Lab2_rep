using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Series3D1.Entities;
using Series3D1.Managers;
using Series3D1.Components;
using Series3D1.Components.Other;
using Microsoft.Xna.Framework.Input;

namespace Series3D1.Systems
{
    class KeyBoardSystem : IUpdate
    {
        public int Order()
        {
            return 0;
        }

        public void Update(GameTime gametime)
        {
            Vector3 tempMovement = Vector3.Zero;
            Vector3 tempRotation = Vector3.Zero;
            
            foreach(Entity ent in ComponentManager.Instance.GetAllEntitiesWithCertainComp<KeysPressedComponent>())
            {
                KeysPressedComponent keys = ComponentManager.Instance.GetEntityComponent<KeysPressedComponent>(ent);
                TagComponent tag = ComponentManager.Instance.GetEntityComponent<TagComponent>(ent);
                foreach (KeyValuePair<Keys, IAction> action in keys.KeyPressed)
                {
                    action.Value.PerformAction(action.Key, tag.ID);
                } 
            }
        }
    }
}
