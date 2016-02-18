using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class GameForm : Form
    {
        private Game game = new Game();
        public GameForm()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            game.click(e, ClientSize);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            //this.Size = new Size(16 * 16 + 16, 16 * 16 + 16+16+8);
            this.ClientSize = new Size(16 * game.tileX, 16 * game.tileY);
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            game.startGraphics(g);
        }
    }
}
