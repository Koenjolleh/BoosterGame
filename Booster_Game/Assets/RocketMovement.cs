using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{

    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 10f;

    Rigidbody rigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       Thrust();
       Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag){

            case "Friendly":
                print("U alive boi");
                break;
            case "Fuel":
                print("Fuel regain yeah");
                break;
            default:
                print("U dead boi");
                break;
        }        
    }

    private void Thrust()
    {

        //Thrust while rotating
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust);
            playRocketSound();
        }
        else
        {
            stopRocketSound();
        }

    }

    private void Rotate()
    {
        
        rigidbody.freezeRotation = true; // Take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        //Rotating either left or right
        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidbody.freezeRotation = false; // Resume physics control of rotation
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
