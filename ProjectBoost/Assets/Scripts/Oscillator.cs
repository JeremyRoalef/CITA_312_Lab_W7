using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor; //Set range of movementfactor to be between 0 and 1 (creates a slider)


    // Start is called before the first frame update
    void Start()
    {
        //Initialize starting position
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the offset amount
        Vector3 offset = movementVector * movementFactor;
        //Move object's position to new position
        transform.position = startingPos + offset;
    }
}
