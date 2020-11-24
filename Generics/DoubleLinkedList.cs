using System;
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
        IDataNode<TKey, TValue> AppendNode(TKey Key, TValue Value);
        IDataNode<TKey, TValue> Find(TKey Key);
        IDataNode<TKey, TValue> InsertAfter(TKey KeyAfter, TKey InsertKey, TValue InsertValue);
        IDataNode<TKey, TValue> InsertBefore(TKey KeyBefore, TKey InsertKey, TValue InsertValue);
        void Remove(TKey Key);
        string ToString();
    }

    public class DoubleLinkedList<TKey, TValue> : IDoubleLinkedList<TKey, TValue>
    {
        public IDataNode<TKey, TValue> RootNode { get; private set; }
        public IDataNode<TKey, TValue> TerminalNode { get; private set; }


        public IDataNode<TKey, TValue> AppendNode(TKey Key, TValue Value)
        {
            IDataNode<TKey, TValue> Existing = Find(Key);
            if (Existing != null)
                throw new System.ArgumentException("Duplicate keys are not allowed", nameof(Key));

            IDataNode<TKey, TValue> NewNode = new DataNode<TKey, TValue>(Key, Value);
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

        public IDataNode<TKey, TValue> Find(TKey Key)
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

        public void Remove(TKey Key)
        {
            IDataNode<TKey, TValue> Found = Find(Key);
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


        public IDataNode<TKey, TValue> InsertAfter(TKey KeyAfter, TKey InsertKey, TValue InsertValue)
        {
            IDataNode<TKey, TValue> Existing = Find(InsertKey);
            if (Existing != null)
                throw new System.ArgumentException("Duplicate keys are not allowed", nameof(InsertKey)); ;


            IDataNode<TKey, TValue> Found = Find(KeyAfter);
            IDataNode<TKey, TValue> NewNode = new DataNode<TKey, TValue>(InsertKey, InsertValue);
            NewNode.InsertAfter(Found);
            if (NewNode.NextNode == null)
                TerminalNode = NewNode;
            return (NewNode);
        }

        public IDataNode<TKey, TValue> InsertBefore(TKey KeyBefore, TKey InsertKey, TValue InsertValue)
        {
            IDataNode<TKey, TValue> Existing = Find(InsertKey);
            if (Existing != null)
                throw new System.ArgumentException("Duplicate keys are not allowed", nameof(InsertKey)); ;

            IDataNode<TKey, TValue> Found = Find(KeyBefore);
            IDataNode<TKey, TValue> NewNode = new DataNode<TKey, TValue>(InsertKey, InsertValue);
            NewNode.InsertAfter(Found);
            return (NewNode);
        }

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
