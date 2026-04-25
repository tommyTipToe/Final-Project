using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class objectPickup : Interaction
{
    public AudioUI audioEvent;
    public AudioClip packagePickup;
    taskDatabase delObject;
    taskTracker taskObject;

    //when this object is created
    private void Awake()
    { 
        delObject = FindObjectOfType<taskDatabase>();
        taskObject = FindObjectOfType<taskTracker>();
    }

    //-------------------------------------------------------------------------------------------//
    // Interaction Functions
    //-------------------------------------------------------------------------------------------//
    public override void OnInteractionStart(Interactor interactor)
    {
        base.OnInteractionStart(interactor);
        audioEvent = FindObjectOfType<AudioUI>();
        audioEvent.PlayOnClick(packagePickup);
        collect();
    }
    protected override void OnInteractionEnd()
    {
        base.OnInteractionEnd();
    }


    //task object is an object that needs to be colliding with
    private void collect()
    {
        taskassign(taskDatabase.ezDelivery.head);
        taskassign(taskDatabase.meDelivery.head);
        taskassign(taskDatabase.hdDelivery.head);
        //if (taskDatabase.jonahList.head != null)
        //{
        //    OnInteractionEnd();
        //    taskassign(taskDatabase.jonahList.head);
        //}

        // End the interaction
        OnInteractionEnd();
    }

    //assign the task
    private void taskassign(taskDatabase.taskNode currHead)
    {

           //if pickup attribute is this object AND task for that object is active
        for (int i = 0; i < 2; i++)
        {
            //start here
            if (findPick(currHead.pickUp.head))
                return;
            currHead = currHead.next;
        }     
    }
    
    private bool findPick(taskDatabase.pickNode currHead)
    {
        if(currHead != null)
        {
            for (int i = 0; i < 3; i++)
            {

                if (currHead.theBox == this.gameObject)
                {
                    currHead.hasObject = true;
                    //Destroy(gameObject);
                    gameObject.SetActive(false);
                       //notification
                    Notification.Instance.PushNotification("Picked up Package at " + currHead.pickPack);

                    return true;
                }
                currHead = currHead.next;
            }
        }
        else
        {
            Debug.LogError("PICKUP HEAD DNE");
        }
        
        return false;
    }
}
