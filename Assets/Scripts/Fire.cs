using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
	public GameObject shell;
	public Transform shellEjection;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1")) {
			var s = Instantiate(shell, shellEjection);
			s.transform.parent = null;	// scale issue
		}
	}
}
