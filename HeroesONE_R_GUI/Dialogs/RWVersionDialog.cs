using System;
using System.Windows.Forms;
using HeroesONE_R.Structures;
using Reloaded.Native.WinAPI;
using Reloaded_GUI.Styles.Themes.ApplyTheme;
using Reloaded_GUI.Utilities.Windows;

namespace HeroesONE_R_GUI.Dialogs
{
    public partial class RWVersionDialog : Form
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

        private RWVersion RenderWareVersion;

        /// <summary>
        /// Creates an instance of the dialog for modifying a given RenderWare version.
        /// Once this is executed, run <see cref="ShowDialog"/>.
        /// </summary>
        /// <param name="renderWareVersion">The RenderWare version entry to modify.</param>
        public RWVersionDialog(RWVersion renderWareVersion)
        {
            InitializeComponent();
            RenderWareVersion = renderWareVersion;
            ApplyTheme.ThemeWindowsForm(this);
            MakeRoundedWindow.RoundWindow(this, 30, 30);
        }

        /// <summary>
        /// Shows the dialog for modifying an individual RenderWare version.
        /// </summary>
        public new RWVersion ShowDialog()
        {
            // Set the initial contents of the individual text boxes.
            borderless_BuildNumber.Text = Convert.ToString(RenderWareVersion.GetBuild());
            borderless_Major.Text = Convert.ToString(RenderWareVersion.GetMajor());
            borderless_Minor.Text = Convert.ToString(RenderWareVersion.GetMinor());
            borderless_Revision.Text = Convert.ToString(RenderWareVersion.GetRevision());
            borderless_Version.Text = Convert.ToString(RenderWareVersion.GetVersion());

            base.ShowDialog();

            // Set those contents back.
            RenderWareVersion.SetBuild(Convert.ToUInt16(borderless_BuildNumber.Text));
            RenderWareVersion.SetMajor(Convert.ToUInt32(borderless_Major.Text));
            RenderWareVersion.SetMinor(Convert.ToUInt32(borderless_Minor.Text));
            RenderWareVersion.SetRevision(Convert.ToUInt32(borderless_Revision.Text));
            RenderWareVersion.SetVersion(Convert.ToUInt32(borderless_Version.Text));

            return RenderWareVersion;
        }

        private void item_OpenConfigDirectory_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
