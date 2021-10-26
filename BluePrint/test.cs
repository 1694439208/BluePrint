using CPF;
using CPF.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace 蓝图重制版.BluePrint
{
    class test: CpfObject
    {
        [PropertyMetadata("默认值")]
        public string Test
        {
            get
            {
                return (string)GetValue();
            }
            set
            {
                SetValue(value);
            }
        }

        public void Click()
        {
            Test += "test";
            MessageBox.Show(Test);
        }

    }
}
