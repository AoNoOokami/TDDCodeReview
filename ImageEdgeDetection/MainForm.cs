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
        private Bitmap imageFilterResult = null;

        private OpenFileDialog ofd = new OpenFileDialog();
        private SaveFileDialog sfd = new SaveFileDialog();
        private ImageFormat imgFormat = ImageFormat.Png;

        public MainForm()
        {
            InitializeComponent();
            //TODO to delete
           // cmbEdgeDetection.SelectedIndex = 0;
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
                //TODO not necessary
                //ApplyFilter(true, "Open");
                filterLabel.Visible = true;
                cmbFilter.Visible = true;
                btnSaveNewImage.Visible = true;

            }
            //Todo have changed
            picPreview.Image = originalBitmap;
            previewBitmap = originalBitmap;
        }

        private void ApplyFilter(string sender)
        {
            if (previewBitmap == null || cmbEdgeDetection.SelectedIndex == -1)
            {
                return;
            }
            //todo not necessary
            /*if (preview == true)
             {
                 selectedSource = previewBitmap;
             }
             else
             {
                 selectedSource = originalBitmap;
             }
             */

            selectedSource = previewBitmap;

            if (selectedSource != null)
            {
                /*
                if (sender.Equals("Open"))
                {
                    bitmapResult = selectedSource;
                }  */
                if (sender.Equals("cmbEdge"))
                {
                   // TODO have replace this step in method ApplyEdgeDetection
                    EdgeDetection();
                }
                else if (sender.Equals("cmbFilter"))
                {
                    ColorFilter();
                }
            }
            if (bitmapResult != null)
            {
                //todo not necessary
             //   if (preview == true)
               // {
                    picPreview.Image = bitmapResult;
                //}
            }
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            ApplyFilter("Save");
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
            
                // if cmbEdgeDetection is 'None', method couldn't be applied
                if (cmbEdgeDetection.SelectedItem.ToString() != "None")
                    cmbFilter.Enabled = false;
                else
                    cmbFilter.Enabled = true;

                ApplyFilter("cmbEdge");
        }

        private void FilterSelectedEventHandler(object sender, EventArgs e)
        {
            ApplyFilter("cmbFilter");
            edgeLabel.Visible = true;
            cmbEdgeDetection.Visible = true;
        }

        /* Apply Edge Filter according to cmbEdgeDetection value */
        private void EdgeDetection()
        {
            if (imageFilterResult != null)
                selectedSource = imageFilterResult;

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

        /* Apply Image Filter according to cmbFilter value */
        private void ColorFilter()
        {
            if (imageFilterResult != null)
                selectedSource = originalBitmap;

            switch (cmbFilter.SelectedItem.ToString())
            {
                case "None":
                    bitmapResult = selectedSource;
                    imageFilterResult = null;
                    break;
                case "Night Filter":
                    bitmapResult = selectedSource.NightFilter(false);
                    imageFilterResult = bitmapResult;
                    break;
                case "Hell Filter":
                    bitmapResult = selectedSource.HellFilter();
                    imageFilterResult = bitmapResult;
                    break;
                case "Miami Filter":
                    bitmapResult = selectedSource.MiamiFilter();
                    imageFilterResult = bitmapResult;
                    break;
                case "Zen Filter":
                    bitmapResult = selectedSource.ZenFilter();
                    imageFilterResult = bitmapResult;
                    break;
                case "Black and White":
                    bitmapResult = selectedSource.BlackWhiteFilter();
                    imageFilterResult = bitmapResult;
                    break;
                case "Swap Filter":
                    bitmapResult = selectedSource.SwapFilter();
                    imageFilterResult = bitmapResult;
                    break;
                case "Crazy Filter":
                    bitmapResult = selectedSource.CrazyFilter();
                    imageFilterResult = bitmapResult;
                    break;
                case "Mega Filter Green":
                    bitmapResult = selectedSource.MegaFilterGreen();
                    imageFilterResult = bitmapResult;
                    break;
                case "Mega Filter Orange":
                    bitmapResult = selectedSource.MegaFilterOrange();
                    imageFilterResult = bitmapResult;
                    break;
                case "Mega Filter Pink":
                    bitmapResult = selectedSource.MegaFilterPink();
                    imageFilterResult = bitmapResult;
                    break;
                case "Mega Filter Custom":
                    bitmapResult = selectedSource.MegaFilterCustom();
                    imageFilterResult = bitmapResult;
                    break;
                case "Rainbow Filter":
                    bitmapResult = selectedSource.RainbowFilter();
                    imageFilterResult = bitmapResult;
                    break;
            }
        }
    }
}
