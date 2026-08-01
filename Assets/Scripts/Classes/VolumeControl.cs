using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VolumeControl : MonoBehaviour
{
    [Range(0f, 1f)]
    public float volume;

    [SerializeField]
    private UnityEngine.UI.Slider volumeSlider;
    // Start is called before the first frame update

    [SerializeField]
    private UnityEngine.UI.Image fillArea;
    void Start()
    {
        volumeSlider = GetComponentInChildren<UnityEngine.UI.Slider>();
        volume = MusicPlayer.GetVolume();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") / 100;
        
        volume = Mathf.Clamp01(volume + horizontal);

        MusicPlayer.SetVolume(volume);
        volumeSlider.value = volume;

        if(volume > 0 && !fillArea.enabled)
        {
            fillArea.enabled = true;
        }

        if (volume == 0 && fillArea.enabled)
        {
            fillArea.enabled = false;
        }
    }
}
