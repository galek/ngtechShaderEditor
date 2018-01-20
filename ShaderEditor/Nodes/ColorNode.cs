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
        /*
        private ColorNode() :
            base("Color")
        {
            _InitNode(200, 50);
        }

        private ColorNode(string text) :
            base(text)
        {
            _InitNode(200, 50);
            this.Title = text;
        }
        */

        public ColorNode(MaterialEditor _ed, int x, int y) :
            base("Color")
        {
            _InitNode(x, y);
            _ed.GraphControlFormComp.AddNode(this);

            //var textureNode = new Node("Texture");
            //textureNode.Location = new Point(300, 150);
            //var imageItem = new NodeImageItem(null/*Properties.Resources.example*/, 64, 64, false, true) { Tag = 1000f };
            //imageItem.Clicked += new EventHandler<NodeItemEventArgs>(OnImgClicked);
            //textureNode.AddItem(imageItem);
            //graphControl.GraphControl.AddNode(textureNode);

        }

        static string Test() { return ""; }

        private void _InitNode(int x, int y)
        {
            MessageBox.Show(string.Format("_InitNode X: {0} Y: {1}", x, y));

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

        void OnConnectionAdding(object sender, AcceptNodeConnectionEventArgs e)
        {
            //e.Cancel = true;
        }

        void OnImgClicked(object sender, NodeItemEventArgs e)
        {
            MessageBox.Show("IMAGE");
        }

        void OnColClicked(object sender, NodeItemEventArgs e)
        {
            MessageBox.Show("Color");
        }

        void OnConnectionRemoved(object sender, AcceptNodeConnectionEventArgs e)
        {
            //e.Cancel = true;
        }

        static int counter = 1;
        void OnConnectionAdded(object sender, AcceptNodeConnectionEventArgs e)
        {
            //e.Cancel = true;
            e.Connection.Name = "Connection " + counter++;
            e.Connection.DoubleClick += new EventHandler<NodeConnectionEventArgs>(OnConnectionDoubleClick);
        }

        void OnConnectionDoubleClick(object sender, NodeConnectionEventArgs e)
        {
            e.Connection.Name = "Connection " + counter++;
        }

    }
}
