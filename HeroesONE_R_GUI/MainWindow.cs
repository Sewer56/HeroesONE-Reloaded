﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Subsctructures;
using HeroesONE_R_GUI.Dialogs;
using HeroesONE_R_GUI.Misc;
using Microsoft.WindowsAPICodePack.Dialogs;
using Reloaded.Native.WinAPI;
using Reloaded.Paths;
using Reloaded_GUI.Styles.Themes;
using Reloaded_GUI.Utilities.Windows;

namespace HeroesONE_R_GUI
{
    public partial class MainWindow : Form
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

        /// <summary>
        /// Stores the current instance of Reloaded Mod Loader theme.
        /// </summary>
        public Theme ReloadedDefaultTheme;

        /// <summary>
        /// Contains a more native archive representation allowing for easy saving and loading to/from Heroes' ONE structure.
        /// </summary>
        public Archive Archive;

        /// <summary>
        /// Contains the current archive file which we are about to potentially modify.
        /// </summary>
        public ArchiveFile ArchiveFile;

        /// <summary>
        /// Sets up the current window and the Reloaded theme.
        /// </summary>
        /// <param name="openFile">Specifies the file to open on launch, if any.</param>
        public MainWindow(string openFile = "")
        {
            InitializeComponent();

            // Load Reloaded-GUI
            ReloadedDefaultTheme = new Theme();
            Reloaded_GUI.Bindings.WindowsForms.Add(this);
            ReloadedDefaultTheme.LoadCurrentTheme();

            // Custom render settings.
            MakeRoundedWindow.RoundWindow(this, 30, 30);
            categoryBar_MenuStrip.Renderer = new MyRenderer();

            // Initialize default archive.
            if (openFile != "")
            {
                try
                {
                    // Attempt to load the file from disk, parse the .ONE archive and update the GUI.
                    byte[] file = File.ReadAllBytes(openFile);
                    Archive = Archive.FromHeroesONE(ref file);
                    UpdateGUI(ref Archive);
                    this.titleBar_Title.Text = Path.GetFileName(openFile);

                    // If this throws, or there is no file, an empty file is loaded instead.
                }
                catch { Archive = new Archive(CommonRWVersions.Heroes); }
            }
            else
            { Archive = new Archive(CommonRWVersions.Heroes); } 
        }

        /// <summary>
        /// Specifies a ONE File to be opened and populates the box with it.
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Pick ONE file.
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog
            {
                Title = "Select the individual .ONE file to open.",
                Multiselect = false,
                IsFolderPicker = false
            };

            CommonFileDialogFilter filter = new CommonFileDialogFilter("Sonic Heroes ONE Archive", ".one");
            fileDialog.Filters.Add(filter);

            // Load it if selected.
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ResetONEArchive();
                byte[] oneFile = File.ReadAllBytes(fileDialog.FileName);
                Archive = Archive.FromHeroesONE(ref oneFile);
                UpdateGUI(ref Archive);

