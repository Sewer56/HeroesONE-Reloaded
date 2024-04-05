using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Common;
using HeroesONE_R.Structures.Substructures;
using HeroesONE_R_GUI.Dialogs;
using HeroesONE_R_GUI.Misc;
using Ookii.Dialogs.WinForms;
using Reloaded.Native.WinAPI;
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
        private bool _openedShadowArchive;

        /// <summary>
        /// Set to last used directory or open .one file directory, depending on the 'File Picker Starts At Opened File' option.
        /// </summary>
        private string _lastOpenedDirectory;

        /// <summary>
        /// Set to last directory .one opened/saved in
        /// </summary>
        private string _lastONEDirectory;

        /// <summary>
        /// Internal Drag & Drop for moving cells with mouse
        /// </summary>
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private bool restrictDragAndDrop = false;

        /// <summary>
        /// Sets up the current window and the Reloaded theme.
        /// </summary>
        /// <param name="openFile">Specifies the file to open on launch, if any.</param>
        public MainWindow(string openFile = "")
        {
            InitializeComponent();

            ReloadedDefaultTheme = new Theme();
            Reloaded_GUI.Bindings.WindowsForms.Add(this);
            // Load Reloaded-GUI
            ReloadedDefaultTheme.LoadCurrentTheme();

            // Custom render settings.
            MakeRoundedWindow.RoundWindow(this, 0, 0);
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
                    _lastOpenedDirectory = Path.GetDirectoryName(openFile);
                    _lastONEDirectory = Path.GetDirectoryName(openFile);
                    // If this throws, or there is no file, an empty file is loaded instead.
                }
                catch { Archive = new Archive(CommonRWVersions.Heroes); }
            }
            else
            { Archive = new Archive(CommonRWVersions.Heroes); }

            // hack to resize post init, due to custom component resizing issues
            Width = 362;
            Height = 522;
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
            if (archiveType == ONEArchiveType.Shadow050 || archiveType == ONEArchiveType.Shadow060 && _openedShadowArchive == false)
            {
                if (Properties.Settings.Default.HideWarnings == false)
                {
                    _openedShadowArchive = true;
                    MessageBox.Show("Note: You are opening a Shadow The Hedgehog Archive.\n\n" +
                                    "For some of the .ONE files (such as shadow.one), Shadow The Hedgehog seems to expect a strict file order.\n\n" +
                                    "It is highly recommended you either use the Replace button or reimport the files in the same order as the original when creating new archives and reimporting.");
                }
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
            VistaOpenFileDialog fileDialog = new()
            {
                Title = "Select the individual .ONE file to open",
                Multiselect = false,
                InitialDirectory = _lastOpenedDirectory,
                DefaultExt = ".one",
                Filter = "ONE Archive (*.one)|*.one|All files (*.*)|*.*",
            };

            // Load it if selected.
            if (fileDialog.ShowDialog() == DialogResult.OK)
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
                _lastOpenedDirectory = Path.GetDirectoryName(fileDialog.FileName);
                _lastONEDirectory = Path.GetDirectoryName(fileDialog.FileName);
            }
        }

        /// <summary>
        /// Saves the current ONE Archive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            restrictDragAndDrop = true;
            // Pick ONE file.
            VistaSaveFileDialog fileDialog = new()
            {
                Title = "Save Heroes .ONE File",
                FileName = this.titleBar_Title.Text,
                InitialDirectory = _lastOpenedDirectory,
                DefaultExt = ".one",
                Filter = "Sonic Heroes ONE Archive (*.one)|*.one|All files (*.*)|*.*",
            };

            // Save the file to disk.
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] heroesFile = Archive.BuildHeroesONEArchive().ToArray();
                File.WriteAllBytes(fileDialog.FileName, heroesFile);
                if (!Properties.Settings.Default.OpenAtCurrentFile)
                    _lastOpenedDirectory = Path.GetDirectoryName(fileDialog.FileName);
                _lastONEDirectory = Path.GetDirectoryName(fileDialog.FileName);
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
            {
                selectedRow = box_FileList.SelectedCells[0].RowIndex;
            }

            int lastFirstDisplayedScrollingRowIndex = -1;
            if (box_FileList.RowCount != 0)
            {
                lastFirstDisplayedScrollingRowIndex = box_FileList.FirstDisplayedScrollingRowIndex;
            }

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
                { 
                    box_FileList.Rows[selectedRow].Selected = true;
                    if (lastFirstDisplayedScrollingRowIndex == -1)
                        box_FileList.FirstDisplayedScrollingRowIndex = selectedRow;
                    else
                        box_FileList.FirstDisplayedScrollingRowIndex = lastFirstDisplayedScrollingRowIndex;
                }
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
            restrictDragAndDrop = true;
            // Pick file.
            VistaOpenFileDialog fileDialog = new()
            {
                Title = "Select the file to replace the current file contents with",
                Multiselect = false,
                InitialDirectory = _lastOpenedDirectory,
            };

            // Replace the file
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] newFile = File.ReadAllBytes(fileDialog.FileName);
                ArchiveFile.CompressedData = Prs.Compress(ref newFile);
                if (!Properties.Settings.Default.OpenAtCurrentFile)
                    _lastOpenedDirectory = Path.GetDirectoryName(fileDialog.FileName);
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
                ArchiveFile = Archive.Files[box_FileList.SelectedCells[0].RowIndex];

                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        deleteToolStripMenuItem_Click(null, null);
                        break;

                    case Keys.F2:
                        renameToolStripMenuItem_Click(null, null);
                        break;
                }

                if (e.Modifiers == Keys.Control)
                {
                    if (e.KeyCode == Keys.R)
                        replaceToolStripMenuItem_Click(null, null);

                    else if (e.KeyCode == Keys.E)
                        extractToolStripMenuItem_Click(null, null);
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
            restrictDragAndDrop = true;
            // Pick file(s)
            VistaOpenFileDialog fileDialog = new()
            {
                Title = "Select the files to add to the archive",
                Multiselect = true,
                InitialDirectory = _lastOpenedDirectory,
            };

            // Load file(s)
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ParallelOptions options = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                Parallel.ForEach(fileDialog.FileNames, options, file =>
                {
                    Archive.Files.Add(new ArchiveFile(file));
                });
                if (!Properties.Settings.Default.OpenAtCurrentFile)
                    _lastOpenedDirectory = Path.GetDirectoryName(fileDialog.FileName);
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
            restrictDragAndDrop = true;
            // Select path to extract to.
            VistaSaveFileDialog fileDialog = new()
            {
                Title = "Select path to extract to",
                FileName = ArchiveFile.Name,
                InitialDirectory = _lastOpenedDirectory,
            };

            // Save the file to disk.
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ArchiveFile.WriteToFile(fileDialog.FileName);
                if (!Properties.Settings.Default.OpenAtCurrentFile)
                    _lastOpenedDirectory = Path.GetDirectoryName(fileDialog.FileName);
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
            restrictDragAndDrop = true;
            // Get file name.
            RenameDialog searchBufferDialog = new RenameDialog(ArchiveFile.Name);
            ArchiveFile.Name = searchBufferDialog.ShowDialog();
            UpdateGUI(ref Archive);
        }

        private void saveShadow050ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            restrictDragAndDrop = true;
            // Pick ONE file.
            VistaSaveFileDialog fileDialog = new()
            {
                Title = "Save Shadow 0.50 .ONE File",
                FileName = this.titleBar_Title.Text,
                InitialDirectory = _lastOpenedDirectory,
                Filter = "Shadow The Hedgehog 0.50 ONE Archive (*.one)|*.one|All files (*.*)|*.*",
            };

            // Save the file to disk.
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] shadowFile = Archive.BuildShadowONEArchive(false).ToArray();
                File.WriteAllBytes(fileDialog.FileName, shadowFile);
                if (!Properties.Settings.Default.OpenAtCurrentFile)
                    _lastOpenedDirectory = Path.GetDirectoryName(fileDialog.FileName);
                _lastONEDirectory = Path.GetDirectoryName(fileDialog.FileName);
            }
        }

        private void saveShadow060ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            restrictDragAndDrop = true;
            // Pick ONE file.
            VistaSaveFileDialog fileDialog = new()
            {
                Title = "Save Shadow 0.60 .ONE File",
                FileName = this.titleBar_Title.Text,
                InitialDirectory = _lastOpenedDirectory,
                Filter = "Shadow The Hedgehog 0.60 ONE Archive (*.one)|*.one|All files (*.*)|*.*",
            };

            // Save the file to disk.
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] shadowFile = Archive.BuildShadowONEArchive(true).ToArray();
                File.WriteAllBytes(fileDialog.FileName, shadowFile);
                if (!Properties.Settings.Default.OpenAtCurrentFile)
                    _lastOpenedDirectory = Path.GetDirectoryName(fileDialog.FileName);
                _lastONEDirectory = Path.GetDirectoryName(fileDialog.FileName);
            }
        }

        private void categoryBar_ExtractAll_Click(object sender, EventArgs e)
        {
            restrictDragAndDrop = true;
            // Select path to extract to.
            VistaFolderBrowserDialog fileDialog = new()
            {
                Description = "Select folder to extract to",
                UseDescriptionForTitle = true,
                SelectedPath = _lastOpenedDirectory,
            };

            // Save the file(s) to disk.
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                ParallelOptions options = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                Parallel.ForEach(Archive.Files, options, file =>
                {
                    string finalFilePath = Path.Combine(fileDialog.SelectedPath, file.Name);
                    file.WriteToFile(finalFilePath);
                    if (!Properties.Settings.Default.OpenAtCurrentFile)
                        _lastOpenedDirectory = Path.GetDirectoryName(fileDialog.SelectedPath);
                });
            }
        }

        // Other Keyboard shortcuts
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.Modifiers)
                {
                    case Keys.Shift when e.KeyCode == Keys.S:
                        {
                            // Quick Save (by current archive type, no confirmation)
                            if (saveToolStripMenuItem.Checked)
                                QuickSave(QuickSaveType.Heroes);

                            else if (saveShadow050ToolStripMenuItem.Checked)
                                QuickSave(QuickSaveType.Shadow50);

                            else if (saveShadow060ToolStripMenuItem.Checked)
                                QuickSave(QuickSaveType.Shadow60);

                            System.Media.SystemSounds.Beep.Play();
                            break;
                        }
                    case Keys.Control when e.KeyCode == Keys.S:
                        {
                            // Save (by current archive type)
                            if (saveToolStripMenuItem.Checked)
                                saveToolStripMenuItem_Click(null, null);

                            else if (saveShadow050ToolStripMenuItem.Checked)
                                saveShadow050ToolStripMenuItem_Click(null, null);

                            else if (saveShadow060ToolStripMenuItem.Checked)
                                saveShadow060ToolStripMenuItem_Click(null, null);

                            break;
                        }

                    case Keys.Control when e.KeyCode == Keys.O:
                        openToolStripMenuItem_Click(null, null);
                        break;

                    case Keys.Control when e.KeyCode == Keys.N:
                        newToolStripMenuItem_Click(null, null);
                        break;

                    case Keys.Control when e.KeyCode == Keys.A:
                        categoryBar_AddFiles_Click(null, null);
                        break;

                    case Keys.Control when e.KeyCode == Keys.X:
                        categoryBar_ExtractAll_Click(null, null);
                        break;
                }
            }
            catch
            {
                /* No archive opened/selected. */
            }

        }

        /// <summary>
        /// Save without prompt to last opened directory.
        /// </summary>
        private void QuickSave(QuickSaveType type)
        {
            try
            {
                switch (type)
                {
                    case QuickSaveType.Heroes:
                        {
                            byte[] heroesFile = Archive.BuildHeroesONEArchive().ToArray();
                            File.WriteAllBytes(_lastONEDirectory + '\\' + this.titleBar_Title.Text, heroesFile);
                            break;
                        }
                    case QuickSaveType.Shadow50:
                        {
                            byte[] shadow50File = Archive.BuildShadowONEArchive(false).ToArray();
                            File.WriteAllBytes(_lastONEDirectory + '\\' + this.titleBar_Title.Text, shadow50File);
                            break;
                        }
                    case QuickSaveType.Shadow60:
                        {
                            byte[] shadow60File = Archive.BuildShadowONEArchive(true).ToArray();
                            File.WriteAllBytes(_lastONEDirectory + '\\' + this.titleBar_Title.Text, shadow60File);
                            break;
                        }
                }
            }
            catch
            {
                // Save failed, prompt a failure.
                MessageBox.Show("Save failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private enum QuickSaveType
        {
            Heroes,
            Shadow50,
            Shadow60
        }

        private void hideWarningsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hideWarningsToolStripMenuItem.Checked)
                Properties.Settings.Default.HideWarnings = true;
            else
                Properties.Settings.Default.HideWarnings = false;
            Properties.Settings.Default.Save();
        }

        private void filePickerStartsAtOpenedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePickerStartsAtOpenedFileToolStripMenuItem.Checked)
                Properties.Settings.Default.OpenAtCurrentFile = true;
            else
                Properties.Settings.Default.OpenAtCurrentFile = false;
            Properties.Settings.Default.Save();
        }

        private void replaceSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            restrictDragAndDrop = true;
            MessageBox.Show("This batch action will replace the selected file name in the current file across all .ones in the folder you pick (recursive).\n\n1. Choose the folder to modify .ones\n2. Choose the replacement file content");
            try
            {
                ArchiveFile = Archive.Files[box_FileList.SelectedCells[0].RowIndex];
            }
            catch
            {
                return;
            }
            if (ArchiveFile == null)
                return;
            MessageBox.Show("File name match to modify:\n\n" + ArchiveFile.Name);
            VistaFolderBrowserDialog folderDialog = new()
            {
                Description = "Select the folder to scan for .ones (recursive)",
                UseDescriptionForTitle = true,
                SelectedPath = _lastOpenedDirectory,
            };
            if (folderDialog.ShowDialog() != DialogResult.OK)
                return;
            string[] foundOnes = Directory.GetFiles(folderDialog.SelectedPath, "*.one", SearchOption.AllDirectories);

            VistaOpenFileDialog filePicker = new()
            {
                Title = "Select the file content to perform batch replacement",
                Multiselect = false,
                InitialDirectory = _lastOpenedDirectory,
                Filter = "All files (*.*)|*.*",
            };
            if (filePicker.ShowDialog() != DialogResult.OK)
                return;
            byte[] replacementFile = File.ReadAllBytes(filePicker.FileName);
            var prsCompressedDFF = Prs.Compress(ref replacementFile);
            for (int i = 0; i < foundOnes.Length; i++)
            {
                byte[] readFile = File.ReadAllBytes(foundOnes[i]);
                Archive currentONE = Archive.FromONEFile(ref readFile);
                ONEArchiveType archiveType = ONEArchiveTester.GetArchiveType(ref readFile);
                var targetFound = false;
                for (int j = 0; j < currentONE.Files.Count; j++)
                {
                    if (currentONE.Files[j].Name.Equals(ArchiveFile.Name))
                    {
                        targetFound = true;
                        currentONE.Files[j].CompressedData = prsCompressedDFF;
                    }
                }
                List<byte> outputFile;
                if (!targetFound)
                    continue;

                if (archiveType == ONEArchiveType.Shadow050)
                    outputFile = currentONE.BuildShadowONEArchive(false);
                else if (archiveType == ONEArchiveType.Shadow060)
                    outputFile = currentONE.BuildShadowONEArchive(true);
                else
                    outputFile = currentONE.BuildHeroesONEArchive();

                File.WriteAllBytes(foundOnes[i], outputFile.ToArray());
            }
            MessageBox.Show("DONE");
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.HideWarnings)
                hideWarningsToolStripMenuItem.Checked = true;
            else
                hideWarningsToolStripMenuItem.Checked = false;

            if (Properties.Settings.Default.OpenAtCurrentFile)
                filePickerStartsAtOpenedFileToolStripMenuItem.Checked = true;
            else
                filePickerStartsAtOpenedFileToolStripMenuItem.Checked = false;
        }

        private void box_FileList_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = box_FileList.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1 && e.Button == MouseButtons.Left) {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                    dragSize);
            } else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void box_FileList_MouseUp(object sender, MouseEventArgs e)
        {
            dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void box_FileList_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = box_FileList.DoDragDrop(
                    box_FileList.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void box_FileList_DragOver(object sender, DragEventArgs e)
        {
            if (restrictDragAndDrop)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (e.AllowedEffect == DragDropEffects.Move)
                e.Effect = DragDropEffects.Move;
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void box_FileList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var temp = Path.Combine(Path.GetTempPath(), Archive.Files[e.RowIndex].Name);
            File.WriteAllBytes(temp, Archive.Files[e.RowIndex].DecompressThis());
            System.Diagnostics.Process.Start(temp);
        }

        private void box_FileList_DragLeave(object sender, EventArgs e)
        {
            if (Archive.Files.Count == 0)
                return;
            string outfile = Path.Combine(Path.GetTempPath(), Archive.Files[box_FileList.SelectedRows[0].Index].Name);
            File.WriteAllBytes(outfile, Archive.Files[box_FileList.SelectedRows[0].Index].DecompressThis());
            DoDragDrop(new DataObject(DataFormats.FileDrop, new string[] { outfile }), DragDropEffects.Copy);
            restrictDragAndDrop = true;
        }

        /// <summary>
        /// Cause the data to be copied from the source to the target.
        /// </summary>
        private void FileList_DragEnter(object sender, DragEventArgs e)
        {
            if (restrictDragAndDrop)
            {
                restrictDragAndDrop = false;
                e.Effect = DragDropEffects.None;
                return;
            }
            // Contains the paths to the individual files.
            if (e.AllowedEffect == DragDropEffects.Move)
                e.Effect = DragDropEffects.Move;
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Get the file paths of the files that were dropped onto the archiver windows.
        /// </summary>
        private void FileList_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = box_FileList.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
                box_FileList.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                if (rowIndexOfItemUnderMouseToDrop < 0)
                {
                    return;
                }
                ArchiveFile copy = Archive.Files[rowIndexFromMouseDown];
                Archive.Files.RemoveAt(rowIndexFromMouseDown);
                Archive.Files.Insert(rowIndexOfItemUnderMouseToDrop, copy);

            }
            else if (e.Effect == DragDropEffects.Copy)
            {
                // Contains the paths to the individual files.
                List<string> addedFilePaths = ((string[])e.Data.GetData(DataFormats.FileDrop, false)).ToList();

                if (addedFilePaths.Count == 1 && Path.GetFileName(addedFilePaths[0]).ToLower().EndsWith(".one"))
                {
                    try
                    {
                        // Attempt to load the file from disk, parse the .ONE archive and update the GUI.
                        byte[] file = File.ReadAllBytes(addedFilePaths[0]);
                        Archive = Archive.FromONEFile(ref file);
                        // If this throws, or there is no file, Archive will not have been set, so we can return with an error
                        UpdateGUI(ref Archive);
                        this.titleBar_Title.Text = Path.GetFileName(addedFilePaths[0]);
                        SetCheckboxHint(ref file);

                        _lastOpenedDirectory = Path.GetDirectoryName(addedFilePaths[0]);
                        _lastONEDirectory = Path.GetDirectoryName(addedFilePaths[0]);
                        return;
                    } catch
                    {
                        MessageBox.Show("Error loading .one\n If you wanted to add this .one to the current .one, use the 'Add Files' button", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                for (int i = 0; i < Archive.Files.Count; i++)
                {
                    for (int j = addedFilePaths.Count - 1; j >= 0; j--)
                    {
                        if (Archive.Files[i].Name.Equals(Path.GetFileName(addedFilePaths[j]), StringComparison.OrdinalIgnoreCase))
                        {
                            var data = File.ReadAllBytes(addedFilePaths[j]);
                            Archive.Files[i].CompressedData = Prs.Compress(ref data);
                            addedFilePaths.RemoveAt(j);
                            break;
                        }
                    }
                    if (addedFilePaths.Count == 0)
                        break;
                }

                foreach (var file in addedFilePaths)
                {
                    try
                    {
                        var selectedIndex = box_FileList.SelectedRows[0].Index + 1;
                        Archive.Files.Insert(selectedIndex, new ArchiveFile(file));
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Archive.Files.Add(new ArchiveFile(file));
                    }
                }
            }
            UpdateGUI(ref Archive);
        }

        private string[] shadowOneExtensionOrder =
        [
            "SNB",
            "EFD",
            "DFF",
            "TXD",
            "UVA",
            "BIN",
            "CCL",
            "BON",
            "MTN",
            "MTP",
            "DMA",
            "PTP",
            "PTB",
            "BDT",
            "ADB",
            "GNCP"
        ];

        private void SortByExtensionsShadowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Archive == null || Archive.Files.Count == 0)
                return;
            List<ArchiveFile> sortedData = [];
            foreach (var extension in shadowOneExtensionOrder)
            {
                foreach (var file in Archive.Files)
                {
                    if (file.Name.EndsWith(extension))
                    {
                        sortedData.Add(file);
                    }
                }
            }
            if (Archive.Files.Count != sortedData.Count)
            {
                MessageBox.Show("Warning! Unsupported extensions were found in the file, and deleted after the sort operation.\nPlease report this to the developers!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Archive.Files = sortedData;
            UpdateGUI(ref Archive);
        }

        int Mx;
        int My;
        int Cw;
        int Ch;
        int Sw;
        int Sh;

        bool mov;

        private void resizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            My = MousePosition.Y;
            Mx = MousePosition.X;
            Cw = ClientSize.Width;
            Ch = ClientSize.Height;
            Sw = Width;
            Sh = Height;
        }

        private void resizeButton_MouseLeave(object sender, EventArgs e)
        {
            mov = false;
        }

        private void resizeButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == true)
            {
                ClientSize = new Size(MousePosition.X - Mx + Cw, MousePosition.Y - My + Ch);
                Size = new Size(MousePosition.X - Mx + Sw, MousePosition.Y - My + Sh);
            }
        }

        private void resizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            mov = false;
        }
    }
}
