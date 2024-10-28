using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace pong
{
    public partial class ResultsForm : Form
    {
        public ResultsForm()
        {
            InitializeComponent();
            LoadResults();
        }

        private void LoadResults()
        {
            string filePath = "game_results.txt";
            if (File.Exists(filePath))
            {
                var results = File.ReadAllLines(filePath).ToList();
                var lastThreeGames = results.Skip(Math.Max(0, results.Count - 24)).ToList();

                // Wyœwietl trzy ostatnie gry
                lblLastThreeGames.Text = string.Join(Environment.NewLine, lastThreeGames);

                // Wyœwietl ostatni wynik gracza
                var lastPlayerScore = lastThreeGames.LastOrDefault(line => line.StartsWith("Wynik gracza:"));
                lblLastPlayerScore.Text = lastPlayerScore;

                // Wyœwietl ostatni wynik przeciwnika
                var lastOpponentScore = lastThreeGames.LastOrDefault(line => line.StartsWith("Wynik przeciwnika:"));
                lblLastOpponentScore.Text = lastOpponentScore;

                // Wyœwietl ostatni wynik gry
                var lastGameResult = lastThreeGames.LastOrDefault(line => line.StartsWith("Liczba rozgrywek:"));
                lblLastGameResult.Text = lastGameResult;
            }
            else
            {
                lblLastThreeGames.Text = "Brak wyników do wyœwietlenia.";
            }
        }

        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            lblLastPlayerScore = new Label();
            groupBox3 = new GroupBox();
            lblLastOpponentScore = new Label();
            groupBox4 = new GroupBox();
            lblLastGameResult = new Label();
            lblLastThreeGames = new RichTextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblLastThreeGames);
            groupBox1.Location = new Point(12, 22);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 157);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "ostatnie trzy wyniki";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblLastPlayerScore);
            groupBox2.Location = new Point(12, 185);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 100);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "wynik gracza";
            // 
            // lblLastPlayerScore
            // 
            lblLastPlayerScore.AutoSize = true;
            lblLastPlayerScore.Location = new Point(6, 49);
            lblLastPlayerScore.Name = "lblLastPlayerScore";
            lblLastPlayerScore.Size = new Size(16, 15);
            lblLastPlayerScore.TabIndex = 0;
            lblLastPlayerScore.Text = "...";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(lblLastOpponentScore);
            groupBox3.Location = new Point(12, 323);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(200, 100);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "wynik komputer";
            // 
            // lblLastOpponentScore
            // 
            lblLastOpponentScore.AutoSize = true;
            lblLastOpponentScore.Location = new Point(6, 49);
            lblLastOpponentScore.Name = "lblLastOpponentScore";
            lblLastOpponentScore.RightToLeft = RightToLeft.No;
            lblLastOpponentScore.Size = new Size(16, 15);
            lblLastOpponentScore.TabIndex = 0;
            lblLastOpponentScore.Text = "...";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(lblLastGameResult);
            groupBox4.Location = new Point(12, 463);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(200, 100);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "wynik gry";
            // 
            // lblLastGameResult
            // 
            lblLastGameResult.AutoSize = true;
            lblLastGameResult.Location = new Point(6, 49);
            lblLastGameResult.Name = "lblLastGameResult";
            lblLastGameResult.Size = new Size(16, 15);
            lblLastGameResult.TabIndex = 0;
            lblLastGameResult.Text = "...";
            // 
            // lblLastThreeGames
            // 
            lblLastThreeGames.Location = new Point(0, 22);
            lblLastThreeGames.Name = "lblLastThreeGames";
            lblLastThreeGames.ReadOnly = true;
            lblLastThreeGames.Size = new Size(194, 129);
            lblLastThreeGames.TabIndex = 1;
            lblLastThreeGames.Text = "";
            // 
            // ResultsForm
            // 
            ClientSize = new Size(314, 611);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "ResultsForm";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label lblLastPlayerScore;
        private GroupBox groupBox3;
        private Label lblLastOpponentScore;
        private GroupBox groupBox4;
        private RichTextBox lblLastThreeGames;
        private Label lblLastGameResult;
    }
}
