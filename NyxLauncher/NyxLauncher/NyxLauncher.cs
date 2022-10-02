using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
//using Microsoft.Extensions.Configuration.Ini;

/**
 * Not focus text size: 220width
 * In focus text size : 170width
 * 
 * Start location: 6; 25
 * 
 * Row length: 10 
 */

// Yea boi it's first LonaRPG launcher!
// Und ja, all code logic writen in one file.
// So, there exist shity code. Sorry.

namespace NyxLauncher
{
    
    public partial class NyxLauncher : Form
    {
#if DEBUG
        readonly bool DEBUG = true;
#else
        readonly bool DEBUG = false;
#endif

        // Main game folder info
        private string m_gamePath           ;
        private string m_modScriptsPath     ;
        private string m_modINIPath         ;
        private string m_gameFolderPath     ;

        //public delegate void AddListItem();
        //public AddListItem myDelegate;
        private FileSystemWatcher m_watcher ;
        private FileSystemWatcher m_watcherINI;
        private Image m_imgCheckBox = 
            global::NyxLauncher.Properties.Resources.istoggle3;

        // Small define
        private string m_gameExeName = "Game.exe"   ;
        private string m_modFoldName = "ModScripts/_Mods" ;
        private string m_ININame = "GameMods.ini";

        // mem buffer
        private Panel m_oldPanel    ;
        private Panel m_panelSelect ;
        private int   m_selectedPos ;

        //private CModRow[] m_rows;
        //List<string> authors = new List<string>();  
        private List<ModRow> m_rows = new List<ModRow>();
        private int m_rowPos = 0;

        private bool canBeManagment = false;

        public NyxLauncher()
        {
            InitializeComponent();
        }

        private void NyxLauncher_Load(object sender, EventArgs e)
        {
            if (DEBUG)
                Console.WriteLine("DEBUG MODE - TOGGLE ON!!!");

            // Set version number on TextLabel
            Assembly thisAssem = typeof(NyxLauncher).Assembly;
            AssemblyName thisAssemName = thisAssem.GetName();

            Version ver = thisAssemName.Version;

            Label VerText = (Label)this.Controls[9];
            VerText.Text = "Version: " + ver;

            // Check - exist path or not
            if (Properties.Settings.Default.GAME_PATH != "null")
            {
                m_gamePath = Properties.Settings.Default.GAME_PATH;
                m_modScriptsPath = Properties.Settings.Default.MOD_PATH;

                int _num = (m_gamePath.Length - m_gameExeName.Length);
                string _pathMod = m_gamePath.Substring(0, _num);

                m_modINIPath = _pathMod + m_ININame;
                m_gameFolderPath = _pathMod;

                Button m_pathButton = (Button)this.Controls[4];
                m_pathButton.Text = m_gamePath;

                // Create file watcher
                CreateFolderWatcher ();
                CreateINIWatcher    ();

                // INI
                GetINIInfo();
            }

            //myDelegate = new AddListItem(GetINIInfo);

        }

        // Get ini info
        private void GetINIInfo()
        {
            m_rows = new List<ModRow>();

            string[] m_config = File.ReadAllLines(m_modINIPath);
            for (int i = 0; i < m_config.Length; i++)
            {
                string[] _ini_info = m_config[i].Split('[', ']');
                if (_ini_info.Length > 1)
                {
                    ModRow newMod = new ModRow();
                    newMod.name = _ini_info[1];

                    string[] _toggle_info = m_config[i+1].Split('=');
                    if (_toggle_info.Length == 2)
                    {
                        newMod.toggle = int.Parse(_toggle_info[1]);

                        // Check, banned or not
                        if (newMod.toggle == 1)
                        {
                            newMod.toggle = 0;
                        }
                        else
                        {
                            newMod.toggle = 1;
                        }
                    }

                    string[] _pri_info = m_config[i + 2].Split('=');
                    if (_pri_info.Length == 2)
                        newMod.priority = int.Parse(_pri_info[1]);

                    m_rows.Add(newMod);
                }
            }

            // Sort for priority
            m_rows = m_rows.OrderBy(ModRow => ModRow.priority).ToList();

            // DEBUG: Check exist mods
            if (DEBUG)
            {
                for (int i = 0; i < m_rows.Count; i++)
                {
                    Console.WriteLine(
                        "Name: " + m_rows[i].name +
                        ", Toggle: " + m_rows[i].toggle +
                        ", Priority: " + m_rows[i].priority);
                }
            }

            UpdateRowList();
        }

