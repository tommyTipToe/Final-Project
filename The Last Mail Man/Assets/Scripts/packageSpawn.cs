using UnityEngine;

public class packageSpawn : MonoBehaviour
{
    public GameObject prefab;
    private GameObject package;

    public void spawn()
    {
           //if the prefab exists then spawn the package
        if(prefab)
        {
               //spawns package
            package = Instantiate(prefab, transform.position, transform.rotation);
            
            
               //sets parent as the spawn location since the task
               //pickup depends on the package being located there in the heirarchy
            package.transform.SetParent(this.gameObject.transform);
        }
        else
        {
            Debug.LogError("PACKAGE PREFAB DOESN'T EXIST");
        }
    }
}
