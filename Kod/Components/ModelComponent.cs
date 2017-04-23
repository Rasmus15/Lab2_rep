using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Series3D1.Components
{
    class ModelComponent : IComponent
    {
        public Model Model { get; set; }
        public Matrix[] MeshMatrices { get; set; }

        //VertexBuffer modelVertexBuffer { get; set; }

        public ModelComponent(Model model)
        {
            Model = model;
            MeshMatrices = new Matrix[model.Bones.Count];
        }
    }
}
