using System;

namespace ShaderEditor.MaterialNodes
{
    [Serializable]
    internal class MaterialBaseNode
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