using System.Drawing;
using System.Windows.Forms;

namespace NotepadPlus.Overrides
{
    /// <summary>
    /// Класс для переопределения цветов отрисовки.
    /// </summary>
    public class OverrideColors : ProfessionalColorTable
    {
        public override Color ToolStripGradientBegin => Color.FromArgb(45, 45, 45);
        
        public override Color ToolStripGradientEnd => Color.FromArgb(45, 45, 45);

        public override Color ToolStripBorder => Color.FromArgb(30, 30, 30);
        
        public override Color ToolStripDropDownBackground => Color.FromArgb(45, 45, 45);

        public override Color MenuStripGradientBegin => Color.FromArgb(30, 30, 30);

        public override Color MenuStripGradientEnd => Color.FromArgb(30, 30, 30);
        
        public override Color MenuBorder => Color.FromArgb(30, 30, 30);

        public override Color MenuItemBorder => Color.FromArgb(30, 30, 30);
        
        public override Color MenuItemSelected => Color.FromArgb(45, 45, 45);
        
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(75, 75, 75);

        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(75, 75, 75);

        public override Color MenuItemPressedGradientBegin => Color.FromArgb(105, 105, 105);

        public override Color MenuItemPressedGradientEnd => Color.FromArgb(105, 105, 105);

        public override Color ImageMarginGradientBegin => Color.FromArgb(45, 45, 45);

        public override Color ImageMarginGradientMiddle => Color.FromArgb(45, 45, 45);
        
        public override Color ImageMarginGradientEnd => Color.FromArgb(45, 45, 45);

        public override Color SeparatorDark => Color.FromArgb(150, 150, 150);

        public override Color SeparatorLight => Color.FromArgb(150, 150, 150);
    }
}