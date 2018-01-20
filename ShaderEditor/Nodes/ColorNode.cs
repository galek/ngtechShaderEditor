using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGEd;
using Graph;
using Graph.Items;
using System.Drawing;
using System.Windows;

namespace ShaderEditor.Nodes
{
    public class ColorNode : Node
    {
        public ColorNode(MaterialEditor _ed, int x, int y) :
      base("Color")
        {
            _InitNode(x, y);
            _ed.GraphControlFormComp.AddNode(this);

        }

        private void _InitNode(int x, int y)
        {
            //MessageBox.Show(string.Format("_InitNode X: {0} Y: {1}", x, y));

            this.Location = new System.Drawing.Point(x, y);
            var redChannel = new NodeSliderItem("R", 64.0f, 16.0f, 0, 1.0f, 0.0f, false, false);
            var greenChannel = new NodeSliderItem("G", 64.0f, 16.0f, 0, 1.0f, 0.0f, false, false);
            var blueChannel = new NodeSliderItem("B", 64.0f, 16.0f, 0, 1.0f, 0.0f, false, false);
            var colorItem = new NodeColorItem("Color", Color.Black, false, true) { Tag = 1337 };

            EventHandler<NodeItemEventArgs> channelChangedDelegate = delegate (object sender, NodeItemEventArgs args)
            {
                var red = redChannel.Value;
                var green = blueChannel.Value;
                var blue = greenChannel.Value;
                colorItem.Color = Color.FromArgb((int)Math.Round(red * 255), (int)Math.Round(green * 255), (int)Math.Round(blue * 255));
            };
            redChannel.ValueChanged += channelChangedDelegate;
            greenChannel.ValueChanged += channelChangedDelegate;
            blueChannel.ValueChanged += channelChangedDelegate;

            this.AddItem(redChannel);
            this.AddItem(greenChannel);
            this.AddItem(blueChannel);

            colorItem.Clicked += new EventHandler<NodeItemEventArgs>(OnColClicked);
            this.AddItem(colorItem);
        }

    }
}
