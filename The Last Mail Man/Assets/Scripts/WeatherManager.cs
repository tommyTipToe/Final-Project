using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour
{
    [SerializeField]private float windSpeed = 0f;
    [SerializeField]private bool fog = false;
    [SerializeField]private float timer = 15; // Seconds

    public static WeatherManager instance;

    void Awake() {
        instance = this;
    }

    // Call this in the gameObjects that are affected by wind in the fixedupdate function
    public void Wind(GameObject o) {
        var rb = o.GetComponent<Rigidbody>();
        Vector3 dir = transform.right;
        rb.AddForce(dir * windSpeed);
    }

    // Call this in the GameManager that holds the gametime so we don't change the wind too often
    public void ChangeWind(float n) {
        Debug.Log("Setting wind to " + n);
        float temp = windSpeed;
        windSpeed = n;
        StartCoroutine(Timed(temp));
    }

    // takes in the original speed and returns to it after the timer length
    IEnumerator Timed(float n)
    {
        yield return new WaitForSeconds(timer);
        Debug.Log("Resetting wind");
        windSpeed = n;
    }

}
