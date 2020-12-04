using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class ChannelSend
    {
        public List<int> sendChannel(int[] List, Double chance)
        {
            Random rand = new Random();
            List<int> positions=new List<int>();

            for (int i = 0; i < List.Length; i++)
            {
                if (rand.NextDouble() < chance)
                {

                    positions.Add(i+1);

                }
            }
            return positions;
        }

        public int[] changeBits(List<int> positions, int[] list)
        {
            if (positions.Count == 0) return list;

            int j = 0;

            for (int i = 0; i < list.Length; i++)
            {

                if (i+1 == positions[j])
                {
                    if (list[i] == 0) list[i] = 1;
                    else list[i] = 0;
                    if (positions.Count-1 > j) j++;
                    else return list;

                }
            }
            return list;
        }

    }
}
