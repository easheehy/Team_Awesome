using UnityEngine;
using System.Collections;

public class MovingPlatforms : MonoBehaviour {

    public GameObject pointsObject;
    private Transform[] points;
    private int targetPoint;
    public int moveSpeed;

	// Use this for initialization
	void Start () {
        //Sets up points to move platforms to and fro
        points = pointsObject.GetComponentsInChildren<Transform>();
        targetPoint = 1;
        transform.position = points[0].position;
	}
	
	// Update is called once per frame
	void Update () {
        //If the platform reaches the target destination, make increment the index to move to the next one
	    if (transform.position == points[targetPoint].position) {
            targetPoint++;
        }
        //Resets the counter if it goes over the amount of points
        if (targetPoint >= points.Length) {
            targetPoint = 0;
        }
        //Moves the platform
        transform.position = Vector3.MoveTowards(transform.position, points[targetPoint].position, moveSpeed * Time.deltaTime);
	}

}
