using Graph;
using Graph.Items;
using NGEd;
using System;
using System.Drawing;

namespace ShaderEditor.Nodes
{
    public class TextureNode : BaseNode
    {
        public TextureNode(MaterialEditor _ed, int x, int y) :
      base("Texture", _ed, x, y)
        {
        }

        public override void _InitNode(int x, int y)
        {
            //MessageBox.Show(string.Format("_InitNode X: {0} Y: {1}", x, y));

            var imageItem = new NodeImageItem(null, 64, 64, false, true) { Tag = 1000f };
            imageItem.Clicked += ImageItem_Clicked;
            this.AddItem(imageItem);

            this.Location = new System.Drawing.Point(x, y);
        }

        private void ImageItem_Clicked(object sender, NodeItemEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}