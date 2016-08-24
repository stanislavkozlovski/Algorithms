using System;

class BinomialCoefficients
{
    static int[,] matrix;
    static int[,] savedCalculations;
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int k = int.Parse(Console.ReadLine());

        savedCalculations = new int[n + 1, n+1];
        matrix = new int[n+1, n+1];

        Console.WriteLine(BinomCoeff(matrix[n,k], n, k));
    }
    static int BinomCoeff(int value, int n, int k)
    {
        if(n == 0 || k == 0)
        {
            return 1;
        }

        if(savedCalculations[n,k] != 0) // checks if we haven't already calculated it
        {
            matrix[n, k] = savedCalculations[n, k]; //saves us from further recursion
        }
        else
        {
            matrix[n, k] = BinomCoeff(matrix[n, k], n - 1, k - 1) + BinomCoeff(matrix[n, k], n - 1, k); // uses recursion to calculate it
            savedCalculations[n, k] = matrix[n, k]; //saves the calculation for later use
        }

        matrix[n, 0] = matrix[n, n] = 1; // in the binomial coefficient, the first and last index is always 1

        return matrix[n,k];
    }
}

