using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    [SerializeField]
    AudioClip[] clips;

    int currentClip = 0;

    void Start()
    {
        gameObject.audio.clip = clips[0];
    }

    public void PlayClip(int index)
    {
        index %= clips.Length;


        if (gameObject.audio.isPlaying)
            gameObject.audio.Stop();

        gameObject.audio.clip = clips[index];

        gameObject.audio.Play();
    }

    public void StopPlaying()
    {
        if (gameObject.audio.isPlaying)
            gameObject.audio.Stop();
    }

}
