using Networking;
using StickNStonesShared.StickNStonesShared.Messages;
using TMPro;
using UnityEngine;


// TODO: put this script in the wait scene and connect UI
public class WaitGame : MonoBehaviour{
    public TextMeshProUGUI player1Name;
    public TextMeshProUGUI player2Name;

    MatchInfoMessage _pendingMessage;

    void Start(){
        ServerConnection.Instance.Broker.Subscribe<MatchInfoMessage>(OnMatchInfoReceived);
    }

    void OnDestroy(){
        ServerConnection.Instance.Broker.UnSubscribe<MatchInfoMessage>(OnMatchInfoReceived);
    }

    void Update(){
        if (_pendingMessage != null){
            //Consume message
            var message = _pendingMessage;
            _pendingMessage = null;
            if (message.matchInfo.player1?.name != null){
                player1Name.text = message.matchInfo.player1.name;
            }
            if (message.matchInfo.player2?.name != null){
                player2Name.text = message.matchInfo.player2.name;
            }
        }
    }

    void OnMatchInfoReceived(MatchInfoMessage message){
        //Produce message
        _pendingMessage = message;
        
    }
}
