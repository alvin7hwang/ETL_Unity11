using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal struct KeyValuePair<TKey, TValue>
    {
        internal KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        internal TKey Key;
        internal TValue Value;
    }

    internal class MyHashtable<TKey, TValue>
    {
        internal MyHashtable(int capacity)
        {
            buckets = new int[capacity];

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = -1; // 유효하지않은값으로 초기화
            }

            entries = new Entry[capacity];
        }


        internal struct Entry
        {
            internal int HashCode;
            internal TKey Key;
            internal TValue Value;
            internal int NextIndex;
        }

        int[] buckets; // Entry 의 시작점 인덱스 참조 배열
        Entry[] entries; // 키-밸류 쌍 데이터 저장하는 배열

        internal void Add(TKey key, TValue value)
        {
            // 1. Key 중복검사.
            // 2. Key 가 중복 ? 
            //      시작 entry 를 가져와서 빈자리가 나올때까지 탐색

            int hashCode = GetHash(key.ToString()); // key 에 대한 hashcode 생성
            int bucketIndex = hashCode % buckets.Length; // hashcode 를 capacity 로 mod 해서 bucketIndex 구함

            // buckets 에서 유효한 값은 양수이므로, 유효하지않은 인덱스값이 나올떄까지 반복
            for (int i = buckets[bucketIndex]; i >= 0; i = entries[i].NextIndex)
            {
                if (entries[i].HashCode == hashCode && entries[i].Key.Equals(key))
                    throw new ArgumentException("The Key already exists.");
            }

            entries[??] = new Entry
            {
                HashCode = hashCode,
                Key = key,
                Value = value,
            };
        }

        // "GI" -> hash? 144
        // "FJ" -> hash? 144
        int GetHash(string name)
        {
            int hash = 0;

            for (int i = 0; i < name.Length; i++)
            {
                hash += name[i];
            }

            return hash;
        }
    }
}
