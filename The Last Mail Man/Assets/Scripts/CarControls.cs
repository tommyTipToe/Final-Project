using UnityEngine;
using TMPro;


public class CarControls : MonoBehaviour
{
    [Header("Car Properties")]
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;
    public float earnedPoints = 0.0f;
    public float transitPoints = 0.0f;
    public float jailPoints = 0.0f;

    private WheelControl[] wheels;
    private Rigidbody rigidBody;

    private CarInputActions carControls; // Reference to the new input system

    private bool held = false;
    [SerializeField] private GameObject oilPrefab;
    [SerializeField] private GameObject oilImage;
    [SerializeField] private GameObject oilImageCooldown;
    [SerializeField] TextMeshProUGUI speedometer;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI packagesHeld;

    private bool onCooldown = false;
    public bool boost = false;

    void Awake()
    {
        carControls = new CarInputActions(); // Initialize Input Actions
    }
    void OnEnable()
    {
        carControls.Enable();
    }

    void OnDisable()
    {
        carControls.Disable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        oilImageCooldown.SetActive(false);
        rigidBody = GetComponent<Rigidbody>();

        // Adjust center of mass to improve stability and prevent rolling
        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rigidBody.centerOfMass = centerOfMass;

        // Get all wheel components attached to the car
        wheels = GetComponentsInChildren<WheelControl>();
    }
            
    // FixedUpdate is called at a fixed time interval
    void FixedUpdate()
    {
        // Read the Vector2 input from the new Input System
        Vector2 inputVector = carControls.Car.Movement.ReadValue<Vector2>();

        packagesHeld.text = "Packages: " + (int)(transitPoints / 15f);

        //Press space to drop an oilspill
        bool oilInput = carControls.Car.Oil.IsPressed();
        if (!onCooldown){
            if (oilInput){
                if (!held){
                    held = true;
                    Vector3 oilPosition = new Vector3(gameObject.transform.position.x, 0.01f, gameObject.transform.position.z);
                    Instantiate(oilPrefab, oilPosition, oilPrefab.transform.rotation);
                    onCooldown = true;
                    oilImage.SetActive(false);
                    oilImageCooldown.SetActive(true);
                    Invoke(nameof(clearCooldown), 15f);
                }
            }
            else{
                held = false;
            }
        }
        

        // Get player input for acceleration and steering
        float vInput = inputVector.y; // Forward/backward input
        float hInput = inputVector.x; // Steering input

        float forwardSpeed = 0f;
        // Calculate current speed along the car's forward axis
        if (boost){
            forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity) * 1.2f;
        }
        else{
            forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        }
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed)); // Normalized speed factor
        speedometer.text = "Speed: " + Mathf.Round(forwardSpeed).ToString();
        // Reduce motor torque and steering at high speeds for better handling
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        // Determine if the player is accelerating or trying to reverse
        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            // Apply steering to wheels that support steering
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                // Apply torque to motorized wheels
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                }
                // Release brakes when accelerating
                wheel.WheelCollider.brakeTorque = 0f;
            }
            else
            {
                // Apply brakes when reversing direction
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
            }
        }
    }

    void clearCooldown()
    {
        onCooldown = false;
        oilImage.SetActive(true);
        oilImageCooldown.SetActive(false);
    }

    public void changeEarnedScore(float pts)
    {
        earnedPoints += pts;
        GameManager.instance.score += (int)pts;
        score.text = "Score: " + GameManager.instance.score;
    }
    public void changeTransitScore(float pts)
    {
        transitPoints += pts;
    }
    public void changeJailScore(float pts)
    {
        jailPoints += pts;
    }



}
