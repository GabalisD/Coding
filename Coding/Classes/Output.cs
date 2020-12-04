using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class Output
    {
        List<int> data = new List<int>();

        public void insert(int a)
        {

            data.Add(a);

           
        }

        public List<int> returnOutput()
        {
            return data;
        }
    }
}
