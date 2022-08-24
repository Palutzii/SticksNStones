using Networking;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public static string LocalPlayerName;
    public TMP_InputField playerNameInput;

    public void OnClick(){
        ServerConnection.Instance.Connect(playerNameInput.text);
        SceneManager.LoadScene("Wait");
    }
}