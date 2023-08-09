using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAnimatorControl : MonoBehaviour
{
    Animator animator;
    Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        player.pistolShot += StartingShotAnim;
    }

    public void StartingShotAnim()
    {
        animator.SetTrigger("Fires");
    }
}
