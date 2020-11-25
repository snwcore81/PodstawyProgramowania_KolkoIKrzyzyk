using System;

namespace KolkoIKrzyzyk
{
    class Program
    {
        const int ROZMIAR = 3;

        const int KRZYZYK = 1;
        const int KOLKO = -1;
        const int PUSTY = 0;

        static int[,] g_iTablicaGry;

        static void InicjalizujTablice()
        {
            g_iTablicaGry = new int[ROZMIAR, ROZMIAR];

            for (int _y=0; _y < ROZMIAR; _y++)
            {
                for (int _x=0; _x < ROZMIAR; _x++ )
                {
                    g_iTablicaGry[_y, _x] = PUSTY;
                }
            }
        }

        static void RysujTablice()
        {
            Console.Clear();

            for (int _y=0;_y<3;_y++)
            {
                for (int _x=0;_x<5;_x++)
                {
                    if (_x % 2 == 0)
                    {
                        switch (g_iTablicaGry[_y,_x/2])
                        {
                            case KOLKO:
                                Console.Write("O"); break;

                            case KRZYZYK:
                                Console.Write("X"); break;

                            default:
                                Console.Write(" "); break;
                        }
                    }
                    else
                    {
                        Console.Write("|");
                    }
                }

                Console.WriteLine();

                if (_y<2)
                    Console.WriteLine("-+-+-");

            }
        }
        static bool CzyGraczWygral (int a_iKtoryGracz) // a_iKtoryGracz = KOLKO(-1) | KRZYZYK (+1)
        {
            int _iSuma = 0;

            _iSuma = g_iTablicaGry[0, 0] + g_iTablicaGry[1, 1] + g_iTablicaGry[2, 2];

            if (_iSuma == 3 * a_iKtoryGracz)
                return true;

            _iSuma = g_iTablicaGry[0, 2] + g_iTablicaGry[1, 1] + g_iTablicaGry[2, 0];

            if (_iSuma == 3 * a_iKtoryGracz)
                return true;


            for (int _y=0;_y<ROZMIAR;_y++)
            {
                _iSuma = 0;

                for (int _x = 0; _x < ROZMIAR; _x++)
                {
                    _iSuma += g_iTablicaGry[_y, _x];
                }

                if (_iSuma == 3 * a_iKtoryGracz)
                    return true;
            }

            for (int _x = 0; _x < ROZMIAR; _x++)
            {
                _iSuma = 0;

                for (int _y = 0; _y < ROZMIAR; _y++)
                {
                    _iSuma += g_iTablicaGry[_y, _x];
                }

                if (_iSuma == 3 * a_iKtoryGracz)
                    return true;
            }

            return false;
        }

        static bool CzyRemis()
        {
            for (int _x = 0; _x < ROZMIAR; _x++)
            {
                for (int _y = 0; _y < ROZMIAR; _y++)
                {
                    if (g_iTablicaGry[_y, _x] == 0)
                        return false;
                }
            }

            return true;
        }

        static int WprowadzWartosc(string a_sTekst,int a_iMin, int a_iMax)
        {
            while(true)
            {
                Console.Write(a_sTekst);

                int _iLiczba = 0;

                if (int.TryParse(Console.ReadLine(), out _iLiczba) == true && _iLiczba >= a_iMin && _iLiczba <= a_iMax)
                {
                    return _iLiczba;
                }

                Console.WriteLine($"Błędna wartość! Musi zawierać się w przedziale {a_iMin}-{a_iMax}!");
            }
        }

        static void PetlaGry()
        {
            InicjalizujTablice();

            int _iGracz = KRZYZYK;

            while (true)
            {
                RysujTablice();

                switch (_iGracz)
                {
                    case KOLKO:
                        Console.WriteLine("Gra gracz KOLKO"); break;

                    case KRZYZYK:
                        Console.WriteLine("Gra gracz KRZYZYK"); break;

                }

                while (true)
                {
                    int _iY = WprowadzWartosc("Podaj wiersz:", 1, 3);
                    int _iX = WprowadzWartosc("Podaj kolumnę:", 1, 3);

                    _iY--;
                    _iX--;

                    if (g_iTablicaGry[_iY,_iX] == PUSTY)
                    {
                        g_iTablicaGry[_iY, _iX] = _iGracz;

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Człowieniu... zerknij na planszę....");
                    }                    
                }

                if (CzyGraczWygral(_iGracz) == true)
                {
                    switch (_iGracz)
                    {
                        case KOLKO:
                            Console.WriteLine("Wygral gracz KOLKO!"); break;

                        case KRZYZYK:
                            Console.WriteLine("Wygral gracz KRZYZYK!"); break;
                    }

                    break;
                }
                else if (CzyRemis() == true)
                {
                    Console.WriteLine("Wystąpił remis!");

                    break;
                }

                _iGracz *= -1;
            }

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            PetlaGry();
        }
    }
}
