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
        T Front();
        T Back();
        bool IsEmpty();
        bool IsFull();
        bool Capacity();
    }

    class Deque<T> : IDeque<T> {
        private const int _blockSize = 8;

        private T[][] Map;

        public Deque() {
            Map = new T[1][];
            Map[0] = new T[_blockSize];
        }

        public bool IsEmpty() {
            for(int i = 0; i < Map.Length; i++) {
                for(int j = 0; j < Map[i].Length; j++) {
                    if(!Map[i][j].Equals(default(T))) {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool IsFull() {
            if(Count == ) {

            }
        }

        public void PopFront() {
            
        }
        public void PopBack() {

        }
        public void PushFront(T value) {

        }
        public void PushBack(T value) {

        }
        public T Front() {

        }
        public T Back() {

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
        public int Capacity() {

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
            for(int i = 0; i < Map.Length; i++) {
                for(int j = 0; j < Map[i].Length; j++) {
                    Map[i][j] = default(T);
                }
            }
        }

        public bool Contains(T item) {
            for(int i = 0; i < Map.Length; i++) {
                for(int j = 0; j < Map[i].Length; j++) {
                    if(Map[i][j].Equals(item)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) {
            if(Map == null) {
                throw new ArgumentNullException();
            }
        }

        public IEnumerator<T> GetEnumerator() {
            return this.GetEnumerator();
        }

        public int IndexOf(T item) {

            for(int i = 0; i < Map.Length; i++) {
                for(int j = 0; j < Map[i].Length; j++) {
                    if(Map[i][j].Equals(item)) {
                        return ((i+1)*_blockSize)-1+j;
                    }
                }
            }
            throw new InvalidOperationException();
        }

        public void Insert(int index, T item) {
            if(index < 0 || index >= _blockSize*Map.Length) {
                throw new InvalidOperationException();
            } 

            int BlockIndex = (_blockSize * Map.Length / (index + 1)) - 1;
            int i = ((index + 1) / (BlockIndex + 1)) - 1;
            Map[BlockIndex][i]=item;
        }

        public bool Remove(T item) {

            for(int i = 0; i < Map.Length; i++) {
                for(int j = 0; j < Map[i].Length; j++) {
                    if(Map[i][j].Equals(item)) {
                        Map[i][j]=default(T);
                    }
                }
            }
            throw new InvalidOperationException();
        }

        public void RemoveAt(int index) {
            int BlockIndex = (_blockSize*Map.Length/(index+1))-1;
            int i = ((index+1)/(BlockIndex+1))-1;
            Map[BlockIndex][i]=default(T);

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