                // Set titlebar.
                this.titleBar_Title.Text = Path.GetFileName(fileDialog.FileName);
            }
        }

        /// <summary>
        /// Saves the current ONE Archive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            // Pick ONE file.
            CommonSaveFileDialog fileDialog = new CommonSaveFileDialog
            {
                Title = "Save .ONE File",
                DefaultFileName = this.titleBar_Title.Text
            };

            CommonFileDialogFilter filter = new CommonFileDialogFilter("Sonic Heroes ONE Archive", ".one");
            fileDialog.Filters.Add(filter);

            // Save the file to disk.
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                byte[] heroesFile = Archive.GenerateONEArchive(ref Archive).ToArray();
                File.WriteAllBytes(fileDialog.FileName, heroesFile);
            }
        }

        /// <summary>
        /// Resets the current Archive information when the user selects "New".
        /// </summary>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetONEArchive();
            Archive = new Archive(CommonRWVersions.Heroes);
            UpdateGUI(ref Archive);
        }

        /// <summary>
        /// Resets the <see cref="Archive"/> member of this class to release the current ONE Archive
        /// and frees memory after it.
        /// </summary>
        private void ResetONEArchive()
        {
            box_FileList.Rows.Clear();
            Archive = null;
            GC.Collect();
        }

        /// <summary>
        /// Populates the listbox with the current contents of the Archive class and updates
        /// the other aspects of the GUI to accomodate the new archive.
        /// </summary>
        /// <param name="archive">The archive to populate the listbox from.</param>
        private void UpdateGUI(ref Archive archive)
        {
            // Get selected row if possible.
            int selectedRow = -1;
            if (box_FileList.SelectedCells.Count > 0)
            { selectedRow = box_FileList.SelectedCells[0].RowIndex; }

            // Update list
            box_FileList.Rows.Clear();

            foreach (ArchiveFile file in archive.Files)
            {
                box_FileList.Rows.Add(file.Name, file.RwVersion.ToString());
            }

            // Update other elements.
            titleBar_RWVersion.Text = $"RW Version: {Archive.RwVersion}";
            titleBar_ItemCount.Text = $"Files: {Archive.Files.Count}";

            // Restore row if possible.
            try
            {
                if (selectedRow != -1)
                { box_FileList.Rows[selectedRow].Selected = true; }
            }
            catch { }
        }

        /// <summary>
        /// Displays the right click menu for replacing a file when the user clicks the datagridview cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void box_FileList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Gets our individual file to manipulate.
                ArchiveFile = Archive.Files[e.RowIndex];

                // Get the mouse coordinates and show options.
                var coordinates = box_FileList.PointToClient(Cursor.Position);
                box_MenuStrip.Show(box_FileList, coordinates.X, coordinates.Y);

                // Select row.
                box_FileList.Rows[e.RowIndex].Selected = true;
            }
        }

        /// <summary>
        /// Replaces the compressed contents of an individual archive file with another file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Pick file.
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog { Title = "Select the file to replace the current file contents with." };
            
            // Replace the file
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                byte[] newFile = File.ReadAllBytes(fileDialog.FileName);
                ArchiveFile.CompressedData = Prs.Compress(ref newFile);
            }
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void categoryBar_Close_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);  
        }

        /// <summary>
        /// Deletes the current item from the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Update archive file list
            Archive.Files = Archive.Files.Where(x => x != ArchiveFile).ToList();

            // Backup selected row & update GUI
            int row = box_FileList.SelectedCells[0].RowIndex;
            UpdateGUI(ref Archive);

            // Try reselecting row.
            try { box_FileList.Rows[row].Selected = true; }
            catch { }
        }

        /// <summary>
        /// Checks the current selection and performs operations on it depending on the key pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void box_FileList_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    ArchiveFile = Archive.Files[box_FileList.SelectedCells[0].RowIndex];
                    deleteToolStripMenuItem_Click(null, null);
                }
            }
            catch
            {
                // Likely no cell selected.
            }
        }

        /// <summary>
        /// Open up new dialog box to change RenderWare Version.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeRWVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RWVersionDialog renderWareDialog = new RWVersionDialog(ArchiveFile.RwVersion);
            ArchiveFile.RwVersion = renderWareDialog.ShowDialog();
            UpdateGUI(ref Archive);
        }

        /// <summary>
        /// Allows the list to select a list of files to add to the archive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void categoryBar_AddFiles_Click(object sender, EventArgs e)
        {
            // Pick file(s)
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog
            {
                Title = "Select the files to add to the archive.",
                Multiselect = true
            };

            // Load file(s)
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (string fileLocation in fileDialog.FileNames)
                {
                    Archive.Files.Add(new ArchiveFile(fileLocation));
                }

                UpdateGUI(ref Archive);
            }
        }

        /// <summary>
        /// Allows the user to select 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Select path to extract to.
            CommonSaveFileDialog fileDialog = new CommonSaveFileDialog
            {
                Title = "Select path to extract to.",
                DefaultFileName = ArchiveFile.Name
            };

            // Save the file to disk.
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ArchiveFile.WriteToFile(fileDialog.FileName);
            }
        }

        /// <summary>
        /// Sets the RenderWare version for the current archive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setArchiveRWVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get RW Version
            RWVersionDialog renderWareDialog = new RWVersionDialog(Archive.RwVersion);
            Archive.RwVersion = renderWareDialog.ShowDialog();

            UpdateGUI(ref Archive);
        }

        private void setAllFileRWVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get RW Version
            RWVersionDialog renderWareDialog = new RWVersionDialog(Archive.RwVersion);
            RWVersion fileRwVersion = renderWareDialog.ShowDialog();

            // Loop and set version
            for (int x = 0; x < Archive.Files.Count; x++)
            { Archive.Files[x].RwVersion = fileRwVersion; }

            // Update GUI
            UpdateGUI(ref Archive);
        }

        /// <summary>
        /// Presents a dialog box that allows for manual setting of compression level/rate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manuallySetCompressionLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get search buffer.
            ChangeCompressionRateDialog searchBufferDialog = new ChangeCompressionRateDialog(Prs.SEARCH_BUFFER_SIZE);
            Prs.SEARCH_BUFFER_SIZE = searchBufferDialog.ShowDialog();
        }

        /// <summary>
        /// Toggle adaptive search buffer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enableAdaptiveCompressionLevelToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (enableAdaptiveCompressionLevelToolStripMenuItem.Checked)
                Prs.ADAPTIVE_SEARCH_BUFFER = true;
            else
                Prs.ADAPTIVE_SEARCH_BUFFER = false;
        }

        /// <summary>
        /// Renames an individual file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get file name.
            RenameDialog searchBufferDialog = new RenameDialog(ArchiveFile.Name);
            ArchiveFile.Name = searchBufferDialog.ShowDialog();
            UpdateGUI(ref Archive);
        }
    }
}