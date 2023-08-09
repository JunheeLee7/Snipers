using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperIK : MonoBehaviour
{
    SetWeapon weap;
    public Transform leftHandPos;
    public Animator anim = null;
    public Transform leftHandPos_pist;
    public Transform leftHandPos_Grenade;

    [Range(0f, 1f)]
    public float wheight = 1.0f;

    private void Start()
    {
        weap = GetComponent<SetWeapon>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!weap.isPist && !weap.isOthers)
        {
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, wheight);
        }
        else if(weap.isPist && !weap.isOthers)
        {
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos_pist.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, wheight);
        }

        //else if(weap.isOthers)
        //{
        //    anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos_Grenade.position);
        //    anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, wheight);
        //}
    }
}
