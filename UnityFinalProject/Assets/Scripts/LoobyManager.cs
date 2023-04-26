using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LoobyManager : MonoBehaviourPunCallbacks
{
    public InputField roomInput;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text roomName;

    void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnClickCreate()
    {
        PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions() { MaxPlayers = 10 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("f");
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
    }

}