        // Change INI file
        private void ChangeINIFile()
        {
            m_rows = m_rows.OrderBy(ModRow => ModRow.priority).ToList();

            // Write file
            string[] _iniLines = new string[(m_rows.Count * 4) + 1];
            for (int i = 0; i < m_rows.Count; i++)
            {
                _iniLines[4 * i] = "[" + m_rows[i].name + "]";
                if (m_rows[i].toggle == 1)
                {
                    m_rows[i].toggle = 0;
                }
                else
                {
                    m_rows[i].toggle = 1;
                }
                _iniLines[4 * i + 1] = "Banned = " + m_rows[i].toggle;
                _iniLines[4 * i + 2] = "LoadOrder = " + m_rows[i].priority;
                _iniLines[4 * i + 3] = "";
            }
            _iniLines[(m_rows.Count * 4)] = "";

            File.WriteAllLines(m_modINIPath, _iniLines);
        }

        // Clear row list
        private void ClearRowList()
        {
            for (int i = 0; i < 10; i++)
            {
                // 0 - togglebox, 1 - name, 2 - scrollpanel
                Panel m_list_panel = (Panel)this.Controls[5].Controls[i];
                m_list_panel.Controls[1].MaximumSize = new Size(220, 0);
                m_list_panel.Visible = false;

                Panel _switchPanel = (Panel)m_list_panel.Controls[2];
                _switchPanel.Visible = false;
            }
        }

        // Update row list
        private void UpdateRowList()
        {
            // Clear row list
            ClearRowList();

            // Check current cursor position
            if (m_selectedPos > m_rows.Count - 1)
            {
                SetSelectPos(m_rows.Count - 1);
            }
            else if (m_selectedPos < 0)
            {
                SetSelectPos(0);
            }

            if (m_rows.Count <= 10)
            {
                for(int i = 0; i < m_rows.Count; i++)
                {
                    // 0 - togglebox, 1 - name, 2 - scrollpanel
                    Panel m_list_panel = (Panel)this.Controls[5].Controls[i];

                    // Button
                    Button _checkBox = (Button)m_list_panel.Controls[0];
                    ChangeToggleBox(_checkBox, m_rows[i].toggle);

                    // Name
                    Label _name = (Label)m_list_panel.Controls[1];
                    _name.Text = m_rows[i].name;
                    _name.MaximumSize = new Size(220, 0);

                    if (m_selectedPos == i && canBeManagment)
                    {
                        Panel _switchPanel = (Panel)m_list_panel.Controls[2];
                        _switchPanel.Visible = true;
                        m_list_panel.Controls[1].MaximumSize = new Size(170, 0);

                        m_panelSelect = m_list_panel;
                    }

                    m_list_panel.Visible = true;
                }
            }
            canBeManagment = true;
        }

        // Get preview.png in mod folder    - used 2
        private void GetPreviewPng()
        {
            PictureBox _imagePreview = (PictureBox)this.Controls[2].Controls[0];

            if (_imagePreview.BackgroundImage != null)
                _imagePreview.BackgroundImage.Dispose();

            if (!canBeManagment) return;
            if (m_selectedPos == null) return;

            _imagePreview.BackgroundImage = Image.FromFile(
                m_modScriptsPath + "/" 
                + m_rows[m_selectedPos].name + "/" 
                + "preview.png");

        }

