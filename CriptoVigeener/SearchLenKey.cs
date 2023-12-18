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
            // Переменная для формирования подмассивов
            List<List<string>> list = new List<List<string>>();
            int LenKey = 0;

            // Выполняется пока не будет найдена длина ключа или пока предпологаемая длина ключа не станет равна дляне зашифрованного текста
            for (int t = 1; t < text.Length; t++)
            {
                list.Clear();

                // Выполняется пока j не станет меньше предпологаемой длины ключа
                // Данный цикл разбивает зашифрованный текс на подмассивы с длиной предпологаемой длины ключа
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

                // Если количество подмассивов равно нулю, то итерация пропускается
                if (list.Count == 0) continue;

                // Массив для полученных I(x)
                List<double> IxList = new List<double>();
                int count = 0;
                int suma = 0;

                // Данный цикл вычисляет I(x) для каждого подмассива
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
                    double prav = (double)suma / (list[0].Count * (list[0].Count - 1));
                    IxList.Add(prav);
                }

                int countIx = 0;
                // Данный цикал увеличивает счётчик countIx если I(x) попало в заданной промежуток
                foreach (var item in IxList)
                {
                    if (item >= 0.045 && item <= 0.10)
                    {
                        countIx++;
                    }
                }

                // Если все I(x) попали в промежуток, то значит мы нашли длину ключа (но это не точно)
                if (countIx == IxList.Count)
                {
                    LenKey = t;
                    return LenKey;

                }
            }
            // Возвращаем длину ключа
            return LenKey;
        }
    }
}
