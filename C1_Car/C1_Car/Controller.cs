using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C1_Car
{
    public partial class Controller : Form
    {
        Game1 game;
        public Controller(Game1 game)
        {
            this.game = game;
            InitializeComponent();
        }

        public int GetRadius()
        {
            return barRadius.Value;
        }

        public float GetFriction()
        {
            return barFriction.Value/100f;
        }

        public int GetSpeed()
        {
            return barSpeed.Value;
        }

        private void Controller_Load(object sender, EventArgs e)
        {

        }

        private void btnSpawn_Click(object sender, EventArgs e)
        {
            game.SpawnCar();
            barRadius.Enabled = false;
            barFriction.Enabled = false;
            barSpeed.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            barRadius.Enabled = true;
            barFriction.Enabled = true;
            barSpeed.Enabled = true;
            game.RemoveCars();
        }
    }
}
