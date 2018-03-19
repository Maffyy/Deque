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
    

    class Deque<T> : IDeque<T> {

        private T[][] Map;
        public int Count { get; private set; }
        public bool IsReadOnly => false;
        private const int _blockSize = 4;
        private int front;
        private int frontBlock;
        private int back => (front + Count - 1) % _blockSize;
        private int backBlock => frontBlock + (front + Count - 1) / _blockSize;

        public Deque() {
            Init();
        }

        private void Init() {
            Map = new T[2][];
            Map[0] = new T[_blockSize];
            Map[1] = new T[_blockSize];
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
            frontBlock = Map.Length - 1;
            Map.CopyTo(temp, Map.Length);
            Map = temp;

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
            for(int i = front; i < _blockSize; i++) {
                yield return Map[frontBlock][i];
            }
            for(int i = frontBlock + 1; i < backBlock; i++) {
                for(int j = 0; j < Map[i].Count(); j++) {
                    yield return Map[i][j];
                }
            }
            for(int i = 0; i < back + 1; i++) {
                yield return Map[backBlock][i];
            }
        }

        public int IndexOf(T item) {
            IEnumerator enumerator = GetEnumerator();
            int i = 0;
            while(enumerator.MoveNext()) {
                if(enumerator.Current.Equals(item)) {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item) {
            throw new NotImplementedException();
        }

        private void ResizePopBack() {
            T[][] temp = new T[Map.Length / 2][];
            for (int i = 0; i < temp.Length; i++) {
                temp[i] = new T[_blockSize];
            }
            for (int i = 0; i < temp.Length; i++) {
                Map[frontBlock +i].CopyTo(temp[i],0);
            }
            Map = temp;
        }

        public void PopBack() {
            if(Count == 0) {
                throw new InvalidOperationException();
            }
            else if(back - 1 < 0) {
                this[Count - 1] = default(T);
                Count--;
                if (Map.Length / (backBlock+1) == 2) {
                    ResizePopBack();
                }
            }
            else {

                Count--;
            }

            
        }

        private void ResizePopFront() {
                T[][] temp = new T[Map.Length / 2][];
                for (int i = 0; i < temp.Length; i++) {
                    temp[i] = new T[_blockSize];
                }
                for (int i = 0; i < temp.Length; i++) {
                    Map[Map.Length/2 +i].CopyTo(temp[i],0);
                }
                
                Map = temp;
                frontBlock = Map.Length / 2 -1;
        }

        public void PopFront() {
            if(Count == 0) {
                throw new InvalidOperationException();
            }
            else if(front+1 >= _blockSize) {
                Count--;
                front = 0;
                frontBlock++;
                if ((Map.Length) / frontBlock == 2) {
                    ResizePopFront();     
                }
               
            }
            else {
                
                this[0] = default(T);
                Count--;
                front++;
            }

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
                Map[0][front] = value;
            //    Count++;
            }
            else if(front < 0) {
                if(frontBlock <= 0) {
                    Resize();
                }
                else {
                    frontBlock--;
                }

               // Count++;//frontBlock = Map.Length/2 - 1;
                front = _blockSize - 1;
                Map[frontBlock][front] = value;
            }
            else {
              //  Count++;
                //front--;
                Map[frontBlock][front] = value;
            }

            front--;
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
