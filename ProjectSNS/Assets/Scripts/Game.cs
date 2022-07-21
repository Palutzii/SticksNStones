using Networking;
using StickNStonesShared.StickNStonesShared.Messages;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


// TODO: put this script in the wait scene and connect UI
public class Game : MonoBehaviour{
    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;

    void Start(){
        ServerConnection.Instance.Broker.Subscribe<MatchInfoMessage>(OnMatchInfoReceived);
    }

    void OnDestroy(){
        ServerConnection.Instance.Broker.UnSubscribe<MatchInfoMessage>(OnMatchInfoReceived);
    }

    public void OnScoreGained(){
        ServerConnection.Instance.Connection.SendMessage(new GainCoinMessage());
    }
    
    void OnMatchInfoReceived(MatchInfoMessage message){
            player1Score.text = message.matchInfo.player1.data.score.ToString();
            player1Score.text = message.matchInfo.player2.data.score.ToString();
    }
}
