using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        public int Count { get; private set; }
        public bool IsReadOnly => false;
        private int _blockSize = 4;

        private int mid => ((Map.Length * _blockSize) / 2) % _blockSize;
        private int front;
        private int back;

        private int frontIndex {
            get {
                int temp = mid - front;
                if(temp < 0) {
                    return _blockSize - front;
                }
                return temp;
            }
        }
        private int backIndex => (mid + back) % _blockSize;
        private int midBlock => Map.Length / 2;

        private int frontBlock => (midBlock * _blockSize + mid - front) / _blockSize;
        private int backBlock => frontBlock + (frontIndex + Count - 1) / _blockSize;

        public Deque() {
            Init();
        }

        private void Init() {
            Map = new T[1][];
            Map[0] = new T[_blockSize];
            Count = front = back = 0;
        }

        public T this[int index] {
            get {
                if(index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException();
                }
                return Map[(frontIndex + index) / _blockSize + frontBlock][(frontIndex + index) % _blockSize];
            }
            set {
                if(index < 0 || index >= Count) {
                    throw new IndexOutOfRangeException();
                }
                Map[(frontIndex + index) / _blockSize + frontBlock][(frontIndex + index) % _blockSize] = value;
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
                array[i + arrayIndex] = this[i];
            }
        }

        public IEnumerator<T> GetEnumerator() {
            for(int i = frontIndex; i < _blockSize; i++) {
                yield return Map[frontBlock][i];
            }

            for(int i = 0; i < backBlock; i++) {
                for(int j = 0; j < _blockSize; j++) {
                    yield return Map[i][j];
                }
            }

            for(int i = 0; i <= backIndex; i++) {
                yield return Map[backIndex][i];
            }
        }

        public int IndexOf(T item) {
            for (int i = 0; i < Count; i++) {
                if (this[i].Equals(item)) {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item) {
            if (index < 0 || index > Count) {
                throw new ArgumentOutOfRangeException();
            }
            if (index > Count / 2) {
                if(backIndex == _blockSize - 1 && backBlock == Map.Length - 1) {
                    ResizePush();
                }
                for (int i = Count-1; i > index; i--) {
                    this[i] = this[i - 1];
                }
                back++;
                Count++;
                this[index] = item;
                
            }
            else {
                if(frontIndex == 0 && frontBlock == 0) {
                    ResizePush();
                }

                for (int i = 0; i < index; i++) {
                    this[i] = this[i + 1];
                }
                front++;
                Count++;
                this[index] = item;
            }
        }

        private void ResizePop() {
            T[] temp = new T[Count];
            int _size = Map.Length / 2;
            Map = new T[_size][];
            for (int i = 0; i < _size; i++) {
                Map[i] = new T[_blockSize]; 
            }
            for (int i = 0; i < temp.Length; i++) {
                this[i] = temp[i];
            }
        }

        public T PopBack() {


            T item = this[Count - 1];
            this[Count - 1] = default(T);
            if(Count != 1) {
                back--;
            }
            Count--;
            if((frontIndex + Count) / _blockSize + frontBlock < Map.Length / 2) {
                ResizePop();
            }
            return item;
        }
        public T PopFront() {
            
            T item = this[0];
            this[0] = default(T);
            if(Count != 1) {
                front--;
            }
            Count--;
            if(frontBlock > Map.Length / 2) {
                ResizePop();
            }
            return item;
        }

        private void ResizePush() {
            T[] temp = new T[Count];
            for(int i = 0; i < Count; i++) {
                temp[i] = this[i];
            }
            int _size = Map.Length * 2;
            Map = new T[_size][];
            for(int i = 0; i < Map.Length; i++) {
                Map[i] = new T[_blockSize];
            }
            for(int i = 0; i < Count; i++) {
                this[i] = temp[i];
            }

        }


        public void PushBack(T value) {
            if(backIndex == _blockSize - 1 && backBlock == Map.Length - 1) {
                ResizePush();
            }
            if(Count == 0) {
                Map[backBlock][backIndex] = value;
                Count++;
                return;
            }
            Count++;
            back++;
            Map[backBlock][backIndex] = value;


        }
        public void PushFront(T value) {
            if(frontIndex == 0 && frontBlock == 0) {
                ResizePush();
            }
            if(Count == 0) {
                Map[frontBlock][frontIndex] = value;
                Count++;
                return;
            }
            front++;
            Count++;

            Map[frontBlock][frontIndex] = value;
        }

        public bool Remove(T item) {
            int index = IndexOf(item);
            if(index == -1)
                return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index) {

            if (index < 0 || index >= Count) {
                throw new ArgumentOutOfRangeException();
            }
            if (index > Count/2) {
                for (int i = index; i < Count-1; i++) {
                    this[i] = this[i + 1];
                }
                if(Count != 1) {
                    back--;
                }
                Count--;
                if((frontIndex + Count) / _blockSize + frontBlock < Map.Length / 2) {
                    ResizePop();
                }
            }
            else {
                for (int i = index; i > 0; i--) {
                    this[i] = this[i - 1];
                }
                if(Count != 1) {
                    front--;
                }
                Count--;
                if(frontBlock > Map.Length / 2) {
                    ResizePop();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
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

        public IEnumerator<T> GetEnumerator() {
            foreach (T item in deque) {
                yield return item;
            }
        }

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
    }

    public static class DequeTest {
        public static IList<T> GetReverseView<T>(Deque<T> d) {
            return new Reverse<T>(d);
        }
    }
}
