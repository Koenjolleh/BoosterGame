using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

    Vector3 startingPos; // must be stored for absolute movement
    float movementFactor; // 0 for not moved 1 for fully moved

    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;
    



    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovementCycle();
    }

    private void MovementCycle()
    {
    
        if(period <= Mathf.Epsilon) { return; } // Protects against NaN if period is 0
        float cycles = Time.time / period; // Grows continually from game start
        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSineWave / 2f) + 0.5f;

        //-movementVector from moving down rather than up
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
