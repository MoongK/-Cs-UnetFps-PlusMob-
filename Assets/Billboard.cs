using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    //public Camera myCam;

    void Start()
    {
        GameObject[] gg = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject g in gg)
        {
            print("billboard : " + g.name);
            //myCam = g.GetComponent<Health>().SendmyCam() != null ? 
        }
    }

    void LateUpdate () {
        //if (myCam != null)
        //{
        //    print("Cam done(" + transform.root.name + ")");
        //    //transform.rotation = myCam.transform.rotation;
        //}
        //else
        //{
        //    print("Camera null(" + transform.root.name + ")");
        //    transform.Rotate(Vector3.up * 3f);
        //}
        if(Camera.current != null)
            transform.rotation = Camera.current.transform.rotation; // Camera.current : Null error 아 빡쳐
    }
}
