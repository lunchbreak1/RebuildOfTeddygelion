using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneOnTrigger : MonoBehaviour
{
    void Update()
    {
        float rightTrigger = Input.GetAxis("TriggerRight"); 

        if (rightTrigger > 0.1f)
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
