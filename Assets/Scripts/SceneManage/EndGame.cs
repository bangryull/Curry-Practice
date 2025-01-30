using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void LobbyBtn()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void MainmenuBtn()
    {
        SceneManager.LoadScene("Mainmenu");
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene("game");
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
