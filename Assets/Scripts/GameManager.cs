using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, UserManager> users = new Dictionary<int, UserManager>();

    public GameObject localPrefab;      // the prefab the active user sees
    public GameObject playerPrefab;     // the prefab the other user(s) see
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

    public void SpawnUser(int _id, string _avatarId, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _user;
        if(_id == Client.instance.myID)
        {
            _user = Instantiate(localPrefab, _position, _rotation);
        }
        else
        {
            _user = Instantiate(playerPrefab, _position, _rotation);
        }

        _user.GetComponent<UserStartup>().ActivateOtherScripts(_avatarId);
        _user.GetComponent<UserManager>().id = _id;
        _user.GetComponent<UserManager>().username = _username;
        _user.GetComponent<UserManager>().avatarId = _avatarId;
        users.Add(_id, _user.GetComponent<UserManager>());
    }
}
