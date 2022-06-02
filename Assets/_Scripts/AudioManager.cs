using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject ui;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] soundTrack;
    [SerializeField] public AudioClip[] clips;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource.playOnAwake)
        {
            audioSource.clip = soundTrack[Random.Range(0, soundTrack.Length)];
            audioSource.Play();
        }
    }
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundTrack[Random.Range(0, soundTrack.Length)];
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
    public void SongSelector(int a)
    {
        audioSource.Stop();
        audioSource.clip = soundTrack[a];
        audioSource.Play();
    }
    public void PlayClip(int a)
    {
        audioSource.PlayOneShot(clips[a]);
    }



}
