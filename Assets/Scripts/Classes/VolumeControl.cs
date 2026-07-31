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
    }
}
