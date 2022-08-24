using Networking;
using StickNStonesShared.StickNStonesShared.Messages;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField] GameObject remotePlayerPrefab;
    [SerializeField] Transform player2Spawner;
    [SerializeField] GameObject localPlayerPrefab;
    [SerializeField] Transform player1Spawner;
    CharacterController _remotePlayerController;
    PlayerManager _playerManager => FindObjectOfType<PlayerManager>();

    void Start(){
        //SpawnRemotePlayer();
        ServerConnection.Instance.Broker.Subscribe<MatchInfoMessage>(MoveRemotePlayer);


        Debug.Log("Spawning Remote Player");
        SpawnRemotePlayer(_playerManager.matchinfo);
        SpawnLocalPlayer(_playerManager.matchinfo);
    }

    // 
    void MoveRemotePlayer(MatchInfoMessage obj){
        Debug.Log("MoveRemotePlayer");
        // Depending on who's player 1, i need to update the remote players position with either player 1 data or player 2 data
        if (obj.matchInfo.player2.data.name == ServerConnection.Instance.Connection.PlayerName){
            // i am player 2, player 1 is the remote player
            var player1Data = obj.matchInfo.player1.data;
            _remotePlayerController.transform.position =
                new Vector3(player1Data.positionX, player1Data.positionY, player1Data.positionZ);
            return;
        }

        // i am player 1, player two is the remote player
        var player2Data = obj.matchInfo.player2.data;
        _remotePlayerController.transform.position =
            new Vector3(player2Data.positionX, player2Data.positionY, player2Data.positionZ);
    }

    void SpawnRemotePlayer(MatchInfoMessage obj){
        Debug.Log(obj.matchInfo.player1.data.name);
        Debug.Log(obj.matchInfo.player2.data.name);
        // Depending who's player 1, I need to spawn the remote player either in spawnPosition 1 or 2.
        if (obj.matchInfo.player2.data.name == ServerConnection.Instance.Connection.PlayerName){
            // i am player 2, player 1 is the remote player
            var remotePlayer = Instantiate(remotePlayerPrefab, player1Spawner.position, Quaternion.identity,
                player1Spawner);
            _remotePlayerController = remotePlayer.GetComponent<CharacterController>();
            // Do I need this if we set remote player in start?
        }
        else{
            // i am player 1, player two is the remote player
            var remotePlayer = Instantiate(remotePlayerPrefab, player2Spawner.position, Quaternion.identity,
                player2Spawner);
            _remotePlayerController = remotePlayer.GetComponent<CharacterController>();
        }
    }

    void SpawnLocalPlayer(MatchInfoMessage obj){
        if (obj.matchInfo.player2.data.name == ServerConnection.Instance.Connection.PlayerName)
            // i am player 2, player 1 is the remote player
            Instantiate(localPlayerPrefab, player2Spawner.position, Quaternion.identity, player2Spawner);
        else
            // i am player 1, player two is the remote player
            Instantiate(localPlayerPrefab, player1Spawner.position, Quaternion.identity, player1Spawner);
    }
    //spawn localPlayer in position 1 or two.
}