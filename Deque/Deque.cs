﻿using System;
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
    class Block<T> {
        private T[] block;
        public int Length => 8;
        public Block() {
            block = new T[Length];
        }

        public T this[int index] {
            get {
                return block[index];
            }
            set {
                block[index] = value;
            }
        }
        public bool Exists(T item) {
            return Array.Exists(block, x => x.Equals(item));
        }

        public T Find(T item) {
            return Array.Find(block, x => x.Equals(item));
        }

        public void Copy() {

        }
        public int Count => block.Count();

    }

    class Method {

    }

    class Deque<T> : IDeque<T> {

        private Block<T>[] Map;
        private int frontCount;
        private int backCount;
        //private int frontBlockCount;
        //private int backBlockCount;
        public int Count { get; private set; }
        public bool IsReadOnly => false;
        private int _blockSize { get; set; }
        private int midIndex => _blockSize*Map.Length / 2 - 1;
        private int frontIndex => midIndex - frontCount % _blockSize;
        private int frontBlockIndex => (midIndex - frontCount) / _blockSize;
        private int backIndex => midIndex + backCount % _blockSize;
        private int backBlockIndex => (midIndex + backCount) / _blockSize;


        public Deque() {
            Map = new Block<T>[1];
            Map[0] = new Block<T>();
            Count = frontCount = backCount = 0;
            _blockSize = Map[0].Length;
        }

        public T this[int index]
        {
            get {
                int block = frontBlockIndex + index / _blockSize;
                int i = index % _blockSize;
                return Map[block][i];
            }
            set {
                int block = frontBlockIndex + index / _blockSize;
                int i = index % _blockSize;
                Map[block][i] = value;
            }
        }

        private void Resize() {
            T[] temp = new T[Count];
            CopyTo(temp, 0);
            int num = Map.Length;
            Map = new Block<T>[num * 2];
            int k = 0;
            for(int i = frontIndex; i < _blockSize; i++) {
                Map[frontBlockIndex][i] = temp[k];
                k++;
            }

            for(int i = frontBlockIndex + 1; i < backBlockIndex; i++) {
                for(int j = 0; j < _blockSize; j++) {
                    Map[i][j] = temp[k];
                    k++;
                }
            }
            for(int i = 0; i < backIndex + 1; i++) {
                Map[backBlockIndex][i] = temp[k];
                k++;
            }
        }

        public void Add(T item) {
            PushBack(item);
        }

        public void Clear() {
            Array.Clear(Map, 0, Map.Length);
            Count = frontCount = backCount = 0;
        }

        public bool Contains(T item) {
            foreach(Block<T> array in Map) {
                if(array.Exists(item)) {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) {
            array = new T[Count];
            int i = arrayIndex;

            while(GetEnumerator().MoveNext()) {
                array[i] = GetEnumerator().Current;
                i++;
            }
        }

        public IEnumerator<T> GetEnumerator() {
            for(int i = frontIndex; i < _blockSize; i++) {
                yield return Map[frontBlockIndex][i];
            }
            for(int i = frontBlockIndex + 1; i < backBlockIndex; i++) {
                for(int j = 0; j < Map[i].Length; j++) {
                    yield return Map[i][j];
                }
            }
            for(int i = 0; i < backIndex + 1; i++) {
                yield return Map[backBlockIndex][i];
            }
        }

        public int IndexOf(T item) {
            int i = 0;
            while(GetEnumerator().MoveNext()) {
                T current = GetEnumerator().Current;
                if(current.Equals(item)) {
                    return i;
                }
                i++;
            }
            throw new InvalidOperationException($"Element '{item}' is not in the Deque");
        }

        public void Insert(int index, T item) {
            throw new NotImplementedException();
        }

        public void PopBack() {
            if(Count == 0) {
                throw new InvalidOperationException();
            }
            Map[backBlockIndex][backIndex] = default(T);
            Count--;
            backCount--;

        }

        public void PopFront() {
            if(Count == 0) {
                throw new InvalidOperationException();
            }
            Map[frontBlockIndex][frontIndex] = default(T);
            Count--;
            frontCount--;

        }

        public void PushBack(T value) {
            // same algorithm as in PushFront method
            if(backIndex + 1 == _blockSize) {

                if(backBlockIndex == Map.Length - 1) {
                    Resize();
                }
                Map[backBlockIndex + 1] = new Block<T>();
                Map[backBlockIndex + 1][0] = value;
            }
            else {
                Map[backBlockIndex][backIndex + 1] = value;
            }
            Count++;
            backCount++;
        }

        public void PushFront(T value) {
            // if the block has been already filled.
            if(frontIndex - 1 < 0) {
                // if the are not empty blocks at the front side of the Map.
                if(frontBlockIndex == 0) {
                    Resize();
                }
                // allocate a new block and add a new element there.
                // if the Map is full or not, still in both cases we have to allocate new block of memory
                Map[frontBlockIndex - 1] = new Block<T>();
                Map[frontBlockIndex - 1][_blockSize - 1] = value;
            } // just add new element
            else {
                Map[frontBlockIndex][frontIndex - 1] = value;
            }
            Count++;
            frontCount++;
        }

        public bool Remove(T item) {
            // remove in O(1) from back or front
            if (Map[frontBlockIndex][frontIndex].Equals(item)) {
                Map[frontBlockIndex][frontIndex] = default(T);
                Count--;
                frontCount--;
                return true;
            }
            if (Map[backBlockIndex][backIndex].Equals(item)) {
                Map[backBlockIndex][backIndex] = default(T);
                Count--;
                backCount--;
                return true;
            }

            



            int index = 0;
            for(int i = frontIndex+1; i < _blockSize; i++) {
                if (Map[frontBlockIndex][i].Equals(item)) {
                    Map[frontBlockIndex][i] = default(T);
                    Map[frontBlockIndex]
                    Array.Copy(Map[frontBlockIndex],i-1,Map[frontBlockIndex],i,i-1-frontIndex);
                }
            }
            for(int i = frontBlockIndex + 1; i < backBlockIndex; i++) {
                for(int j = 0; j < Map[i].Length; j++) {
                    if (Map[i][j].Equals(item)) {
                        
                    }
                }
            }
            for(int i = 0; i < backIndex; i++) {
                if (Map[backBlockIndex][i].Equals(item)) {
                    
                }
            }

            return false;
        }

        public void RemoveAt(int index) {
            throw new NotImplementedException();
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
