using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class Encode
    {
       public string encode(string ibinary)
        {
            lastele lastele = new lastele();
            Output output = new Output();

            ibinary += "000000";
            int[] binary = ibinary.ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();

            for (int i = 0; i < ibinary.Length; i++)
            {
                output.insert(binary[i]);
                int n = lastele.return1(binary[i]);
                output.insert(n);
                lastele.insert(binary[i]);

            }
            List<int> list = output.returnOutput();
            var result = string.Join("", list.Select(x => x.ToString()).ToArray());
            list.Clear();
            return result;

        }
    }
}
