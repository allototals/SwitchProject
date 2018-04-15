using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trx.Messaging;
using Trx.Messaging.Channels;
using Trx.Messaging.FlowControl;
using Trx.Messaging.Iso8583;

namespace SourceNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Connect();
        }
        public static  void Connect()
        {
            ClientPeer client = new ClientPeer(aSinkNode.ID.ToString(),
                        new TwoBytesNboHeaderChannel(new Iso8583Ascii1987BinaryBitmapMessageFormatter(), sink.IPAddress, sink.Port),
                        new BasicMessagesIdentifier(11, 41)); //11 => (STAN) Systems Trace Audit Number 
                //41 => Card Acceptor (Channel) Terminal Identification
                client.RequestDone += new PeerRequestDoneEventHandler(Client_RequestDone);
                client.RequestCancelled += new PeerRequestCancelledEventHandler(Client_RequestCancelled);
        }


//where (Client_RequestDone) and (Client_RequestCancelled) are methods you need to implement.
//Their signatures are as follows:


        //When the requested send to a Sink (client) is done
        static void Client_RequestDone(object sender, PeerRequestDoneEventArgs e)
        {
            Iso8583Message response = e.Request.ResponseMessage as Iso8583Message;
            SourceNode sourceNode = e.Request.Payload as SourceNode;
	    
	    //continue coding
	   }

	//When the requested send to a Sink (client) is NOT done
	   static void Client_RequestCancelled(object sender, PeerRequestCancelledEventArgs e)
        {
            Iso8583Message message = e.Request.RequestMessage as Iso8583Message;
            SourceNode source = e.Request.Payload as SourceNode;

	    //continue coding
	  }
        
    }
}
