using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGEd;
using Graph;
using Graph.Items;
using System.Drawing;

namespace ShaderEditor.Nodes
{
    public class ColorNode : Node
    {
        private ColorNode() :
            base("Color")
        {
            _InitNode();
        }

        private ColorNode(string text) :
            base(text)
        {
            _InitNode();
            this.Title = text;
        }


        public ColorNode(MaterialEditor _ed) :
            base("Color")
        {
            _InitNode();
            _ed.RegistredNodes.Add(this);
        }

        private void _InitNode()
        {
            this.Location = new Point(200, 50);
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
            graphControl.GraphControl.AddNode(colorNode);

            var textureNode = new Node("Texture");
            textureNode.Location = new Point(300, 150);
            var imageItem = new NodeImageItem(null/*Properties.Resources.example*/, 64, 64, false, true) { Tag = 1000f };
            imageItem.Clicked += new EventHandler<NodeItemEventArgs>(OnImgClicked);
            textureNode.AddItem(imageItem);
            graphControl.GraphControl.AddNode(textureNode);

          
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
