using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Series3D1.Systems
{
    class CollisionSystem : IUpdate
    {
        private static int ORDER = 3;

        public int Order()
        {
            return ORDER;
        }

        public void Update(GameTime gametime)
        {
            
        }
    }
}
