using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    private Touch initialTouch = new Touch();
    private Camera currentCamera;
    private bool isDateView;
    public Camera dateCamera;
    public Camera viewCamera;

    private float xRotation;
    private float yRotation;
    private Vector3 originalRotation;

    public float rotationSpeed = 0.5f;
    public float direction = -1;

    // Start is called before the first frame update
    void Start()
    {
        currentCamera = dateCamera;                // always start with date view
        viewCamera.gameObject.SetActive(false);    // ensure only one camera is active as any given time
        originalRotation = currentCamera.transform.eulerAngles;
        xRotation = originalRotation.x;
        yRotation = originalRotation.y;
        isDateView = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
//#if UNITY_ANDROID || UNITY_IOS
        //Touch cameraTouch = Input.GetTouch(0);
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                initialTouch = Input.GetTouch(0);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // lol
                float deltaPosX = initialTouch.position.x - Input.GetTouch(0).position.x;
                float deltaPosY = initialTouch.position.y - Input.GetTouch(0).position.y;

                xRotation -= deltaPosY * Time.deltaTime * rotationSpeed * direction;
                yRotation += deltaPosX * Time.deltaTime * rotationSpeed * direction;

                currentCamera.transform.eulerAngles = new Vector3(xRotation, yRotation, 0f);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                initialTouch = new Touch();
            }
        }
//#endif
    }

    public void ChangeCamera()
    {
        if(isDateView)
        {
            viewCamera.enabled = true;
            dateCamera.enabled = false;

            dateCamera.gameObject.SetActive(false);
            viewCamera.gameObject.SetActive(true);

            currentCamera = viewCamera;
            isDateView = false;
        }
        else if(!isDateView)
        {
            dateCamera.enabled = true;
            viewCamera.enabled = false;

            viewCamera.gameObject.SetActive(false);
            dateCamera.gameObject.SetActive(true);

            currentCamera = dateCamera;
            isDateView = true;
        }
    }
}
