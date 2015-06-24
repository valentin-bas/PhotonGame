using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using PhotonHostRuntimeInterfaces;
using Server.Operations;
using ServerCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MmoPeer : Peer, IOperationHandler
    {
        [CLSCompliant(false)]
        public MmoPeer(IRpcProtocol rpcProtocol, IPhotonPeer nativePeer)
            : base(rpcProtocol, nativePeer)
        {
            // this is the operation handler before entering a world
            this.SetCurrentOperationHandler(this);
        }

        public static OperationResponse InvalidOperation(OperationRequest request)
        {
            return new OperationResponse(request.OperationCode)
            {
                ReturnCode = (int)ErrorCode.InvalidOperation,
                DebugMessage = "InvalidOperation: " + (OperationCode)request.OperationCode
            };
        }

        public OperationResponse OperationEnterWorld(PeerBase peer, OperationRequest request, SendParameters sendParameters)
        {
            var operation = new EnterWorld(peer.Protocol, request);
            if (!operation.IsValid)
            {
                return new OperationResponse(request.OperationCode) { ReturnCode = (int)ErrorCode.InvalidOperationParameter, DebugMessage = operation.GetErrorMessage() };
            }

            MmoWorld world = MmoWorld.Instance;

            var actor = new MmoActor(peer, world, interestArea);
            var avatar = new MmoItem(world, operation.Position, operation.Rotation, operation.Properties, actor, operation.Username, (byte)ItemType.Avatar);

            while (world.ItemCache.AddItem(avatar) == false)
            {
                Item otherAvatarItem;
                if (world.ItemCache.TryGetItem(avatar.Type, avatar.Id, out otherAvatarItem))
                {
                    avatar.Dispose();
                    actor.Dispose();
                    interestArea.Dispose();

                    ((Peer)((MmoItem)otherAvatarItem).Owner.Peer).DisconnectByOtherPeer(this, request, sendParameters);

                    // request continued later, no response here
                    return null;
                }
            }

            // init avatar
            actor.AddItem(avatar);
            actor.Avatar = avatar;

            ((Peer)peer).SetCurrentOperationHandler(actor);

            // set return values
            var responseObject = new EnterWorldResponse
            {
            };

            // send response; use item channel to ensure that this event arrives before any move or subscribe events
            var response = new OperationResponse(request.OperationCode, responseObject);
            sendParameters.ChannelId = Settings.ItemEventChannel;
            peer.SendOperationResponse(response, sendParameters);

            avatar.Spawn(operation.Position);

            // response already sent
            return null;
        }

        #region IOperationHandler

        public void OnDisconnect(PeerBase peer)
        {
            this.SetCurrentOperationHandler(null);
            this.Dispose();
        }

        public void OnDisconnectByOtherPeer(PeerBase peer)
        {
            // disconnect after any queued events are sent
            peer.RequestFiber.Enqueue(() => peer.RequestFiber.Enqueue(peer.Disconnect));
        }

        public OperationResponse OnOperationRequest(PeerBase peer, OperationRequest operationRequest, SendParameters sendParameters)
        {
            switch ((OperationCode)operationRequest.OperationCode)
            {
                case OperationCode.EnterWorld:
                    return this.OperationEnterWorld(peer, operationRequest, sendParameters);

                case OperationCode.ExitWorld:
                case OperationCode.Move:
                    return InvalidOperation(operationRequest);
            }

            return new OperationResponse(operationRequest.OperationCode)
            {
                ReturnCode = (int)ErrorCode.OperationNotSupported,
                DebugMessage = "OperationNotSupported: " + operationRequest.OperationCode
            };
        }
        #endregion

    }
}
