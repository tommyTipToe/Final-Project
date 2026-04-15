using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour
{
    [SerializeField]private float windSpeed = 0f;
    [SerializeField]private bool fog = false;
    [SerializeField]private float timer = 15; // Seconds
    [SerializeField]private Vector3 direction = new Vector3(1,0,0);
    [SerializeField]private Vector3 gravity = new Vector3(0,-10,0);

    public static WeatherManager instance;

    void Start() {
        Physics.gravity = gravity;
    }

    void Awake() {
        instance = this;
    }

    // Call this in the gameObjects that are affected by wind in the fixedupdate function
    public void Wind(GameObject o) {
        var rb = o.GetComponent<Rigidbody>();
        rb.AddForce(direction * windSpeed);
    }

    // Call this in the GameManager that holds the gametime so we don't change the wind too often
    public void ChangeWind(float n) {
        Debug.Log("Setting wind to " + n);
        float temp = windSpeed;
        windSpeed = n;
        StartCoroutine(Timed(temp));
    }

    // Takes in the original speed and returns to it after the timer length
    IEnumerator Timed(float n)
    {
        yield return new WaitForSeconds(timer);
        Debug.Log("Resetting wind");
        windSpeed = n;
    }

    // Should not be used very frequently, really only from menus
    // Notable gravity settings (approximations
    //  Moon:   (0,-2,0);
    //  Earth:  (0,-10,0);
    //  Jupiter:(0,-25,0);
    void ChangeGravity(Vector3 v) {
        gravity = v;
        Physics.gravity = gravity;
    }

    public void ChangeFog(bool b) {
        fog = b;
    }

}
