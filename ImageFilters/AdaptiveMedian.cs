using System;

namespace ImageFilters
{
    public class AdaptiveMedian : ImageOperations
    {
        public static byte NextPixelVal;
        public static int[] ZOfIntegers = new int[2];



        public static byte[,] Filter(int T, int WSize, byte[,] ImageMatrix)
        {
            byte[,] WindowArray = new byte[WSize, WSize];

            int col = ImageOperations.GetWidth(ImageMatrix);//Width of original image
            int row = ImageOperations.GetHeight(ImageMatrix);//Height of original image

            int padSize = calculatePadSize(WSize);

            byte[,] paddedImageMatrix = padMatrix(ImageMatrix, padSize, row, col);//pads the original image
            byte[,] FilteredMatrix = new byte[row + padSize, col + padSize];//Matrix that will contain the filtered image with padding


            for (int yImage = padSize; yImage < row; yImage++)//Outer for loops that moves across each pixel in original image
            {

                for (int xImage = padSize; xImage < col; xImage++)
                {



                    for (int yWindow = 0; yWindow < WSize; yWindow++)//Inner for loops for creating a window around each pixel
                    {

                        for (int xWindow = 0; xWindow < WSize; xWindow++)
                        {
                            WindowArray[yWindow, xWindow] = paddedImageMatrix[(yImage - padSize) + yWindow, (xImage - padSize) + xWindow];//Starts adding pixels to window starting with 0,0 position pixel

                        }


                    }

                    //Sorting.countsort(WindowArray);
                      //                  byte mean = CalculateMedian(WindowArray[WSize, WSize], WindowArray[WSize / 2, WSize / 2], WindowArray[0, 0], WSize, ImageMatrix, yImage, xImage);//Gets mean value of window

                    FilteredMatrix[yImage, xImage] = NextPixelVal;//Adds mean value to the pixel in the filtered matrix        			       
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

        //CalculateMedian(WindowMatrix[WSize, WSize], WindowMatrix[WSize / 2, WSize / 2], WindowMatrix[0, 0], WSize, ImageMatrix, yImage, xImage);
        static int sortRowWise(byte[,] m)
        {
            // loop for rows of matrix
            for (int i = 0;
                     i < m.GetLength(0); i++)
            {

                // loop for column of matrix
                for (int j = 0;
                         j < m.GetLength(1); j++)
                {

                    // loop for comparison and swapping
                    for (int k = 0;
                             k < m.GetLength(1) - j - 1; k++)
                    {
                        if (m[i, k] > m[i, k + 1])
                        {

                            // swapping of elements
                            byte t = m[i, k];
                            m[i, k] = m[i, k + 1];
                            m[i, k + 1] = t;
                        }
                    }
                }
            }

            //// printing the sorted matrix
            //for (int i = 0;
            //         i < m.GetLength(0); i++)
            //{
            //    for (int j = 0;
            //             j < m.GetLength(1); j++)
            //        Console.Write(m[i, j] + " ");
            //    Console.WriteLine();
            //}
            return 0;
        }
        //static int[] To1DArray(byte[,] input)
        //{
        //    // Step 1: get total size of 2D array, and allocate 1D array.
        //    int size = input.Length;
        //    int[] result = new int[size];

        //    // Step 2: copy 2D array elements into a 1D array.
        //    int write = 0;
        //    for (int i = 0; i <= input.GetUpperBound(0); i++)
        //    {
        //        for (int z = 0; z <= input.GetUpperBound(1); z++)
        //        {
        //            result[write++] = input[i, z];
        //        }
        //    }
        //    // Step 3: return the new array.
        //    return result;
        //}
        private static int[] ConvertToInt(int Zmax, int Zmed, int Zmin)
        {

            ZOfIntegers[0] = Convert.ToInt32(Zmax);
            ZOfIntegers[1] = Convert.ToInt32(Zmed);
            ZOfIntegers[2] = Convert.ToInt32(Zmed);

            return ZOfIntegers;
        }

        private static byte[] ConvertToByte(int[] Aint)
        {
            byte[] A = new byte[1];

            A[0] = Convert.ToByte(Aint[0]);
            A[1] = Convert.ToByte(Aint[1]);
            return A;
        }


        private static byte[] Step1(byte Zmax, byte Zmed, byte Zmin)
        {
            ZOfIntegers = ConvertToInt(Zmax, Zmed, Zmin);

            int[] Aint = new int[1];
            Aint[0] = ZOfIntegers[1] - ZOfIntegers[2];//Zmed-Zmin
            Aint[1] = ZOfIntegers[0] - ZOfIntegers[1];//Zmax-Zmed

            return ConvertToByte(Aint);

        }

        private static int Step2(byte[,] ImageMatrix, int yImage, int xImage, byte Zmax, byte Zmed, byte Zmin)
        {
            int[] Bint = new int[1];
            byte yImageTypeinteger;
            yImageTypeinteger = Convert.ToByte(ImageMatrix[yImage, xImage]);
            ZOfIntegers = ConvertToInt(Zmax, Zmed, Zmin);

            Bint[0] = yImageTypeinteger - ZOfIntegers[2]; //fe 7aga 8lt //Zxy-Zmin
            Bint[1] = ZOfIntegers[0] - ZOfIntegers[2];//Zmax-Zmin

            byte[] B = new byte[1];

            B = ConvertToByte(Bint);

            if (B[0] > 0 && B[2] > 0)
                return NextPixelVal = ImageMatrix[yImage, xImage]; //NextPixelVal = Zxy

            else
                return NextPixelVal = Zmed; //NextPixelVal = Zmed


        }
        private static byte CalculateMedian(byte Zmax, byte Zmed, byte Zmin, int WSize, byte[,] ImageMatrix, int yImage, int xImage)
        {


            byte[] A = new byte[1];
            A = Step1(Zmax, Zmed, Zmin);
            if (A[0] > 0 && A[1] > 0)
                Step2(ImageMatrix, yImage, xImage, Zmax, Zmed, Zmin);
            else
            {
                WSize += 2;
                byte[,] WindowMatrix = new byte[WSize, WSize];
                if (WindowMatrix.Length < ImageMatrix.Length)

                    NextPixelVal = ImageMatrix[yImage, xImage];

                else
                    NextPixelVal = Zmed;
            }
            return NextPixelVal;

        }

    }
}
