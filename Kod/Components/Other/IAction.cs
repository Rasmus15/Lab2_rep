﻿using Series3D1.Entities;
using Series3D1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series3D1.Components.Other
{
    interface IAction
    {
        void PerformAction(InteractiveKeys key, String entity);
    }
}
