using UnityEngine;

public class packageSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   public GameObject prefab;
   private GameObject package;

   public void spawn()
   {
      if(prefab)
      {
         package = Instantiate(prefab, transform.position, transform.rotation);

	 package.transform.SetParent(this.gameObject.transform);
      }
      else
      {
         Debug.LogError("PACKAGE PREFAB DOESN'T EXIST");
      }
   }
}
