using UnityEngine;

public class PickupLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
       
   }

    // Update is called once per frame
   void Update()
   {
      //Debug.Log("YES");    
   }

   void OnTriggerEnter(Collider other)
   {
      if(other.gameObject.tag = "Package")
      {
         Destroy(other.gameObject);
      }
   }
}
