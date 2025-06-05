using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedListLibrary
{
    public class DoublyLinkedList : IEnumerable<int>
    {
        private ListNode head;
        private ListNode tail;
        private int length;

        public ListNode Head {  get { return head; } }
        public ListNode Tail { get { return tail; } }
        public int Length { get { return length; } }

        public DoublyLinkedList()
        {
            length = 0;
        }

        public int this[int nodeIndex]
        {
            get
            {
                IndexValidation(nodeIndex);
                ListNode node = GetNodeByTheIndex(nodeIndex);
                return node.Value;
            }
            set
            {
                IndexValidation(nodeIndex);
                ListNode node = GetNodeByTheIndex(nodeIndex);
                node.Value = value;
            }
        }

        public void AddNode(int nodeValue)
        {
            ListNode node;
            if(tail is ListNode)
            {
                node = new ListNode(nodeValue, tail, null);
                tail.Next = node;
                tail = node;
            }
            else
            {
                node = new ListNode(nodeValue, null, null);
                head = node;
                tail = node;
            }
            length++;
        }

        public void RemoveNode(int nodeIndex)
        {
            IndexValidation(nodeIndex);
            ListNode node = GetNodeByTheIndex(nodeIndex);
            ListNode previousNode = node.Previous;
            ListNode nextNode = node.Next;

            if (previousNode != null)
            {
                previousNode.Next = nextNode;
            }
            else
            {
                head = nextNode;
                if (nextNode != null)
                    nextNode.Previous = null;
            }

            if (nextNode != null)
            {
                nextNode.Previous = previousNode;
            }
            else
            {
                tail = previousNode;
                if (previousNode != null)
                    previousNode.Next = null;
            }

            length--;
        }

        public int FirstEntry(int value)
        {
            int counter = 0;
            foreach (var nodeValue in this) 
            {
                if(nodeValue == value)
                {
                    return counter;
                }
                counter++;
            }
            return -1;
        }

        public int FindSumOnOddPositions()
        {
            int sum = 0;
            int position = 1;
            foreach(var nodeValue in this)
            {
                if(position % 2 != 0)
                {
                    sum += nodeValue;
                }
                position++;
            }
            return sum;
        }

        public DoublyLinkedList GetNewListWithHigherElements(int value)
        {
            DoublyLinkedList newList = new DoublyLinkedList();
            foreach (var nodeValue in this) 
            {
                if(nodeValue > value)
                {
                    newList.AddNode(nodeValue);
                }
            }
            return newList;
        }

        public void RemoveElementsHigherAverageValue()
        {
            if (length == 0)
                return;

            double averageValue = 0.0;
            int elementsSum = 0;

            foreach (var nodeValue in this)
            {
                elementsSum += nodeValue;
            }

            averageValue = (double)elementsSum / length;

            for (int i = length - 1; i >= 0; i--)
            {
                if (this[i] > averageValue)
                {
                    RemoveNode(i);
                }
            }
        }

        private void IndexValidation(int index)
        {
            if (index >= length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
        }

        private ListNode GetNodeByTheIndex(int nodeIndex)
        {
            ListNode node = head;
            for (int i = 0; i < nodeIndex; i++)
            {
                node = node.Next;
            }
            return node;
        }

        public IEnumerator<int> GetEnumerator()
        {
            ListNode current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}