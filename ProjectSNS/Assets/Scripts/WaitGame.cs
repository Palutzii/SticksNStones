using StickNStonesShared.StickNStonesShared.Messages;
using StickNStonesShared.StickNStonesShared.Networking;
using TMPro;
using UnityEngine;


// TODO: put this script in the wait scene and connect UI
public class WaitGame : MonoBehaviour{
    public TextMeshProUGUI player1Name;
    public TextMeshProUGUI player2Name;

    void Awake(){
        ConnectionSingleton.Instance.Connection.matchInfoMessageReceived += OnMatchInfoMessageReceived;
    }

    void OnDestroy(){
        ConnectionSingleton.Instance.Connection.matchInfoMessageReceived -= OnMatchInfoMessageReceived;

    }

    void OnMatchInfoMessageReceived(MatchInfoMessage obj){
        // TODO: Update player names
        // TODO: if matchInfo.ready => switch to game scene
        Debug.Log("Update UI here");
    }
}
