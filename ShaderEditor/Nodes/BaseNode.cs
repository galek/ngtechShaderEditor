using Graph;
using NGEd;

namespace ShaderEditor.Nodes
{
    public class BaseNode : Node
    {
        public BaseNode(string _name, MaterialEditor _ed, int x, int y) :
      base(_name)
        {
            _InitNode(x, y);
            _ed.GraphControlFormComp.AddNode(this);
        }

        public virtual void _InitNode(int x, int y)
        {
        }
    }
}