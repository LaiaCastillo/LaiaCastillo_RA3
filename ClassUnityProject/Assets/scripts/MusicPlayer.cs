using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip; 
    [SerializeField] private float volume = 1f;
    private static MusicPlayer instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = musicClip;
        audioSource.loop = true;       
        audioSource.volume = volume;
        audioSource.playOnAwake = false; 

        audioSource.Play();
    }

    public void StopMusic() => audioSource.Stop();
    public void PauseMusic() => audioSource.Pause();
    public void ResumeMusic() => audioSource.UnPause();
}