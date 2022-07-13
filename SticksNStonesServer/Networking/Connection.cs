using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using SticksNStonesServer.Messages;
using SticksNStonesServer.Model;

namespace SticksNStonesServer;

/// <summary>
/// Contains all code for one player's connection
/// Use <see cref="SendMessage{T}"/> to send a Message to this Player.
/// The class waits for incoming Messages on a seperate Thread.
/// </summary>
public class Connection{
    readonly SticksNStonesMatch _match;
    readonly PlayerInfo _playerInfo;
    readonly StreamWriter _streamWriter;

    readonly JsonSerializerOptions options = new(){
        IncludeFields = true
    };

    public Connection(TcpClient client, SticksNStonesMatch match, PlayerInfo playerInfo){
        _match = match;
        _playerInfo = playerInfo;
        this.Client = client;
        this._streamWriter = new StreamWriter(client.GetStream());
        new Thread(ReadPlayer).Start();
    }

    TcpClient Client{ get; }

    public void SendMessage<T>(T message){
        _streamWriter.WriteLine(JsonSerializer.Serialize(message,options));
        _streamWriter.Flush();
    }
    
    void ReadPlayer(){
        
        var streamReader = new StreamReader(Client.GetStream());
        var options = new JsonSerializerOptions(){
            IncludeFields = true
        };
        while (true){
            //block the reading thread until a whole line of information has arrived.
            string? json = streamReader.ReadLine();
            if (_playerInfo.isReady){
                // then, it should be a score message
                // TODO: increase score by one _playerInfo.score++;
            }
            else{ // if the player is not ready yet, we expect a LoginMessage.
                var loginMessage = JsonSerializer.Deserialize<LoginMessage>(json, options); 
                Console.WriteLine($"[#{_match.Id}] Player '{loginMessage.playerName}' logged in."); 
                _playerInfo.name = loginMessage.playerName; 
                _playerInfo.isReady = true;
            }
            
            _match.DistributeMatchInfo();
        }
    }
}