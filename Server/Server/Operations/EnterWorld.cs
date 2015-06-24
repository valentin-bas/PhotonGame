using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerCode;

namespace Server.Operations
{
    public class EnterWorld : Operation
    {
        public EnterWorld(IRpcProtocol protocol, OperationRequest request)
            : base(protocol, request)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="Actor.Avatar"/>'s initial position.
        /// This request parameter is mandatory.
        /// </summary>
        [DataMember(Code = (byte)ParameterCode.Position)]
        public float[] Position { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Actor.Avatar"/>'s initial properties.
        /// This request parameter is optional.
        /// </summary>
        [DataMember(Code = (byte)ParameterCode.Properties, IsOptional = true)]
        public Hashtable Properties { get; set; }

        /// <summary>
        /// Gets or sets the new rotation.
        /// This request parameter is optional.
        /// </summary>
        [DataMember(Code = (byte)ParameterCode.Rotation, IsOptional = true)]
        public float[] Rotation { get; set; }

        /// <summary>
        /// Gets or sets the client's username. This will be the <see cref="Actor.Avatar"/>'s <see cref="Item.Id">Item Id</see>.
        /// This request parameter is mandatory.
        /// </summary>
        [DataMember(Code = (byte)ParameterCode.Username)]
        public string Username { get; set; }

        public OperationResponse GetOperationResponse(short errorCode, string debugMessage)
        {
            var responseObject = new EnterWorldResponse();
            return new OperationResponse(this.OperationRequest.OperationCode, responseObject) { ReturnCode = errorCode, DebugMessage = debugMessage };
        }

        public OperationResponse GetOperationResponse(MethodReturnValue returnValue)
        {
            return this.GetOperationResponse(returnValue.Error, returnValue.Debug);
        }
    }
}
