using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pong
{
    public partial class DifficultySelectionForm : Form
    {
        public string SelectedDifficulty { get; private set; }

        public DifficultySelectionForm()
        {
            InitializeComponent();
            difficultyComboBox.Items.AddRange(new string[] { "Easy", "Normal", "Hard" });
            difficultyComboBox.SelectedIndex = 1; // Domyślnie "Normal"
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            SelectedDifficulty = difficultyComboBox.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
