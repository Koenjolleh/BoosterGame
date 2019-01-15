using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{

    Rigidbody rigidbody;

    AudioSource audioSource;

    bool play;
    bool toggleChange;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            rigidbody.AddRelativeForce(Vector3.up);
            playRocketSound();
        } else {
            stopRocketSound();
        }

        //Rotating either left or right
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward);
        } else if (Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward);
        }
    }

    private void stopRocketSound()
    {
        if(audioSource.isPlaying){
            audioSource.Stop();
        }
    }

    private void playRocketSound(){
        if(!audioSource.isPlaying){
            audioSource.Play();
        }
    }
}
