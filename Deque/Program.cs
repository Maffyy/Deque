using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deque
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[29];
            array[0]=1;
            array[1]=2;
            IList<int> a;
            List<int> b;
            Console.WriteLine(array.Count());
            Console.WriteLine(array.Length);
        }
    }
}
