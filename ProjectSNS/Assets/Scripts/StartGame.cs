using System.Net;
using System.Net.Sockets;
using StickNStonesShared.StickNStonesShared.Networking;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour{
    public TMP_InputField playerNameInput;
    
    public void OnClick(){
        
        ServerConnection.Instance.Connect(playerNameInput.text);
        SceneManager.LoadScene("Wait");
    }
}
