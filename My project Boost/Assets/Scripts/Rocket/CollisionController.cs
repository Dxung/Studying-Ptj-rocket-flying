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

    [Header("Component")]
                     SoundController mySoundController;

    private void Start()
    {
        mySoundController = this.GetComponentInChildren<SoundController>();
        Debug.Log(mySoundController);
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
        isTransitioning = true;
        mySoundController.StopSound();
        mySoundController.StartSound(crashClip);
        this.GetComponent<Movement>().enabled = false;      
        Invoke("ReloadScene", timeToWait);
        }

    private void AccessNextStage()
    {
        isTransitioning = true;
        mySoundController.StopSound();
        mySoundController.StartSound(finishClip);
        this.GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", timeToWait);
    }


}
