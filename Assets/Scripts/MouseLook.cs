using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public float sensitivity = 500f;
    float rotationX;
    float rotationY;

	GameObject character;

	void Start () {
		character = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update () {
        float mouseMoveX = Input.GetAxis("Mouse X");
        float mouseMoveY = Input.GetAxis("Mouse Y");

        rotationY += mouseMoveX * sensitivity * Time.deltaTime;
        rotationX += mouseMoveY * sensitivity * Time.deltaTime;

        if (rotationX > 45f)
            rotationX = 45f;
        if (rotationX < -20f)
            rotationX = -20f;

		transform.localRotation = Quaternion.AngleAxis(-rotationX,  Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis(rotationY, character.transform.up);

        if (Input.GetKeyDown("escape"))
			Cursor.lockState = CursorLockMode.None;        
    }
}
