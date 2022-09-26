using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    float movementFactor;
    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period > Mathf.Epsilon)
        {
            float cycles = Time.time / period;
            float rawSinWave = Mathf.Sin(cycles * Mathf.PI);
            movementFactor = (rawSinWave + 1f) / 2f;

            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPos + offset;
        }
        
    }
}
