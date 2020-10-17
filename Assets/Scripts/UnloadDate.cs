using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnloadDate : MonoBehaviour
{
    private Button leaveButton;

    // Start is called before the first frame update
    void Start()
    {
        leaveButton = GetComponentInParent<Button>();
        leaveButton.onClick.AddListener(LeaveOnClick);
    }

    void LeaveOnClick()
    {

    }

    private void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("Leave Date");
            }
        }
    }
}
