using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketMovement : MonoBehaviour
{
    //SerializeField make it so you can adjust the value in the inspector in Unity
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 10f;

    Rigidbody rigidbody;
    AudioSource audioSource;

    enum State {Alive,Dead,Transcending}
    [SerializeField] State state = State.Alive;


    // Start is called before the first frame update
    void Start()
    {   
        //Gets the components from game object in Unity
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate(); 
        }
    }

    void OnCollisionEnter(Collision collision)
    {



        //Handles collision of the rocket based on the tags of the objects it bumps into
        switch(collision.gameObject.tag){

            case "Friendly":
                print("U alive boi"); //Remove
                break;
            case "Fuel":
                print("Fuel regain yeah"); //Todo maybe remove
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                state = State.Dead;
                Invoke("HandlePlayerDeath", 1.5f);
                break;
        }        
    }

    private void HandlePlayerDeath()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); //Make other levels available. Make currentlevel prob.
    }

    private void Thrust()
    {
        //Thrust while rotating
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust);
            PlayRocketSound();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddRelativeForce(Vector3.down * mainThrust);
            PlayRocketSound();
        }
        else
        {
            StopRocketSound();
        }
    }

    private void Rotate()
    {       
        rigidbody.freezeRotation = true; // Take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        //Rotating either left or right
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        } 
        
        rigidbody.freezeRotation = false; // Resume physics control of rotation
    }

    private void StopRocketSound()
    {
        if(audioSource.isPlaying){
            audioSource.Stop();
        }
    }

    private void PlayRocketSound(){
        if(!audioSource.isPlaying){
            audioSource.Play();
        }
    }
}
