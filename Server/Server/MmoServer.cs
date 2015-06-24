using Photon.SocketServer;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MmoServer : ApplicationBase
{
    protected override PeerBase CreatePeer(InitRequest initRequest)
    {
        return new MmoPeer(initRequest.Protocol, initRequest.PhotonPeer);
    }

    protected override void Setup()
    {
    }

    protected override void TearDown()
    {
    }
}
