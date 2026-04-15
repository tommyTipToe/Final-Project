using UnityEngine;

public class BoxS : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WeatherManager.instance.ChangeWind(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        WeatherManager.instance.Wind(this.gameObject);
    }
}
