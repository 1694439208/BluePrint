using CPF;
using CPF.Controls;
using CPF.Drawing;
using System;
using System.Collections.Generic;
using System.Text;
//using 蓝图重制版.BluePrint.Node;

namespace 蓝图重制版.BluePrint
{
    public interface BP_IJoin
    {
        Point GetPos(bool IsZ);
        UIElement GetParnt();

        Control GetThis();
    }
}