using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;
    private static AudioSource musicSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        musicSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public static void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public static float GetVolume()
    {
        return musicSource.volume;
    }
}