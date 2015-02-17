using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B1_Box
{
    public partial class Controller : Form
    {
        public Controller()
        {
            InitializeComponent();
        }

        private void BarKinetic_Scroll(object sender, EventArgs e)
        {
            if (BarStatic.Value < BarKinetic.Value)
            {
                BarStatic.Value = BarKinetic.Value ;
            }
        }

        private void BarStatic_Scroll(object sender, EventArgs e)
        {
            if (BarStatic.Value < BarKinetic.Value)
            {
                BarKinetic.Value = BarStatic.Value;
            }
        }
    }
}
