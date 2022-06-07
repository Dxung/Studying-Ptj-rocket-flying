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

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody>();
        myAudio = gameObject.GetComponentInChildren<SoundController>(); // take the SoundController component from its own child component : "SoundController"
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
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    void RocketThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            myAudio.StartSound(thrustClip);

            myRigidBody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        }
        else
        {
            myAudio.StopSound();
        }
    }

    // In here, we need to freeze the rotation by physics engine cause if we dont, rotating manually and rotating by physics engine at the same time will result in a weird bug.
    void ApplyRotation(float rotationSpeed)
    {
        myRigidBody.freezeRotation = true; //freeze rotation by physics engine so we can do it manually
        this.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        myRigidBody.freezeRotation = false;

    }
}
