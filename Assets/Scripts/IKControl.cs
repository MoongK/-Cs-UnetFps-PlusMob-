using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IKControl : NetworkBehaviour {

    public bool ikActive = true;
    public Transform rightHandTarget, leftHandTarget;
    public Transform lookTarget;
    public Transform body, chest;
    public Transform mouse;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (ikActive)
        {
            print("active");
            anim.SetLookAtWeight(1f);
            anim.SetLookAtPosition(lookTarget.position);

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
            //anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            //anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
            //anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            //anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
        }
        else
        {
            print("nonactive");
            anim.SetLookAtWeight(0);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }
    }

}
