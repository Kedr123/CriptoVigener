using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptoVigeener
{
    internal class MaxLenString
    {
        public static string getSubstring(string text)
        {
            
            string item = "";
            var listValue = new List<string>();
            var listCountValue = new List<int>();

            for (int i = 3; i < text.Length; i++)
            {

                for (int j = 0; j < text.Length / i; j += i)
                {
                    item = "";

                    for (int k = 0; k < i; k++)
                    {
                        item += text[j + k];
                    }

                    listValue.Add(item);

                    //textBox2.Text += "\n" + item;

                }
            }

            int count = 0;

            for (int i = 0; i < listValue.Count; i++)
            {
                count = 0;

                for (int j = 0; j < text.Length - listValue[i].Length; j++)
                {
                    if (text.Substring(j, listValue[i].Length) == listValue[i])
                    {
                        count++;
                    }
                }
                listCountValue.Add(count);
                count = 0;
            }

            int maxValueIndex = 0; int maxValue = 0;

            for (int i = 0; i < listCountValue.Count; i++)
            {
                if (maxValue < listCountValue[i] /*&& listValue[maxValueIndex].Length < listValue[i].Length*/)
                {
                    maxValue = listCountValue[i];
                    maxValueIndex = i;
                }
            }

            Console.WriteLine(listValue);

            return listValue[maxValueIndex];
        }
    }
}
