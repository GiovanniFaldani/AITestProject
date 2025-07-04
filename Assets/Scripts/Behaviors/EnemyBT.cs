using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBT : BehaviourTree.Tree
{
    private AIController controller;

    protected override Node SetupTree()
    {
        controller = GetComponent<AIController>();
        Node goToPoint = new GoToPointBehavior(controller.agent, AIManager.Instance.pointHomes, controller.destinationMargin);
        Node root = new Selector(new List<Node>
        {
            goToPoint
        });

        return root;
    }
}