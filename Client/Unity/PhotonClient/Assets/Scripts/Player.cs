using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;

public class Player : MonoBehaviour, IPhotonPeerListener
{
    public string IP;

    bool connected = false;
    PhotonPeer peer;

    void Start()
    {
        peer = new PhotonPeer(this, ConnectionProtocol.Tcp);
        peer.Connect(IP, "MmoServer");
    }

    void Update()
    {
        peer.Service();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(level + ": " + message);
    }

    public void OnEvent(EventData eventData)
    {
        Debug.Log("Event: " + eventData.Code);
        if (eventData.Code == 1)
        {
            Debug.Log("Chat: " + eventData.Parameters[1]);
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        Debug.Log("Response: " + operationResponse.OperationCode);
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        if (statusCode == StatusCode.Connect)
        {
            connected = true;
        }
        else
        {
            Debug.Log("Status: " + statusCode);
        }
    }
}