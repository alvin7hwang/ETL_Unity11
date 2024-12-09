﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicArray
{
    internal class Node<T>
    {
        public Node(MyLinkedList<T> owner, T value)
        {
            Owner = owner;
            Value = value;
        }


        internal MyLinkedList<T> Owner;
        internal T Value;
        internal Node<T> Next;
        internal Node<T> Prev;
    }

    internal class MyLinkedList<T>
    {
        internal Node<T> First => _first;
        internal Node<T> Last => _last;

        private Node<T> _first, _last;
        private int _size;


        /// <summary>
        /// 특정 노드 앞에 삽입
        /// </summary>
        /// <param name="node"> 기준 노드 </param>
        /// <param name="value"> 기준노드 앞에 삽입하려는 값</param>
        internal void AddBefore(Node<T> node, T value)
        {
            // 1. node 가 현재 linkedlist 에 속해있는지 검증
            // 2. 새 노드 생성
            // 3. node Prev가 존재한다면 
            //      node 의 Prev의 Next 를 새 노드로
            //      새 노드의 Prev를 node 의 이전노드로
            //
            //    node Prev가 존재하지않는다면 (node 가 first 라면)
            //      first 를 새노드로
            // 4. node 의 Prev를 새 노드로
            // 5. 새 노드의 Next를 node 로
            // 6. size 1 증가

            if (node.Owner != this)
                throw new InvalidOperationException("Node does not belong to this LinkedList");

            Node<T> newNode = new Node<T>(this, value);

            if (node.Prev != null)
            {
                node.Prev.Next = newNode;
                newNode.Prev = node.Prev;
            }
            else
            {
                _first = newNode;
            }

            node.Prev = newNode;
            newNode.Next = node;
            _size++;
        }

        /// <summary>
        /// 특정 노드 뒤에 삽입
        /// </summary>
        /// <param name="node"> 기준 노드 </param>
        /// <param name="value"> 기준노드 뒤에 삽입하려는 값</param>
        internal void AddAfter(Node<T> node, T value)
        {
            if (node.Owner != this)
                throw new InvalidOperationException("Node does not belong to this LinkedList");

            Node<T> newNode = new Node<T>(this, value);

            if (node.Next != null)
            {
                node.Next.Prev = newNode;
                newNode.Next = node.Next;
            }
            else
            {
                _last = newNode;
            }

            node.Next = newNode;
            newNode.Prev = node;
            _size++;
        }

        internal void AddFirst(T value)
        {
            AddBefore(_first, value);
        }

        internal void AddLast(T value)
        {
            AddAfter(_last, value);
        }

        /// <summary>
        /// First 부터 Last 까지 match 조건에 맞는 노드를 찾음
        /// </summary>
        /// <param name="match"> 탐색 조건 </param>
        /// <returns> 찾은 노드 </returns>
        internal Node<T> Find(Predicate<T> match)
        {
            // 1. First 노드 참조 가져옴 
            // 2. 현재 탐색중인 노드의 Next 가 없을때까지 순회하면서 match 조건 체크

            Node<T> tmp = _first;

            while (tmp != null)
            {
                if (match.Invoke(tmp.Value))
                    return tmp;

                tmp = tmp.Next;
            }

            return default;
        }

        /// <summary>
        /// Last 부터 First 까지 match 조건에 맞는 노드를 찾음
        /// </summary>
        /// <param name="match"> 탐색 조건 </param>
        /// <returns> 찾은 노드 </returns>
        internal Node<T> FindLast(Predicate<T> match)
        {
            Node<T> tmp = _last;

            while (tmp != null)
            {
                if (match.Invoke(tmp.Value))
                    return tmp;

                tmp = tmp.Prev;
            }

            return default;
        }

        internal bool Remove(Node<T> node)
        {
            // 1. node 가 null 인지 확인
            // 2. node 가 현재 linkedlist 에 속한지 검증
            // 3. node 의 Prev 가 존재한다면
            //      node 의 Prev 의 Next 를 node 의 Next 로
            //    그렇지않다면
            //      first 가 지워지는것이므로 first 를 node 의 Next로 
            // 4. node 의 Next 가 존재한다면
            //      node 의 Next 의 Prev 를 node 의 Prev 로
            //    그렇지않다면
            //      last 가 지워지는것이므로 last 를 node 의 Prev 로
            // 5. size 1 감소

            if (node == null)
                return false;

            if (node.Owner != this)
                throw new InvalidOperationException("The node does not belong to this linkedList");

            if (node.Prev != null)
                node.Prev.Next = node.Next;
            else
                _first = node.Next;

            if (node.Next != null)
                node.Next.Prev = node.Prev;
            else
                _last = node.Prev;

            _size--;
            return true;
        }

        internal bool Remove(T value)
        {
            return Remove(Find(x => x.Equals(value)));
        }

        internal bool RemoveLast(T value)
        {
            return Remove(FindLast(x => x.Equals(value)));
        }
    }
}
