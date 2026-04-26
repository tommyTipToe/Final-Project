using UnityEngine;

public class PickupLogic : MonoBehaviour
{
      //the delivery object that handles the package logic
   taskDatabase delObject;

   private void Awake()
   {
      delObject = FindFirstObjectByType<taskDatabase>();
      collect();
   }

   private void OnTriggerEnter(Collider other)
   {
      //if(other.gameObject.tag == "Player")   
      if(other.transform.root.CompareTag("Player")) 
      Destroy(this.gameObject);
   }



      //task object is an object that needs to be colliding with
   private void collect()
   {
      taskAssign(taskDatabase.ezDelivery.head);
      taskAssign(taskDatabase.meDelivery.head);
      taskAssign(taskDatabase.hdDelivery.head);
   }

   private void taskAssign(taskDatabase.taskNode currHead)
   {

      for(int i = 0;i < 2; i++)
      {
         if(findPick(currHead.pickUp.head))
            return;
	 currHead = currHead.next;
      }
   }

   private bool findPick(taskDatabase.pickNode currHead)
   {
      if(currHead != null)
      {
         for(int i = 0;i < 3;i++)
         {
            if(currHead.theBox == this.gameObject)
	    {
               currHead.hasObject = true;
	       gameObject.SetActive(false);
	       return true;
	    }
	    currHead = currHead.next;
         }
      }
      else
         Debug.LogError("PICKUP HEAD DNE");
      return false;
   }
}
