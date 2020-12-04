using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding

{
    class lastele
    {
        int[] data = { 0, 0, 0, 0, 0, 0 };


        public void insert(int a)
        {
            for (int i = 5; i > 0; i--)
            {
                data[i] = data[i - 1];

            }
            data[0] = a;
        }
        public int[] getData()
        {
            return data;
        }

        public int return1(int i)
        {
            int n = i + data[1] + data[4] + data[5];
            return n % 2;
        }

    }
}
