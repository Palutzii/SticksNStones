using Messages;
using TMPro;
using UnityEngine;


// TODO: put this script in the game scene and connect UI
public class Game : MonoBehaviour{
    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;
    
    void Awake(){
        Connection.Instance.matchInfoMessageReceived += OnMatchInfoMessageReceived;
    }

    void OnDestroy(){
        Connection.Instance.matchInfoMessageReceived -= OnMatchInfoMessageReceived;

    }

    void OnMatchInfoMessageReceived(MatchInfoMessage obj){
        // TODO: Update Score UIs
        Debug.Log("Update UI here");
    }
    
    public void OnClick(){
        //Connection.Instance.SendMessage(new ScoringMessage());
    }
}
