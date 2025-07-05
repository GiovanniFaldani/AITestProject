using BehaviourTree;
using System.Collections.Generic;

namespace BehaviourTree
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            NodeState childStatus = children[currentChild].Evaluate();

            if (childStatus == NodeState.RUNNING) return NodeState.RUNNING;

            if (childStatus == NodeState.SUCCESS)
            {
                currentChild = 0;
                return NodeState.SUCCESS;
            }

            currentChild++;
            if (currentChild >= children.Count)
            {
                currentChild = 0;
                return NodeState.FAILURE;
            }

            return NodeState.RUNNING;
        }

    }

}