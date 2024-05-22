using System;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
 
class Program
{
    static void Main(string[] args)
    {
        for (; ; )
        {

            Console.Write("Wybierz opcje: \n" +
                "1. Oblicz logarytmów\n" +
                "2. Działania na logarytmach\n" + //Obliczanie logarytmu, dod, odej, mno, dziel
                "3. Obliczenie ciągu\n" +
                "4. Wyjdź z programu\n\n");

            int choosed = 0;
            try
            {
                choosed = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                continue;
            }

            double[] result = null;

            switch (choosed)
            {
                case 1:
                    
                    Console.WriteLine($"wybrana opcja 1");
                    Console.WriteLine($"Podaj logarytm np. log_2(16)/log_7(x)=2/log_x(16)=2 ");



                    string option = Console.ReadLine();

                    result = ObliczanieLogarytmu(option);

                    Console.WriteLine($"\nLogarytm: {result[0]}");
                    Console.WriteLine($"Podstawa: {result[1]}");
                    Console.WriteLine($"Liczba logarytmowana: {result[2]}\n");


                    break;
                case 2:
                    Console.WriteLine($"wybrana opcja 2");
                    Console.WriteLine($"Podaj jakie dzialanie na logarytmach chcesz przeprowadzic np. log_2(16)+log_4(16)*log_3(9)");

                    string wczyt = Console.ReadLine();
                    string[] dzialania = wczyt.Split('+', '-', '*', '/');

                    foreach (string element in dzialania)
                    {
                        result = ObliczanieLogarytmu(element);

                        Console.WriteLine($"\nLogarytm: {result[0]}");
                        Console.WriteLine($"Podstawa: {result[1]}");
                        Console.WriteLine($"Liczba logarytmowana: {result[2]}\n");

                        dzialania[Array.IndexOf(dzialania, element)] = Convert.ToString(result[0]); //int index = Array.IndexOf(myArray, "input");
                    }

                    string[] splitted = Regex.Split(wczyt, @"(?<=[\+\-\*/])|(?=[\+\-\*/])");

                    List<string> operators = new List<string>();
                    foreach (string element in splitted)
                    {
                        if (element == "+" || element == "-" || element == "*" || element == "/")
                        {
                            operators.Add(element);
                            Console.WriteLine(element);
                        }
                    }

                    string rownanie = "";
                    for (int i = 0; i < Math.Max(dzialania.Length, operators.Count); i++)
                    {
                        if (i < dzialania.Length)
                        {
                            rownanie += dzialania[i];
                        }
                            
                        if (i < operators.Count)
                        {
                            rownanie += operators[i];
                        }
                    }

                    //log_2(16)*log_7(x)=2/log_x(16)=2

                    // Tworzymy obiekt DataTable
                    DataTable dt = new DataTable();
                    // Obliczamy wynik równania
                    var aaaa = dt.Compute(rownanie, "");
                    // Konwertujemy wynik do double i wyświetlamy
                    Console.WriteLine(Convert.ToDouble(aaaa));


                    break;
                case 3:
                    Console.WriteLine($"wybrana opcja 3");
                    Console.WriteLine($"Podaj 3 kolejne elementy ciagu w podany sposob np. 1,3,5 ");
                    
                    string wczyt1 = Console.ReadLine();
                    string[] elCiagu = wczyt1.Split(',');

                    int el1 = Convert.ToInt32(elCiagu[0]);
                    int el2 = Convert.ToInt32(elCiagu[1]);
                    int el3 = Convert.ToInt32(elCiagu[2]);


                    if (el3 < el2 && el2 < el1)
                    {
                        Console.WriteLine("\nCiag jest malejacy\n");
                    }
                    else if (el3 > el2 && el2 > el1)
                    {
                        Console.WriteLine("\nCiag jest rosnący\n");
                    }
                    else
                    {
                        Console.WriteLine("\nCiag jest niemonotoniczny\n");
                    }


                    int roznica = el2 - el1;
                    bool Arytmetyczny = false;
                    if ((el2+roznica) == el3)
                    {
                        Arytmetyczny = true;
                    }

                    
                    bool Geometryczny = false;
                    if (el1 != 0 && el2 != 0) //pamietaj ******* nigdy nie dziel przez zero :3
                    {
                        //a2/a1 = a3/a2 = a4/a3 = ... =  a1 + 1 / a1
                        if (el2 / el1 == el3 / el2)
                        {
                            Geometryczny = true;
                        }
                    }

                    if(el1 == el2 && el2 == el3 && el3 == el1)
                    {
                        Arytmetyczny = false;
                        Geometryczny = false;
                    }


                    if (Arytmetyczny)
                    {
                        Console.Write($"Ciąg arytmetyczny: {el1} ");
                        for (int i = 1; i < 6; i++)
                        {
                            Console.Write($"{el1 + i * roznica} ");
                        }
                        Console.WriteLine("\n");
                    }
                    else if (Geometryczny)
                    {
                        Console.Write($"Ciąg geometryczny: {el1} ");
                        for (int i = 1; i < 6; i++)
                        {
                            Console.Write($"{el1 * Math.Pow(el2 / el1, i)} ");
                        }
                        Console.WriteLine("\n");
                    }
                    else
                    {
                        Console.WriteLine("Ciąg nie jest ani arytmetyczny, ani geometryczny.\n");
                    }

                    break;
                case 4:
                    Console.WriteLine($"wybrana opcja 4");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine($"Zostala podana zla liczba (lub ciag znakow)\n");
                    break;
            }
        }
    }

    static double[] ObliczanieLogarytmu(string input)
    {
        double[] result = new double[3];

        char targetChar = '=';
        bool found = false;

        foreach (char c in input)
        {
            if (c == targetChar)
            {
                found = true;
                break;
            }
        }
        
        if(found == true)
        {
            int nawPocz = input.IndexOf('(');
            int nawKon = input.IndexOf(')');

            int index = input.IndexOf('x');
            if (index != -1 && index > nawPocz && index < nawKon)
            {
                string[] parts = input.Split('_');
                parts = parts[1].Split('=');

                double wynik = Convert.ToInt32(parts[1]);

                parts[0] = parts[0].Replace("(x)", "").TrimEnd(')');
                double podst = Convert.ToInt32(parts[0]);


                double liczbLogary = 1;
                for(int i = 0; i< wynik; i++)
                {
                    liczbLogary *= podst;
                }


                result[0] = wynik;
                result[1] = podst;
                result[2] = liczbLogary;

                return result;
            }
            else
            {
                //string input1 = "log_x(16)=2";

                string[] parts = input.Split('_');
                parts = parts[1].Split('=');

                double wynik = Convert.ToInt32(parts[1]);
                parts[0] = parts[0].Replace("x(", "").TrimEnd(')');
                double liczbLogary = Convert.ToInt32(parts[0]);

                double podst = Math.Pow(liczbLogary, (1 / wynik));


                result[0] = wynik;
                result[1] = podst;
                result[2] = liczbLogary;

                return result;
            }
        }
        else
        {
            string[] parts = input.Split('_');
            parts = parts[1].Split('(');

            double podst = Convert.ToInt32(parts[0]);

            parts[1].TrimEnd(')'); //jak mam tylko jedno trimend to nie dziala :3     .Remove(0, 1)

            double liczbLogary = Convert.ToInt32(parts[1].TrimEnd(')'));

            double wynik = 0;
            double guard = 1;
            while (guard != liczbLogary)
            {
                guard *= podst;
                wynik++;
            }


            result[0] = wynik;
            result[1] = podst;
            result[2] = liczbLogary;

            return result;
        }
    }
}

