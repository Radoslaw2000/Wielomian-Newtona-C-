using System;


class Interpolacja
{
    /// <summary>
    /// Funkcja obliczajaca tablice rownan zwyklych do wzoru Newtona.
    /// Elementy sa ukladane kolejno: f(x), delta f(x), ...
    /// </summary>
    /// <returns>Tablica wielkosci n</returns>
    static double[] obliczTabliceRoznicZwyklych(double[,] y, int n)
    {
        int n2 = n;
        double[] rozniceZwykle = new double[n];
        for (int i= 0; i < n; i++)
        {
            n2--;
            for(int j = n2; j>0; j--)
            {
                y[i + 1, j-1] = y[i,j] - y[i,j-1];
            }
            rozniceZwykle[i] = y[i,0];
        }
        return rozniceZwykle;
    }

    /// <summary>
    /// Funkcja obliczajaca silnie
    /// </summary>
    /// <returns>Silnia z "n"</returns>
    static int silnia(int n)
    {
        int result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }


    /// <summary>
    /// Funkcja obliczajaca wielomiany ktore beda podstawione do wzoru Newtona
    /// </summary>
    /// <returns>Tablica wielkosci n+1</returns>
    static double[,] obliczWielomiany(double[] x, int n)
    {
        int n2 = n + 1;
        double[,] wielomiany = new double[n2, n2];

        wielomiany[0, 0] = 1;

        wielomiany[0, 1] = (-x[0]);
        wielomiany[1, 1] = 1;

        for (int i = 2; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                switch (j)
                {
                    case 0:
                        wielomiany[j, i] = wielomiany[j, i - 1] * (-x[i - 1]);
                        break;
                    default:
                        wielomiany[j, i] = wielomiany[j - 1, i - 1] + wielomiany[j, i - 1] * (-x[i - 1]);
                        break;
                }
            }
            wielomiany[i, i] = 1;
        }
        return wielomiany;
    }

    /// <summary>
    /// Funkcja obliczajaca interpolacje wielomianem Newtona
    /// </summary>
    /// <returns>Tablica wielkosci n+1</returns>
    static double[] obliczWielomianNewtona(double[] x, double[,] y, double h, int n)
    {
        double[,] wielomiany = obliczWielomiany(x, n);
        double[] rozniceZwykleY = obliczTabliceRoznicZwyklych(y, n);
        int n2 = n + 1;
        double[] w = new double[n2]; //wielomian

        //Obliczanie wartosci przez ktora pomnozymy wielomian
        for (int i = 0; i < n; i++)
        {
            w[i] = rozniceZwykleY[i] / (silnia(i) * Math.Pow(h, i));
        }

        //Mnozenie przez wielomianu
        for (int i = 0; i<n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                wielomiany[j, i] = wielomiany[j, i] * w[i];
            }
        }

        //Sumowanie wyrazow podobnych
        for (int i = 0; i < n2; i++)
        {
            w[i] = 0;
            for (int j = 0; j < n2; j++)
            {
                w[i] += wielomiany[i, j];
            }
        }
        return w;
    }

    public static void Main()
    {
        int n = 12; //ilosc elementow
        double h = 0.005; // krok
        double[,] y = new double[n, n];
        double[] x = { 1.515, 1.52, 1.525, 1.53, 1.535, 1.54, 1.545, 1.55, 1.555, 1.56, 1.565, 1.57};
        y[0, 0] = 0.788551;
        y[0, 1] = 0.789599;
        y[0, 2] = 0.790637;
        y[0, 3] = 0.791667;
        y[0, 4] = 0.792687;
        y[0, 5] = 0.793698;
        y[0, 6] = 0.7947;
        y[0, 7] = 0.795693;
        y[0, 8] = 0.796677;
        y[0, 9] = 0.797653;
        y[0, 10] = 0.7977;
        y[0, 11] = 0.79786;


        double[] wielomianNewtona = obliczWielomianNewtona(x, y, h, n);

        for (int i = n-1; i >= 0; i--)
        {
            Console.WriteLine("         x(" + i + ") *  " + wielomianNewtona[i]);
        }
    }
}
