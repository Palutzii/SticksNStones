using System;
using System.Net.Sockets;
using SticksNStonesServer.Messages;

namespace SticksNStonesServer;


/// <summary>
/// Holds the Match info for a PVP Match.
/// As well as both player's connections.
/// </summary>
public class SticksNStonesMatch{
    static int _id;
    public int Id{ get; }
    Connection? Player1{ get; set; }
    Connection? Player2{ get; set; }

    readonly MatchInfo matchInfo = new();

    public SticksNStonesMatch(){
        Id = ++_id;
    }

    public void InitPlayer1(TcpClient client){
        Player1 = new Connection(client,this,matchInfo.player1);
    }
    public void InitPlayer2(TcpClient client){
        Player2 = new Connection(client,this,matchInfo.player2);
    }
    
    /// <summary>
    /// Helper method to synchronize all players.
    /// </summary>
    public void DistributeMatchInfo(){
        var message = new MatchInfoMessage(){
            matchInfo = this.matchInfo
        };
        Player1?.SendMessage(message);
        Player2?.SendMessage(message);
    }
    
    /// <summary>
    /// Main Game Loop
    /// </summary>
    public void Start(){
        while (true){
            if (!matchInfo.isStarted){
                if (matchInfo.player2.isReady && matchInfo.player1.isReady){
                    // start game
                    Console.WriteLine("Start Game");
                    matchInfo.isStarted = true;
                    DistributeMatchInfo();
                }
            }
            else{
                // TODO: check for winner (e.g 10 score)
            }
        }
    }
}