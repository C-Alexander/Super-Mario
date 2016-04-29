﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller.Maths;

namespace Controller
{
    public abstract class Controller
    {
        void Forward()
        {
            
        }

        void Backward()
        {
            
        }

        Vector2 Jump()
        {
            return new Vector2(0, 50);
        }

    }
}
