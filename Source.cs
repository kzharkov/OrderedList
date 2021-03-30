using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class Node<T>
    {
        public T value;
        public Node<T> next, prev;

        public Node(T _value)
        {
            value = _value;
            next = null;
            prev = null;
        }
    }

    public class OrderedList<T>
    {
        public Node<T> head, tail;
        private bool _ascending;

        public OrderedList(bool asc)
        {
            head = null;
            tail = null;
            _ascending = asc;
        }

        public int Compare(T v1, T v2)
        {
            int result;
            if (typeof(T) == typeof(String))
            {
                result = String.Compare((v1 as String).Trim(), (v2 as String).Trim(), StringComparison.Ordinal);
                if (result < 0) result = -1;
                if (result > 0) result = 1;
            }
            else
            {
                result = Comparer<T>.Default.Compare(v1, v2);
            }

            return result;
            // -1 если v1 < v2
            // 0 если v1 == v2
            // +1 если v1 > v2
        }

        public void Add(T value)
        {
            if (head == null)
            {
                Node<T> newNode = new Node<T>(value);
                head = newNode;
                tail = newNode;
                return;
            }
            Node<T> node = head;
            while (node != null)
            {
                if ((Compare(value, node.value) != -1 && !_ascending) ||
                    (Compare(value, node.value) != 1 && _ascending))
                {
                    Node<T> newNode = new Node<T>(value)
                    {
                        next = node,
                        prev = node.prev
                    };
                    if (node == head)
                    {
                        head = newNode;
                    }
                    else
                    {
                        node.prev.next = newNode;
                    }
                    node.prev = newNode;
                    return;
                }
                if (node == tail)
                {
                    Node<T> newNode = new Node<T>(value)
                    {
                        prev = node
                    };
                    tail = newNode;
                    node.next = newNode;
                    return;
                }
                node = node.next;
            }
        }

        public Node<T> Find(T val)
        {
            Node<T> node = head;
            while (node != null)
            {
                if (Compare(val, node.value) == 0) return node;
                if (((Compare(val, node.value) == -1) && _ascending) || ((Compare(val, node.value) == 1) && !_ascending))
                {
                    return null;
                }
                node = node.next;
            }
            return null;
        }

        public void Delete(T val)
        {
            Node<T> node = Find(val);
            if (node == null) return;
            if (node == head)
            {
                if (node == tail)
                {
                    head = null;
                    tail = null;
                    return;
                }
                node.next.prev = null;
                head = node.next;
                return;
            }
            if (node == tail)
            {
                node.prev.next = null;
                tail = node.prev;
                return;
            }

            node.next.prev = node.prev;
            node.prev.next = node.next;
        }

        public void Clear(bool asc)
        {
            _ascending = asc;
            Node<T> node = head;
            while (node != null)
            {
                Node<T> current = node;
                node = node.next;
                current.next = null;
            }
            head = null;
            tail = null;
        }

        public int Count()
        {
            int count = 0;
            Node<T> node = head;
            while (node != null)
            {
                node = node.next;
                count++;
            }
            return count;
        }

        List<Node<T>> GetAll() // выдать все элементы упорядоченного 
                               // списка в виде стандартного списка
        {
            List<Node<T>> r = new List<Node<T>>();
            Node<T> node = head;
            while (node != null)
            {
                r.Add(node);
                node = node.next;
            }
            return r;
        }
    }

}