using Graph;
using Graph.Items;
using NGEd;
using System;
using System.Drawing;

namespace ShaderEditor.Nodes
{
    public class SimpleCheckNode : BaseNode
    {
        public SimpleCheckNode(MaterialEditor _ed, int x, int y) :
      base("SimpleCheckNode", _ed, x, y)
        {
        }

        public override void _InitNode(int x, int y)
        {
            this.AddItem(new NodeCheckboxItem("Check 1", true, false) { Tag = 31337 });
            this.AddItem(new NodeCheckboxItem("Check 2", true, false) { Tag = 31337 });
            this.AddItem(new NodeCheckboxItem("Check 3", true, false) { Tag = 22 });

            // Nick: Only float(for texture and etc)
            this.AddItem(new NodeCheckboxItem("Check Float", true, false) { Tag = 23f });

            this.Location = new System.Drawing.Point(x, y);
        }

        private void ImageItem_Clicked(object sender, NodeItemEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}