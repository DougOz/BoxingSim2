using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weebul.Scripting
{
    public class ParseResult
    {
        public string Text { get; set; }

        public bool Cheat { get; set; }
        public int LineNumber { get; set; }
    }
}
