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
namespace NyxLauncher
{
    public partial class NyxLauncher : Form
    {

        // Main game folder info
        private string m_gamePath;
        private string m_modScriptsPath;
        private Image m_imgCheckBox = global::NyxLauncher.Properties.Resources.istoggle3;

        private string m_gameExeName = "Game.exe";
        private string m_modFoldName = "ModScripts";

        // mem buffer
        private Panel m_oldPanel;
        private Panel m_panelSelect;

        public NyxLauncher()
        {
            InitializeComponent();
        }

        private void NyxLauncher_Load(object sender, EventArgs e)
        {

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
                    return;

                m_gamePath = openFileDialog1.FileName;

                // So, again (dirty method, but nevermind)
                int _num = (m_gamePath.Length - m_gameExeName.Length);
                string _pathMod = m_gamePath.Substring(0, _num);

                m_modScriptsPath = _pathMod + m_modFoldName;

                // So, now we change button text
                Button m_pathButton = (Button)this.Controls[1];
                m_pathButton.Text = m_gamePath;
            }
        }

        // Run Lona's Game.exe 
        private void OnPlayGameExe(object sender, EventArgs e)
        {

        }

        // Check box event handler
        private void OnToggleBoxChanged(object sender, EventArgs e)
        {
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
