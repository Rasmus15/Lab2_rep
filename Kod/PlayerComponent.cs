using Microsoft.Xna.Framework;
using Series3D1.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1
{
    class PlayerComponent : IComponent
    {
        public Vector3 CharDirectionNormal { get; set; }

        public PlayerComponent(Vector3 dir)
        {
            CharDirectionNormal = dir;
        }
    }
}
