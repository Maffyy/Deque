using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deque {
    class Program {

        static void Main(string[] args) {
            
            Deque<int> deque = new Deque<int>();
            deque.Add(123);
            deque.Add(2);
            deque.Add(3);
            deque.Add(4);
            deque.Add(5);
            deque.Add(6);
            int[] a = new int[6];
            deque.CopyTo(a,0);
            //  Console.WriteLine(deque[2]);
            //foreach (var i in deque) {
            //    Console.WriteLine(i);
            //}
        }
    }
}
