using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace ImageEdgeDetection
{
    /// <summary>
    /// This class calls filters and edge detections to test if the full method
    /// </summary>
    public class Wrapper
    {
        //Test KirschFilter Edge Detection
        public Bitmap KirschFilter(Bitmap image)
        {
            return ExtBitmap.KirschFilter(image);
        }
        //Test Night Filter
        public Bitmap NightFilter(Bitmap sourceBitmap, bool grayscale = true)
        {
            return ExtBitmap.NightFilter(sourceBitmap, grayscale);
        }
        //Test SobelFilter Edge Detection
        public Bitmap SobelFilter(Bitmap sourceBitmap, bool grayscale = true)
        {
            return ExtBitmap.Sobel3x3Filter(sourceBitmap, grayscale);
        }
        //Test Laplacian3x3Filter Edge Detection
        public Bitmap Laplacian3x3Filter(Bitmap sourceBitmap, bool grayscale = true)
        {
            return ExtBitmap.Laplacian3x3Filter(sourceBitmap, grayscale);
        }
        //Test if the generated method and the test method matches to control results
        public void CompareTwoImages(Bitmap img1, Bitmap img2)
        {
            ExtBitmap.CompareTwoImages(img1, img2);
        }
    }
}
