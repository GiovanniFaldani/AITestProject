using BehaviourTree;
using System.Collections.Generic;

namespace BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            NodeState childStatus = children[currentChild].Evaluate();

            if (childStatus == NodeState.RUNNING || childStatus == NodeState.FAILURE)
                return childStatus;

            currentChild++;

            if (currentChild >= children.Count)
            {
                currentChild = 0;
                return NodeState.SUCCESS;
            }

            return NodeState.RUNNING;
        }

    }

}