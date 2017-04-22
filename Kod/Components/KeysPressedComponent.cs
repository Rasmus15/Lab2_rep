using Series3D1.Components.Other;
using Series3D1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Components
{
    class KeysPressedComponent : IComponent
    {
        public List<KeyValuePair<InteractiveKeys, IAction>> KeyPressed { get; set; }
    }
}
