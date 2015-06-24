using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using System.Text;

public class Game : IPhotonPeerListener
{
    private PhotonPeer peer;
    private IGameListener listener;
    private IGameLogicStrategy stateStrategy;
    private Settings settings;

    public PhotonPeer Peer
    {
        get
        {
            return peer;
        }
    }

    public Game(IGameListener listener, Settings settings, string avatarName)
    {
        this.listener = listener;
        this.settings = settings;

        this.stateStrategy = Disconnected.Instance;
    }

    public void DebugReturn(DebugLevel level, string debug)
    {
        if (listener.IsDebugLogEnabled)
            listener.LogDebug(this, string.Concat("@avatarId", ": ", debug));
    }

    public void OnEvent(EventData ev)
    {
        if (listener.IsDebugLogEnabled)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}: received event {1}:", "@avatarId", (EventCode)ev.Code);
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        throw new System.NotImplementedException();
    }

    public void Initialize(PhotonPeer photonPeer)
    {
        peer = photonPeer;
        stateStrategy = WaitingForConnect.Instance;
        //photonPeer.Connect()
    }

    public void OnUnexpectedEventRecive(EventData @event)
    {
        listener.LogError(this, string.Format("{0}: unexpected event {1}", "@avatarNameToDefine", @event.Code));
    }

    public void OnUnexpectedOperationError(OperationResponse operationResponse)
    {
        string message = string.Format(
            "{0}: unexpected operation error {1} from operation {2} in state {3}",
            "@avatarNameToDefine",
            operationResponse.DebugMessage,
            operationResponse.ReturnCode,
            stateStrategy.State);
        listener.LogError(this, message);
    }

    public void OnUnexpectedPhotonReturn(OperationResponse operationResponse)
    {
        listener.LogError(this, string.Format("{0}: unexpected return {1}", "@avatarNameToDefine", operationResponse.OperationCode));
    }

    public void SetDisconnected(StatusCode returnCode)
    {
        stateStrategy = Disconnected.Instance;
        listener.OnDisconnect(this, returnCode);
    }
}
