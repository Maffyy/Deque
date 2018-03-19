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

    }


    class Method {

    }

    class Deque<T> : IDeque<T> {

        private T[][] Map;
        public int Count { get; private set; }
        public bool IsReadOnly => false;
        private const int _blockSize = 4;
        private int front;
        private int frontBlock;
        private int back => (front + Count - 1) % _blockSize;
        private int backBlock => (front + Count - 1) / _blockSize;

        public Deque() {
            Map = new T[1][];
            Map[0] = new T[_blockSize];
            Count = frontBlock = 0;
            front = _blockSize / 2;
        }

        public T this[int index] {
            get {
                if(index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException();
                }
                return Map[(front + index) / _blockSize + frontBlock][(front + index) % _blockSize];
            }
            set {
                if(index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException();
                }
                Map[(front + index) / _blockSize + frontBlock][(front + index) % _blockSize] = value;
            }
        }

        private void Resize() {
            T[][] temp = new T[Map.Length * 2][];
            for(int i = 0; i < temp.Length; i++) {
                temp[i] = new T[_blockSize];
            }
            frontBlock = Map.Length;
            Map.CopyTo(temp, frontBlock);
            Map = temp;

        }

        public void Add(T item) {
            PushBack(item);
        }

        public void Clear() {
            Array.Clear(Map, 0, Map.Length);
            Count = front = frontBlock = 0;

        }

        public bool Contains(T item) {

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) {

            for(int i = 0; i < Count; i++) {
                array[i + arrayIndex] = this[i];
            }
        }

        public IEnumerator<T> GetEnumerator() {
            for(int i = front; i < _blockSize; i++) {
                yield return Map[frontBlock][i];
            }
            for(int i = frontBlock; i < backBlock + 1; i++) {
                for(int j = 0; j < Map[i].Count(); j++) {
                    yield return Map[i][j];
                }
            }
        }

        public int IndexOf(T item) {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item) {
            throw new NotImplementedException();
        }

        public void PopBack() {

        }

        public void PopFront() {


        }

        public void PushBack(T value) {

            if(Count == 0) {
                Map[0][front] = value;
            }
            else if(back + 1 == _blockSize) {
                if(backBlock == Map.Length - 1) {
                    Resize();
                }
                Map[backBlock + 1][0] = value;
            }
            else {
                Map[backBlock][back + 1] = value;
            }
            Count++;
        }
        public void PushFront(T value) {
            if(Count == 0) {
                Map[0][0] = value;
            }
            else if(front - 1 < 0) {
                if(frontBlock - 1 <= 0) {
                    Resize();
                }
                frontBlock--;
                front = _blockSize - 1;
                Map[frontBlock][front] = value;
            }
            else {
                front--;
                Map[frontBlock][front] = value;
            }
            Count++;
        }

        public bool Remove(T item) {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index) {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
    public static class DequeTest {
        //public static IList<T> GetReverseView<T>(Deque<T> d) {
        //    return null;
        //}
    }
}
