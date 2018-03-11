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

    

    class Deque<T> : IDeque<T> {

        private const int _blockSize = 4;
        private T[][] Front;
        private T[][] Back;
        private Method method = new Method();

        class Method {
            internal bool IsEmpty(T[][] array) {
                for(int i = 0; i < array.Length; i++) {
                    for(int j = 0; j < array[i].Length; j++) {
                        if(!array[i][j].Equals(default(T))) {
                            return false;
                        }
                    }
                }
                return true;

            }
            internal bool IsFull(T[][] array) {
                for(int i = 0; i < array.Length; i++) {
                    for(int j = 0; j < array[i].Length; j++) {
                        if(array[i][j].Equals(default(T))) {
                            return false;
                        }
                    }
                }
                return true;
            }
            internal int Count(T[][] array) {
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
            internal T[][] ResizeArray(T[][] array) {
                T[][] temp = array;
                array = new T[array.Length + 1][];

                for(int i = 0; i < temp.Length; i++) {
                    for(int j = 0; j < temp[i].Length; j++) {
                        array[i][j] = temp[i][j];
                    }
                }
                return array;
            }
            internal int BlockIndex(T[][] array) {
                int count = Count(array);
                for(int i = 0; i < array.Length; i++) {
                    if(count > 8) {
                        count -= _blockSize;
                    } else {
                        break;
                    }
                }

                return count - 1;
                
            }
             internal void Push(T[][] array, T value) {
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
            internal T Peek(T[][] array) {
                int i = BlockIndex(array);
                return array[array.Length - 1][i];
            }
            internal void Pop(T[][] array) {
                if(IsEmpty(array)) {
                    throw new InvalidOperationException();
                } else {
                    array[array.Length-1][BlockIndex(array)] = default(T);
                }
            }
            internal T GetElement(T[][] f, T[][] b, int index) {
                
                int temp = index;
                for(int i = 0; i < (f.Length+b.Length); i++) {
                    if(temp < _blockSize) {
                        break;
                    }
                    temp -= _blockSize;
                }
                int BlockNumber = 0;
                for(int i = 1; i < (f.Length + b.Length)+1; i++) {
                    if(index <= i*_blockSize) {
                        BlockNumber = i-1;
                        break;
                    }
                }
                if(BlockNumber < f.Length) {
                    return f[BlockNumber][temp];
                } else {
                    BlockNumber -= f.Length;
                    return b[BlockNumber][temp];
                }
            }
            internal void FindElement(T[][] f, T[][] b, int index, out int k, out int l) {
                l = index;
                for(int i = 0; i < (f.Length + b.Length); i++) {
                    if(l < _blockSize) {
                        break;
                    }
                    l -= _blockSize;
                }
                k = 0;
                for(int i = 1; i < (f.Length + b.Length) + 1; i++) {
                    if(index <= i * _blockSize) {
                        k = i - 1;
                        break;
                    }
                }
            }
        }

        public Deque() {
            Front = new T[1][];
            Front[0] = new T[_blockSize];
            Back = new T[1][];
            Back[0] = new T[_blockSize];
        }

        public T this[int index] {
            
            get {
                return method.GetElement(Front,Back,index);
            }
            set {
                int k,l;
                method.FindElement(Front,Back,index,out k,out l);
                if(k < Front.Length) {
                    Front[k][l] = value;
                } else {
                    k -= Front.Length;
                    Back[k][l] = value;
                }
            }
        }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => false;

        public void Add(T item) => PushBack(item);

        public void Clear() {
            for(int i = 0; i < Front.Length; i++) {
                for(int j = 0; j < Front[i].Length; j++) {
                    Front[i][j] = default(T);
                }
            }
            for(int i = 0; i < Back.Length; i++) {
                for(int j = 0; j < Back[i].Length; j++) {
                    Back[i][j] = default(T);
                }
            }
        }

        public bool Contains(T item) {
            for(int i = 0; i < Front.Length; i++) {
                for(int j = 0; j < Front[i].Length; j++) {
                    if(Front[i][j].Equals(item)) {
                        return true;
                    }
                   
                }
            }
            for(int i = 0; i < Back.Length; i++) {
                for(int j = 0; j < Back[i].Length; j++) {
                    if(Back[i][j].Equals(item)) {
                        return true;
                    }
                    
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator() {
            throw new NotImplementedException();
        }

        public int IndexOf(T item) {
            int index = 0;
            for(int i = 0; i < Front.Length; i++) {
                for(int j = 0; j < Front[i].Length; j++) {
                    if(Front[i][j].Equals(item)) {
                        return index;
                    }
                    index++;
                }
            }
            for(int i = 0; i < Back.Length; i++) {
                for(int j = 0; j < Back[i].Length; j++) {
                    if(Back[i][j].Equals(item)) {
                        break;
                    }
                    index++;
                }
            }
            return index;
        }

        public void Insert(int index, T item) {
            throw new NotImplementedException();
        }

        public T PeekBack() => method.Peek(Back);

        public T PeekFront() => method.Peek(Front);

        public void PopBack() => method.Pop(Back);

        public void PopFront() => method.Pop(Front);

        public void PushBack(T value) => method.Push(Front, value);

        public void PushFront(T value) => method.Push(Back, value);

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
        //public static IList<T> GetReverseView<T>(Deque<T> d) {
        //    return null;
        //}
    }
}
