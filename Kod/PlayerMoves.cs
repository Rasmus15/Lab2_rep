using Series3D1.Components.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series3D1.Enums;
using Series3D1.Entities;
using Series3D1.Managers;
using Series3D1.Components;
using Microsoft.Xna.Framework;

namespace Series3D1
{
    class PlayerMoves : IAction
    {
        private static float MOVEMENT = 2.0f;
        public void PerformAction(InteractiveKeys key, String entityName)
        {
            List<Entity> entities = SceneManager.Instance.GetActiveSceneEntities();
            switch (entityName)
            {
                case "chopper":
                    Entity ent = ComponentManager.Instance.GetEntityWithTag("chopper", entities);
                    TransformComponent t = ComponentManager.Instance.GetEntityComponent<TransformComponent>(ent);
                    CameraComponent c = ComponentManager.Instance.GetEntityComponent<CameraComponent>(ent);
                    switch (key)
                    {
                        case InteractiveKeys.W:
                            t.Position = new Vector3(MOVEMENT, 0, 0);
                            break;
                        case InteractiveKeys.A:
                            t.Position = new Vector3(MOVEMENT, 0, 0);
                            break;
                        case InteractiveKeys.S:
                            t.Position = new Vector3(MOVEMENT, 0, 0);
                            break;
                        case InteractiveKeys.D:
                            t.Position = new Vector3(MOVEMENT, 0, 0);
                            break;
                        case InteractiveKeys.R:
                            t.Position = new Vector3(MOVEMENT, 0, 0);
                            break;
                        case InteractiveKeys.F:
                            t.Position = new Vector3(MOVEMENT, 0, 0);
                            break;
                        case InteractiveKeys.Q:
                            t.Position = new Vector3(MOVEMENT, 0, 0);
                            break;
                        case InteractiveKeys.E:
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
