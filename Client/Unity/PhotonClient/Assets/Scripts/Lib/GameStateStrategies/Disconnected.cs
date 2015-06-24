using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public class Disconnected : IGameLogicStrategy
{
    public static readonly IGameLogicStrategy Instance = new Disconnected();

    public GameState State
    {
        get
        {
            return GameState.Disconnected;
        }
    }

    public void OnEventReceive(Game game, EventData @event)
    {
        game.OnUnexpectedEventRecive(@event);
    }

    public void OnOperationReturn(Game game, OperationResponse operationResponse)
    {
        game.OnUnexpectedPhotonReturn(operationResponse);
    }

    public void OnPeerStatusCallback(Game game, StatusCode returnCode)
    {
        game.DebugReturn(DebugLevel.ERROR, returnCode.ToString());
    }

    public void OnUpdate(Game game)
    {
    }

    public void SendOperation(Game game, OperationCode operationCode, Dictionary<byte, object> parameter, bool sendReliable, byte channelId)
    {
    }
}
