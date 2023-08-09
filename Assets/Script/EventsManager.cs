using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;

    public GameObject gre;
    Grenades grenades;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ShootingNow()
    {
        grenades = GetComponentInChildren<Grenades>();
        gre.transform.GetChild(0).SetParent(null);
        grenades.ShootGrenades();
    }
    public void CheckedConstraints()
    {
        rb = gre.transform.GetChild(0).GetComponent<Rigidbody>();
        rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
    }
}
