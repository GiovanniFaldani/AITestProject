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
        Node seekCover = new SeekCoverBehavior(controller.agent, AIManager.Instance.coverHomes, controller.destinationMargin);
        Node seekHealthPack = new SeekHealthPackBehavior(controller.agent, AIManager.Instance.healthPacks, 
            controller.destinationMargin, controller);
        Node getInRange = new GetInRangeBehavior(controller.agent, controller);
        Node attack = new AttackBehavior(controller);

        // c'è bisogno di: sequencer che fa GoToPoint, poi StayOnPoint finché non arriva in vantaggio l'AI, e poi TakeCover
        // A priorità maggiore (più in basso a sx del BT), ci saranno Fight/ShootPlayerInRange e FindHealthPack con nodi selector
        Node root = new Selector(new List<Node>
        {
            // Add GetInRange and Attack behavior here as a sequence that can fail
            new Sequence(  new List<Node>
            {
               getInRange,
               attack
            }),
            new Sequence( new List<Node>
            {
                seekHealthPack,
                goToPoint,
                stayOnPoint,
                seekCover
            })
        });


        return root;
    }
}