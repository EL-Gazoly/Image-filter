using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
namespace ImageFilters
{

    public class AlphaTrim
    {

        public static byte[,] Filter(int T, int WSize, byte[,] ImageMatrix)
        {
            int[] WindowArray = new int[WSize * WSize];
            int[] WA = new int[WSize * WSize];

            int col = ImageOperations.GetWidth(ImageMatrix);//Width of original image
            int row = ImageOperations.GetHeight(ImageMatrix);//Height of original image

            int padSize = calculatePadSize(WSize);

            byte[,] paddedImageMatrix = padMatrix(ImageMatrix, padSize, row, col);//pads the original image
            byte[,] FilteredMatrix = new byte[row + padSize, col + padSize];//Matrix that will contain the filtered image with padding


            for (int yImage = padSize; yImage < row; yImage++)//Outer for loops that moves across each pixel in original image
            {

                for (int xImage = padSize; xImage < col; xImage++)
                {


                    int i = 0;
                    for (int yWindow = 0; yWindow < WSize; yWindow++)//Inner for loops for creating a window around each pixel
                    {

                        for (int xWindow = 0; xWindow < WSize; xWindow++)
                        {
                            WindowArray[i] = paddedImageMatrix[(yImage - padSize) + yWindow, (xImage - padSize) + xWindow];//Starts adding pixels to window starting with 0,0 position pixel
                            i++;
                        }


                    }

                    //Sorting.countsort(WindowArray);
                    int mean = calculateMean(WSize, WindowArray, T);//Gets mean value of window
                    byte m = Convert.ToByte(mean);
                    FilteredMatrix[yImage, xImage] = m;//Adds mean value to the pixel in the filtered matrix        			       
                }

            }

            return FilteredMatrix;

        }

        public static int calculatePadSize(int WSize)//Determines required pad size for image
        {
            int padSize = 0;

            while (WSize > 1)
            {
                WSize -= 2;
                padSize++;
            }

            return padSize;
        }


        public static byte[,] padMatrix(byte[,] ImageMatrix, int padSize, int row, int col)//pads the original image matrix
        {

            byte[,] paddedMatrix = new byte[row + padSize, col + padSize];


            for (int y = 0; y < row - padSize; y++)
                for (int x = 0; x < col - padSize; x++)
                    paddedMatrix[y + padSize, x + padSize] = ImageMatrix[y, x];


            return paddedMatrix;

        }


        public static int calculateMean(int WSize, int[] windowArray, int T)
        {

            int sum = 0;
            int count = 0;
            int w = WSize * WSize;

            Array.Sort(windowArray);

            for (int i = T; i < (w - T); i++)//Only sums the trimmed parts of the array       	
            {
                sum += windowArray[i];
                count++;
            }

            int mean = sum / count;

            return mean;

        }


    }
}