using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LoobyManager : MonoBehaviourPunCallbacks
{
    public InputField roomInput;
    public InputField joinInput;
    public GameObject PlayerPrefab;

    void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnClickCreate()
    {
        PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions() { MaxPlayers = 10 });
    }

    public void OnClickJoin()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
        //PhotonNetwork.Instantiate(PlayerPrefab.name);
    }

}
