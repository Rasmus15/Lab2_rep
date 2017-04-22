using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Components
{
    class HeightmapComponent : IComponent
    {

        #region Properties
        public Texture2D HeightMap { get; set; }
        public VertexPositionTexture[] Vertices { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int[] Indices { get; set; }
        public BasicEffect Effect { get; set; }
        public Matrix World { get; set; }
        public float[,] heightMapData { get; set; } 
        #endregion

        public HeightmapComponent(Texture2D heightmap, Texture2D heightmapT, GraphicsDevice gd)
        {
            HeightMap = heightmap;
            Width = heightmap.Width;
            Height = heightmap.Height;
            Effect = new BasicEffect(gd);
            Effect.Texture = heightmapT;
            World = Matrix.Identity;
        }
    }
}
