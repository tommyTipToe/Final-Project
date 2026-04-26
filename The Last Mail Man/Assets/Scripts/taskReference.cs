using UnityEngine;
using System.Linq;


//public partial class taskDatabase : MonoBehaviour
public partial class taskDatabase
{

   public static int[] intPackage = {-1, -1, -1, -1, -1, -1, -1, -1, -1};
   public static GameObject[] destList = new GameObject[9];   

                                            //PICKUPLIST DROPOFF PAYOUT DIFFICULTY
public static taskNode[] ezTasks = {        new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0),
                                            new taskNode(null, null, EZ_PAY, 0)};


    //20 ME TASKS
    [HideInInspector]
    public static taskNode[] meTasks = {    new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1),
                                            new taskNode(null, null, ME_PAY, 1)};
    //30 min time limit


    //10 HD TASKS
    [HideInInspector]
    public static taskNode[] hdTasks = {    new taskNode(null, null, HD_PAY, 2),
                                            new taskNode(null, null, HD_PAY, 2),
                                            new taskNode(null, null, HD_PAY, 2),
                                            new taskNode(null, null, HD_PAY, 2),
                                            new taskNode(null, null, HD_PAY, 2),
                                            new taskNode(null, null, HD_PAY, 2),
                                            new taskNode(null, null, HD_PAY, 2),
                                            new taskNode(null, null, HD_PAY, 2),
                                            new taskNode(null, null, HD_PAY, 2),
                                            new taskNode(null, null, HD_PAY, 2)};
    //30 min time limit


   public static taskDatabase delObject;



    private void Awake()
    {
        delObject = FindFirstObjectByType<taskDatabase>();

        for (int i = 0; i < possiblePick.Length - 1; i++)
        {
            possiblePick[i].GetComponent<packageSpawn>().spawn();
            setFalse(possiblePick[i].transform.GetChild(0).gameObject);
        }

        //spawnObj = FindObjectOfType<packageSpawn>();
        //this is assigning the dropoff kiosk to all tasks
            foreach (taskNode t in ezTasks)
            {
                t.dropOff = dropLoc;
            }

            foreach (taskNode t in meTasks)
            {
                t.dropOff = dropLoc;
            }

            foreach (taskNode t in hdTasks)
            {
                t.dropOff = dropLoc;
            }

        //foreach(taskNode x in ezTasks)
        //{
        //    Debug.Log(x.isActive);
        //    Debug.Log(x.assignTime);
        //}
        Initialize();
        createpackages();


    }
   


    private void Initialize()
    {
        uncompleteEZ = new int[] { 2, 2, 2, 2, 2 };//new int[5];
        uncompleteME = new int[] { 2, 2, 2, 2, 2 };//new int[5];
        uncompleteHD = new int[] { 1, 1, 1, 1, 1 };//new int[5];
    }

    public void createpackages()
    {
       if (ezDelivery.head == null && meDelivery.head == null && hdDelivery.head == null)
       {


                //makes intPackages randomly filled with non repeating integers
                GGnonRe();



                //randomly selects which tier of tasks are used xTasks[sel1]
                int sel1 = Random.Range(0, ezTasks.Length - 1);
                //other option for a task
                int sel2;
                //sel2 is always sel1 + 1, so if it is the end set it to subtract from sel1
                if (sel1 < ezTasks.Length - 1)
                {
                    sel2 = sel1 + 1;
                }
                else
                {
                    sel2 = 0;
                }





                //unhiding packages
                for (int x = 0; x < 9; x++)
                {
                    setTrue(possiblePick[intPackage[x]].transform.GetChild(0).gameObject);
                    //storing the packages at spawn for easy destruction at the end of the day
                    destList[x] = possiblePick[intPackage[x]].transform.GetChild(0).gameObject;
                }

                //spawnObj.spawn(possiblePick[0]);

                newPk(PL0, possiblePick[intPackage[0]].transform.GetChild(0).gameObject, 0);
                newEZ(ezDelivery, ezTasks[sel1], PL0);


                //index of package MUST equal index of package description
                newPk(PL1, possiblePick[intPackage[1]].transform.GetChild(0).gameObject, 0);
                newEZ(ezDelivery, ezTasks[sel2], PL1);


                //medium tasks
                newME(meDelivery, meTasks[sel1], PL2, possiblePick[intPackage[2]].transform.GetChild(0).gameObject, possiblePick[intPackage[3]].transform.GetChild(0).gameObject);



                newME(meDelivery, meTasks[sel2], PL3, possiblePick[intPackage[4]].transform.GetChild(0).gameObject, possiblePick[intPackage[5]].transform.GetChild(0).gameObject);

                //resetting sel1 to avoid outOfBounds error
                sel1 = Random.Range(0, hdTasks.Length - 1);

                //hard tasks
                newHD(hdDelivery, hdTasks[sel1], PL4, possiblePick[intPackage[6]].transform.GetChild(0).gameObject, possiblePick[intPackage[7]].transform.GetChild(0).gameObject, possiblePick[intPackage[8]].transform.GetChild(0).gameObject);


     }
     else
     {
        Debug.LogError("EXISTENCE IS PAIN");
     }

}



    //create an array of non-repeating randomized integers
    private void GGnonRe()
    {
        int index = 0;
        int whileIters = 0;
        //chunks through intPackages and doesn't increase the index unless it's a different value
        while (intPackage[8] == -1)
        {
            int rng = Random.Range(0, possiblePick.Length - 1);

            if (!(intPackage.AsQueryable().Any(val => val == rng)))
            {
                intPackage[index] = rng;
                index++;
            }


            if (whileIters++ > 1000000000)
            {
                Debug.LogError("GGnoRe() GONE ON TOO LONG");
                break;
            }
        }
    }






       //set package.Active to false
    private void setFalse(GameObject pack)
    {
        pack.SetActive(false);
    }

       //set package.Active to true
    public static void setTrue(GameObject pack)
    {
        pack.SetActive(true);
    }


}
