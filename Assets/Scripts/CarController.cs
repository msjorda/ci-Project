using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform frontwheel;
    public Transform backwheel;
    public float vel;                           //Note: public variables can be accessed in the side panel of Unity, and also changed on the fly while the game is running
    public static Rigidbody car;                //Note: static is required for this variable and its components to be accessed in other scripts
    public Quaternion rot;
    public static Vector3 direction;

    
    // Start is called before the first frame update

    void Start()
    {
        car = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = frontwheel.position - backwheel.position; //easiest way i could find to calculate a vector for which way the car is facing
       
        rot = car.rotation;
        car.transform.Rotate(0.0f, moveHorizontal, 0.0f, Space.Self);       //rotate car with arrow keys
        car.AddForce(direction * vel * moveVertical);                       //move car with arrow keys  
    }  
}