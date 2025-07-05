using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBT : BehaviourTree.Tree
{
    private AIController controller;

    protected override Node SetupTree()
    {
        controller = GetComponent<AIController>();

        // Behavior definitions
        Node goToPoint = new GoToPointBehavior(controller.agent, AIManager.Instance.pointHomes, controller.destinationMargin);
        Node stayOnPoint = new StayOnPointBehavior(controller.agent, AIManager.Instance.pointHomes, controller.destinationMargin);


        Node root = new Selector(new List<Node>
        {
            // Add higher priority behaviours

            new Sequence(new List<Node>
            {
                goToPoint,
                stayOnPoint,
            })
        });

        // c'� bisogno di: sequencer che fa GoToPoint, poi StayOnPoint finch� non arriva in vantaggio l'AI, e poi TakeCover
        // A priorit� maggiore (pi� in basso a sx del BT), ci saranno Fight/ShootPlayerInRange e FindHealthPack con nodi selector

        return root;
    }
}