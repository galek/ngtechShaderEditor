namespace ShaderEditor.Tools
{
    public class LastClickPositionHelperC
    {
        public int XPos { get { return xpos; } set { xpos = value; } }
        public int YPos { get { return ypos; } set { ypos = value; } }

        private int xpos = 0;
        private int ypos = 0;
    }
}