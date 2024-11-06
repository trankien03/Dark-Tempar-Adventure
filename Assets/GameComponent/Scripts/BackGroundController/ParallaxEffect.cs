using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// the way parallaxEffect work exactly: https://www.youtube.com/watch?v=tMXgLBwtsvI
public class ParallaxEffect : MonoBehaviour
{

    public Camera cam;
    public Transform followTarget;

    //starting position for the parallax game object
    Vector2 startingPosition;

    //start z value of the parallax game obj
    float startingZ;
    Vector2 canMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    //if obj is in front of target use near clip plane, if behind target, use farclipPlane
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //when the target moves, move the parallax obj the same distance times a multiplier
        Vector2 newPosition = startingPosition + canMoveSinceStart * parallaxFactor;

        //the x/y position changes based on target travel speed times the parallax fator, but z tays consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
