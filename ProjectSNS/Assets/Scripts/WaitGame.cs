using Networking;
using StickNStonesShared.StickNStonesShared.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


// TODO: put this script in the wait scene and connect UI
public class WaitGame : MonoBehaviour{
    public TextMeshProUGUI player1Name;
    public TextMeshProUGUI player2Name;

    void Start(){
        ServerConnection.Instance.Broker.Subscribe<MatchInfoMessage>(OnMatchInfoReceived);
    }

    void OnDestroy(){
        ServerConnection.Instance.Broker.UnSubscribe<MatchInfoMessage>(OnMatchInfoReceived);
    }
    
    
    void OnMatchInfoReceived(MatchInfoMessage message){
        if (!string.IsNullOrEmpty(message.matchInfo.player1.data.name)){
            player1Name.text = message.matchInfo.player1.data.name;
        }
        if (!string.IsNullOrEmpty(message.matchInfo.player2.data.name)){
            player2Name.text = message.matchInfo.player2.data.name;
        }

        if (message.matchInfo.isStarted){
            SceneManager.LoadScene("Game");
        }
    }
}
