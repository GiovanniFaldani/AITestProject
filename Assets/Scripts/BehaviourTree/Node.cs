using System.Collections.Generic;
using UnityEngine;

// code sourced from https://github.com/MinaPecheux/UnityTutorials-BehaviourTrees/tree/master/Assets/Scripts/BehaviorTree
// for the basic BT implementation

namespace BehaviourTree
{
    public enum NodeState { SUCCESS, FAILURE, RUNNING }


    public class Node
    {
        public NodeState state;

        public Node parent;
        public List<Node> children = new List<Node>();

        // Questo sarà la blackboard del BT
        private Dictionary<string, object> dataContext = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }
        public Node(List<Node> children)
        {
            foreach (Node child in children)
                Attach(child);
        }

        private void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        // Recursive to allow to fetch data from parent nodes
        public object GetData(string key)
        {
            object value = null;
            if (dataContext.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        // Recursive to allow to delete data from parent nodes
        public bool ClearData(string key)
        {
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}
