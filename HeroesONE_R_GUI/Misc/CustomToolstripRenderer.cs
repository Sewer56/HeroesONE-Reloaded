using System.Drawing;
using System.Windows.Forms;

namespace HeroesONE_R_GUI.Misc
{
    /// <summary>
    /// Custom renderer for the Menustrip items.
    /// </summary>
    public class MyRenderer : ToolStripProfessionalRenderer
    {
        public MyRenderer() : base(new MyColors()) { }
    }

    /// <summary>
    /// Shuts down the default highlight colours.
    /// </summary>
    public class MyColors : ProfessionalColorTable
    {
        public override Color MenuItemBorder
        {
            get { return Color.Transparent; }
        }

        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(30, 0x28, 0x28, 0x28); }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.Transparent; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.Transparent; }
        }
    }
}
