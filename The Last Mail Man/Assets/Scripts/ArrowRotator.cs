using UnityEngine;

public class ArrowRotator : MonoBehaviour
{
    private Vector3 target = new Vector3(1000,0,1000);
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }

    public void ChangeTarget(Vector3 t) {
        t *= 1000;
        target = t;
    }
}