        // Get info.txt in mod folder       - used 1
        private void GetTextModInfo()
        {
            Label _textPreview = (Label)this.Controls[1].Controls[0];
            _textPreview.Text = "";

            if (!canBeManagment) return;
            if (m_selectedPos == null) return;

            _textPreview.Text = File.ReadAllText(
                m_modScriptsPath + "/"
                + m_rows[m_selectedPos].name + "/"
                + "info.txt");
        }

        // Set select pos
        private void SetSelectPos(int pos)
        {
            m_selectedPos = pos;

            // Get info mod
            GetTextModInfo();
            GetPreviewPng();
        }

        // Create file watcher
        private void CreateFolderWatcher()
        {
            // Delete exist's file watcher
            if (m_watcher != null)
            {
                m_watcher = null;
                GC.Collect();
            }

            // So, now we create new object
            m_watcher = new FileSystemWatcher(m_modScriptsPath);
            // Filter?
            m_watcher.NotifyFilter = //NotifyFilters.Attributes
                                 //| NotifyFilters.CreationTime
                                  NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName;
                                 //| NotifyFilters.LastAccess
                                 //| NotifyFilters.LastWrite
                                 //| NotifyFilters.Security
                                 //| NotifyFilters.Size;
            // Events
            m_watcher.Changed += OnWatcherChanged;
            m_watcher.Created += OnWatcherCreated;
            m_watcher.Deleted += OnWatcherDeleted;
            m_watcher.Renamed += OnWatcherRenamed;
            // Idk realy
            m_watcher.Filter = "";   // Check all files
            m_watcher.IncludeSubdirectories = false;
            m_watcher.EnableRaisingEvents = true;
        }

        private void ChangeFolderWatcher()
        {
            CreateFolderWatcher();
        }

        // Create INI wathcer
        private void CreateINIWatcher()
        {
            // Delete exist's file watcher
            if (m_watcherINI != null)
            {
                m_watcherINI = null;
                GC.Collect();
            }

            // So, now we create new object
            m_watcherINI = new FileSystemWatcher(m_gameFolderPath);
            // Filter?
            m_watcherINI.NotifyFilter = NotifyFilters.Size;

            // Events
            m_watcherINI.Changed += OnINIChanged;
            m_watcherINI.Created += OnINICreated;
            m_watcherINI.Deleted += OnINIDeleted;
            //m_watcherINI.Renamed += OnINIRenamed;
            // Idk realy
            m_watcherINI.Filter = "*.ini";   // Check all files
            m_watcherINI.IncludeSubdirectories = false;
            m_watcherINI.EnableRaisingEvents = true;
        }

        private void ChangeINIWatcher()
        {
            CreateINIWatcher();
        }

        // file changed
        private void OnWatcherChanged(object sender, FileSystemEventArgs e)
        {
            if (DEBUG)
                Console.WriteLine(e.Name);

        }

        // file Created
        private void OnWatcherCreated(object sender, FileSystemEventArgs e)
        {
            if (DEBUG)
                Console.WriteLine(e.Name);
        }

        // file Deleted
        private void OnWatcherDeleted(object sender, FileSystemEventArgs e)
        {
            if (DEBUG)
                Console.WriteLine(e.Name);
        }

        // file Renamed
        private void OnWatcherRenamed(object sender, FileSystemEventArgs e)
        {
            if (DEBUG)
                Console.WriteLine(e.Name);
        }

        // ini changed
        private void OnINIChanged(object sender, FileSystemEventArgs e)
        {
            if (DEBUG) Console.WriteLine(e.Name);

            if (e.Name != m_ININame) return;

            this.Invoke((MethodInvoker)delegate
            {
                this.GetINIInfo();
            });

        }

        // ini Created
        private void OnINICreated(object sender, FileSystemEventArgs e)
        {
            if (DEBUG) Console.WriteLine(e.Name);

            if (e.Name != m_ININame) return;

            this.Invoke((MethodInvoker)delegate
            {
                this.GetINIInfo();
            });
        }

        // ini Deleted
        private void OnINIDeleted(object sender, FileSystemEventArgs e)
        {
            if (DEBUG) Console.WriteLine(e.Name);

            if (e.Name != m_ININame) return;

            //TODO: SOmething code
        }

