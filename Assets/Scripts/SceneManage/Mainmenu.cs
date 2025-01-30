using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("game");
    }
    public void LobbyBtn()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void QuitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
