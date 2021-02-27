namespace app.Tabaldi.PACT.App.Features.AttendanceAgg
{
    partial class AttendanceUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbPeriodType = new System.Windows.Forms.ComboBox();
            this.panel = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbPeriodType);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Periodo";
            // 
            // cmbPeriodType
            // 
            this.cmbPeriodType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodType.FormattingEnabled = true;
            this.cmbPeriodType.Location = new System.Drawing.Point(17, 19);
            this.cmbPeriodType.Name = "cmbPeriodType";
            this.cmbPeriodType.Size = new System.Drawing.Size(309, 21);
            this.cmbPeriodType.TabIndex = 0;
            this.cmbPeriodType.SelectedIndexChanged += new System.EventHandler(this.cmbPeriodType_SelectedIndexChanged);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Location = new System.Drawing.Point(3, 58);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(935, 546);
            this.panel.TabIndex = 1;
            // 
            // AttendanceUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.groupBox1);
            this.Name = "AttendanceUserControl";
            this.Size = new System.Drawing.Size(942, 609);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbPeriodType;
        private System.Windows.Forms.Panel panel;
    }
}
