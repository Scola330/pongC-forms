namespace pong
{
    partial class DifficultySelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            difficultyComboBox = new ComboBox();
            label1 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // difficultyComboBox
            // 
            difficultyComboBox.FormattingEnabled = true;
            difficultyComboBox.Location = new Point(35, 49);
            difficultyComboBox.Name = "difficultyComboBox";
            difficultyComboBox.Size = new Size(121, 23);
            difficultyComboBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 31);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 1;
            label1.Text = "trudność:";
            // 
            // button1
            // 
            button1.Location = new Point(189, 49);
            button1.Name = "button1";
            button1.Size = new Size(89, 23);
            button1.TabIndex = 2;
            button1.Text = "zacznij";
            button1.UseVisualStyleBackColor = true;
            button1.Click += startButton_Click;
            // 
            // DifficultySelectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(325, 111);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(difficultyComboBox);
            Name = "DifficultySelectionForm";
            Text = "DifficultySelectionForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox difficultyComboBox;
        private Label label1;
        private Button button1;
    }
}