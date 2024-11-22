using ClassicUO.Assets;
using ClassicUO.Game.Data;
using ClassicUO.Game.GameObjects;
using ClassicUO.Game.UI.Controls;
using ClassicUO.Renderer.Gumps;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using static ClassicUO.Game.Data.StaticFilters;
using static ClassicUO.Renderer.UltimaBatcher2D;

namespace ClassicUO.Game.UI.Gumps
{
    internal class StaticFilterGump : Gump
    {
        private readonly World _world;
        private int _gumpEndPosX, _gumpEndPosY;

        private const ushort HUE_FONT = 0xFFFF;
        private const ushort BACKGROUND_COLOR = 999;
        private const ushort GUMP_WIDTH = 500; //X
        private const ushort GUMP_HEIGHT = 500;//Y
        private const ushort GUMP_PADDING = 20;
        private static string Type = "";
        private static ushort Graphic;
        private static TileFlag Flags;
        private int CurrentIndex = -1;
        private int MaxIndex;
        private BaseGameObject _seletedObject;
        public StaticFilterGump(World world, BaseGameObject seletedObject) : base(world, 0, 0)
        {
            _world = world;
            _seletedObject = seletedObject;
            CanMove = true;
            AcceptMouseInput = true;
            AcceptKeyboardInput = true;
            EnsureItens();
            EnsureSelectedObject();
            GumpBuild();
        }

        private void EnsureItens()
        {
            if (WallTiles != null)
            {
                CurrentIndex = 0;
                MaxIndex = WallTiles.Count();
            }
        }

        private static bool EnsureSelectedObject()
        {
            bool result = false;
            if (SelectedObject.Object != null)
            {
                Type = SelectedObject.Object?.GetType().Name;

                if (SelectedObject.Object is Land land)
                {
                    Flags = land.TileData.Flags;
                    Graphic = land.Graphic;
                }
                else if (SelectedObject.Object is Static stat)
                {
                    Flags = stat.ItemData.Flags;
                    Graphic = stat.Graphic;
                }
                else if (SelectedObject.Object is Item item)
                {
                    Flags = item.ItemData.Flags;
                    Graphic = item.Graphic;
                }
                result = true;
            }
            return result;
        }

        #region GumpBuild             
        private void GumpBuild()
        {
            EnsurePositions();
            AddBackGround();
            AddBorder();
            AddHeader();
            AddBody();
            AddFooter();
        }
        private void EnsurePositions()
        {
            _gumpEndPosX = X + GUMP_WIDTH;
            _gumpEndPosY = Y + GUMP_HEIGHT;
        }
        private void AddBackGround()
        {
            Add(new AlphaBlendControl(0.95f) { X = 1, Y = 1, Width = GUMP_WIDTH, Height = GUMP_HEIGHT, Hue = BACKGROUND_COLOR, AcceptMouseInput = true, CanMove = true, CanCloseWithRightClick = true });
        }
        private void AddBorder()
        {
            #region Boarder
            //TOP
            Add(new Line(X, Y, GUMP_WIDTH, 1, Color.White.PackedValue));
            //BOTTOM
            Add(new Line(X, GUMP_WIDTH + Y, GUMP_WIDTH, 1, Color.White.PackedValue));
            //LEFT
            Add(new Line(X, Y, 1, GUMP_HEIGHT, Color.White.PackedValue));
            //RIGHT
            Add(new Line(GUMP_WIDTH + X, Y, 1, GUMP_HEIGHT, Color.White.PackedValue));
            #endregion
        }
        public void AddHeader()
        {
            //Type Description
            Add(new Label("Type:", true, HUE_FONT, 185, 255, FontStyle.BlackBorder) { X = X + 10, Y = Y + 5 });
            string type = SelectedObject.Object?.GetType().Name;
            Add(new NiceButton(X + 40, Y + 5, 50, 20, ButtonAction.ChangeContext, type));
            //Apply
            Add(new NiceButton(_gumpEndPosX - (GUMP_PADDING + 50), Y + 5, 50, 20, ButtonAction.Apply, "APPLY"));
            //Type Description
            Add(new Label("Name:", true, HUE_FONT, 185, 255, FontStyle.BlackBorder) { X = X + 10, Y = Y + 20 });
            Add(new StbTextBox(5, 30, 190, false, hue: 0x034F)
            {
                X = X + 10,
                Y = Y + 5,
                Width = 190,
                Height = 25
            });

            //Add(new Label($"{_gumpInitPosX}/{_gumpEndPosX} - {_gumpInitPosY}/{_gumpEndPosY}", true, HUE_FONT, 185, 255, FontStyle.BlackBorder) { X = _gumpInitPosX + 40, Y = _gumpInitPosY + 5 });
            //Line
            Add(new Line(X, Y + 30, GUMP_WIDTH, 1, Color.Red.PackedValue));
        }
        private void AddBody()
        {
            AddReplaceItens();
        }
        private void AddReplaceItens()
        {

        }
        private void AddFooter()
        {
            Add(new Line(X, _gumpEndPosY - (GUMP_PADDING), GUMP_WIDTH, 1, Color.Gray.PackedValue));
            //Remove
            Add(new NiceButton(X + 5, _gumpEndPosY - GUMP_PADDING, 20, 20, ButtonAction.Remove, "-"));
            //Add
            Add(new NiceButton((X + 5) + 25, _gumpEndPosY - GUMP_PADDING, 20, 20, ButtonAction.Add, "+"));
            //Previous
            Add(new NiceButton(_gumpEndPosX - (GUMP_PADDING + 5) - 25, _gumpEndPosY - GUMP_PADDING, 20, 20, ButtonAction.PreviousPage, "<<"));
            //Next
            Add(new NiceButton(_gumpEndPosX - (GUMP_PADDING + 5), _gumpEndPosY - GUMP_PADDING, 20, 20, ButtonAction.NextPage, ">>"));
        }
        #endregion GumpBuild
        #region ButtonsEvent
        public override void OnButtonClick(int buttonID)
        {
            switch (buttonID)
            {
                case (int)ButtonAction.Add:
                    Add();
                    break;
                case (int)ButtonAction.Remove:
                    Remove();
                    break;
                case (int)ButtonAction.PreviousPage:
                    PreviousPage();
                    break;
                case (int)ButtonAction.NextPage:
                    NextPage();
                    break;
                case (int)ButtonAction.ChangeContext:
                    ChangeContext();
                    break;
                case (int)ButtonAction.Apply:
                    Apply();
                    break;
                default:
                    break;
            }
        }
        private void Apply()
        {
            Client.Game.UO.LoadTileData();
            OptionsGump optionsGump = new(_world);
            optionsGump.Apply();
        }
        private void ChangeContext()
        {

        }
        private void NextPage()
        {

        }
        private void PreviousPage()
        {

        }
        public void Remove()
        {

        }
        private void Add()
        {

        }
        #endregion  ButtonsEvent     
        #region JsonManipulation
        public static void RemoveGraphic()
        {
            if (EnsureSelectedObject())
            {

            }
        }
        #endregion JsonManipulation
    }
}
