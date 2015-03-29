using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneratorTest
{
    public interface IParser
    {
        IEnumerable<string[]> Parse(string text);

        string Parse2(string test);
    }
}
