using Series3D1.Components.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Series3D1.Components
{
    class KeysPressedComponent : IComponent
    {
        public List<KeyValuePair<Keys, IAction>> KeyPressed { get; set; }
    }
}
