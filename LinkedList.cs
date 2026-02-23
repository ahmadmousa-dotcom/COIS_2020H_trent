using System;

namespace DataStructures.Lists
{
         /// ABSTRACTION: Defines the common interface for all Linked List variants.
         public interface ILinkedListUnit<T>
    {
        void InsertFirst(T data);
        void InsertLast(T data);
        void Delete(T data);
        bool Search(T data);
        void PrintAll();
        int Count { get; }
    }

         /// SINGLY LINKED LIST: Forward-only traversal. 
    /// Optimized with Head and Tail pointers for O(1) insertions at both ends.
         public class SinglyListUnit<T> : ILinkedListUnit<T>
    {
        private class Node
        {
            public T Data { get; }
            public Node Next { get; set; }
            public Node(T data) { Data = data; Next = null; }
        }

        private Node _head;
        private Node _tail;
        private int _count;

        public int Count => _count;

        public void InsertFirst(T data)
        {
            // O(1): Create new node and update Head pointer
            Node newNode = new Node(data);
            if (_head == null) _tail = newNode;
            else newNode.Next = _head;
            
            _head = newNode;
            _count++;
        }

        public void InsertLast(T data)
        {
            // O(1): Append using the Tail pointer
            Node newNode = new Node(data);
            if (_head == null) _head = newNode;
            else _tail.Next = newNode;
            
            _tail = newNode;
            _count++;
        }

        public bool Search(T data)
        {
            // O(n): Sequential search starting from Head
            Node current = _head;
            while (current != null)
            {
                if (current.Data.Equals(data)) return true;
                current = current.Next;
            }
            return false;
        }

        public void Delete(T data)
        {
            // O(n): Must find the node AND its predecessor to bridge the gap
            if (_head == null) return;

            if (_head.Data.Equals(data))
            {
                _head = _head.Next;
                if (_head == null) _tail = null;
                _count--;
                return;
            }

            Node current = _head;
            while (current.Next != null && !current.Next.Data.Equals(data))
                current = current.Next;

            if (current.Next != null)
            {
                if (current.Next == _tail) _tail = current;
                current.Next = current.Next.Next;
                _count--;
            }
        }

        public void PrintAll()
        {
            Node current = _head;
            while (current != null)
            {
                Console.Write($"{current.Data} -> ");
                current = current.Next;
            }
            Console.WriteLine("NULL");
        }
    }

         /// DOUBLY LINKED LIST: Forward and Backward traversal.
    /// Uses Next and Previous pointers for enhanced flexibility.
         public class DoublyListUnit<T> : ILinkedListUnit<T>
    {
        private class Node
        {
            public T Data { get; }
            public Node Next { get; set; }
            public Node Prev { get; set; }
            public Node(T data) { Data = data; }
        }

        private Node _head;
        private Node _tail;
        private int _count;

        public int Count => _count;

        public void InsertFirst(T data)
        {
            // O(1): Update both Head and the former head's Prev link
            Node newNode = new Node(data);
            if (_head == null) _tail = newNode;
            else
            {
                newNode.Next = _head;
                _head.Prev = newNode;
            }
            _head = newNode;
            _count++;
        }

        public void InsertLast(T data)
        {
            // O(1): Direct access via Tail pointer
            Node newNode = new Node(data);
            if (_tail == null) _head = newNode;
            else
            {
                _tail.Next = newNode;
                newNode.Prev = _tail;
            }
            _tail = newNode;
            _count++;
        }

        public bool Search(T data)
        {
            // O(n): Traverses forward from Head
            Node current = _head;
            while (current != null)
            {
                if (current.Data.Equals(data)) return true;
                current = current.Next;
            }
            return false;
        }

        public void Delete(T data)
        {
            // O(n) to find, but O(1) to re-link because we have the Prev pointer
            Node current = _head;
            while (current != null && !current.Data.Equals(data))
                current = current.Next;

            if (current == null) return;

            if (current.Prev != null) current.Prev.Next = current.Next;
            else _head = current.Next;

            if (current.Next != null) current.Next.Prev = current.Prev;
            else _tail = current.Prev;

            _count--;
        }

        public void PrintAll()
        {
            Node current = _head;
            while (current != null)
            {
                Console.Write($"{current.Data} <-> ");
                current = current.Next;
            }
            Console.WriteLine("NULL");
        }
    }
}