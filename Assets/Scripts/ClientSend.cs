﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }


    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myID);
            _packet.Write(ClientUI.instance.usernameField.text);
            _packet.Write(ClientUI.instance.avatarIdField.text);

            SendTCPData(_packet);
        }
    }

    public static void UserInputs(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.userInputs))
        {
            _packet.Write(_inputs.Length);
            foreach(bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.users[Client.instance.myID].transform.rotation);

            SendUDPData(_packet);
        }
    }
    #endregion
}
