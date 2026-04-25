using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public partial class taskDatabase : MonoBehaviour
{
    //// <summary>
    // EVERYTHING SHOULD BE ZERO INDEXED, LET ME KNOW IF OTHERWISE - Santiago
    // THE CIRCULAR LINKED LIST DECISION WAS NOT ARBITRARY - Santiago
    //// </summary>

    const float EZ_PAY = 20.0f;
    const float ME_PAY = 40.0f;
    const float HD_PAY = 80.0f;


    //max 25 tasks so 25 elements
    //public Dictionary<int, float> timePerTask = new Dictionary<int, float>();
    [HideInInspector] public float[] timePerTask = new float[25];
    //[0] is day 1, [1] is day 2...
    [HideInInspector] public int[] uncompleteEZ = new int[]{ 2, 2, 2, 2, 2 };//new int[5];
    [HideInInspector] public int[] uncompleteME = new int[]{ 2, 2, 2, 2, 2 };//new int[5];
    [HideInInspector] public int[] uncompleteHD = new int[]{ 1, 1, 1, 1, 1 };//new int[5];


    //there is no reason to use a list over an array, that's just how it worked out
    public GameObject[] possiblePick;
    public string[] pickDescript;
    public GameObject dropLoc;
       

    //three lists for different difficulties of tasks
    [HideInInspector] public static taskList ezDelivery = new taskList();
    [HideInInspector] public static taskList meDelivery = new taskList();
    [HideInInspector] public static taskList hdDelivery = new taskList();

    //five pickup objects for five tasks 
    [HideInInspector] public static pickList PL0 = new pickList();
    [HideInInspector] public static pickList PL1 = new pickList();
    [HideInInspector] public static pickList PL2 = new pickList();
    [HideInInspector] public static pickList PL3 = new pickList();
    [HideInInspector] public static pickList PL4 = new pickList();

    //creating a linked list to simplify things
    
    //NODE
    [Serializable]
    public class taskNode
    {
        //ATTRIBUTES

        public bool isActive;       //is the task active
        public bool isCompleted;    //is the task completed

        public int taskID;          //what is the current ID of the task

        public float assignTime;    //what time to assigne object            

        public string taskName;     //name of task
        public pickList pickUp;     //pick up list
        [NonSerialized] public GameObject dropOff;  //drop off location
        public float payOut;        //how much the job pays
        public string[] pickDescript = new string[3]; //description for app
        public string dropDescript; //description for app
        public int difficulty;      //difficulty of the task
        public taskNode head;       //head of the node
        public taskNode next;       //points to the next task
        //ATTRIBUTES



        //CONSTRUCTOR
        public taskNode(int tID,
                        int dID,
                        float expT,
                        float aTime,
                        string name,
                        string desc,
                        pickList PU,
                        GameObject DO,
                        float pay,
                        string PD0,
                        string PD1,
                        string PD2,
                        string DD,
                        int diff)
        {
            isActive = false;
            isCompleted = false;
            isOverdue = false;
            taskID = tID;
            dayID = dID;
            taskEnd = 0;
            expireTime = expT;
            lateTime = 0;
            assignTime = aTime;
            taskName = name;
            taskDesc = desc;
            pickUp = PU;
            dropOff = DO;
            payOut = pay;
            pickDescript[0] = PD0;
            pickDescript[1] = PD1;
            pickDescript[2] = PD2;
            dropDescript = DD;
            difficulty = diff;
            next = null;
        }
        //CONSTRUCTOR
    }
    //NODE

    //NODE
    [Serializable]
    public class pickNode
    {
        //ATTRIBUTES
        [NonSerialized] public GameObject theBox;   //the actual box
        public bool hasObject;      //does the player have the package
        public int difficulty;
        public string pickPack;
        public pickNode head;
        public pickNode next;
        //ATTRIBUTES

        public pickNode(GameObject box, int diff, string PP)
        {
            theBox = box;
            hasObject = false;
            difficulty = diff;
            pickPack = PP;
            next = null;
        }
    }
    //NODE

    //HEAP
    [Serializable]
    public class taskList
    {
        internal taskNode head;
    }
    //HEAP

    //HEAP
    [Serializable]
    public class pickList
    {
        internal pickNode head;
    }
    //HEAP

       //creates new link on pickUp list 
    public void newPk(pickList list, GameObject box, int diff, string PP)
    {
        pickNode node = new pickNode(box, diff, PP);

           //circular linked list connection
        if (list.head == null)
        {
            list.head = node;
            list.head.next = list.head;
        }
        else
        {
            node.next = list.head;
            list.head = node;
        }
    }

    //NEW NODE
    public void newEZ(  taskList list,
                        taskNode node,
                        pickList pList,
                        string PD0)
    {
        
        node.pickDescript[0] = PD0;
        node.pickUp = pList;
        if (list.head == null)
        {
            list.head = node;
            list.head.next = list.head;
            //Debug.Log("head created");
        }
        else
        {
            node.next = list.head;
            list.head = node;
            //Debug.Log("node added");
        }
    }
    //NEW NODE
    
    //NEW NODE
    public void newME(taskList list,
                        taskNode node,
                        pickList pList,
                        GameObject P1,
                        GameObject P2,
                        string PD0,
                        string PD1)
    {
        newPk(pList, P1, 1, PD0);
        newPk(pList, P2, 1, PD1);
        node.pickDescript[0] = PD0;
        node.pickDescript[1] = PD1;

        node.pickUp = pList;
        if (list.head == null)
        {
            list.head = node;
            list.head.next = list.head;
            //Debug.Log("head created");
        }
        else
        {
            node.next = list.head;
            list.head = node;
            //Debug.Log("node added");
        }
    }
    //NEW NODE

    //NEW NODE
    public void newHD(taskList list,
                        taskNode node,
                        pickList pList,
                        GameObject P1,
                        GameObject P2,
                        GameObject P3,
                        string PD0,
                        string PD1,
                        string PD2)
    {
        newPk(pList, P1, 2, PD0);
        newPk(pList, P2, 2, PD1);
        newPk(pList, P3, 2, PD2);
        node.pickDescript[0] = PD0;
        node.pickDescript[1] = PD1;
        node.pickDescript[2] = PD2;

        node.pickUp = pList;
        if (list.head == null)
        {
            list.head = node;
            list.head.next = list.head;
            //Debug.Log("head created");
        }
        else
        {
            node.next = list.head;
            list.head = node;
            //Debug.Log("node added");
        }
    }
    //NEW NODE


    
    //MUST USE Start() TO PREVENT ERRORS
    







    public static void delTask(taskList list)
    {
        list.head = null;
    }

    public static void delPick(pickList list)
    {
        list.head = null;
    }

