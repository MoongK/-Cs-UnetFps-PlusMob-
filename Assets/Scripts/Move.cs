using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public float moveSpeed = 6f;
	public float slideSpeed = 3f;
	public GameObject healFX;

	CharacterController	con;
	Vector3 moveDirection = Vector3.zero;

	float jumpSpeed = 8f;
	float gravity = 20f;

	bool isHealing = false;
	GameObject fx;

	bool isOnSlope = false;
	Vector3 hitNormal;
	Vector3 hitPoint;

	// Use this for initialization
	void Start () {
		con = GetComponent<CharacterController>();
	}

	void Update () {
		if (con.isGrounded) {
			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");

			moveDirection = (new Vector3(h, 0f, v)).normalized;
			
			// transform.LookAt(transform.position + moveDirection);			// for TPS camera
			moveDirection = transform.TransformDirection(moveDirection);		// for FPS camera

			moveDirection *= moveSpeed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
		}
		moveDirection.y -= gravity * Time.deltaTime;

		isOnSlope = Vector3.Angle (Vector3.up, hitNormal) >  con.slopeLimit;
		Vector3 slideDirection = Vector3.zero;
		if (isOnSlope) {
			slideDirection = (new Vector3(hitNormal.x, 0f, hitNormal.z)) * slideSpeed;
			Debug.DrawRay(hitPoint, slideDirection, Color.blue, 3f);
		}		
		con.Move((moveDirection + slideDirection) * Time.deltaTime);
	}


	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.collider.gameObject.tag == "Wall") {
			if (hit.collider.gameObject.name == "Step3") {
				if (!isHealing) {
					fx = Instantiate(healFX, transform.Find("FXPos"));
					isHealing = true;
					Invoke("RemoveHealFX", 1.9f);
				}
			}

			hitNormal = hit.normal;
			hitPoint = hit.point;
		}
	}

	void RemoveHealFX() {
		Destroy(fx.gameObject);
		isHealing = false;
	}
}