        // ini Renamed
        /*
        private void OnINIRenamed(object sender, FileSystemEventArgs e)
        {
            if (DEBUG) Console.WriteLine(e.Name);

            if (e.Name != m_ININame) return;

            GetINIInfo();
        }
        */

        // Change game folder
        private void OnChangeFolder(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Game.exe (*.exe)|*.exe";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                // So, now we need check - exist's lonarpg exe or not
                string[] _path = openFileDialog1.FileName.Split('\\');
                string isGameExe = _path[_path.Length-1];

                if (isGameExe != m_gameExeName)
                {
                    Button _pathButton = (Button)this.Controls[4];
                    _pathButton.Text = "No find 'Game.exe'!";

                    // Delete exist's file watcher
                    if (m_watcher != null)
                    {
                        m_watcher = null;
                        GC.Collect();
                    }
                    if (m_watcherINI != null)
                    {
                        m_watcherINI = null;
                        GC.Collect();
                    }

                    // Disable
                    canBeManagment = false;
                    m_gamePath = null;

                    return;
                }

                // Set game path (for exe file)
                m_gamePath = openFileDialog1.FileName;

                // So, again (dirty method, but nevermind)
                int _num = (m_gamePath.Length - m_gameExeName.Length);
                string _pathMod = m_gamePath.Substring(0, _num);

                m_modScriptsPath = _pathMod + m_modFoldName ;
                m_modINIPath     = _pathMod + m_ININame     ;
                m_gameFolderPath = _pathMod;

                // So, now we change button text
                Button m_pathButton = (Button)this.Controls[4];
                m_pathButton.Text = m_gamePath;

                // Save config
                Properties.Settings.Default.GAME_PATH       = m_gamePath        ;
                Properties.Settings.Default.MOD_PATH        = m_modScriptsPath  ;

                Properties.Settings.Default.Save();

                // Change folder watcher
                ChangeFolderWatcher();
                ChangeINIWatcher();

                // Enable
                GetINIInfo();
            }
        }

