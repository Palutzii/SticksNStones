using StickNStonesShared.StickNStonesShared.Messages;
using SticksNStonesServer.Model;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public PlayerData player1;
    [SerializeField] public PlayerData player2;
    public MatchInfoMessage matchinfo;

    void Awake(){
        DontDestroyOnLoad(this);
    }
}