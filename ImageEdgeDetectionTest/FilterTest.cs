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

            wrapper.CompareTwoImages(Modified, KirschFilter);
        }
        //Compare one formated picture with the result of one method who format the picture to find the same result
        [TestMethod]
        public void TestLaplacian3x3Filter()
        {
            Bitmap Original = Resources.No_Filter;
            Bitmap Laplacian3x3Filter = Resources.Laplacian_3x3_Filter;
            Bitmap Modified = wrapper.Laplacian3x3Filter(Original);

            wrapper.CompareTwoImages(Modified, Laplacian3x3Filter);
        }
        //Compare one formated picture with the result of one method who format the picture to find the same result
        [TestMethod]
        public void TestNightFilter()
        {
            Bitmap Original = Resources.No_Filter;
            Bitmap NightFilter = Resources.Night_Filter;
            Bitmap Modified = wrapper.NightFilter(Original);

            wrapper.CompareTwoImages(Modified, NightFilter);
        }
        //Compare one formated picture with the result of one method who format the picture to find the same result
        [TestMethod]
        public void TestSobelFilter()
        {
            Bitmap Original = Resources.No_Filter;
            Bitmap SobelFilter = Resources.Sobel_3x3_Filter;
            Bitmap Modified = wrapper.SobelFilter(Original);

            wrapper.CompareTwoImages(Modified, SobelFilter);
        }
        
    }
}
