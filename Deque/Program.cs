using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deque {
    class Program {

        static void Main(string[] args) {
            
            Deque<int> deque = new Deque<int>();
            List<int> a = new List<int>();
            Stack<int> c = new Stack<int>();
            c.Push(1);
            //deque.Add(123);
            //deque.Add(2);
            deque.PushFront(-1);
            deque.PushBack(1);
            deque.PushFront(-1);
            deque.PushBack(1);
            deque.PushFront(-1);
            deque.PushBack(1);
            deque.PushFront(-1);
            deque.PushBack(1);
            deque.PushFront(-1);
            deque.PushBack(1);
            deque.PushBack(1);
            deque.PushBack(1);
            deque.PushBack(1);
            
            
        }
    }
}
