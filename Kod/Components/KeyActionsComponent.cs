using Series3D1.Components.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Series3D1.Components
{
    class KeyActionsComponent : IComponent
    {
        public Dictionary<Keys, IAction> KeyAction{ get; set; }

        public KeyActionsComponent(Dictionary<Keys, IAction> actions)
        {
            KeyAction = actions;
        }
    }
}
