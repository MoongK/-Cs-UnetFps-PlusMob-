using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FirstPersonController : NetworkBehaviour {
    public float mouseSensitivityX = 2;
    public float mouseSensitivityY = 2;
    public float walkSpeed;
    public float jumpForce = 220;
    public LayerMask groundedMask;

    bool grounded;
    Vector3 moveAmount;
    float verticalLookRotation;
    Transform cameraTransform;
    Rigidbody rb;
    Animator anim;

    private void Awake()
    {
        GetComponentInChildren<Camera>().enabled = false;
        GetComponentInChildren<AudioListener>().enabled = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        walkSpeed = 3f;
    }

    public override void OnStartLocalPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GetComponentInChildren<Renderer>().material.color = Color.magenta;
        cameraTransform = GetComponentInChildren<Camera>().transform;
        GetComponentInChildren<Camera>().enabled = true;
        GetComponentInChildren<AudioListener>().enabled = true;
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);

        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        anim.SetFloat("DirX", inputX);
        anim.SetFloat("DirY", inputY);

        Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
        moveAmount = moveDir * walkSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
                rb.AddForce(transform.up * jumpForce);
        }

        Ray ray = new Ray(transform.position + new Vector3(0, 0.02f), -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f, groundedMask))
            grounded = true;
        else
            grounded = false;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + localMove);
    }
}
