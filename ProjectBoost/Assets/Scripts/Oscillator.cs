using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float fltPeriod = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize starting position
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (fltPeriod <= Mathf.Epsilon) {return;}

        float fltCycles = Time.time / fltPeriod;
        const float fltTau = 2 * Mathf.PI; //tau = 6.28...
        float fltRawSineWave = Mathf.Sin(fltTau * fltCycles);

        movementFactor = (fltRawSineWave + 1f)/2f;

        //Get the offset amount
        Vector3 offset = movementVector * movementFactor;
        //Move object's position to new position
        transform.position = startingPos + offset;
    }
}
