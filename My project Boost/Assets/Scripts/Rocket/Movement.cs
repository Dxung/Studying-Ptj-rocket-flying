using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header ("Speed")]                 
    [SerializeField] float thrustSpeed = 1000f;
    [SerializeField] float rotationSpeed = 100f;

    [Header("Audio Clip")]
    [SerializeField] AudioClip thrustClip;

    [Header ("Component")]
                     Rigidbody myRigidBody;
                     SoundController myAudio;

    [Header("Particles")]
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem rightRotateParticle;
    [SerializeField] ParticleSystem leftRotateParticle;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody>();
        myAudio = this.GetComponentInChildren<SoundController>(); // take the SoundController component from its own child component : "SoundController"
    }

    // Update is called once per frame
    void Update()
    {
        RocketThrust();
        RocketRotate();
    }


    void RocketRotate()
    {

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
            PlayParticle(rightRotateParticle);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
            PlayParticle(leftRotateParticle);
        }
        else
        {
            StopParticle(leftRotateParticle);
            StopParticle(rightRotateParticle);
        }
    }

    void RocketThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlaySound(thrustClip);
            AddRelativeForce(Vector3.up, thrustSpeed);
            PlayParticle(thrustParticle);
        }
        else
        {
            StopSound();
            StopParticle(thrustParticle);

        }
    }

    // In here, we need to freeze the rotation by physics engine cause if we dont, rotating manually and rotating by physics engine at the same time will result in a weird bug.
    void ApplyRotation(float rotationSpeed)
    {
        myRigidBody.freezeRotation = true; //freeze rotation by physics engine so we can do it manually
        this.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        myRigidBody.freezeRotation = false;

    }

    //Add Relative Force
    private void AddRelativeForce(Vector3 direction, float speed)
    {
        myRigidBody.AddRelativeForce(direction * speed * Time.deltaTime);
    }

    //Particles
    private void PlayParticle(ParticleSystem myParticle)
    {
        if (!myParticle.isPlaying) // the reason why this is EXTREMELY necessary is: if the particle be replayed over and over many times 1 second (its in Update() ),
                                   // it can "only played for a shot time (0.001s) before being reloaded", so we will not be able to see any particle .                          
        {
            myParticle.Play();
        }
       
    }

    private void StopParticle(ParticleSystem myPartical)
    {
        myPartical.Stop();
    }

    private void PlaySound(AudioClip myClip)
    {
        myAudio.StartSound(thrustClip);
    }

    private void StopSound()
    {
        myAudio.StopSound();

    }
}
