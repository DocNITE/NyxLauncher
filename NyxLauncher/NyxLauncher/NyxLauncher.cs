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

        private FileSystemWatcher m_watcher ;
        private Image m_imgCheckBox = 
            global::NyxLauncher.Properties.Resources.istoggle3;

        // Small define
        private string m_gameExeName = "Game.exe"   ;
        private string m_modFoldName = "ModScripts" ;

        // mem buffer
        private Panel m_oldPanel    ;
        private Panel m_panelSelect ;

        private CModRow[] m_rows;

        public NyxLauncher()
        {
            InitializeComponent();
        }

        private void NyxLauncher_Load(object sender, EventArgs e)
        {
            if (DEBUG)
                Console.WriteLine("DEBUG MODE - TOGGLE ON!!!");

            CModRow d = new CModRow();
            d = null;

            if (Properties.Settings.Default.GAME_PATH != "null")
            {
                m_gamePath = Properties.Settings.Default.GAME_PATH;
                m_modScriptsPath = Properties.Settings.Default.MOD_PATH;

                Button m_pathButton = (Button)this.Controls[1];
                m_pathButton.Text = m_gamePath;

                // Create file watcher
                CreateFolderWatcher();
            }

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
                    Button _pathButton = (Button)this.Controls[1];
                    _pathButton.Text = "No find 'Game.exe'!";

                    return;
                }

                // Set game path (for exe file)
                m_gamePath = openFileDialog1.FileName;

                // So, again (dirty method, but nevermind)
                int _num = (m_gamePath.Length - m_gameExeName.Length);
                string _pathMod = m_gamePath.Substring(0, _num);

                m_modScriptsPath = _pathMod + m_modFoldName;

                // So, now we change button text
                Button m_pathButton = (Button)this.Controls[1];
                m_pathButton.Text = m_gamePath;

                // Save config
                Properties.Settings.Default.GAME_PATH       = m_gamePath        ;
                Properties.Settings.Default.MOD_PATH        = m_modScriptsPath  ;

                Properties.Settings.Default.Save();

                // Change folder watcher
                ChangeFolderWatcher();
            }
        }

        // Run Lona's Game.exe 
        private void OnPlayGameExe(object sender, EventArgs e)
        {

        }

        // Check box event handler
        private void OnToggleBoxChanged(object sender, EventArgs e)
        {
            if (DEBUG)
                Console.WriteLine("CheckBox pressed");

            Button _checkBox = (Button)sender;
            
            if ((bool)_checkBox.Tag == true)
            {
                _checkBox.Image = null;
                _checkBox.Tag = false;
            }
            else
            {
                _checkBox.Image = m_imgCheckBox;
                _checkBox.Tag = true;
            }

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

            // Change old switch buttons to off
            if (m_panelSelect != null)
            {
                Panel m_switch = (Panel)m_panelSelect.Controls[2];
                m_switch.Visible = false;
            }

            m_panelSelect = _panel;
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

            // Change old switch buttons to off
            if (m_panelSelect != null)
            {
                Panel m_switch = (Panel)m_panelSelect.Controls[2];
                m_switch.Visible = false;
            }

            m_panelSelect = (Panel)_panel.Parent;
        }

        // Check mouse position in panel
        private bool MouseIsOverControl(Panel btn)
        {
            return btn.ClientRectangle.Contains(btn.PointToClient(Cursor.Position));
        }

        // Detect mouse focus
        private void OnLabelMouseEnter(object sender, EventArgs e)
        {
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
    }
}
