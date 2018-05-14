using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingWalk : StateMachineBehaviour {
    public int currentPoint = 0;
    public float moveSpeed;

    List<Transform> wayPoints;
    Vector3 nextPoint;
    public Transform targetPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        moveSpeed = 1f; 

        wayPoints = new List<Transform>();
        foreach (Transform t in GameObject.Find("WayPoints").transform)
            wayPoints.Add(t);

        if (currentPoint >= wayPoints.Count)
            currentPoint = 0;
        if (wayPoints.Count >= 2 && currentPoint < wayPoints.Count)
            nextPoint = wayPoints[currentPoint].transform.position;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (targetPlayer != null)
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, targetPlayer.position, moveSpeed * Time.deltaTime);
            //animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(targetPlayer.position), 10f);
            animator.transform.LookAt(targetPlayer);
        }
        else
        {
            if (wayPoints.Count >= 2)
            {
                //float dist = Vector3.Distance(animator.transform.position, nextPoint);
                float dist = Vector2.Distance(new Vector2(animator.transform.position.x, animator.transform.position.z), new Vector2(nextPoint.x, nextPoint.z));
                if (dist < 0.1f)
                {
                    currentPoint++;
                    if (currentPoint >= wayPoints.Count)
                        currentPoint = 0;
                    nextPoint = wayPoints[currentPoint].transform.position;
                }
                Vector3 dir = nextPoint - animator.transform.position;
                dir.y = 0f;

                animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(dir), 0.1f);
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, nextPoint, moveSpeed * Time.deltaTime);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
