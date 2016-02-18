using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MineSweeper
{
    class GCanvas
    {
        private Graphics drawHandler;
        public GCanvas(Graphics g)
        {
            drawHandler = g;
        }

        public void render(Tiles[,] tiles, int x, int y)
        {
            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                {
                    drawHandler.DrawImage(tiles[i, j].getTexture(), i * 16, j * 16, 16, 16);
                }
            }
        }
    }
}
