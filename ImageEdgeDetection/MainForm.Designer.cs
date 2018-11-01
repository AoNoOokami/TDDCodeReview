namespace ImageEdgeDetection
{
    partial class MainForm
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
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.btnOpenOriginal = new System.Windows.Forms.Button();
            this.btnSaveNewImage = new System.Windows.Forms.Button();
            this.cmbEdgeDetection = new System.Windows.Forms.ComboBox();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.filterLabel = new System.Windows.Forms.Label();
            this.edgeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.Transparent;
            this.picPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picPreview.Location = new System.Drawing.Point(12, 12);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(709, 600);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 13;
            this.picPreview.TabStop = false;
            // 
            // btnOpenOriginal
            // 
            this.btnOpenOriginal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenOriginal.Location = new System.Drawing.Point(12, 618);
            this.btnOpenOriginal.Name = "btnOpenOriginal";
            this.btnOpenOriginal.Size = new System.Drawing.Size(207, 123);
            this.btnOpenOriginal.TabIndex = 15;
            this.btnOpenOriginal.Text = "Load image";
            this.btnOpenOriginal.UseVisualStyleBackColor = true;
            this.btnOpenOriginal.Click += new System.EventHandler(this.btnOpenOriginal_Click);
            // 
            // btnSaveNewImage
            // 
            this.btnSaveNewImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNewImage.Location = new System.Drawing.Point(571, 618);
            this.btnSaveNewImage.Name = "btnSaveNewImage";
            this.btnSaveNewImage.Size = new System.Drawing.Size(150, 123);
            this.btnSaveNewImage.TabIndex = 16;
            this.btnSaveNewImage.Text = "Save Image";
            this.btnSaveNewImage.UseVisualStyleBackColor = true;
            this.btnSaveNewImage.Visible = false;
            this.btnSaveNewImage.Click += new System.EventHandler(this.btnSaveNewImage_Click);
            // 
            // cmbEdgeDetection
            // 
            this.cmbEdgeDetection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEdgeDetection.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEdgeDetection.FormattingEnabled = true;
            this.cmbEdgeDetection.Items.AddRange(new object[] {
            "None",
            "Laplacian 3x3",
            "Laplacian 3x3 Grayscale",
            "Laplacian 5x5",
            "Laplacian 5x5 Grayscale",
            "Laplacian of Gaussian",
            "Laplacian 3x3 of Gaussian 3x3",
            "Laplacian 3x3 of Gaussian 5x5 - 1",
            "Laplacian 3x3 of Gaussian 5x5 - 2",
            "Laplacian 5x5 of Gaussian 3x3",
            "Laplacian 5x5 of Gaussian 5x5 - 1",
            "Laplacian 5x5 of Gaussian 5x5 - 2",
            "Sobel 3x3",
            "Sobel 3x3 Grayscale",
            "Prewitt",
            "Prewitt Grayscale",
            "Kirsch",
            "Kirsch Grayscale"});
            this.cmbEdgeDetection.Location = new System.Drawing.Point(225, 704);
            this.cmbEdgeDetection.Name = "cmbEdgeDetection";
            this.cmbEdgeDetection.Size = new System.Drawing.Size(340, 37);
            this.cmbEdgeDetection.TabIndex = 20;
            this.cmbEdgeDetection.Visible = false;
            //TODO added it
            this.cmbEdgeDetection.SelectedIndex = 0;
            this.cmbEdgeDetection.SelectedIndexChanged += new System.EventHandler(this.NeighbourCountValueChangedEventHandler);
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Items.AddRange(new object[] {
            "None",
            "Night Filter",
            "Hell Filter",
            "Miami Filter",
            "Zen Filter",
            "Black and White",
            "Swap Filter",
            "Crazy Filter",
            "Mega Filter Green",
            "Mega Filter Orange",
            "Mega Filter Pink",
            "Mega Filter Custom",
            "Rainbow Filter"});
            this.cmbFilter.Location = new System.Drawing.Point(225, 642);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(340, 37);
            this.cmbFilter.TabIndex = 20;
            this.cmbFilter.Visible = false;
            //todo added it
            this.cmbFilter.SelectedIndex = 0;
            this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.FilterSelectedEventHandler);
            // 
            // filterLabel
            // 
            this.filterLabel.AutoSize = true;
            this.filterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterLabel.Location = new System.Drawing.Point(308, 615);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(168, 29);
            this.filterLabel.TabIndex = 21;
            this.filterLabel.Text = "Choose a filter";
            this.filterLabel.Visible = false;
            // 
            // edgeLabel
            // 
            this.edgeLabel.AutoSize = true;
            this.edgeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edgeLabel.Location = new System.Drawing.Point(269, 677);
            this.edgeLabel.Name = "edgeLabel";
            this.edgeLabel.Size = new System.Drawing.Size(296, 29);
            this.edgeLabel.TabIndex = 22;
            this.edgeLabel.Text = "Choose an edge detection";
            this.edgeLabel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(733, 749);
            this.Controls.Add(this.edgeLabel);
            this.Controls.Add(this.filterLabel);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.cmbEdgeDetection);
            this.Controls.Add(this.btnSaveNewImage);
            this.Controls.Add(this.btnOpenOriginal);
            this.Controls.Add(this.picPreview);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Edge Detection";
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnOpenOriginal;
        private System.Windows.Forms.Button btnSaveNewImage;
        private System.Windows.Forms.ComboBox cmbEdgeDetection;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.Label filterLabel;
        private System.Windows.Forms.Label edgeLabel;
    }
}

