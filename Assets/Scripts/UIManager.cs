using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public Text platformLabel;
    public Button leaveButton;
    public Button changeViewButton;
    public Canvas extrasPanel;

    private Vector3 basePanelPosition;
    private bool panelInView;

    private int tapCount;
    private float maxDoubleTapTime = 0.25f;
    private float currentDoubleTap;
    // Start is called before the first frame update
    void Start()
    {
        leaveButton.onClick.AddListener(LeaveOnClick);
        basePanelPosition = extrasPanel.transform.position;
        extrasPanel.gameObject.SetActive(false);
        tapCount = 0;

#if UNITY_IOS
        platformLabel.text = "Current Platform: iOS";
#elif UNITY_ANDROID
        platformLabel.text = "Current Platform: Android";
#elif UNITY_EDITOR
        platformLabel.text = "Current Platform: Unity Editor";
#endif
    }
    public void LeaveOnClick()
    {
#if UNITY_IOS
        Application.Unload();
#elif UNITY_ANDROID
        Application.Quit();
#elif UNITY_EDITOR
        Application.Quit();
#endif
    }

    public void SwitchView(User activeUser)
    {
        activeUser.ChangeView();
    }

    public void MoveExtrasPanel()
    {
        if(!panelInView)
        {
            panelInView = true;
            extrasPanel.gameObject.SetActive(true);
            Debug.Log("Moveup");
        }
        else
        {
            panelInView = false;
            extrasPanel.gameObject.SetActive(false);
            Debug.Log("Movedown");
        }
    }

    private void Update()
    {
#if UNITY_ANDROID
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended)
            {
                tapCount++;
            }

            if(tapCount==1)
            {
                currentDoubleTap = Time.time + maxDoubleTapTime;
            }
            else if(tapCount == 2 && Time.time <= currentDoubleTap)
            {
                MoveExtrasPanel();
            }
        }
        if(Time.time > currentDoubleTap)
        {
            tapCount = 0;
        }
#elif UNITY_IOS
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended)
            {
                tapCount++;
            }

            if(tapCount==1)
            {
                currentDoubleTap = Time.time + maxDoubleTapTime;
            }
            else if(tapCount == 2 && Time.time <= currentDoubleTap)
            {
                MoveExtrasPanel();
            }
        }
        if(Time.time > currentDoubleTap)
        {
            tapCount = 0;
        }
#elif UNITY_EDITOR
        
#endif
    }
}
