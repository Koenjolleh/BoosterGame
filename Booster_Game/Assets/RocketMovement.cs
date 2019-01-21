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

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip succes;
    [SerializeField] AudioClip death;

    //Could not get this to work. Current Unity version dose not support Legacy Particle System
    //[SerializeField] ParticleSystem mainEngineParticles;
    //[SerializeField] ParticleSystem succesParticles;
    //[SerializeField] ParticleSystem deathParticles;

    Rigidbody rigidbody;
    AudioSource audioSource;
    const float LevelLoadingDelay = 1.5f;

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
        //Fix rocket sound evt. med else StopSound
        if (state == State.Alive)
        {
            Thrust();
            Rotate(); 
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //If rocket already died or is transcending to next lvl. Collision will not be handled.
        if (state != State.Alive){ return; }

        //Handles collision of the rocket based on the tags of the objects it bumps into
        switch(collision.gameObject.tag){

            case "Friendly":
                //Todo something spicy
                break;
            case "Fuel":
                //Todo add some spicy fuel
                break;
            case "Finish":
                StartCompleteLevelSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }        
    }

    private void StartCompleteLevelSequence()
    {
        state = State.Transcending;
        PlayLevelCompleteSound();
        //succesParticles.Play();
        Invoke("LoadNextLevel", 1f);
    }

    private void StartDeathSequence()
    {
        state = State.Dead;
        PlayDeathSound();
        //deathParticles.Play();
        Invoke("LoadFirstLevel", LevelLoadingDelay);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1); //Make other levels available. Make currentlevel prob.
    }

    private void Thrust()
    {

        float thrustThisFrame = mainThrust * Time.deltaTime;

        //Thrust while rotating
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddRelativeForce(Vector3.up * thrustThisFrame);
            PlayRocketSound();
            //mainEngineParticles.Play();
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddRelativeForce(Vector3.down * thrustThisFrame);
            PlayRocketSound();
            //mainEngineParticles.Play();
        }
        else
        {
            StopSound();
            //mainEngineParticles.Stop();
        }
    }

    private void Rotate()
    {       
        rigidbody.freezeRotation = true; // Take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        //Rotate either left or right
        
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

    private void StopSound()
    {
        if(audioSource.isPlaying){
            audioSource.Stop();
        }
    }

    private void PlayRocketSound(){
        if(!audioSource.isPlaying){
            audioSource.PlayOneShot(mainEngine);
        }
    }

    private void PlayDeathSound()
    {
        StopSound();
        audioSource.PlayOneShot(death);
    }

    private void PlayLevelCompleteSound()
    {
        StopSound();
        audioSource.PlayOneShot(succes);
    }
}
