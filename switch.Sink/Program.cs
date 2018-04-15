using Switch.Core.Model;
using Switch.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trx.Messaging;
using Trx.Messaging.Channels;
using Trx.Messaging.FlowControl;
using Trx.Messaging.Iso8583;

namespace @switch.Sink
{
    class Program
    {
        static void Main(string[] args)
        {
            SourcePeers();
            SinkPeers();
            Console.ReadKey();
        }
        public static void SourcePeers()
        {

            List<SourceNode> nodes = new GenericService<SourceNode>().FilterBy(x => x.Status == true).ToList();
            foreach (var node in nodes)
            {
                //  ISourceNode source= new 
                Console.WriteLine("Listening for connection on Port: " + node.Port);
                TcpListener tcpListener = new TcpListener(node.Port);
                tcpListener.LocalInterface = node.IPAdress;// "127.0.0.1";// SourceNode.IPAddress.ToString();
                tcpListener.Start();

                ListenerPeer listener = new ListenerPeer(node.Id.ToString(), new TwoBytesNboHeaderChannel
                        (new Iso8583Ascii1987BinaryBitmapMessageFormatter(), node.IPAdress/*SourceNode.IPAddress.ToString()*/, node.Port),
                         new BasicMessagesIdentifier(11, 41), tcpListener);

                listener.Receive += new PeerReceiveEventHandler(Listener_Receive);
                listener.Connected += new PeerConnectedEventHandler(Listener_Connected);
            }
           // Thread.Sleep(15000);
        }
        public static void SinkPeers()
        {
            IList<SinkNode> sinkNodes = new GenericService<SinkNode>().FilterBy(x=>x.Status==true);
            foreach (var sinkNode in sinkNodes)
            {
                Console.Write("Initializing ClientPerrs peers\n");
                ClientPeer client = new ClientPeer(sinkNode.Id.ToString(), new TwoBytesNboHeaderChannel(new Iso8583Ascii1987BinaryBitmapMessageFormatter(), sinkNode.IPAdress, sinkNode.Port), new BasicMessagesIdentifier(11, 41));
                client.RequestDone += new PeerRequestDoneEventHandler(Client_RequestDone);
               // Log(string.Format("Request Done"));
                client.RequestCancelled += new PeerRequestCancelledEventHandler(Client_RequestCancelled);
                //ListOfClient.Add(client);
                client.Connect();
            }

        }
        public static void Client_RequestDone(object sender, PeerRequestDoneEventArgs e)
        {
          Iso8583Message mess =  e.Request.Payload as Iso8583Message;
        }
        public static void Client_RequestCancelled(object sender, PeerRequestCancelledEventArgs e )
        {
          Iso8583Message mess =  e.Request.Payload as Iso8583Message;
        }
        static void Listener_Connected(object sender, EventArgs e)
        {
            Console.WriteLine("The Peer has connected.");
        }
        static void Listener_Receive(object sender, ReceiveEventArgs e)
        {
            Console.WriteLine("Listener Received is called.");
            //Cast event sender as ClientPeer
            ListenerPeer sourcePeer = sender as ListenerPeer;

            // Get source node from client - client Name = SourceNode ID
            long sourceID = Convert.ToInt64(sourcePeer.Name); //wher message is coming from

            //then use the ID to retrieve the source node

            //Get the Message received
            Iso8583Message originalMessage = e.Message as Iso8583Message;

            //continue coding

        }

        public enum SourceNodess
        {
            ID=1,
            Port=2003,
            //IPAddress="127.0.0.1"
            
        }
    }
   
}
