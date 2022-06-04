using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {

        switch (other.gameObject.tag) 
        {
            case "Friendly": 
                Debug.Log("Friendly");
                break;

            case "Finish":
                Debug.Log("you finished");
                break;
            default:
                ReloadScene();
                break;

        }
    }

    private void ReloadScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex; // get the build index of the scene that is active
        SceneManager.LoadScene(index);
    }
}
