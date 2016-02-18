using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MineSweeper
{
    class Tiles
    {
        private int value;
        private bool bomb;
        private Bitmap texture;
        private bool visible;
        private bool marked;

        public Tiles()
        {
            value = 0;
            bomb = false;
            visible = false;
            marked = false;
            texture = fetchTexture();
        }

        public Bitmap getTexture()
        {
            return texture;
        }

        public bool isBomb()
        {
            return bomb;
        }

        public void setBomb()
        {
            bomb = true;
            texture = fetchTexture();
        }

        public void setValue(int n)
        {
            value = n;
            texture = fetchTexture();
        }

        public int getValue()
        {
            return value;
        }

        public void mark()
        {
            marked = true;
            texture = fetchTexture();
        }

        public bool isMarked()
        {
            return marked;
        }

        public void unMark()
        {
            marked = false;
            texture = fetchTexture();
        }

        public void setVisible()
        {
            visible = true;
            texture = fetchTexture();
        }

        public bool isVisible()
        {
            return visible;
        }

        private Bitmap fetchTexture()
        {
            if (marked)
                return MineSweeper.Properties.Resources.flag;
            if (!visible)
                return MineSweeper.Properties.Resources.hidden;
            if (bomb)
                return MineSweeper.Properties.Resources.mine;
            switch (value)
            {
                case 1:
                    return MineSweeper.Properties.Resources.empty1;
                case 2:
                    return MineSweeper.Properties.Resources.empty2;
                case 3:
                    return MineSweeper.Properties.Resources.empty3;
                case 4:
                    return MineSweeper.Properties.Resources.empty4;
                case 5:
                    return MineSweeper.Properties.Resources.empty5;
                case 6:
                    return MineSweeper.Properties.Resources.empty6;
                case 7:
                    return MineSweeper.Properties.Resources.empty7;
                case 8:
                    return MineSweeper.Properties.Resources.empty8;
                default:
                    return MineSweeper.Properties.Resources.empty0;
            }
        }
    }
}
