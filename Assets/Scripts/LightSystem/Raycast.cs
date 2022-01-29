using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class Raycast : MonoBehaviour {
    public Light2D light2d;
    Vector2 origin, directionLeft, directionRight;
    public Transform lightPivot;
    public float pointDistance = 100;
    float leftAngle = 0f;
    float rightAngle = 0f;
    Raycast leftRaycast;
    Raycast rightRaycast;
    public float raycastMaxDistance = 100;
    // Start is called before the first frame update
    void Start() {
        // Get respective angles based on pivot rotation.
        leftAngle = lightPivot.transform.rotation.z - (light2d.pointLightOuterAngle / 2f);
        rightAngle = lightPivot.transform.rotation.z + (light2d.pointLightOuterAngle / 2f);

        origin = lightPivot.position;

        leftAngle = lightPivot.transform.rotation.z - (light2d.pointLightOuterAngle / 2f);
        rightAngle = lightPivot.transform.rotation.z + (light2d.pointLightOuterAngle / 2f);

        // Get reference points based on left and right angles. This is used to give raycasts a direction.
        directionLeft = new Vector2(pointDistance * Mathf.Sin(leftAngle), pointDistance * Mathf.Cos(leftAngle));
        directionRight = new Vector2(pointDistance * Mathf.Sin(rightAngle), pointDistance * Mathf.Cos(rightAngle));

        
    }

    // Update is called once per frame
    void Update() {
        Debug.Log("alive");
        Debug.DrawLine(origin, transform.TransformDirection(directionLeft), Color.white);
        if (Physics.Raycast(origin, transform.TransformDirection(directionLeft), raycastMaxDistance) || Physics.Raycast(origin, transform.TransformDirection(directionRight), raycastMaxDistance)) {
            Debug.Log("ha u ded");
        }
    }
}
