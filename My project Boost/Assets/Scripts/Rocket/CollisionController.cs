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

    [Header("For Debug")]
                     bool collisionDisable;
                     


    private void Start()
    {
        //the ".gameObject" refers to the gameObject that this script is attaching to - in this case, it is "Rocket Outlook" 
        //the ".transform" refers to the transform component of the gameObject this script is attaching to - in this case, it is "Rocket Outlook"
        //the "transform.parent" refers to the parent gameObject of the gameObject that has this transform component. 
        mySoundController = this.GetComponentInChildren<SoundController>();
    }

    private void Update()
    {
        ForDebug();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable ) { return; } // if we are transitioning, no other method in collision should be called. If not, it will play crash/finish sound the second time
                                             // or call method ReloadScene()/ LoadNextScene() the second time
        
        
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

    private void ForDebug()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            collisionDisable = !collisionDisable ;
            Debug.Log(collisionDisable);
        }else if (Input.GetKeyDown(KeyCode.P))
        {
            LoadNextScene();
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
