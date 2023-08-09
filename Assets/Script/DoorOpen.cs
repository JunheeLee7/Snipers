using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Animator anims;
    public bool isOpen = false;

    private void Start()
    {
        anims = GetComponent<Animator>();
    }

    private void Update()
    {

        if (isOpen)
        {
            anims.SetBool("isOpen", true);
        }
    }
}
