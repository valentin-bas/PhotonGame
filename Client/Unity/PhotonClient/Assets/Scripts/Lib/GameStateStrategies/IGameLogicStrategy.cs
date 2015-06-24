using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System.Collections.Generic;

public interface IGameLogicStrategy
{
    GameState State { get; }

    void OnEventReceive(Game gameLogic, EventData eventData);
    void OnOperationReturn(Game gameLogic, OperationResponse operationResponse);
    void OnPeerStatusCallback(Game gameLogic, StatusCode returnCode);
    void OnUpdate(Game gameLogic);
    void SendOperation(Game game, OperationCode operationCode, Dictionary<byte, object> parameter, bool sendReliable, byte channelId);
}
