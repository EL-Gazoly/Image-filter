using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    internal class QuickSort
    {
        static int sortRowWise(int[,] m)
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
                            int t = m[i, k];
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
    }
}
