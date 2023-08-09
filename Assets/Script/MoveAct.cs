using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAct : MonoBehaviour
{
    public Animator anima = null;
    public Transform root = null;

    Vector3 moveDelta = Vector3.zero;

    private void FixedUpdate()
    {
        root.position += moveDelta;
        moveDelta = Vector3.zero;
    }

    private void OnAnimatorMove()
    {
        moveDelta += anima.deltaPosition;
        root.rotation *= anima.deltaRotation;
    }
}
