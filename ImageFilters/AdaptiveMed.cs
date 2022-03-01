using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    internal class AdaptiveMed
    {

        public static byte[,] Filter(int MaxWize, int WSize, byte[,] ImageMatrix)
        {
            int[] WindowArray = new int[WSize * WSize];

            int col = ImageOperations.GetWidth(ImageMatrix);//Width of original image//O(N)
            int row = ImageOperations.GetHeight(ImageMatrix);//Height of original image//O(N)

            int padSize = calculatePadSize(WSize);//O(N)

            byte[,] paddedImageMatrix = padMatrix(ImageMatrix, padSize, row, col);//pads the original image//O(N)
            byte[,] FilteredMatrix = new byte[row + padSize, col + padSize];//Matrix that will contain the filtered image with padding

            //O(n^2)*(m^2)
            for (int yImage = padSize; yImage < row; yImage++) //O(n)//Outer for loops that moves across each pixel in original image
            {
                for (int xImage = padSize; xImage < col; xImage++) //O(N)
                {
                    int i = 0; //O(M^2)
                    for (int yWindow = 0; yWindow < WSize; yWindow++) //O(M)//Inner for loops for creating a window around each pixel 
                    {

                        for (int xWindow = 0; xWindow < WSize; xWindow++) //O(M)
                        {
                            WindowArray[i] = paddedImageMatrix[(yImage - padSize) + yWindow, (xImage - padSize) + xWindow];//Starts adding pixels to window starting with 0,0 position pixel
                            i++;
                        }


                    }
                    int Z = ImageMatrix[yImage, xImage]; //O(1)
                    //Array.Sort(windowArray);
                    //int []NewArray = new int[(WSize * WSize)-(2*T)];
                    int Min = WindowArray[0];//O(1)
                    int Med = WindowArray[((WSize * WSize) - 1) / 2];//O(1)
                    int Max = WindowArray[(WSize * WSize) - 1];//O(1)
                    int A1 = Med - Min;//O(1)
                    int A2 = Max - Med;//O(1)
                    int median = 0;//O(1)

                    if (A1 > 0 && A2 > 0)
                    {//O(1)

                        int B1 = Z - Min;//O(1)
                        int B2 = Max - Z;
                        //O(1)
                        if (B1 > 0 && B2 > 0)//O(1)
                            median = Z;//O(1)
                        else
                            median = Med; //O(1)
                    }
                    else
                    {
                        if (WSize <= MaxWize)//ba5od input mn el user b el max size//O(1)
                            Filter(MaxWize, WSize + 2, ImageMatrix);
                        else
                            median = Med;//O(1)
                    }


                    int mean = median;//CalculateMedian(WSize, WindowArray, Z, MaxWize, ImageMatrix);//O(?);//Gets mean value of window
                    byte m = Convert.ToByte(mean); //O(1)
                    FilteredMatrix[yImage, xImage] = m; //O(1)//Adds mean value to the pixel in the filtered matrix        			       
                }

            }

            return FilteredMatrix; //O(1)

        }

        public static int calculatePadSize(int WSize)//Determines required pad size for image
        {
            int padSize = 0;

            while (WSize > 1)//O(N)
            {
                WSize -= 2;
                padSize++;
            }

            return padSize;
        }


        public static byte[,] padMatrix(byte[,] ImageMatrix, int padSize, int row, int col)//pads the original image matrix
        {

            byte[,] paddedMatrix = new byte[row + padSize, col + padSize];


            for (int y = 0; y < row - padSize; y++) //O(N)
                for (int x = 0; x < col - padSize; x++) //O(N)
                    paddedMatrix[y + padSize, x + padSize] = ImageMatrix[y, x];

            return paddedMatrix;

        }

        public static int CalculateMedian(int WSize, int[] windowArray, int Z, int MaxWSize, byte[,] ImageMatrix) {

            //Array.Sort(windowArray);
            //int []NewArray = new int[(WSize * WSize)-(2*T)];
            int Min = windowArray[0];//O(1)
            int Med = windowArray[((WSize * WSize) - 1) / 2];//O(1)
            int Max = windowArray[(WSize * WSize) - 1];//O(1)
            int A1 = Med - Min;//O(1)
            int A2 = Max - Med;//O(1)
            int median = 0;//O(1)

            if (A1 > 0 && A2 > 0)
            {//O(1)

                int B1 = Z - Min;//O(1)
                int B2 = Max - Z;
                //O(1)
                if (B1 > 0 && B2 > 0)//O(1)
                    median = Z;//O(1)
                else
                    median = Med; //O(1)
            }
            else {
                if (WSize <= MaxWSize)//ba5od input mn el user b el max size//O(1)
                    Filter(MaxWSize, WSize + 2, ImageMatrix); 
                else
                    median = Med;//O(1)
            }

            return median;//O(1)
        }


    }
}



