using ClassicUO.Game.UI.Controls;
using ClassicUO.Resources;
using Microsoft.Xna.Framework;
using System;

namespace ClassicUO.Game.UI.Gumps
{
    internal class ScreenMatrixGump : TextContainerGump
    {
        private readonly int GUMP_INIT_X;
        private readonly int GUMP_INIT_Y;
        private int GUMP_END_X;
        private int GUMP_END_Y;
        private readonly int GUMP_WIDTH;
        private readonly int GUMP_HEIGHT;
        private readonly uint _linesColor = Color.White.PackedValue;
        private int lineEspaces = 50; //px      

        public void Close()
        {
            CloseWithRightClick();
        }

        public ScreenMatrixGump(World world) : base(world, 0, 0)
        {
            CanCloseWithRightClick = true;
            GUMP_INIT_X = Client.Game.Scene.Camera.Bounds.Width - 1;
            GUMP_INIT_Y = Client.Game.Scene.Camera.Bounds.Height - 1;
            AddLines();
        }

        public ScreenMatrixGump(World world, int x, int y, int width, int height) : base(world, 0, 0)
        {
            CanCloseWithRightClick = true;
            GUMP_INIT_X = x;
            GUMP_INIT_Y = y;
            GUMP_WIDTH = width;
            GUMP_HEIGHT = height;
            AddLines();
        }

        public void EnsurePositions()
        {
            if (GUMP_WIDTH == 0) { GUMP_END_X = Client.Game.Scene.Camera.Bounds.Width - lineEspaces; }
            else { GUMP_END_X = GUMP_INIT_X + GUMP_WIDTH; }
            if (GUMP_HEIGHT == 0) { GUMP_END_X = Client.Game.Scene.Camera.Bounds.Height - lineEspaces; }
            else { GUMP_END_Y = GUMP_INIT_Y + GUMP_HEIGHT; }
        }

        private void AddLines()
        {
            EnsurePositions();
            int initX = GUMP_INIT_X;
            int initY = GUMP_INIT_Y;

            //vertical
            while (initX < GUMP_END_X)
            {
                Add(new Line(initX, initY, 1, 1, Color.Red.PackedValue));
                initX += lineEspaces;
            }

            //horizontal
            while (initY < GUMP_END_Y)
            {
                Add(new Line(initX, initY, 1, 1, Color.Blue.PackedValue));
                initY += lineEspaces;
            }
        }
    }
}
