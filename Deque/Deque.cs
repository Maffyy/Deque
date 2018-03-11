using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deque {
    interface IDeque<T> : IList<T> {
        void PushFront(T value);
        void PushBack(T value);
        void PopFront();
        void PopBack();
        T PeekFront();
        T PeekBack();
        bool IsEmpty();
        bool IsFull();
        int Capacity { get; set; }
    }

    class Deque<T> : IDeque<T> {

        private const int _blockSize = 4;
        private T[][] Front;
        private T[][] Back;
        private int frontIndex;
        private int backIndex;
        private int index;

        public Deque() {
            Front = new T[1][];
            Front[0] = new T[_blockSize];
            Back = new T[1][];
            Back[0] = new T[_blockSize];
        }

        private bool IsEmpty(T[][] array) {
            for(int i = 0; i < array.Length; i++) {
                for(int j = 0;  j < array[i].Length;  j++) {
                    if(!array[i][j].Equals(default(T))) {
                        return false;
                    }
                }
            }
            return true;
            
        }
        private bool IsFull(T[][] array) {
            for(int i = 0; i < array.Length; i++) {
                for(int j = 0; j < array[i].Length; j++) {
                    if(array[i][j].Equals(default(T))) {
                        return false;
                    }
                }
            }
            return true;
        }
        private int Count(T[][] array) {
            int c = 0;
            for(int i = 0; i < array.Length; i++) {
                for(int j = 0; j < array[i].Length; j++) {
                    if(array[i][j].Equals(default(T))) {             // if there is a null, we will stop counting
                        break;
                    }
                    c++;
                }
            }
            return c;
        }
        private int BlockIndex(T[][] array) {
            int count = Count(array);
            for(int i = 0; i < array.Length; i++) {
                if(count > 8) {
                    count -= _blockSize;
                } else {
                    break;
                }
            }
            
            return count-1;;
        }

        public void PopFront() {
            
        }
        public void PopBack() {

        }
        private T[][] ResizeArray(T[][] array) {
            T[][] temp = array;
            array = new T[array.Length+1][];

            for(int i = 0; i < temp.Length; i++) {
                for(int j = 0; j < temp[i].Length; j++) {
                    array[i][j] = temp[i][j];
                }
            }
            return array;
        }
        private void Push(T[][] array,T value) {
            if(IsEmpty(array)) {
                array[0][0] = value;
            } else if(IsFull(array)) {
                array = ResizeArray(array);
                array[array.Length - 1][0] = value;
            } else {
                int i = BlockIndex(array);
                array[array.Length - 1][i] = value;
            }
        }
        public void PushFront(T value) {
            Push(Front,value);
        }
        public void PushBack(T value) {
            Push(Back,value);
        }
        private T Peek(T[][] array) {
            int i = BlockIndex(array);
            return array[array.Length-1][i];
        }

        public T PeekFront() {
            return Peek(Front);
        }
        public T PeekBack() {
            return Peek(Back);
        }

        public T this[int index] {
            get {
                return this[index];
            }
            set {
                if(value == null && default(T) != null) {
                    throw new ArgumentNullException();
                }
                this[index] = (T)value;
            }
        }
        public int Capacity {
            get {
                
            }
            set {

            }
            
        }
        public int Count {
            get {

            }
        }

        public bool IsReadOnly => false;

        public void Add(T item) {
            PushBack(item);
        }

        public void Clear() {
            
        }

        public bool Contains(T item) {
            
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) {
          
        }

        public IEnumerator<T> GetEnumerator() {
            return this.GetEnumerator();
        }

        public int IndexOf(T item) {

            
            throw new InvalidOperationException();
        }

        public void Insert(int index, T item) {
            
        }

        public bool Remove(T item) {

            
            throw new InvalidOperationException();
        }

        public void RemoveAt(int index) {
            

        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
    public static class DequeTest {
        public static IList<T> GetReverseView<T>(Deque<T> d) {
            return null;
        }
    }
}
