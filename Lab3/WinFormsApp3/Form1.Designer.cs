namespace WinFormsApp3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            studentBindingSource = new BindingSource(components);
            controlDataTreeTable1 = new ControlsLibraryNet60.Data.ControlDataTreeTable();
            contextMenuStrip1 = new ContextMenuStrip(components);
            bigTextComponent1 = new LabLibrary2.BigTextComponent(components);
            componentTablePdf1 = new ComponentsLibraryNet60.DocumentWithTable.ComponentDocumentWithTableMultiHeaderPdf(components);
            componentChartPieExcel1 = new ComponentsLibraryNet60.DocumentWithChart.ComponentDocumentWithChartPieExcel(components);
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).BeginInit();
            SuspendLayout();
            // 
            // studentBindingSource
            // 
            studentBindingSource.DataSource = typeof(Student);
            // 
            // controlDataTreeTable1
            // 
            controlDataTreeTable1.Location = new Point(13, 14);
            controlDataTreeTable1.Margin = new Padding(4, 5, 4, 5);
            controlDataTreeTable1.Name = "controlDataTreeTable1";
            controlDataTreeTable1.Size = new Size(774, 422);
            controlDataTreeTable1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(controlDataTreeTable1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private BindingSource studentBindingSource;
        private ControlsLibraryNet60.Data.ControlDataTreeTable controlDataTreeTable1;
        private ContextMenuStrip contextMenuStrip1;
        private LabLibrary2.BigTextComponent bigTextComponent1;
        private ComponentsLibraryNet60.DocumentWithTable.ComponentDocumentWithTableMultiHeaderPdf componentTablePdf1;
        private ComponentsLibraryNet60.DocumentWithChart.ComponentDocumentWithChartPieExcel componentChartPieExcel1;
    }
}
