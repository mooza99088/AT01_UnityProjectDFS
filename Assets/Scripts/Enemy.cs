using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Tooltip("Movement speed modifier.")]
    [SerializeField] private float speed = 3;
    private Node currentNode;
    private Vector3 currentDir;
    private bool playerCaught = false;

    public delegate void GameEndDelegate();
    public event GameEndDelegate GameOverEvent = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        InitializeAgent();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCaught == false)
        {
            if (currentNode != null)
            {
                //If within 0.25 units of the current node.
                if (Vector3.Distance(transform.position, currentNode.transform.position) > 0.25f)
                {
                    transform.Translate(currentDir * speed * Time.deltaTime);
                }
                //Implement path finding here
            }
            else
            {
                Debug.LogWarning($"{name} - No current node");
            }

            Debug.DrawRay(transform.position, currentDir, Color.cyan);
        }
    }

    //Called when a collider enters this object's trigger collider.
    //Player or enemy must have rigidbody for this to function correctly.
    private void OnTriggerEnter(Collider other)
    {
        if (playerCaught == false)
        {
            if (other.tag == "Player")
            {
                playerCaught = true;
                GameOverEvent.Invoke(); //invoke the game over event
            }
        }
    }

    /// <summary>
    /// Sets the current node to the first in the Game Managers node list.
    /// Sets the current movement direction to the direction of the current node.
    /// </summary>
    void InitializeAgent()
    {
        currentNode = GameManager.Instance.Nodes[0];
        currentDir = currentNode.transform.position - transform.position;
        currentDir = currentDir.normalized;
    }

    //Implement DFS algorithm method here
   // public void DepthFirstSearch()
   // {
        public string CurrentNode;
        public bool TargetFound;

        public class unsearchedNodes
        {
            public static void Main(string[] args)
            {
                var nodes = new List<string>();
                nodes.Add("RootNode");
                nodes.Add("Node1");
                nodes.Add("Node2");
                nodes.Add("Node3");
                nodes.Add("Node4");
                nodes.Add("Node5");
                nodes.Add("Node6");
                nodes.Add("Node7");
                nodes.Add("Node8");
                nodes.Add("Node9");

                foreach (var node in nodes)
                {
                    Console.WriteLine(node);
                }
            }
        }
    }
//}




//VARIABLE FOR 'NODE CURRENTLY BEING SEARCHED'

    //BOOLEAN FOR 'TARGET FOUND'

    //LIST FOR TYPE 'NODE' STORING 'UNSEARCHED NODES' (THIS IS YOUR STACK)

    //SET 'TARGET FOUND' FALSE

    //ASSIGN GAMEMANAGER.INSTANCE.NODES[0] TO YOUR 'UNSEARCHED NODES' LIST 

    //LOOP STARTS HERE

    //WHILE TARGET FOUND IS FALSE, CONTINUE THE LOOP

    //1. TAKE LAST ITEM IN 'UNSEARCHED NODES' LIST AND ASSIGN IT TO 'NODE CURRENTLY BEING SEARCHED'

    //2. CHECK IF 'NODE CURRENTLY BEING SEARCHED' IS THE SAME AS *EITHER*
    //THE TARGET NODE OF THE PLAYER (NODE THEY ARE HEADING TOWARDS)
    //THE CURRENT NODE OF THE PLAYER (THE LAST NODE THEY VISITED)
    //IF THIS IS TRUE ('NODE CURRENTLY BEING SEARCHED' IS THE ONE WE WANT);
    //ASSIGN ' NODE CURRENTLY BEING SEARCHED' AS 'CURRENTNODE'
    //BREAK THE LOOP AND FINISH THIS METHOD
    //IF IT ISN'T TRUE CONTINUE

    //3. USE A FOR LOOP TO ADD EACH CHILD OF 'NODE CURRENTLY BEING SEARCHED' TO UNSEARCHED NODES LIST

    //4. REMOVE 'NODE CURRENTLY BEING SEARCHED' FROM UNSEARCHED NODES LIST

    //5. RETURN TO START OF LOOP
