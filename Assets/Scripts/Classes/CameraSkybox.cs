using UnityEngine;

public class CameraSkybox : MonoBehaviour
{
    [SerializeField] private Material skyboxMaterial;

    private void Start()
    {
        RenderSettings.skybox = skyboxMaterial;

        // Update ambient lighting if needed
        DynamicGI.UpdateEnvironment();
    }
}