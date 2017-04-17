using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Series3D1.Components;
using Series3D1.Managers;
using Series3D1.Entities;

namespace Series3D1.Systems
{
    class StaticObjectsSystem : ILoadContent, IDraw
    {
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void LoadContent()
        {
            List<Entity> entities = ComponentManager.Instance.GetAllEntitiesWithCertainComp<TextureComponent>();
            foreach(Entity ent in entities)
            {
                TextureComponent texComp = ComponentManager.Instance.GetEntityComponent<TextureComponent>(ent);
                texComp.Effect.
            }
        }

        public int Order()
        {
            throw new NotImplementedException();
        }
    }
}
