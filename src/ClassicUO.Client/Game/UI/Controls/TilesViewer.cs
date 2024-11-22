using ClassicUO.Custom.Model;
using ClassicUO.Game.UI.Gumps;
using ClassicUO.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicUO.Game.UI.Controls
{
    internal class TilesViewer : Control
    {
        public bool IsPartialHue { get; set; }
        public bool ContainsByBounds { get; set; }
        public ushort Hue { get; set; }
        public CustomItens ReplaceItens;
        private readonly int _Width = 25;
        private readonly int _Height = 25;
        private readonly int Padding = 10;
        private int MaxTilesLines = 10;
        public TilesViewer(int x, int y, CustomItens replaceItens)
        {
            X = x;
            Y = y;            
            IsFromServer = true;
            ReplaceItens = replaceItens;            
        }      

        public override bool Draw(UltimaBatcher2D batcher, int x, int y)
        {
            if (IsDisposed)
            {
                return false;
            }

            return InternalDraw(batcher, x, y);
        }

        private void AddBorder(int x, int y, int w, int h)
        {
            #region Boarder
            //TOP
            Add(new Line(x, y, w, 1, Color.White.PackedValue));
            //BOTTOM
            Add(new Line(x, w + y, w, 1, Color.White.PackedValue));
            //LEFT
            Add(new Line(x, x, 1, h, Color.White.PackedValue));
            //RIGHT
            Add(new Line(w + x, y, 1, h, Color.White.PackedValue));
            #endregion
        }

        private bool InternalDraw(UltimaBatcher2D batcher, int x, int y)
        {
            int _x = x;
            int _y = y;
            Vector3 hueVector = ShaderHueTranslator.GetHueVector(0, IsPartialHue, Alpha, true);

            for (int i = 0; i < ReplaceItens.ToReplaceGraphicArray.Count; i++)
            {
                ref readonly var artInfo = ref Client.Game.UO.Arts.GetArt(ReplaceItens.ToReplaceGraphicArray[i]);
               
                if (artInfo.Texture != null)
                {
                    batcher.Draw(
                        artInfo.Texture,
                        new Rectangle(_x, _y, _Width, _Height),
                        artInfo.UV,
                        hueVector
                    );
                }

                //arreda para o proximo
                _x += _Width+Padding;
                //se não tem resto chegou na quantidade por linhas precisa quebrar
                if (i != 0 && i % MaxTilesLines == 0)
                {
                    _y += _Height + Padding;
                    //proxima linha volta para o inicio
                    _x = x;
                }
            }

            return base.Draw(batcher, x, y);
        }
    }
}

