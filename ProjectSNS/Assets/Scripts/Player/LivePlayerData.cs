using SticksNStonesServer.Model;
using UnityEngine;

public class LivePlayerData : MonoBehaviour
{
    PlayerData _playerData;

    void Start(){
        transform.position = new Vector3(_playerData.positionX, _playerData.positionY, _playerData.positionZ);
    }
}