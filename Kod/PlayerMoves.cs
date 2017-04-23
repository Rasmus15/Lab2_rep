using Series3D1.Components.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series3D1.Entities;
using Series3D1.Managers;
using Series3D1.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Series3D1
{
    class PlayerMoves : IAction
    {
        private static float MOVEMENT = 2.0f;
        public void PerformAction(Keys key, String entityName)
        {
            List<Entity> entities = SceneManager.Instance.GetActiveSceneEntities();
            switch (entityName)
            {
                case "player":
                    Entity ent = ComponentManager.Instance.GetEntityWithTag("player", entities);
                    TransformComponent t = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);
                    CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(ent);
                    PlayerComponent p = ComponentManager.Instance.GetEntityComponent<PlayerComponent>(ent);
                    switch (key)
                    {
                        case Keys.W:
                            t.Position = p.CharDirectionNormal * MOVEMENT;
                            break;
                        case Keys.A:
                            t.Position = p.CharDirectionNormal ;
                            break;
                        case Keys.S:
                            //t.Position = ;
                            break;
                        case Keys.D:
                            t.Position = new Vector3(MOVEMENT, 0, 0);
                            break;
                        case Keys.L:
                            t.Position = new Vector3(MOVEMENT, 0, 0);
                            break;
                        case Keys.Right:
                            t.Position = new Vector3(MOVEMENT, 0, 0);
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
