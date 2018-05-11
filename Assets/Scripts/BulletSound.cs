using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour {
    public void Play()
    {
        GetComponent<AudioSource>().Play();
    }
}
