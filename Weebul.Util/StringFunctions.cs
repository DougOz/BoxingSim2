using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Util
{
    public static class StringFunctions
    {

        public static int LeftVal(string text)
        {

            StringBuilder sb = new StringBuilder();

            foreach(char c in text)
            {
                if (!Char.IsDigit(c)) break;
                sb.Append(c);
            }
            return int.Parse(sb.ToString());
        }
    }
}
