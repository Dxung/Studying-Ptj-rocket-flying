using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour
{
    [Header("scene Transitioning")] 
    [SerializeField] float timeToWait = 1f;
    [SerializeField] bool isTransitioning = false;

    [Header("Audio Clip")]
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip finishClip;

    [Header("Particles System")]
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;

    [Header("Component")]
                     SoundController mySoundController;

    private void Start()
    {
        //the ".gameObject" refers to the gameObject that this script is attaching to - in this case, it is "Rocket Outlook" 
        //the ".transform" refers to the transform component of the gameObject this script is attaching to - in this case, it is "Rocket Outlook"
        //the "transform.parent" refers to the parent gameObject of the gameObject that has this transform component. 
        mySoundController = this.GetComponentInChildren<SoundController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning == false) // this has to be false, so in case "we crashed and the rocket falls on some obstacles, it will not play the crash sound the 2nd time.
                                      // or play finish sound
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Friendly");
                    break;

                case "Finish":
                    AccessNextStage();
                    break;
                default:
                    ReAccessStage();
                    break;

            }
        }
    }

    private void ReloadScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex; // get the build index of the scene that is active
        SceneManager.LoadScene(index);
    }

    private void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentIndex + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
            SceneManager.LoadScene(nextScene);
    }
    private void ReAccessStage()
    {
        //change status
        isTransitioning = true;

        //play crash sound
        mySoundController.StopSound();
        mySoundController.StartSound(crashClip);

        //disable control
        this.GetComponent<Movement>().enabled = false;

        //play crash particle
        crashParticle.Play();
        
        //reload scene
        Invoke("ReloadScene", timeToWait);
        }

    private void AccessNextStage()
    {
        //change status
        isTransitioning = true;

        //play success sound
        mySoundController.StopSound();
        mySoundController.StartSound(finishClip);

        //disable control
        this.GetComponent<Movement>().enabled = false;

        //play success particle
        successParticle.Play();

        //load next scene
        Invoke("LoadNextScene", timeToWait);
    }


}
