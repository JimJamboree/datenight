using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _message = _packet.ReadString();
        int _myID = _packet.ReadInt();

        Debug.Log($"Message from server: {_message}");
        Client.instance.myID = _myID;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnUser(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        string _avatarId = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnUser(_id, _avatarId, _username, _position, _rotation);
    }

    public static void UserPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.users[_id].transform.position = _position;
    }

    public static void UserRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.users[_id].transform.rotation = _rotation;
    }
}
