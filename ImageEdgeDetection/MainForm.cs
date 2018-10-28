/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ImageEdgeDetection
{
    public partial class MainForm : Form
    {
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        private Bitmap bitmapResult = null;
        private Bitmap selectedSource = null;

        private OpenFileDialog ofd = new OpenFileDialog();
        private SaveFileDialog sfd = new SaveFileDialog();
        private ImageFormat imgFormat = ImageFormat.Png;

        public MainForm()
        {
            InitializeComponent();

            cmbEdgeDetection.SelectedIndex = 0;
        }

        private void btnOpenOriginal_Click(object sender, EventArgs e)
        {
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                previewBitmap = originalBitmap.CopyToSquareCanvas(picPreview.Width);
                picPreview.Image = previewBitmap;
                ApplyFilter(true, "Open");
                filterLabel.Visible = true;
                cmbFilter.Visible = true;
                btnSaveNewImage.Visible = true;

            }
        }

        private void ApplyFilter(bool preview, string sender)
        {
            if (previewBitmap == null || cmbEdgeDetection.SelectedIndex == -1)
            {
                return;
            }

            if (preview == true)
            {
                selectedSource = previewBitmap;
            }
            else
            {
                selectedSource = originalBitmap;
            }

            if (selectedSource != null)
            {
                if (sender.Equals("Open"))
                {
                    bitmapResult = selectedSource;
                }
                else if (sender.Equals("cmbEdge"))
                {
                    switch (cmbEdgeDetection.SelectedItem.ToString())
                    {
                        case "None":
                            bitmapResult = selectedSource;
                            break;
                        case "Laplacian 3x3":
                            bitmapResult = selectedSource.Laplacian3x3Filter(false);
                            break;
                        case "Laplacian 3x3 Grayscale":
                            bitmapResult = selectedSource.Laplacian3x3Filter(true);
                            break;
                        case "Laplacian 5x5":
                            bitmapResult = selectedSource.Laplacian5x5Filter(false);
                            break;
                        case "Laplacian 5x5 Grayscale":
                            bitmapResult = selectedSource.Laplacian5x5Filter(true);
                            break;
                        case "Laplacian of Gaussian":
                            bitmapResult = selectedSource.LaplacianOfGaussianFilter();
                            break;
                        case "Laplacian 3x3 of Gaussian 3x3":
                            bitmapResult = selectedSource.Laplacian3x3OfGaussian3x3Filter();
                            break;
                        case "Laplacian 3x3 of Gaussian 5x5 - 1":
                            bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter1();
                            break;
                        case "Laplacian 3x3 of Gaussian 5x5 - 2":
                            bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter2();
                            break;
                        case "Laplacian 5x5 of Gaussian 3x3":
                            bitmapResult = selectedSource.Laplacian5x5OfGaussian3x3Filter();
                            break;
                        case "Laplacian 5x5 of Gaussian 5x5 - 1":
                            bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter1();
                            break;
                        case "Laplacian 5x5 of Gaussian 5x5 - 2":
                            bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter2();
                            break;
                        case "Sobel 3x3":
                            bitmapResult = selectedSource.Sobel3x3Filter(false);
                            break;
                        case "Sobel 3x3 Grayscale":
                            bitmapResult = selectedSource.Sobel3x3Filter();
                            break;
                        case "Prewitt":
                            bitmapResult = selectedSource.PrewittFilter(false);
                            break;
                        case "Prewitt Grayscale":
                            bitmapResult = selectedSource.PrewittFilter();
                            break;
                        case "Kirsch":
                            bitmapResult = selectedSource.KirschFilter(false);
                            break;
                        case "Kirsch Grayscale":
                            bitmapResult = selectedSource.KirschFilter();
                            break;
                    }
                }
                else if (sender.Equals("cmbFilter"))
                {
                    switch (cmbFilter.SelectedItem.ToString())
                    {
                        case "None":
                            bitmapResult = selectedSource;
                            break;
                        case "Night Filter":
                            bitmapResult = selectedSource.NightFilter(false);
                            break;
                        case "Hell Filter":
                            bitmapResult = selectedSource.HellFilter();
                            break;
                        case "Miami Filter":
                            bitmapResult = selectedSource.MiamiFilter();
                            break;
                        case "Zen Filter":
                            bitmapResult = selectedSource.ZenFilter();
                            break;
                        case "Black and White":
                            bitmapResult = selectedSource.BlackWhiteFilter();
                            break;
                        case "Swap Filter":
                            bitmapResult = selectedSource.SwapFilter();
                            break;
                        case "Crazy Filter":
                            bitmapResult = selectedSource.CrazyFilter();
                            break;
                        case "Mega Filter Green":
                            bitmapResult = selectedSource.MegaFilterGreen();
                            break;
                        case "Mega Filter Orange":
                            bitmapResult = selectedSource.MegaFilterOrange();
                            break;
                        case "Mega Filter Pink":
                            bitmapResult = selectedSource.MegaFilterPink();
                            break;
                        case "Mega Filter Custom":
                            bitmapResult = selectedSource.MegaFilterCustom();
                            break;
                        case "Rainbow Filter":
                            bitmapResult = selectedSource.RainbowFilter();
                            break;
                    }
                }
            }
            if (bitmapResult != null)
            {
                if (preview == true)
                {
                    picPreview.Image = bitmapResult;
                }
            }
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            ApplyFilter(false, "Save");
            if (bitmapResult != null)
            {
                sfd.Title = "Specify a file name and file path";
                sfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
                sfd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();


                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }

                    StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                    bitmapResult.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();

                    bitmapResult = null;
                }
            }
        }

        private void NeighbourCountValueChangedEventHandler(object sender, EventArgs e)
        {
            ApplyFilter(true, "cmbEdge");
        }

        private void FilterSelectedEventHandler(object sender, EventArgs e)
        {
            ApplyFilter(true, "cmbFilter");
            edgeLabel.Visible = true;
            cmbEdgeDetection.Visible = true;
        }

        //TODO create cmbFilter and cmbEdge methods 
    }
}
