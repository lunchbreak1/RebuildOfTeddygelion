using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneOnTrigger : MonoBehaviour
{
    void Update()
    {
        // For most gamepads, the right trigger is on axis "TriggerRight" or "9th axis" (depends on setup)
        float rightTrigger = Input.GetAxis("TriggerRight"); // Or "RT" if you’ve mapped it in Input Manager

        // Alternative: use the new Input System (see below)
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
