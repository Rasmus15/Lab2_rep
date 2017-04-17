using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Components
{
    public class BoundingSphere3D : IComponent
    {
        public BoundingSphere BoundingSphere { get; set; }
        public BoundingSphere LocalBoundingSphere { get; set; }

        public BoundingSphere3D(BoundingSphere boundingSphere, BoundingSphere localBoundingSphere)
        {
            BoundingSphere = boundingSphere;
            LocalBoundingSphere = localBoundingSphere;
        }
    }
}
