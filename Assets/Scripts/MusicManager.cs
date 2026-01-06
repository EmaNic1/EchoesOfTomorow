using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] AudioSource audioSource;

    [SerializeField] float timeToSwitch = 5f;

    [SerializeField] AudioClip playOnStart;

    void Start()
    {
        Play(playOnStart, true);
    }

    public void Play(AudioClip musicToPlay, bool interrupt = false)
    {

        if(musicToPlay == null) { return; }

        if(interrupt == true)
        {
            audioSource.volume = 0.3f;
            audioSource.clip = musicToPlay;
            audioSource.Play();
        }
        else
        {
            switchTo = musicToPlay;
            StartCoroutine(SmoothAwitchMusic());
        }
    }


    AudioClip switchTo;
    float volume;

    IEnumerator SmoothAwitchMusic()
    {
        volume = 0.3f;
        while(volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch;
            if(volume < 0f){ volume = 0f; }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }
        Play(switchTo, true);
    }
}
