using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class WaitingForConnect : IGameLogicStrategy
{
    public static readonly IGameLogicStrategy Instance = new WaitingForConnect();

    public GameState State
    {
        get
        {
            return GameState.WaitForConnect;
        }
    }

    public void OnEventReceive(Game game, EventData @event)
    {
        game.OnUnexpectedEventRecive(@event);
    }

    public void OnOperationReturn(Game game, OperationResponse operationResponse)
    {
        game.OnUnexpectedOperationError(operationResponse);
    }

    public void OnPeerStatusCallback(Game game, StatusCode returnCode)
    {
        switch (returnCode)
        {
            case StatusCode.Connect:
                {
                    break;
                }

            case StatusCode.Disconnect:
            case StatusCode.DisconnectByServer:
            case StatusCode.DisconnectByServerLogic:
            case StatusCode.DisconnectByServerUserLimit:
            case StatusCode.TimeoutDisconnect:
                {
                    game.SetDisconnected(returnCode);
                    break;
                }

            default:
                {
                    game.DebugReturn(DebugLevel.ERROR, returnCode.ToString());
                    break;
                }
        }
    }

    public void OnUpdate(Game game)
    {
        game.Peer.Service();
    }

    public void SendOperation(Game game, OperationCode operationCode, Dictionary<byte, object> parameter, bool sendReliable, byte channelId)
    {
    }
}
