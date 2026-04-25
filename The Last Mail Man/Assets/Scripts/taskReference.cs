using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class taskDatabase
{

    //BE SURE TO CHANGE isActive ON ALL TASKS TO FALSE BEFORE v1.0
    //BE SURE TO CHANGE isActive ON ALL TASKS TO FALSE BEFORE v1.0
    //BE SURE TO CHANGE isActive ON ALL TASKS TO FALSE BEFORE v1.0

    [HideInInspector]
    public static int[] intPackage = { -1, -1, -1, -1, -1, -1, -1, -1, -1};
    public static GameObject[] destList = new GameObject[9];

    //time + 900 adding 15 minutes to the clock
    /* TIME LEGEND
     * 09:00 :      32400f
     * 10:00 :      36000f
     * 11:00 :      39600f
     * 12:00 :      43200f
     * 01:00 :      46800f
     * 02:00 :      50400f
     * 03:00 :      54000f
     * 04:00 :      57600f
     * 05:00 :      61200f
     */

    //will go back and comment the times later on - Santiago

    //cannot immediately assign dropoff, so instantiated in dropoff
       //20 EZ TASKS                                    //taskID, dayID, expireTime, assignTime,         name,                                                                                             description, pickup, dropoff,  payout, pickup description,                      dropoff description, difficulty
    [HideInInspector]
    public static taskNode[] ezTasks = {    new taskNode(     0,     0,    36000f,      34200f, "Party Panic", "My daughter's birthday party is tommorow and I haven't bought a gift yet! Please get this done quick.",   null,    null,  EZ_PAY, null, null,   null,     "Dropoff Kiosk Near Jeffrey's Attic",          0),
                                            new taskNode(1, 0, 37800f, 36000f, "START ORDER!",                  "so you just talk and it records your order? oh... like right now?", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 39600f, 37800f, "Anniversary Gift",              "They're just going to love this. I'm so excited!", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 41400f, 39600f, "Pay Day",                       "Just got a bonus so I'm gonna splurge today", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 43200f, 41400f, "Birthday",                      "Spending some birthday money from Grandma", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 45000f, 43200f, "Some Essentials",               "Ran out of these so buying them last minute", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 46800f, 45000f, "New Hobby",                     "I've been feeling in a rut recently so I'm buying some stuff to kickstart a new hobby", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 48600f, 46800f, "Replacemnt",                    "Old one broke", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 50400f, 48600f, "Gift Card Expiration",          "I usually don't go here, but I got an expiring giftcard so...", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 52200f, 50400f, "nlp8i8",                        "fsdfe78", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 54000f, 52200f, "Lottery BABYYYYY",              "King for a day", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 55800f, 54000f, "Self Care",                     "I need to treat myself every once in a while", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 57600f, 55800f, "Inspiration",                   "Something great is coming out of this", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 59400f, 57600f, "Stuck at Home",                 "I'm bored", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 60300f, 58500f, "Allowance Spending",            "Thanks Dad!", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 56700f, 54900f, "Girlfriend Surprise",           "She's going to love this", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 53100f, 51300f, "Curiosity",                     "I see everyone checking this out so I am too", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 38700f, 36900f, "Boyfriend Surprise",            "He's going to love this", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 35100f, 33300f, "Date Night",                    "We're going all out to wine & dine in tonight", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0),
                                            new taskNode(1, 0, 47700f, 45900f, "New Home",                      "Need to buy some new decorations and homey things", null, null, EZ_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 0)};
    //30 min time limit


    //20 ME TASKS
    [HideInInspector]
    public static taskNode[] meTasks = {    new taskNode(2, 0, 36000f, 34200f, "START ORDER!",                  "so you just talk and it records your order? oh... like right now?", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(3, 0, 39600f, 37800f, "Anniversary Gift",              "They're just going to love this. I'm so excited!", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 43200f, 41400f, "Pay Day",                       "Just got a bonus so I'm gonna splurge today", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 46800f, 45000f, "Birthday",                      "Spending some birthday money from Grandma", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 50400f, 48600f, "Some Essentials",               "Ran out of these so buying them last minute", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 54000f, 52200f, "New Hobby",                     "I've been feeling in a rut recently so I'm buying some stuff to kickstart a new hobby", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 61200f, 59400f, "Replacemnt",                    "Old one broke", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 60300f, 58500f, "Gift Card Expiration",          "I usually don't go here, but I got an expiring giftcard so...", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 59400f, 57600f, "nlp8i8uwredfvj",                "sadf89w51", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 55800f, 54000f, "Lottery BABYYYYY",              "King for a day", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 52200f, 50400f, "Self Care",                     "I need to treat myself every once in a while", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 48600f, 46800f, "Inspiration",                   "Something great is coming out of this", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 45000f, 43200f, "Stuck at Home",                 "im bored", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 41400f, 39600f, "Allowance Spending",            "Thanks Dad!", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 37800f, 36000f, "Girlfriend Surprise",           "She's going to love this", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 38700f, 36900f, "Curiosity",                     "I see everyone checking this out so I am too", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 42300f, 40500f, "Boyfriend Surprise",            "He's going to love this", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 49500f, 47700f, "Date Night",                    "We're going all out to wine & dine in tonight", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 60300f, 58500f, "New Home",                      "Need to buy some new decorations and homey things", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1),
                                            new taskNode(1, 0, 56700f, 54900f, "Party Panic",                   "My daughter's birthday party is tommorow and I haven't bought a gift yet! Please get this done quick.", null, null, ME_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 1)};
    //30 min time limit


    //10 HD TASKS
    [HideInInspector]
    public static taskNode[] hdTasks = {    new taskNode(4, 0, 37800f, 36000f, "Lottery BABYYYYY",              "King for a day", null, null, HD_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 2),
                                            new taskNode(1, 0, 41400f, 39600f, "New Home",                      "I've been feeling in a rut recently so I'm buying some stuff to kickstart a new hobby", null, null, HD_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 2),
                                            new taskNode(1, 0, 45000f, 43200f, "Some Essentials",               "Ran out of these so buying them last minute", null, null, HD_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 2),
                                            new taskNode(1, 0, 48600f, 46800f, "New Hobby",                     "Need to buy some new decorations and homey things", null, null, HD_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 2),
                                            new taskNode(1, 0, 52200f, 50400f, "Pay Day",                       "Just got a bonus so I'm gonna splurge today", null, null, HD_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 2),
                                            new taskNode(1, 0, 55800f, 54000f, "Stuck at Home",                 "im bored", null, null, HD_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 2),
                                            new taskNode(1, 0, 59400f, 57900f, "Curiosity",                     "I see everyone checking this out so I am too", null, null, HD_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 2),
                                            new taskNode(1, 0, 60300f, 58500f, "Gift Card Expiration",          "I usually don't go here, but I got an expiring giftcard so...", null, null, HD_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 2),
                                            new taskNode(1, 0, 38700f, 36900f, "Inspiration",                   "Something great is coming out of this", null, null, HD_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 2),
                                            new taskNode(1, 0, 45900f, 44100f, "Birthday",                      "Spending some birthday money from Grandma", null, null, HD_PAY, null, null, null, "Dropoff Kiosk Near Jeffrey's Attic", 2)};
    //30 min time limit


    public static taskDatabase delObject;

    private void Awake()
    {
        delObject = FindObjectOfType<taskDatabase>();

        for (int i = 0; i < possiblePick.Length - 1; i++)
        {
            possiblePick[i].GetComponent<packageSpawn>().spawn();
            setFalse(possiblePick[i].transform.GetChild(0).gameObject);
        }

        //spawnObj = FindObjectOfType<packageSpawn>();
        //this is assigning the dropoff kiosk to all tasks
        {
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
        }

        //foreach(taskNode x in ezTasks)
        //{
        //    Debug.Log(x.isActive);
        //    Debug.Log(x.assignTime);
        //}

        //createpackages();
    }


    private void Initialize()
    {
        uncompleteEZ = new int[] { 2, 2, 2, 2, 2 };//new int[5];
        uncompleteME = new int[] { 2, 2, 2, 2, 2 };//new int[5];
        uncompleteHD = new int[] { 1, 1, 1, 1, 1 };//new int[5];
    }

    public void createpackages()
    {
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

                newPk(PL0, possiblePick[intPackage[0]].transform.GetChild(0).gameObject, 0, pickDescript[intPackage[0]]);
                newEZ(ezDelivery, ezTasks[sel1], PL0, pickDescript[intPackage[0]]);


                //index of package MUST equal index of package description
                newPk(PL1, possiblePick[intPackage[1]].transform.GetChild(0).gameObject, 0, pickDescript[intPackage[1]]);
                newEZ(ezDelivery, ezTasks[sel2], PL1, pickDescript[intPackage[1]]);


                //medium tasks
                newME(meDelivery, meTasks[sel1], PL2, possiblePick[intPackage[2]].transform.GetChild(0).gameObject, possiblePick[intPackage[3]].transform.GetChild(0).gameObject, pickDescript[intPackage[2]], pickDescript[intPackage[3]]);



                newME(meDelivery, meTasks[sel2], PL3, possiblePick[intPackage[4]].transform.GetChild(0).gameObject, possiblePick[intPackage[5]].transform.GetChild(0).gameObject, pickDescript[intPackage[4]], pickDescript[intPackage[5]]);

                //resetting sel1 to avoid outOfBounds error
                sel1 = Random.Range(0, hdTasks.Length - 1);

                //hard tasks
                newHD(hdDelivery, hdTasks[sel1], PL4, possiblePick[intPackage[6]].transform.GetChild(0).gameObject, possiblePick[intPackage[7]].transform.GetChild(0).gameObject, possiblePick[intPackage[8]].transform.GetChild(0).gameObject, pickDescript[intPackage[6]], pickDescript[intPackage[7]], pickDescript[intPackage[8]]);


                //deactivating packages so that they can pop up when they are actually assigned
                for (int x = 0; x < 9; x++)
                {
                    setFalse(possiblePick[intPackage[x]].transform.GetChild(0).gameObject);
                    //if(possiblePick[intPackage[x]].transform.GetChild(0).gameObject.activeInHierarchy == false)
                    //setTrue(possiblePick[intPackage[x]].transform.GetChild(0).gameObject);
                }
            }
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
