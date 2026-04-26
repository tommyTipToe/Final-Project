using UnityEngine;
//using System
public partial class taskDatabase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
   const int EZ_PAY = 10;
   const int ME_PAY = 20;
   const int HD_PAY = 40;
   
   [HideInInspector] public int[] uncompleteEZ = new int[]{ 2, 2, 2, 2, 2 };//new int[5];
   [HideInInspector] public int[] uncompleteME = new int[]{ 2, 2, 2, 2, 2 };//new int[5];
   [HideInInspector] public int[] uncompleteHD = new int[]{ 1, 1, 1, 1, 1 };//new int[5];

   public GameObject[] possiblePick;
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
    //[SerializeField]
    public class taskNode
    {
        //ATTRIBUTES

   [SerializeField] public bool isActive;       //is the task active
   [SerializeField] public bool isCompleted;    //is the task completed
   [SerializeField] public pickList pickUp;     //pick up list
   [SerializeField] public GameObject dropOff;  //drop off location
   [SerializeField] public float payOut;        //how much the job pays
   [SerializeField] public int difficulty;      //difficulty of the task
   [SerializeField] public taskNode head;       //head of the node
   [SerializeField] public taskNode next;       //points to the next task
        //ATTRIBUTES



        //CONSTRUCTOR
        public taskNode(pickList PU,
                        GameObject DO,
                        int pay,
                        int diff)
        {
            isActive = true;
            isCompleted = false;
            pickUp = PU;
            dropOff = DO;
            payOut = pay;
            difficulty = diff;
            next = null;
        }
        //CONSTRUCTOR
    }
    //NODE

    //NODE
   public class pickNode
   {
        //ATTRIBUTES
      public GameObject theBox;   //the actual box
      [SerializeField]public bool hasObject;      //does the player have the package
      [SerializeField] public int difficulty;
      public pickNode head;
      public pickNode next;
        //ATTRIBUTES

      public pickNode(GameObject box, int diff)
      {
         theBox = box;
         hasObject = false;
         difficulty = diff;
         next = null;
        }
    }
    //NODE

    //HEAP
   public class taskList
   {
      internal taskNode head;
   }
    //HEAP

    //HEAP
    public class pickList
    {
       internal pickNode head;
    }
    //HEAP

       //creates new link on pickUp list 
    public void newPk(pickList list, GameObject box, int diff)
    {
        pickNode node = new pickNode(box, diff);

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
                        pickList pList)
    {
        
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
                       GameObject P2
                       )
   {
      newPk(pList, P1, 1);
      newPk(pList, P2, 1);

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
                       GameObject P3
                       )
   {
      newPk(pList, P1, 2);
      newPk(pList, P2, 2);
      newPk(pList, P3, 2);

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


   public static void delTask(taskList list)
   {
      list.head = null;
   }

   public static void delPick(pickList list)
   {
      list.head = null;
   }
}
