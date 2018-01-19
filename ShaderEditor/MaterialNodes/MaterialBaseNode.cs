using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShaderEditor.MaterialNodes
{
    [Serializable]
    class MaterialBaseNode
    {
        public string Name { get; set; }
        public int Year { get; set; }

        public MaterialBaseNode(string name, int year)
        {
            Name = name;
            Year = year;
        }

        public MaterialBaseNode()
        {
            Name = "Test";
            Year = 1993;
        }
    }
}
