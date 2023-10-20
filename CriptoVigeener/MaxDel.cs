using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptoVigeener
{
    internal class MaxDel
    {
        public static int getMaxDel(List<int> array)
        {
            int delitel = array.Min();
            Console.WriteLine(delitel);
            int x = 0;

            for (int i = 0; i < array.Min() - 1; i++)
            {
                x = 0;
                for (int j = 0; j < array.Count; j++)
                {
                    if (array[j] % delitel == 0)
                    {
                        Debug.WriteLine(delitel);
                        x++;
                    }
                }

                if (x == array.Count)
                {
                    Console.WriteLine(delitel);
                    return delitel;
                }
                delitel -= 1;
            }

            return 0;
        }
    }
}
