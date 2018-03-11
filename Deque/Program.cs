using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deque {
    class Program {

        static void Main(string[] args) {
            Deque<int> deque = new Deque<int>();
            deque.PushBack(1);
            deque.PushBack(2);
            deque.PushBack(1);
            deque.PushBack(1);
            Console.WriteLine(deque[1]);
        }
    }
}
