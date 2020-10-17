using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class User : MonoBehaviour
{
    public float walkingSpeed = 0.5f;
    public float runningSpeed = 1.5f;
    public float jumpSpeed = 2.0f;
    public float gravity = 20.0f;

    public Camera dateCamera;
    public Camera viewCamera;
    private Camera currentCamera;
    private Vector3 dateViewPosition;
    private Quaternion dateViewRotation;

    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public float speed = 3.5f;
    private float X;
    private float Y;

    public CharacterController characterController;

    public UIManager ui;
    public SwipeControls cameraManager;
    private GameObject cameraAnchor;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        // characterController = GetComponent<CharacterController>();

        // Lock cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        dateViewPosition = dateCamera.transform.position;
        dateViewRotation = dateCamera.transform.rotation;
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        cameraAnchor = GameObject.FindGameObjectWithTag("Camera Anchor");

        viewCamera.enabled = false;
    }

    public void ChangeView()
    {
        cameraManager.ChangeCamera();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ui.MoveExtrasPanel();
        }
    }
}