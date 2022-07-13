using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SticksNStonesServer;

    /// <summary>
    /// Matchmaking class. It assigns newly connected players.
    /// To SticksNStoneMatch instances.
    /// </summary>
    public static class Program{
        public static void Main(){
            // start listening to new connections on the given socket
            var tcpListener = new TcpListener(IPAddress.Any, 12244);
            tcpListener.Start();

            SticksNStonesMatch match = null;

            while (true){
                Console.WriteLine("Waiting for connections...");
                // Wait for a client to establish a connection and return that connection
                var tcpClient = tcpListener.AcceptTcpClient();
    
                Console.WriteLine($"Client {tcpClient.Client.RemoteEndPoint} connected!");

                if (match == null){
                    Console.WriteLine("Starting new Match for Player. Waiting for Second Player");
                    match = new SticksNStonesMatch();
                    match.InitPlayer1(tcpClient);
                }
                else{
                    Console.WriteLine("Assigning PLayer to existing Match. Starting Match");
                    match.InitPlayer2(tcpClient);
                    new Thread(match.Start).Start();
                    match = null;
                }
            }
        }
    }

