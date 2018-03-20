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
        T PopFront();
        T PopBack();

    }
    public class Deque<T> : IDeque<T> {

        private T[][] Map;
        private const int _blockSize = 8;
        private int front;
        private int frontBlock;
        public long Extent { get; private set; }
        public int Count { get; private set; }
        public bool IsReadOnly => false;

        public T this[int index] {
            get {
                if(index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();
                return Map[(index + front) / _blockSize + frontBlock][(index + front) % _blockSize];
            }
            set {
                if(index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();
                Map[(index + front) / _blockSize + frontBlock][(index + front) % _blockSize] = value;
                Extent++;
            }
        }

        public Deque() {
            Init();
        }

        public Deque(T[] array) {
            Init();
            foreach (T i in array) {
                PushBack(i);
            }
        }
        private void Init() {
            Map = new T[1][];
            Map[0] = new T[_blockSize];
            frontBlock = front = Count = 0;
            Extent++;
        }

        private void ResizePushBack() {
            if(((Count + front) / _blockSize) + frontBlock >= Map.Length) {
                T[][] temp = new T[Map.Length * 2][];
                for(int i = 0; i < temp.Length; i++)
                    temp[i] = new T[_blockSize];
                Map.CopyTo(temp, 0);
                Map = temp;
            }
        }

        void ResizePushFront() {
            if(frontBlock < 0) {
                frontBlock += Map.Length;
                T[][] temp = new T[Map.Length * 2][];
                for(int i = 0; i < temp.Length; i++)
                    temp[i] = new T[_blockSize];
                Map.CopyTo(temp, Map.Length);
                Map = temp;
            }
        }
        void checkSizePopBack() {
            if((front + Count) / _blockSize + frontBlock < Map.Length / 2) {
                T[][] temp = new T[Map.Length / 2][];
                for(int i = 0; i < temp.Length; i++)
                    temp[i] = Map[i];
                Map = temp;
            }
        }
        void ResizePopFront() {
            if(frontBlock > Map.Length / 2) {
                frontBlock -= Map.Length / 2;
                T[][] temp = new T[Map.Length / 2][];
                for(int i = 0; i < temp.Length; i++)
                    temp[i] = new T[_blockSize];
                for(int i = 0; i < temp.Length; i++)
                    Map[Map.Length / 2 + i].CopyTo(temp[i], 0);
                Map = temp;
            }
        }

        public void Add(T item) {
            PushBack(item);
        }

        public void Clear() {
            Init();
        }

        public bool Contains(T item) {
            foreach(T value in this) {
                if(value.Equals(item)) {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) {
            for(int i = 0; i < Count; i++) {
                array[arrayIndex + i] = this[i];
            }
        }

        public IEnumerator<T> GetEnumerator() {
            return new Enumerator(this);
        }

        public int IndexOf(T item) {
            for(int i = 0; i < Count; i++) {
                if(Equals(item, this[i])) {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item) {
            if(index < 0 || index > Count)
                throw new ArgumentOutOfRangeException();
            Count++;
            if(index > Count / 2) {
                ResizePushBack();
                for(int i = Count - 1; i > index; i--)
                    this[i] = this[i - 1];
                this[index] = item;
            }
            else {
                if(front == 0) {
                    front = _blockSize - 1;
                    frontBlock--;
                }
                else
                    front--;
                ResizePushFront();
                for(int i = 0; i < index; i++)
                    this[i] = this[i + 1];
                this[index] = item;
            }
            Extent++;
        }

        public T PopBack() {
            Extent++;
            T item = this[Count - 1];
            Count--;
            checkSizePopBack();
            return item;
        }

        public T PopFront() {
            Extent++;
            T item = this[0];
            Count--;
            if(front == _blockSize - 1) {
                front = 0;
                frontBlock++;
            }
            else
                front++;
            ResizePopFront();
            return item;
        }

        public void PushBack(T value) {
            Extent++;
            Count++;
            ResizePushBack();
            this[Count - 1] = value;
        }

        public void PushFront(T value) {
            Extent++;
            Count++;
            if(front == 0) {
                front = _blockSize - 1;
                frontBlock--;
            }
            else
                front--;
            ResizePushFront();
            this[0] = value;
        }

        public bool Remove(T item) {
            int index = IndexOf(item);
            if(index == -1)
                return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index) {
            Extent++;
            if(index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException();
            if(index > Count / 2) {
                for(int i = index; i < Count - 1; i++)
                    this[i] = this[i + 1];
                Count--;
                checkSizePopBack();
            }
            else {
                for(int i = index; i > 0; i--)
                    this[i] = this[i - 1];
                Count--;
                if(front == _blockSize - 1) {
                    front = 0;
                    frontBlock++;
                }
                else
                    front++;
                ResizePopFront();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
        public class Enumerator : IEnumerator<T> {
            Deque<T> deque;
            int index = -1;
            long extent;
            public Enumerator(Deque<T> deque) {
                this.deque = deque;
                extent = deque.Extent;
            }

            public T Current {
                get {
                    if(extent != deque.Extent)
                        throw new InvalidOperationException();
                    return deque[index];
                }

            }

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext() {
                if(++index >= deque.Count)
                    return false;
                return true;
            }

            public void Reset() {
                index = -1;
            }
        }
    }

    public class Reverse<T> : IDeque<T> {
        private Deque<T> deque;

        public Reverse(Deque<T> deque) {
            this.deque = deque;
        }

        public T this[int index] {
            get { return deque[deque.Count - 1 - index]; }
            set { deque[deque.Count - 1 - index] = value; }
        }

        public int Count => deque.Count;

        public bool IsReadOnly => false;

        public void Add(T item) => deque.PushBack(item);

        public void Clear() => deque.Clear();

        public bool Contains(T item) => deque.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) {
            for(int i = 0; i < deque.Count; i++) {
                array[arrayIndex + i] = this[deque.Count - 1 - i];
            }
        }

        public IEnumerator<T> GetEnumerator() => new Enumerator(deque);

        public int IndexOf(T item) {
            for(int i = 0; i < deque.Count; i++) {
                if(Equals(item, this[i])) {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item) {
            deque.Insert(deque.Count - index, item);
        }

        public T PopBack() => deque.PopBack();

        public T PopFront() => deque.PopFront();

        public void PushBack(T value) => deque.PushBack(value);

        public void PushFront(T value) => deque.PushFront(value);

        public bool Remove(T item) => deque.Remove(item);

        public void RemoveAt(int index) => deque.RemoveAt(deque.Count - 1 - index);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public class Enumerator : IEnumerator<T> {
            Deque<T> deque;
            int index = -1;
            long extent;
            public Enumerator(Deque<T> deque) {
                this.deque = deque;
                extent = deque.Extent;
            }
            public T Current {
                get {
                    if(deque.Extent != extent)
                        throw new InvalidOperationException();
                    return deque[deque.Count - 1 - index];
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext() {
                if(++index >= deque.Count)
                    return false;
                return true;
            }

            public void Reset() {
                index = -1;
            }
        }
    }
    public static class DequeTest {
        public static IList<T> GetReverseView<T>(Deque<T> d) {
            return new Reverse<T>(d);
        }
    }
}
