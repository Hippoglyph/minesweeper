using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace MineSweeper
{
    class Game
    {
        private GCanvas gc;
        private Tiles[,] tiles;
        public int tileX;
        public int tileY;
        private int amoutOfBombs;
        private double upperBombLimit = 0.25;
        private double lowerBombLimit = 0.05;
        private bool restart;
        private int tilesFound;
        private bool placebombs;

        struct tileCoord
        {
            public int x;
            public int y;
        }

        public Game()
        {
            reset();
        }

        public void reset()
        {
            tileX = 16;
            tileY = 16;
            tiles = new Tiles[tileX, tileY];
            restart = false;
            for (int i = 0; i < tileX; i++)
            {
                for (int j = 0; j < tileY; j++)
                {
                    tiles[i, j] = new Tiles();
                }
            }
            tilesFound = 0;
            placebombs = true;
        }

        private void init(tileCoord tile)
        {
            generateBombs(tile);
            calculateValue();
        }

        private void calculateValue()
        {
            for(int x = 0; x < tileX; x++)
            {
                for(int y = 0; y < tileY; y++)
                {
                    if (tiles[x, y].isBomb())
                        continue;
                    int value = 0;
                    for(int xx = x-1; xx <= x + 1; xx++)
                    {
                        for (int yy = y - 1; yy <= y + 1; yy++)
                        {
                            if (xx < 0 || yy < 0 || xx >= tileX || yy >= tileY)
                                continue;
                            if (tiles[xx, yy].isBomb())
                                value += 1;
                        }
                    }
                    tiles[x, y].setValue(value);
                }
            }
        }

        private void generateBombs(tileCoord noBTile)
        {
            Random rnd = new Random();
            amoutOfBombs = rnd.Next((int)(lowerBombLimit*(tileX*tileY)), (int)(upperBombLimit * (tileX * tileY)));
            for (int i = 0; i < amoutOfBombs; i++)
            {
                int x = rnd.Next(tileX);
                int y = rnd.Next(tileY);
                if (tiles[x, y].isBomb() || (noBTile.x == x && noBTile.y == y))
                    i--;
                else
                    tiles[x, y].setBomb();
            }
        }

        public void startGraphics(Graphics g)
        {
            gc = new GCanvas(g);
            rerender();
        }

        private void rerender()
        {
            gc.render(tiles, tileX, tileY);
        }

        public void click(MouseEventArgs e, Size screenSize)
        {
            if (restart)
            {
                reset();
                rerender();
                return;
            }
            else if (placebombs)
            {
                tileCoord tile = calculateTileHit(e, screenSize);
                init(tile);
                placebombs = false;
            }

            if (e.Button == MouseButtons.Left)
            {
                tileCoord tile = calculateTileHit(e, screenSize);
                if (tiles[tile.x, tile.y].isVisible())
                    return;
                reveal(tile);
            }
            else if (e.Button == MouseButtons.Right)
            {
                tileCoord tile = calculateTileHit(e, screenSize);
                mark(tile);
            }
                
        }

        private tileCoord calculateTileHit(MouseEventArgs e, Size screenSize)
        {
            int x = tileX * e.X / screenSize.Width;
            int y = tileY * e.Y / screenSize.Height;
            tileCoord tile;
            tile.x = x;
            tile.y = y;
            return tile;
        }

        private void mark(tileCoord tile)
        {
            if (!tiles[tile.x, tile.y].isVisible())
            {
                if (tiles[tile.x, tile.y].isMarked())
                    tiles[tile.x, tile.y].unMark();
                else
                    tiles[tile.x, tile.y].mark();
                rerender();
            }
        }

        private void reveal(tileCoord tile)
        {
            if (tiles[tile.x, tile.y].isMarked())
                return;
            if (tiles[tile.x, tile.y].isBomb())
            {
                lose();
                rerender();
                return;
            }
            DFS(tile.x, tile.y);
            if (tileX * tileY - tilesFound <= amoutOfBombs)
                win();
            rerender();
        }

        private void win()
        {
            foreach(Tiles tile in tiles)
            {
                if (tile.isBomb())
                    tile.mark();
            }
            restart = true;
        }

        private void lose()
        {
            foreach (Tiles tile in tiles){
                if (!tile.isVisible())
                    tile.setVisible();
                if (tile.isMarked())
                    tile.unMark();
            }
            restart = true;
        }

        private void DFS(int x, int y)
        {
            Stack stack = new Stack();
            tileCoord tile;
            tile.x = x;
            tile.y = y;
            stack.Push(tile);
            while (stack.Count != 0)
            {
                tile = (tileCoord)stack.Pop();
                if (!tiles[tile.x, tile.y].isVisible() && !tiles[tile.x, tile.y].isMarked())
                {
                    tilesFound += 1;
                    tiles[tile.x, tile.y].setVisible();
                    if(tiles[tile.x,tile.y].getValue() == 0)
                        pushAdjacent(stack, tile);
                }
            }
        }

        private void pushAdjacent(Stack stack, tileCoord tile)
        {
            int x = tile.x;
            int y = tile.y;
            for (int xx = x - 1; xx <= x + 1; xx++)
            {
                for (int yy = y - 1; yy <= y + 1; yy++)
                {
                    if (xx < 0 || yy < 0 || xx >= tileX || yy >= tileY || tiles[xx,yy].isVisible() || tiles[xx,yy].isMarked())
                        continue;
                    tile.x = xx;
                    tile.y = yy;
                    stack.Push(tile);
                }
            }
        }
    }
}