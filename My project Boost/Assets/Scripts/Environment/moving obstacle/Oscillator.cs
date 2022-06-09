using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 3f;
    Vector3 startingPosition;
    


    private void Start()
    {
        startingPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }
       
        float cycles = Time.time / period;
        const float val = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * val);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;

    }
}
