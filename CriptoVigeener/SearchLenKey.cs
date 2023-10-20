using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CriptoVigeener
{
    internal class SearchLenKey
    {
        public static int getLenKey(string text, string[] alpha, int M )
        {
            List<List<string>> list = new List<List<string>>();
            int LenKey = 0;

            for (int t = 1; t < text.Length; t++)
            {
                list.Clear();

                for (int j = 0; j < t; j++)
                {
                    list.Add(new List<string>());

                    for (int k = 0; k < text.Length / t; k++)
                    {
                        try
                        {
                            list[j].Add(text[k * t + j].ToString());
                        }
                        catch
                        {
                            Debug.WriteLine(k);
                            Debug.WriteLine(t);
                            Debug.WriteLine(j);
                        }

                    }
                }
                if (list.Count == 0) continue;

                List<double> IxList = new List<double>();
                int count = 0;
                int suma = 0;

                for (int i = 0; i < t; i++)
                {
                    suma = 0;
                    for (int j = 0; j <= M - 1; j++)
                    {
                        count = 0;

                        foreach (var item in list[i])
                        {
                            if (item.ToLower() == alpha[j].ToLower())
                            {
                                count++;
                            }
                        }
                        suma += count * (count - 1);
                    }
                   // Debug.WriteLine(suma);
                   // Debug.WriteLine(list[0].Count);
                   // Debug.WriteLine(list[0].Count - 1);
                   // Debug.WriteLine((list[0].Count * (list[0].Count - 1)));
                   // Debug.WriteLine(suma / (list[0].Count * (list[0].Count - 1)));
                    double prav = (double)suma / (list[0].Count * (list[0].Count - 1));
                    IxList.Add(prav);
                }

                //Debug.WriteLine(IxList);
                int countIx = 0;
                foreach (var item in IxList)
                {
                    if (item >= 0.045 && item <= 0.10)
                    {
                        countIx++;
                    }
                }

                if (countIx == IxList.Count)
                {
                    LenKey = t;
                    return LenKey;

                }
            }

            return LenKey;
        }
    }
}
