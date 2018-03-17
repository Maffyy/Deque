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

    }
    class Block<T> {
        private const int _size = 8;
        private T[] block;

        public Block()
        {
            block = new T[_size];
        }

        public T this[int index] {
            get {
                return block[index];
            }
            set {
                block[index] = value;
            }
        }
    }

    class Deque<T> : IDeque<T> {

        private Block<T>[] Map;
        private int frontIndex;
        private int backIndex;
        public int Count { get; private set; } = 0;

        public Deque() {
            Map = new Block<T>[0];
        }

        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(T item) {
            throw new NotImplementedException();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public bool Contains(T item) {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator() {
            throw new NotImplementedException();
        }

        public int IndexOf(T item) {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item) {
            throw new NotImplementedException();
        }

        public T PeekBack() {
            throw new NotImplementedException();
        }

        public T PeekFront() {
            throw new NotImplementedException();
        }

        public void PopBack() {
            throw new NotImplementedException();
        }

        public void PopFront() {
            throw new NotImplementedException();
        }

        public void PushBack(T value) {
            throw new NotImplementedException();
        }

        public void PushFront(T value) {
            throw new NotImplementedException();
        }

        public bool Remove(T item) {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index) {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
    public static class DequeTest {
        public static IList<T> GetReverseView<T>(Deque<T> d) {
            return null;
        }
    }
}
