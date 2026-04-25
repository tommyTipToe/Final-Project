using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class objectDropoff : MonoBehaviour
{
       //other task objects
    private static money_tracking_system wallet;   
    private static taskTracker taskObject;
    private static taskDatabase delObject;
    private static ClockUI clockObject;

       //variables
    private static float retVal = 0.0f;
    private static int index = 0;
    private static int gameID = 0;

    private void Start()
    {
        taskObject = FindObjectOfType<taskTracker>();
        delObject = FindObjectOfType<taskDatabase>();
        //clockObject = FindObjectOfType<ClockUI>();
        gameID = this.gameObject.GetInstanceID();
    }

    //dropoff is a trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") // && taskTracker.currTaskVar != taskTracker.CurrTaskType.unassigned
        {
            endTask(taskDatabase.ezDelivery.head);
            endTask(taskDatabase.meDelivery.head);
            endTask(taskDatabase.hdDelivery.head);
            //print(count(delObject.delivery.head));
        }
    }

    private static int count(taskDatabase.taskNode header)
    {
        if (header == null)
        {
            return 0;
        }
        int n = 1;
        taskDatabase.taskNode current = header.next;
        //while(current != header)
        for(int i = 0;i < 6;i++)
        {
            n++;
            current = current.next;
        }

        return n;
    }
    

    private static void endTask(taskDatabase.taskNode currHead)
    {

        retVal = 0.0f;

           //taskDatabase.taskNode temp = currHead;
           //as long as the node exists
        if (currHead != null)
        {
               //iterating twice because the Xdelivery list is at most two nodes long
               //should probably have spent more time making one long task list, but whatevs
            for (int i = 0; i < 2; i++)
            {
                   //start here
                if (currHead.dropOff.GetInstanceID() == gameID && currHead.isCompleted == false && findDrop(currHead.pickUp.head))
                {
                    
                       //set appropriate values to completion
                    currHead.isActive = false;
                    currHead.isCompleted = true;                    
                    taskObject.taskFinCount++;
                    delObject.saveData(currHead, index);
                    index++;
                       
                    Debug.Log("Payout: " + retVal);
                    wallet.addFunds(retVal);
                    Notification.Instance.PushNotification("Package(s) Delivered! Good Job!");
                }
                currHead = currHead.next;
            }
        }
        else
        {
               //error checking
            return;
        }
           //update appropriate task tracking UI
    }

    private static bool findDrop(taskDatabase.pickNode currHead)
    {
        switch(currHead.difficulty)
        {
            case 0:
                   //if easy job and has the package
                if (currHead.hasObject)
                    return true;
                break;
            case 1:
                   //variable for collected objects
                int colleted = 0;
                   //length of medium string
                for(int i = 0;i < 2;i++)
                {
                       //if has object in that chain
                       //increment collected objects
                    if (currHead.hasObject)
                    {
                           
                        colleted++;
                    }
                    currHead = currHead.next;
                }
                   //if all links are collected
                   //return true
                if (colleted == 2)
                    return true;
                break;
            case 2:
                   //same as case: 1
                colleted = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (currHead.hasObject)
                    {
                        colleted++;
                    }
                    currHead = currHead.next;
                }
                if (colleted == 3)
                    return true;
                break;
            default:
                break;
        }
        
           //if not enough return false 
        return false;
    }    
}
