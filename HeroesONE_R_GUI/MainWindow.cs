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
using HeroesONE_R.Structures.Common;
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
        /// Set to true once a shadow archive has been opened at least once.
        /// </summary>
        private bool OpenedShadowArchive;

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
                    Archive = Archive.FromONEFile(ref file);
                    UpdateGUI(ref Archive);
                    this.titleBar_Title.Text = Path.GetFileName(openFile);
                    SetCheckboxHint(ref file);

                    // Conditionally display Shadow the Edgehog warning.
                    CheckShadowWarning(ref file);

                    // If this throws, or there is no file, an empty file is loaded instead.
                }
                catch { Archive = new Archive(CommonRWVersions.Heroes); }
            }
            else
            { Archive = new Archive(CommonRWVersions.Heroes); } 
        }

        /// <summary>
        /// Used to conditionally display a warning on opening Shadow The Hedgehog .ONE files pleading the user
        /// to retain the file order.
        /// </summary>
        /// <param name="oneArchive">Byte array containing a .ONE File.</param>
        private void CheckShadowWarning(ref byte[] oneArchive)
        {
            // Know if we're dealing with Shadow050 or Shadow060
            ONEArchiveType archiveType = ONEArchiveTester.GetArchiveType(ref oneArchive);
            if (archiveType == ONEArchiveType.Shadow050 || archiveType == ONEArchiveType.Shadow060 && OpenedShadowArchive == false)
            {
                OpenedShadowArchive = true;
                MessageBox.Show("Note: You are opening a Shadow The Hedgehog Archive.\n\n" +
                                "For some of the .ONE files (such as shadow.one), Shadow The Hedgehog seems to expect a strict file order.\n\n" +
                                "It is highly recommended you either use the Raplace button or reimport the files in the same order as the original when creating new archives and reimporting.");
            }

        }

        /// <summary>
        /// Tries to check the type of .ONE file that the supplied byte array is
        /// and sets the checkbox hint beside the save buttons appropriately.
        /// </summary>
        /// <param name="oneFile"></param>
        private void SetCheckboxHint(ref byte[] oneFile)
        {
            // Update toolstrip save button hint.
            var archiveType = ONEArchiveTester.GetArchiveType(ref oneFile);
            saveShadow050ToolStripMenuItem.Checked = false;
            saveShadow060ToolStripMenuItem.Checked = false;
            saveToolStripMenuItem.Checked = false;

            switch (archiveType)
            {
                case ONEArchiveType.Heroes:
                    saveToolStripMenuItem.Checked = true;
                    break;
                case ONEArchiveType.Shadow050:
                    saveShadow050ToolStripMenuItem.Checked = true;
                    break;
                case ONEArchiveType.Shadow060:
                    saveShadow060ToolStripMenuItem.Checked = true;
                    break;
            }
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
                // Reset archive, load new archive in.
                ResetONEArchive();
                byte[] oneFile = File.ReadAllBytes(fileDialog.FileName);
                Archive = Archive.FromONEFile(ref oneFile);

                // Update the GUI
                UpdateGUI(ref Archive);
                SetCheckboxHint(ref oneFile);

                // Conditionally display Shadow the Edgehog warning.
                CheckShadowWarning(ref oneFile);

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
                Title = "Save Heroes .ONE File",
                DefaultFileName = this.titleBar_Title.Text
            };

            CommonFileDialogFilter filter = new CommonFileDialogFilter("Sonic Heroes ONE Archive", ".one");
            fileDialog.Filters.Add(filter);

            // Save the file to disk.
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                byte[] heroesFile = Archive.BuildHeroesONEArchive().ToArray();
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
                ParallelOptions options = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                Parallel.ForEach(fileDialog.FileNames, options, file =>
                {
                    Archive.Files.Add(new ArchiveFile(file));
                });
             
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

        private void saveShadow050ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Pick ONE file.
            CommonSaveFileDialog fileDialog = new CommonSaveFileDialog
            {
                Title = "Save Shadow 0.50 .ONE File",
                DefaultFileName = this.titleBar_Title.Text
            };

            CommonFileDialogFilter filter = new CommonFileDialogFilter("Shadow The Hedgehog ONE Archive", ".one");
            fileDialog.Filters.Add(filter);

            // Save the file to disk.
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                byte[] shadowFile = Archive.BuildShadowONEArchive(false).ToArray();
                File.WriteAllBytes(fileDialog.FileName, shadowFile);
            }
        }

        private void saveShadow060ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Pick ONE file.
            CommonSaveFileDialog fileDialog = new CommonSaveFileDialog
            {
                Title = "Save Shadow 0.60 .ONE File",
                DefaultFileName = this.titleBar_Title.Text
            };

            CommonFileDialogFilter filter = new CommonFileDialogFilter("Shadow The Hedgehog ONE Archive", ".one");
            fileDialog.Filters.Add(filter);

            // Save the file to disk.
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                byte[] shadowFile = Archive.BuildShadowONEArchive(true).ToArray();
                File.WriteAllBytes(fileDialog.FileName, shadowFile);
            }
        }

        /// <summary>
        /// Cause the data to be copied from the source to the target.
        /// </summary>
        private void FileList_DragEnter(object sender, DragEventArgs e)
        {
            // Contains the paths to the individual files.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Get the file paths of the files that were dropped onto the archiver windows.
        /// </summary>
        private void FileList_DragDrop(object sender, DragEventArgs e)
        {
            // Contains the paths to the individual files.
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string filePath in filePaths)
            { Archive.Files.Add(new ArchiveFile(filePath)); }
            UpdateGUI(ref Archive);
        }

        private void categoryBar_ExtractAll_Click(object sender, EventArgs e)
        {
            // Select path to extract to.
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog
            {
                Title = "Select folder to extract to.",
                IsFolderPicker = true
            };

            // Save the file(s) to disk.
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                ParallelOptions options = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                Parallel.ForEach(Archive.Files, options, file =>
                {
                    string finalFilePath = Path.Combine(fileDialog.FileName, file.Name);
                    file.WriteToFile(finalFilePath);
                });
            }
        }
    }
}
