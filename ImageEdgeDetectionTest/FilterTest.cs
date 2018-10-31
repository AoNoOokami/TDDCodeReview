using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageEdgeDetection;
using System.Drawing;
using ImageEdgeDetectionTest.Properties;

namespace ImageEdgeDetectionTest
{
    [TestClass]
    public class FilterTest
    {
        //creation de wrapper pour le method test
        Wrapper wrapper = new Wrapper();
        //Compare one formated picture with the result of one method who format the picture to find the same result
        [TestMethod]
        public void TestKirschFilter()
        {
            Bitmap Original = Resources.No_Filter;
            Bitmap KirschFilter = Resources.Kirsch_Filter;
            Bitmap Modified = wrapper.KirschFilter(Original);

            Assert.IsTrue(wrapper.CompareTwoImages(Modified, KirschFilter));
        }
        //Compare one formated picture with the result of one method who format the picture to find the same result
        [TestMethod]
        public void TestLaplacian3x3Filter()
        {
            Bitmap Original = Resources.No_Filter;
            Bitmap Laplacian3x3Filter = Resources.Laplacian_3x3_Filter;
            Bitmap Modified = wrapper.Laplacian3x3Filter(Original);

            Assert.IsTrue(wrapper.CompareTwoImages(Modified, Laplacian3x3Filter));
        }

        //Compare one formated picture with the result of one method who format the picture to find the same result
        [TestMethod]
        public void TestSobelFilter()
        {
            Bitmap Original = Resources.No_Filter;
            Bitmap SobelFilter = Resources.Sobel_3x3_Filter;
            Bitmap Modified = wrapper.SobelFilter(Original);

            Assert.IsTrue(wrapper.CompareTwoImages(Modified, SobelFilter));
        }

        /* @author : Alicia
        * Filter tested : Nigth Filter in Wrapper class.
        * A small custom bitmap is tested with corresponding method and arbitrary values for ARGB parameters (1, 120, 50, 150).*/
        [TestMethod]
        public void TestNightFilter()
        {
            // Custom image used for test
            Bitmap TestImg = new Bitmap(100, 100);
            // Method result for comparison
            Bitmap Result;

            for (int y = 0; y < TestImg.Height; y++)
                for (int x = 0; x < TestImg.Width; x++)
                {
                    TestImg.SetPixel(x, y, Color.FromArgb(120, 50, 150));
                }

            Result = wrapper.NightFilter(TestImg);

            Assert.IsTrue(IsNightFilterApplied(Result));

        }

        /* @author : Alicia 
         * Check of ApplyFilter method with following parameters :
         * Bitmap Result, 
         * int alpha = 1, 
         * int red = 1, 
         * int green = 25,
         * int blue = 1 
         * Expected result for reference image color : 120, 2, 150 */
        public bool IsNightFilterApplied(Bitmap Result)
        {

            // method result, default value is false
            bool IsEqual = false;
            // color variable used for comparison test
            Color color;

            // checking if color modification is correctly applied
            for (int y = 0; y < Result.Height; y++)
                for (int x = 0; x < Result.Width; x++)
                {
                    color = Result.GetPixel(x, y);
                    if (color.R == 120 && color.G == 2 && color.B == 150)
                        IsEqual = true;
                    else
                        IsEqual = false;
                }

            return IsEqual;
        }
    }
}
