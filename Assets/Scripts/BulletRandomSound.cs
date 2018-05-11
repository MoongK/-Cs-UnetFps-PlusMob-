using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRandomSound : MonoBehaviour {
    public AudioClip[] clips;

    AudioSource sound;

    private void Awake()
    {
        sound = gameObject.AddComponent<AudioSource>();
    }

    public void Play()
    {
        int rnd = Random.Range(0, clips.Length);
        sound.PlayOneShot(clips[rnd], 1f);
    }
}
