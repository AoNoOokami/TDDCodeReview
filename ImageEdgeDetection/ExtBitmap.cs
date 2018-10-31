/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace ImageEdgeDetection
{
    public static class ExtBitmap
    {
        private static Bitmap tempBitmap;
        
        public static Bitmap CopyToSquareCanvas(this Bitmap sourceBitmap, int canvasWidthLenght)
        {
            float ratio = 1.0f;
            int maxSide = sourceBitmap.Width > sourceBitmap.Height ?
                          sourceBitmap.Width : sourceBitmap.Height;

            ratio = (float)maxSide / (float)canvasWidthLenght;

            Bitmap bitmapResult = (sourceBitmap.Width > sourceBitmap.Height ?
                                    new Bitmap(canvasWidthLenght, (int)(sourceBitmap.Height / ratio))
                                    : new Bitmap((int)(sourceBitmap.Width / ratio), canvasWidthLenght));

            using (Graphics graphicsResult = Graphics.FromImage(bitmapResult))
            {
                graphicsResult.CompositingQuality = CompositingQuality.HighQuality;
                graphicsResult.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsResult.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphicsResult.DrawImage(sourceBitmap,
                                        new Rectangle(0, 0,
                                            bitmapResult.Width, bitmapResult.Height),
                                        new Rectangle(0, 0,
                                            sourceBitmap.Width, sourceBitmap.Height),
                                            GraphicsUnit.Pixel);
                graphicsResult.Flush();
            }

            return bitmapResult;
        }

        private static Bitmap ConvolutionFilter(Bitmap sourceBitmap,
                                             double[,] filterMatrix,
                                                  double factor = 1,
                                                       int bias = 0,
                                             bool grayscale = false)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                 PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            if (grayscale == true)
            {
                float rgb = 0;

                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }

            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;

            int filterWidth = filterMatrix.GetLength(1);
           // dan: value never used 
            // int filterHeight = filterMatrix.GetLength(0);

            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blue = 0;
                    green = 0;
                    red = 0;

                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {

                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            blue += (double)(pixelBuffer[calcOffset]) *
                                    filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            green += (double)(pixelBuffer[calcOffset + 1]) *
                                     filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            red += (double)(pixelBuffer[calcOffset + 2]) *
                                   filterMatrix[filterY + filterOffset,
                                                      filterX + filterOffset];
                        }
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    if (blue > 255)
                    { blue = 255; }
                    else if (blue < 0)
                    { blue = 0; }

                    if (green > 255)
                    { green = 255; }
                    else if (green < 0)
                    { green = 0; }

                    if (red > 255)
                    { red = 255; }
                    else if (red < 0)
                    { red = 0; }

                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                 PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public static Bitmap ConvolutionFilter(this Bitmap sourceBitmap,
                                                double[,] xFilterMatrix,
                                                double[,] yFilterMatrix,
                                                      double factor = 1,
                                                           int bias = 0,
                                                 bool grayscale = false)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                  PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            if (grayscale == true)
            {
                float rgb = 0;

                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;

                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }

            double blueX = 0.0;
            double greenX = 0.0;
            double redX = 0.0;

            double blueY = 0.0;
            double greenY = 0.0;
            double redY = 0.0;

            double blueTotal = 0.0;
            double greenTotal = 0.0;
            double redTotal = 0.0;

            int filterOffset = 1;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blueX = greenX = redX = 0;
                    blueY = greenY = redY = 0;

                    blueTotal = greenTotal = redTotal = 0.0;

                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            blueX += (double)(pixelBuffer[calcOffset]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            greenX += (double)(pixelBuffer[calcOffset + 1]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            redX += (double)(pixelBuffer[calcOffset + 2]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            blueY += (double)(pixelBuffer[calcOffset]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            greenY += (double)(pixelBuffer[calcOffset + 1]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            redY += (double)(pixelBuffer[calcOffset + 2]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];
                        }
                    }

                    blueTotal = Math.Sqrt((blueX * blueX) + (blueY * blueY));
                    greenTotal = Math.Sqrt((greenX * greenX) + (greenY * greenY));
                    redTotal = Math.Sqrt((redX * redX) + (redY * redY));

                    if (blueTotal > 255)
                    { blueTotal = 255; }
                    else if (blueTotal < 0)
                    { blueTotal = 0; }

                    if (greenTotal > 255)
                    { greenTotal = 255; }
                    else if (greenTotal < 0)
                    { greenTotal = 0; }

                    if (redTotal > 255)
                    { redTotal = 255; }
                    else if (redTotal < 0)
                    { redTotal = 0; }

                    resultBuffer[byteOffset] = (byte)(blueTotal);
                    resultBuffer[byteOffset + 1] = (byte)(greenTotal);
                    resultBuffer[byteOffset + 2] = (byte)(redTotal);
                    resultBuffer[byteOffset + 2] = (byte)(redTotal);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                  PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        //apply color filter at your own taste
        public static Bitmap ApplyFilter(Bitmap bmp, int alpha, int red, int blue, int green)
        {

            Bitmap temp = new Bitmap(bmp.Width, bmp.Height);


            for (int i = 0; i < bmp.Width; i++)
            {
                for (int x = 0; x < bmp.Height; x++)
                {
                    Color c = bmp.GetPixel(i, x);
                    Color cLayer = Color.FromArgb(c.A / alpha, c.R / red, c.G / green, c.B / blue);
                    temp.SetPixel(i, x, cLayer);
                }

            }
            return temp;
        }

        public static Bitmap Laplacian3x3Filter(this Bitmap sourceBitmap,
                                                    bool grayscale = true)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                                    Matrix.Laplacian3x3, 1.0, 0, grayscale);

            return resultBitmap;
        }

        public static Bitmap Laplacian5x5Filter(this Bitmap sourceBitmap,
                                                    bool grayscale = true)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                                    Matrix.Laplacian5x5, 1.0, 0, grayscale);

            return resultBitmap;
        }

        public static Bitmap LaplacianOfGaussianFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                                  Matrix.LaplacianOfGaussian, 1.0, 0, true);

            return resultBitmap;
        }

        public static Bitmap Laplacian3x3OfGaussian3x3Filter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                                   Matrix.Gaussian3x3, 1.0 / 16.0, 0, true);

            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap,
                                 Matrix.Laplacian3x3, 1.0, 0, false);

            return resultBitmap;
        }

        public static Bitmap Laplacian3x3OfGaussian5x5Filter1(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                             Matrix.Gaussian5x5Type1, 1.0 / 159.0, 0, true);

            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap,
                                 Matrix.Laplacian3x3, 1.0, 0, false);

            return resultBitmap;
        }

        public static Bitmap Laplacian3x3OfGaussian5x5Filter2(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                             Matrix.Gaussian5x5Type2, 1.0 / 256.0, 0, true);

            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap,
                                 Matrix.Laplacian3x3, 1.0, 0, false);

            return resultBitmap;
        }

        public static Bitmap Laplacian5x5OfGaussian3x3Filter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                                   Matrix.Gaussian3x3, 1.0 / 16.0, 0, true);

            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap,
                                 Matrix.Laplacian5x5, 1.0, 0, false);

            return resultBitmap;
        }

        public static Bitmap Laplacian5x5OfGaussian5x5Filter1(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                             Matrix.Gaussian5x5Type1, 1.0 / 159.0, 0, true);

            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap,
                                 Matrix.Laplacian5x5, 1.0, 0, false);

            return resultBitmap;
        }

        public static Bitmap Laplacian5x5OfGaussian5x5Filter2(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                                                   Matrix.Gaussian5x5Type2,
                                                     1.0 / 256.0, 0, true);

            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap,
                                 Matrix.Laplacian5x5, 1.0, 0, false);

            return resultBitmap;
        }

        public static Bitmap Sobel3x3Filter(this Bitmap sourceBitmap,
                                                bool grayscale = true)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                                                 Matrix.Sobel3x3Horizontal,
                                                   Matrix.Sobel3x3Vertical,
                                                        1.0, 0, grayscale);

            return resultBitmap;
        }

        public static Bitmap PrewittFilter(this Bitmap sourceBitmap,
                                               bool grayscale = true)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                                               Matrix.Prewitt3x3Horizontal,
                                                 Matrix.Prewitt3x3Vertical,
                                                        1.0, 0, grayscale);

            return resultBitmap;
        }

        public static Bitmap KirschFilter(this Bitmap sourceBitmap,
                                              bool grayscale = true)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap,
                                                Matrix.Kirsch3x3Horizontal,
                                                  Matrix.Kirsch3x3Vertical,
                                                        1.0, 0, grayscale);

            return resultBitmap;
        }

        public static Bitmap NightFilter(this Bitmap sourceBitmap, bool grayscale)
        {
            Bitmap resultBitmap = ApplyFilter(new Bitmap(sourceBitmap), 1, 1, 1, 25);

            return resultBitmap;
        }

        public static Bitmap HellFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ApplyFilter(new Bitmap(sourceBitmap), 1, 1, 10, 15);

            return resultBitmap;
        }

        public static Bitmap MiamiFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ApplyFilter(new Bitmap(sourceBitmap), 1, 1, 10, 1);

            return resultBitmap;
        }

        public static Bitmap ZenFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ApplyFilter(new Bitmap(sourceBitmap), 1, 10, 1, 1);

            return resultBitmap;
        }

        public static Bitmap BlackWhiteFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap);

            int rgb;
            Color c;

            for (int y = 0; y < resultBitmap.Height; y++)
            {
                for (int x = 0; x < resultBitmap.Width; x++)
                {
                    c = resultBitmap.GetPixel(x, y);
                    rgb = (int)((c.R + c.G + c.B) / 3);
                    resultBitmap.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            }
            return resultBitmap;
        }

        public static Bitmap SwapFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap);

            for (int x = 0; x < sourceBitmap.Width; x++)
            {
                for (int y = 0; y < sourceBitmap.Height; y++)
                {
                    Color c = sourceBitmap.GetPixel(x, y);
                    Color cLayer = Color.FromArgb(c.A, c.G, c.B, c.R);
                    resultBitmap.SetPixel(x, y, cLayer);
                }
            }
            return resultBitmap;
        }

        public static Bitmap CrazyFilter(this Bitmap sourceBitmap)
        {
            Image temp = ApplyFilterSwapDivide(new Bitmap(sourceBitmap), 1, 1, 2, 1);
            Bitmap resultBitmap = SwapFilter(new Bitmap(temp));

            return resultBitmap;
        }

        public static Bitmap MegaFilterGreen(this Bitmap sourceBitmap)
        {
            Color c = Color.Green;
            Bitmap resultBitmap = ApplyFilterMega(new Bitmap(sourceBitmap), 230, 110, c);

            return resultBitmap;
        }

        public static Bitmap MegaFilterOrange(this Bitmap sourceBitmap)
        {
            Color c = Color.Orange;
            Bitmap resultBitmap = ApplyFilterMega(new Bitmap(sourceBitmap), 230, 110, c);

            return resultBitmap;
        }

        public static Bitmap MegaFilterPink(this Bitmap sourceBitmap)
        {
            Color c = Color.Pink;
            Bitmap resultBitmap = ApplyFilterMega(new Bitmap(sourceBitmap), 230, 110, c);

            return resultBitmap;
        }

        public static Bitmap MegaFilterCustom(this Bitmap sourceBitmap)
        {
            Color c = Color.Black;
            Bitmap resultBitmap = ApplyFilterMega(new Bitmap(sourceBitmap), 230, 110, c);

            return resultBitmap;
        }

        public static Bitmap RainbowFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            int raz = sourceBitmap.Width / 4;

            for (int i = 0; i < sourceBitmap.Width; i++)
            {
                for (int x = 0; x < sourceBitmap.Height; x++)
                {
                    if (i < (raz))
                    {
                        resultBitmap.SetPixel(i, x, Color.FromArgb(sourceBitmap.GetPixel(i, x).R / 5, sourceBitmap.GetPixel(i, x).G, sourceBitmap.GetPixel(i, x).B));
                    }
                    else if (i < (raz * 2))
                    {
                        resultBitmap.SetPixel(i, x, Color.FromArgb(sourceBitmap.GetPixel(i, x).R, sourceBitmap.GetPixel(i, x).G / 5, sourceBitmap.GetPixel(i, x).B));
                    }
                    else if (i < (raz * 3))
                    {
                        resultBitmap.SetPixel(i, x, Color.FromArgb(sourceBitmap.GetPixel(i, x).R, sourceBitmap.GetPixel(i, x).G, sourceBitmap.GetPixel(i, x).B / 5));
                    }
                    else
                    {
                        resultBitmap.SetPixel(i, x, Color.FromArgb(sourceBitmap.GetPixel(i, x).R / 5, sourceBitmap.GetPixel(i, x).G, sourceBitmap.GetPixel(i, x).B / 5));
                    }

                }
            }
            return resultBitmap;
        }

        //apply color filter to swap pixel colors
        public static Bitmap ApplyFilterMega(Bitmap bmp, int max, int min, Color co)
        {

            Bitmap temp = new Bitmap(bmp.Width, bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int x = 0; x < bmp.Height; x++)
                {

                    Color c = bmp.GetPixel(i, x);
                    if (c.G > min && c.G < max)
                    {
                        Color cLayer = Color.White;
                        temp.SetPixel(i, x, cLayer);
                    }
                    else
                    {
                        temp.SetPixel(i, x, co);
                    }

                }

            }
            return temp;
        }

        //apply color filter to swap pixel colors
        public static Bitmap ApplyFilterSwapDivide(Bitmap bmp, int a, int r, int g, int b)
        {

            Bitmap temp = new Bitmap(bmp.Width, bmp.Height);


            for (int i = 0; i < bmp.Width; i++)
            {
                for (int x = 0; x < bmp.Height; x++)
                {
                    Color c = bmp.GetPixel(i, x);
                    Color cLayer = Color.FromArgb(c.A / a, c.G / g, c.B / b, c.R / r);
                    temp.SetPixel(i, x, cLayer);
                }

            }
            return temp;
        }

        public static void CompareTwoImages(Bitmap img1, Bitmap img2)
        {
            if (img1.Size != img2.Size)
            {
                Console.Error.WriteLine("Images are of different sizes");
                return;
            }

            float diff = 0;

            for (int y = 0; y < img1.Height; y++)
            {
                for (int x = 0; x < img1.Width; x++)
                {
                    diff += (float)Math.Abs(img1.GetPixel(x, y).R - img2.GetPixel(x, y).R) / 255;
                    diff += (float)Math.Abs(img1.GetPixel(x, y).G - img2.GetPixel(x, y).G) / 255;
                    diff += (float)Math.Abs(img1.GetPixel(x, y).B - img2.GetPixel(x, y).B) / 255;
                }
            }
        }

    }
}