        // Run Lona's Game.exe 
        private void OnPlayGameExe(object sender, EventArgs e)
        {
            if (m_gamePath == null) return;

            //ProcessStartInfo startInfo = new ProcessStartInfo(m_gamePath);
            //startInfo.CreateNoWindow = false;
           // startInfo.UseShellExecute = false;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(m_gamePath))
                {
                    this.Hide();
                    exeProcess.WaitForExit();
                    this.Show();
                }
            }
            catch
            {
                // Log error.
                this.Show();
            }
        }

        // Change box val
        private void ChangeToggleBox(Button _checkBox, int value)   // That fucntion for only set val
        {
            if (value == 1)
            {
                ChangeToggleBox(_checkBox, true);
            }
            else
            {
                ChangeToggleBox(_checkBox, false);
            }
        }

        private bool ChangeToggleBox(Button _checkBox, bool value)
        {
            if (value == false)
            {
                _checkBox.Image.Dispose();
                _checkBox.Image = null;
                _checkBox.Tag = false;
                return false;
            }
            else
            {
                _checkBox.Image = m_imgCheckBox;
                _checkBox.Tag = true;
                return true;
            }
        }

        private bool ChangeToggleBox(Button _checkBox)
        {
            if ((bool)_checkBox.Tag == true)
            {
                _checkBox.Image.Dispose();
                _checkBox.Image = null;
                _checkBox.Tag = false;
                return false;
            }
            else
            {
                _checkBox.Image = m_imgCheckBox;
                _checkBox.Tag = true;
                return true;
            }
        }
        
        // Check box event handler
        private void OnToggleBoxChanged(object sender, EventArgs e)
        {
            if (DEBUG) Console.WriteLine("CheckBox pressed");
            if (!canBeManagment) return;

            Button _checkBox = (Button)sender;
            bool _result = ChangeToggleBox(_checkBox);

            string[] _panelName = _checkBox.Parent.Name.Split('_');
            int _pos = int.Parse(_panelName[1]);
            int _newState;

            if (_result)
            {
                _newState = 1;
            }
            else
            {
                _newState = 0;
            }
            m_rows[_pos].toggle = _newState;

            ChangeINIFile();
        }

        // Check box event handler
        private void OnLabelSelect(object sender, EventArgs e)
        {
            Panel _panel = (Panel)sender;

            if (m_panelSelect == _panel)
                return;

            // Show new switch buttons
            Panel _switch = (Panel)_panel.Controls[2];
            _switch.Visible = true;
            _panel.Controls[1].MaximumSize = new Size(170, 0);

            // Change old switch buttons to off
            if (m_panelSelect != null)
            {
                Panel m_switch = (Panel)m_panelSelect.Controls[2];
                m_switch.Visible = false;
                m_panelSelect.Controls[1].MaximumSize = new Size(220, 0);
            }

            m_panelSelect = _panel;

            string[] _pos_selected = _panel.Name.Split('_');
            SetSelectPos(m_rowPos + (int.Parse(_pos_selected[1])));
        }

        // Check box text event handler
        private void OnLabelTextSelect(object sender, EventArgs e)
        {
            Label _panel = (Label)sender;

            if (m_panelSelect == ((Panel)_panel.Parent))
                return;

            // Show new switch buttons
            Panel _switch = (Panel)_panel.Parent.Controls[2];
            _switch.Visible = true;
            _panel.MaximumSize = new Size(170, 0);

            // Change old switch buttons to off
            if (m_panelSelect != null)
            {
                Panel m_switch = (Panel)m_panelSelect.Controls[2];
                m_switch.Visible = false;
                m_panelSelect.Controls[1].MaximumSize = new Size(220, 0);
            }

            m_panelSelect = (Panel)_panel.Parent;

            string[] _pos_selected = _panel.Parent.Name.Split('_');
            SetSelectPos(m_rowPos + (int.Parse(_pos_selected[1])));
        }

        // Check mouse position in panel
        private bool MouseIsOverControl(Panel btn)
        {
            return btn.ClientRectangle.Contains(btn.PointToClient(Cursor.Position));
        }

        // Detect mouse focus
        private void OnLabelMouseEnter(object sender, EventArgs e)
        {
            if (m_oldPanel != null)
            {
                m_oldPanel.BorderStyle = BorderStyle.None;
                m_oldPanel = null;
            }

            Panel _panel = (Panel)sender;
            _panel.BorderStyle = BorderStyle.FixedSingle;

            m_oldPanel = _panel;
        }

        // Detect mouse leave
        private void OnLabelMouseLeave(object sender, EventArgs e)
        {
            Panel _panel = (Panel)sender;

            if (MouseIsOverControl(_panel))
                return;

            _panel.BorderStyle = BorderStyle.None;
        }

        // Mouse whell scroll
        /*
        private void OnLabelMouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
             if (e.Delta > 0)//Scrolling Up
             {
            
             }
             else //Scrolling Down
             {
             
             }
        }
        */

        // Change priority up
        private void OnPriorityUp(object sender, EventArgs e)
        {
            if (!canBeManagment) return;
            if ((m_selectedPos + 1) > (m_rows.Count - 1)) return;

            // Change order
            m_rows[m_selectedPos+1].priority--;
            m_rows[m_selectedPos].priority++;
            SetSelectPos(m_selectedPos + 1);

            ChangeINIFile();
        }

        // Change priority down
        private void OnPriorityDown(object sender, EventArgs e)
        {
            if (!canBeManagment) return;
            if ((m_selectedPos - 1) < (0)) return;

            // Change order
            m_rows[m_selectedPos-1].priority++;
            m_rows[m_selectedPos].priority--;
            SetSelectPos(m_selectedPos - 1);

            ChangeINIFile();
        }

        // Shity code. Idk how Studio generate this shit...
        // but this realy shit.

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void ToggleBox_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
