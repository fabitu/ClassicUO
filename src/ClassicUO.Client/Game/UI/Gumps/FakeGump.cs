using ClassicUO.Game.Data.PreferencesJson;
using ClassicUO.Game.GameObjects;
using ClassicUO.Game.Managers;
using ClassicUO.Game.UI.Controls;
using ClassicUO.Input;
using ClassicUO.Renderer.Gumps;
using ClassicUO.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ClassicUO.Game.UI.Gumps
{
    /// <summary>
    /// FakeGump
    /// </summary>
    internal class FakeGump : Gump
    {
        private readonly ushort? _graphic;
        private readonly World _world;
        BaseGameObject _seletedObject;

        public FakeGump(World world, BaseGameObject seletedObject) : base(world, 0, 0)
        {
            _world = world;
            _seletedObject = seletedObject;            
            EnsureContextMenu();
            ShowContextMenu();
        }      

        public FakeGump(World world) : base(world, 0, 0)
        {
            _world = world;
            EnsureContextMenu();
            ShowContextMenu();            
        }

        private void EnsureContextMenu()
        {
            ContextMenu = new ContextMenuControl(this);
            ContextMenu.Add("Open", Open);
            if (_graphic != null) { ContextMenu.Add(ResGumps.Remove, RemoveItem); }
        }

        public void ShowContextMenu()
        {
            ContextMenu?.Show();
        }

        private void Open()
        {
            StaticFilterGump staticFilterGump = UIManager.GetGump<StaticFilterGump>();

            if (staticFilterGump == null)
            {
                staticFilterGump = new StaticFilterGump(_world, _seletedObject)
                {
                    X = Mouse.Position.X,
                    Y = Mouse.Position.Y
                };
                UIManager.Add(staticFilterGump);
                staticFilterGump.SetInScreen();
            }
            else
            {
                staticFilterGump.SetInScreen();
                staticFilterGump.BringOnTop();
            }
        }

        private void RemoveItem()
        {
            if (_graphic != null)
            {
                PreferenceWallManager.RemoveGraphic(_graphic.Value);
                Client.Game.UO.LoadTileData();
                OptionsGump optionsGump = new(_world);
                optionsGump.Apply();
            }
        }
    }
}

