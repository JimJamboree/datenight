using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientUI : MonoBehaviour
{
    public static ClientUI instance;

    public GameObject startMenu;
    public InputField usernameField;
    public InputField avatarIdField;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying");
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        avatarIdField.interactable = false;
        Client.instance.ConnectToServer();
    }
}
