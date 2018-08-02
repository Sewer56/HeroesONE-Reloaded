using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reloaded.Native.WinAPI;
using Reloaded_GUI.Styles.Themes.ApplyTheme;
using Reloaded_GUI.Utilities.Windows;

namespace HeroesONE_R_GUI.Dialogs
{
    public partial class ChangeCompressionRateDialog : Form
    {

        #region Compositing
        /// <summary>
        /// Gets the creation parameters.
        /// The parameters are overridden to set the window as composited.
        /// Normally this would go into a child window class and other forms would
        /// derive from this, however this has shown to make the VS WinForm designer
        /// to be buggy.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | (int)Constants.WS_EX_COMPOSITED;
                return cp;
            }
        }

        #endregion

        private int integerToManipulate;

        /// <summary>
        /// Creates an instance of the dialog for modifying the value of a single
        /// integer used to specify search buffer size.
        /// </summary>
        /// <param name="initialInteger">The singular integer value to modify.</param>
        public ChangeCompressionRateDialog(int initialInteger)
        {
            InitializeComponent();
            integerToManipulate = initialInteger;
            ApplyTheme.ThemeWindowsForm(this);
            MakeRoundedWindow.RoundWindow(this, 30, 30);
        }

        /// <summary>
        /// Shows the dialog for modifying an individual RenderWare version.
        /// </summary>
        public new int ShowDialog()
        {
            // Set the initial contents of the individual text boxes.
            borderless_BufferSize.Text = Convert.ToString(integerToManipulate);

            base.ShowDialog();

            // Set those contents back.
            return Convert.ToInt32(borderless_BufferSize.Text) % 8192;
        }

        private void item_OpenConfigDirectory_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
