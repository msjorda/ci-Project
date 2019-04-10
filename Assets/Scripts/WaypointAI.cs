using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaypointAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] path;                    //Array where all waypoint objects are stored
    public Vector3 currentDest;                 //vector from car to current target waypoint
    
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FollowPath()
    {
        while (true)// runs around waypoints indefinitely
        {
            foreach (Transform waypoint in path)
            {
                yield return StartCoroutine(Move(waypoint.position, 5));
            }
        }
    }

    IEnumerator Move(Vector3 destination, float speed)
    {
        while (Mathf.Abs(transform.position.x - destination.x)> 0.0001f && Mathf.Abs(transform.position.z - destination.z)>0.0001f)  //only checks X and Z coords. height is irrelevant. float value indicates how close it needs to be to waypoint for it to register
        {
            currentDest = destination - transform.position; //creates vector pointing from car to next waypoint

            StartCoroutine(RotateFollower(currentDest));
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);      // needs to be replaced with the lerp and slerp functions
            //CarController.car.AddForce(CarController.direction.normalized * 9);       //currently unused method of moving the car, needs more tuning
            yield return null;
        }
    }

    IEnumerator RotateFollower(Vector3 currentDest)
    {
        Vector3 crossProd = Vector3.Cross(CarController.direction, this.currentDest).normalized; //finds crossproduct of the two vectors: (car to next waypoint) and (direction car is facing)
        print(crossProd);                                                                        //crossproduct is used to determine the direction the car should be turning
        transform.Rotate(0.0f, crossProd.y, 0.0f, Space.Self);                                   //rotates car towards waypoint     Note: car rotates on an axis relative to itself, not globally
        yield return null;
    }





}
