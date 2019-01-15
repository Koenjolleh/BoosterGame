using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       processInput();
    }

    private void processInput()
    {
        //Thrust while rotating
        if(Input.GetKey(KeyCode.Space)){
            print("spaaace");
        }

        //Rotating either left or right
        if(Input.GetKey(KeyCode.A)){
            print("leeeeft");
        } else if (Input.GetKey(KeyCode.D)){
            print("riiiigth");
        }
    }
}
