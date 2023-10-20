using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriptoVigeener
{
    internal class ArrayPositionSubstring
    {
        public static List<int> getArrayPositions(string text, string substring)
        {
            var positions = new List<int>();

            for (int i = 0; i < text.Length - substring.Length; i++)
            {
                if(text.Substring(i,substring.Length) == substring)
                {
                    positions.Add(i+1);
                }
            }

            return positions;
        }
    }
}
