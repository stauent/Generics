using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    /// <summary>
    /// Generic interface used by all DataNodes
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    public interface IDataNode<TKey, TValue>
    {
        TKey Key { get; set; }
        IDataNode<TKey, TValue> NextNode { get; set; }
        IDataNode<TKey, TValue> PreviousNode { get; set; }
        TValue Value { get; set; }

        void InsertAfter(IDataNode<TKey, TValue> After);
        void InsertBefore(IDataNode<TKey, TValue> Before);
        string ToString();
    }

    public class DataNode<TKey, TValue> : IDataNode<TKey, TValue>
    {
        public IDataNode<TKey, TValue> NextNode { get; set; }

        public IDataNode<TKey, TValue> PreviousNode { get; set; }
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public DataNode(TKey Key, TValue Value)
        {
            this.Key = Key;
            this.Value = Value;
            NextNode = null;
            PreviousNode = null;
        }
        public void InsertBefore(IDataNode<TKey, TValue> Before)
        {
            if (Before != null)
            {
                PreviousNode = Before.PreviousNode;
                NextNode = Before;
                Before.PreviousNode = this;
            }
        }

        public void InsertAfter(IDataNode<TKey, TValue> After)
        {
            if (After != null)
            {
                NextNode = After.NextNode;
                PreviousNode = After;
                After.NextNode = this;
            }
        }

        public override string ToString()
        {
            return $"{Key}={Value}";
        }
    }


    /// <summary>
    /// Generic interface used by all DoubleLinkedList
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    public interface IDoubleLinkedList<TKey, TValue>
    {
        IDataNode<TKey, TValue> Append(TKey Key, TValue Value);
        IDataNode<TKey, TValue> this[TKey Key] { get; set; }
        IDataNode<TKey, TValue> InsertAfter(TKey KeyAfter, TKey InsertKey, TValue InsertValue);
        IDataNode<TKey, TValue> InsertBefore(TKey KeyBefore, TKey InsertKey, TValue InsertValue);
        void Remove(TKey Key);
        string ToString();
    }

    public class DoubleLinkedList<TKey, TValue> : IDoubleLinkedList<TKey, TValue>
    {

        /// <summary>
        /// Reference to the root of the doubly linked list
        /// </summary>
        public IDataNode<TKey, TValue> RootNode { get; private set; }

        /// <summary>
        /// Reference to the last/terminal node in the doubly linked list 
        /// </summary>
        public IDataNode<TKey, TValue> TerminalNode { get; private set; }


        /// <summary>
        /// Appends a node at the end of the list
        /// </summary>
        /// <param name="Key">Unique key used to look up the associated value</param>
        /// <param name="Value">The value to be placed in the appended node</param>
        /// <returns>IDataNode reference to the newly appended node</returns>
        public IDataNode<TKey, TValue> Append(TKey Key, TValue Value)
        {
            IDataNode<TKey, TValue> Existing = this[Key];
            if (Existing != null)
                throw new System.ArgumentException("Duplicate keys are not allowed", nameof(Key));

            IDataNode<TKey, TValue> NewNode = CreateNode(Key, Value);
            if (RootNode == null)
            {
                RootNode = TerminalNode = NewNode;
            }
            else
            {
                NewNode.InsertAfter(TerminalNode);
                TerminalNode = NewNode;
            }

            return (NewNode);
        }

        /// <summary>
        /// Factory method used to create a new node. It is not inserted into the
        /// list.
        /// </summary>
        /// <param name="Key">Unique key used to look up the associated value</param>
        /// <param name="Value">The value to be placed in the appended node</param>
        /// <returns>IDataNode reference to the newly appended node</returns>
        public IDataNode<TKey, TValue> CreateNode(TKey Key, TValue Value)
        {
            IDataNode<TKey, TValue> NewNode = new DataNode<TKey, TValue>(Key, Value);
            return (NewNode);
        }

        /// <summary>
        /// Indexer allows the caller to get/set node values via array syntax
        /// </summary>
        /// <param name="Key">Lookup key to find value in the list</param>
        /// <returns>IDataNode reference to matching node if found. Null otherwise</returns>
        public IDataNode<TKey, TValue> this[TKey Key]
        {
            get
            {
                IDataNode<TKey, TValue> Found = RootNode;
                while (Found != null)
                {
                    if (Found.Key.Equals(Key))
                        break;
                    else
                    {
                        Found = Found.NextNode;
                    }
                }

                return (Found);
            }
            set
            {
                IDataNode<TKey, TValue> Found = RootNode;
                while (Found != null)
                {
                    if (Found.Key.Equals(Key))
                    {
                        // We are replacing the "Found" node with the
                        // one provided by the caller.
                        value.NextNode = Found.NextNode;
                        value.PreviousNode = Found.NextNode;

                        // Reset the root or terminal node reference if the 
                        // node being replaced was one of those.
                        if (TerminalNode == Found)
                            TerminalNode = value;
                        if (RootNode == Found)
                            RootNode = Found;
                    }
                    else
                    {
                        Found = Found.NextNode;
                    }
                }
            }
        }

        /// <summary>
        /// Removes the node matching the specified key from the list
        /// </summary>
        /// <param name="Key">Key of the node you want to remove</param>
        public void Remove(TKey Key)
        {
            IDataNode<TKey, TValue> Found = this[Key];
            if (Found != null)
            {
                if (Found.NextNode == null)
                    TerminalNode = Found.PreviousNode;
                if (Found.PreviousNode == null)
                    RootNode = Found.NextNode;

                if (Found.PreviousNode != null)
                    Found.PreviousNode.NextNode = Found.NextNode;

                if (Found.NextNode != null)
                    Found.NextNode.PreviousNode = Found.PreviousNode;
            }
        }

        /// <summary>
        /// Inserts a new node after the node specified
        /// </summary>
        /// <param name="KeyAfter">New node is inserted after the node containing this key</param>
        /// <param name="InsertKey">Key of new node being inserted</param>
        /// <param name="InsertValue">Value of new node being inserted</param>
        /// <returns>IDataNode of the new node that was inserted</returns>
        public IDataNode<TKey, TValue> InsertAfter(TKey KeyAfter, TKey InsertKey, TValue InsertValue)
        {
            IDataNode<TKey, TValue> Existing = this[InsertKey];
            if (Existing != null)
                throw new System.ArgumentException("Duplicate keys are not allowed", nameof(InsertKey)); ;


            IDataNode<TKey, TValue> NewNode = new DataNode<TKey, TValue>(InsertKey, InsertValue);
            IDataNode<TKey, TValue> Found = this[KeyAfter];
            NewNode.InsertAfter(Found);

            if (NewNode.NextNode == null)
                TerminalNode = NewNode;

            return (NewNode);
        }

        /// <summary>
        /// Inserts a new node before the node specified
        /// </summary>
        /// <param name="KeyAfter">New node is inserted after the node containing this key</param>
        /// <param name="InsertKey">Key of new node being inserted</param>
        /// <param name="InsertValue">Value of new node being inserted</param>
        /// <returns>IDataNode of the new node that was inserted</returns>
        public IDataNode<TKey, TValue> InsertBefore(TKey KeyBefore, TKey InsertKey, TValue InsertValue)
        {
            IDataNode<TKey, TValue> Existing = this[InsertKey];
            if (Existing != null)
                throw new System.ArgumentException("Duplicate keys are not allowed", nameof(InsertKey)); ;

            IDataNode<TKey, TValue> NewNode = new DataNode<TKey, TValue>(InsertKey, InsertValue);
            IDataNode<TKey, TValue> Found = this[KeyBefore];
            NewNode.InsertBefore(Found);

            if (NewNode.PreviousNode == null)
                RootNode = NewNode;

            return (NewNode);
        }

        /// <summary>
        /// Converts the entire list into a single string
        /// where each node's key/value appear on a separate line
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\r\n==============================================");
            IDataNode<TKey, TValue> Found = RootNode;
            while (Found != null)
            {
                sb.AppendLine(Found.ToString());
                Found = Found.NextNode;
            }
            return (sb.ToString());
        }


    }
}
