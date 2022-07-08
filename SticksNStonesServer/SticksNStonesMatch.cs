using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Xml.Schema;
using SticksNStonesServer.Messages;

namespace SticksNStonesServer;

public class SticksNStonesMatch{
    public TcpClient Player1{ get; private set; }
    public TcpClient Player2{ get; private set; }

    public MatchInfo matchInfo = new MatchInfo();

    public void InitPlayer1(TcpClient client){
        Player1 = client;
        
        // start reading thread
        new Thread(() => {
            
            var streamReader = new StreamReader(client.GetStream());
            var options = new JsonSerializerOptions(){
                IncludeFields = true
            };
            while (true){
                //block the reading thread until a whole line of information has arrived.
                string json = streamReader.ReadLine();
                var loginMessage = JsonSerializer.Deserialize<LoginMessage>(json, options);
                matchInfo.player1 = new PlayerInfo(){
                    name = loginMessage.playerName
                };
            }
        }).Start();
    }
    public void InitPlayer2(TcpClient client){
        Player2 = client;
        
        // start reading thread
        new Thread(() => {
            
            var streamReader = new StreamReader(client.GetStream());
            var options = new JsonSerializerOptions(){
                IncludeFields = true
            };
            while (true){
                //block the reading thread until a whole line of information has arrived.
                string json = streamReader.ReadLine();
                var loginMessage = JsonSerializer.Deserialize<LoginMessage>(json, options);
                matchInfo.player2 = new PlayerInfo(){
                    name = loginMessage.playerName
                };
            }
        }).Start();
    }
    public void Start(){
        while (true){
            if (!matchInfo.isStarted){
                if (matchInfo.player2 != null && matchInfo.player1 != null){
                    // start game
                    Console.WriteLine("Start Game");
                }
            }
        }
    }
}