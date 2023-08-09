using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    TestNav nav;
    Animator animator;

    AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;

    private void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        source.clip = clip1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            source.Play();
        }
    }
}
