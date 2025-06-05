using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedListLibrary
{
    public class ListNode
    {
        private int value;
        private ListNode previous;
        private ListNode next;

        public ListNode Previous { get { return previous; } set { previous = value; } }
        public ListNode Next { get { return next; } set { next = value; } }
        public int Value { get { return value; } set { this.value = value; } }

        public ListNode(int value, ListNode previous, ListNode next)
        {
            this.value = value;
            this.previous = previous;
            this.next = next;
        }
    }
}
