using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deque {
    class A {

    }
    class Enumerator : IEnumerator<int> {
        private A a;
        private int index = -1;
        private bool firstArray = true;
        public int x;



        public IEnumerable<int> Range(int from, int to) {
            if(to < from) {
                throw new ArgumentException($"Value of '{nameof(from)}' has to be less or equal than value of {nameof(to)}");
            }
            Console.WriteLine("+ A.Range: before 1st yield return");
            from--;
            to++;
            for(int i = from; i <= to; i++) {
                yield return x + i;
            }
            x += 100;
        }

        public Enumerator(A a) {
            this.a = a;
        }
        public int Current {
            get {
                if(firstArray) {
                    if(index < 0) {
                        throw new InvalidOperationException();
                    } else {
                        return a.x1[index];
                    }
                } else {

                }
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() {
            throw new NotImplementedException();
        }

        public bool MoveNext() {
            throw new NotImplementedException();
        }

        public void Reset() {
            throw new NotImplementedException();
        }
    }
}
