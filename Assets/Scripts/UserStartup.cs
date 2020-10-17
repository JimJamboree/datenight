using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wolf3D.ReadyPlayerMe.AvatarSDK;

public class UserStartup : MonoBehaviour
{
    [SerializeField] private GameObject user;
    public SwipeControls userCamera;
    public UserAvatar userAvatar;
    public bool isSeated;
    // Start is called before the first frame update
    void Start()
    {
        user = gameObject;
        DontDestroyOnLoad(user);
    }

    public void ActivateOtherScripts(string avatar)
    {
        Debug.Log("Activating other scripts.");
        userCamera.gameObject.SetActive(true);
        userCamera.enabled = true;
        userAvatar.SetAvatarAndWake(avatar);
        userAvatar.enabled = true;
        SceneManager.LoadScene(1);
    }

    public void SeatUser(GameObject seat)
    {
        gameObject.transform.position = seat.transform.position;
        gameObject.transform.rotation = seat.transform.rotation;
        isSeated = true;
    }
}
