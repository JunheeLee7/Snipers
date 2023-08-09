using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSounds : MonoBehaviour
{
    Animator anim;
    public AudioClip clips1;
    public AudioClip clips2;
    AudioSource sources;

    public bool isClip = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sources = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(sources != null)
        {
            if(anim.GetBool("IsRun") == true)
            {
                isClip = true;
                sources.clip = clips2;
            }
            else
            {
                isClip = false;
                sources.clip = clips1;
            }
        }
    }
}
