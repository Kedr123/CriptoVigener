using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptoVigeener
{
    internal class DelitelText
    {
        public static List<List<string>> getArray(string text, int t)
        {
            List<List<string>> list = new List<List<string>>();

            for (int j = 0; j < t; j++)
            {
                list.Add(new List<string>());

                for (int k = 0; k < text.Length / t; k++)
                {
                    list[j].Add(text[k * t + j].ToString());
                }
            }
            return list;
        }
    }
}
