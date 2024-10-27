namespace WinFormsApp3
{
    partial class StudentForm
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
            comboBoxCourse = new ControlsLibraryNet60.Selected.ControlSelectedComboBoxList();
            textBoxFullName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBoxDescription = new TextBox();
            buttonSave = new Button();
            label4 = new Label();
            controlScholarship = new ControlsLibraryNet60.Input.ControlInputNullableInt();
            label5 = new Label();
            SuspendLayout();
            // 
            // comboBoxCourse
            // 
            comboBoxCourse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxCourse.Location = new Point(14, 204);
            comboBoxCourse.Margin = new Padding(5, 8, 5, 8);
            comboBoxCourse.Name = "comboBoxCourse";
            comboBoxCourse.SelectedElement = "";
            comboBoxCourse.Size = new Size(608, 40);
            comboBoxCourse.TabIndex = 1;
            // 
            // textBoxFullName
            // 
            textBoxFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxFullName.Location = new Point(16, 52);
            textBoxFullName.Name = "textBoxFullName";
            textBoxFullName.Size = new Size(608, 27);
            textBoxFullName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 29);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 3;
            label1.Text = "ФИО";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 105);
            label2.Name = "label2";
            label2.Size = new Size(176, 20);
            label2.TabIndex = 4;
            label2.Text = "Краткая характеристика";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 176);
            label3.Name = "label3";
            label3.Size = new Size(41, 20);
            label3.TabIndex = 5;
            label3.Text = "Курс";
            // 
            // textBoxDescription
            // 
            textBoxDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDescription.Location = new Point(16, 128);
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Size = new Size(608, 27);
            textBoxDescription.TabIndex = 6;
            // 
            // buttonSave
            // 
            buttonSave.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonSave.Location = new Point(29, 384);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(560, 66);
            buttonSave.TabIndex = 10;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 252);
            label4.Name = "label4";
            label4.Size = new Size(84, 20);
            label4.TabIndex = 12;
            label4.Text = "Стипендия";
            // 
            // controlScholarship
            // 
            controlScholarship.Location = new Point(14, 280);
            controlScholarship.Margin = new Padding(5, 8, 5, 8);
            controlScholarship.Name = "controlScholarship";
            controlScholarship.Size = new Size(608, 39);
            controlScholarship.TabIndex = 13;
            controlScholarship.Value = null;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ControlDarkDark;
            label5.Location = new Point(16, 322);
            label5.Name = "label5";
            label5.Size = new Size(174, 15);
            label5.TabIndex = 14;
            label5.Text = "*Отметьте если стипендии нет";
            // 
            // StudentForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(636, 531);
            Controls.Add(label5);
            Controls.Add(controlScholarship);
            Controls.Add(label4);
            Controls.Add(buttonSave);
            Controls.Add(textBoxDescription);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxFullName);
            Controls.Add(comboBoxCourse);
            Name = "StudentForm";
            Text = "StudentForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ControlsLibraryNet60.Selected.ControlSelectedComboBoxList comboBoxCourse;
        private TextBox textBoxFullName;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBoxDescription;
        private Button buttonSave;
        private Label label4;
        private ControlsLibraryNet60.Input.ControlInputNullableInt controlScholarship;
        private Label label5;
    }
}