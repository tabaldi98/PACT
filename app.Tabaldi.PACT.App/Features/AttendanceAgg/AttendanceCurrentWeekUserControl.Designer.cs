namespace app.Tabaldi.PACT.App.Features.AttendanceAgg
{
    partial class AttendanceCurrentWeekUserControl
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
            this.dgAttendances = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgAttendances)).BeginInit();
            this.SuspendLayout();
            // 
            // dgAttendances
            // 
            this.dgAttendances.AllowUserToAddRows = false;
            this.dgAttendances.AllowUserToDeleteRows = false;
            this.dgAttendances.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAttendances.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgAttendances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAttendances.Location = new System.Drawing.Point(3, 0);
            this.dgAttendances.Name = "dgAttendances";
            this.dgAttendances.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAttendances.Size = new System.Drawing.Size(869, 507);
            this.dgAttendances.TabIndex = 1;
            // 
            // AttendanceCurrentDayUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgAttendances);
            this.Name = "AttendanceCurrentDayUserControl";
            this.Size = new System.Drawing.Size(875, 510);
            ((System.ComponentModel.ISupportInitialize)(this.dgAttendances)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgAttendances;
    }
}
