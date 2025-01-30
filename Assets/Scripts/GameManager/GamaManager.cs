using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        player.playerDie.AddListener(playerDied);
        boss.bossDie.AddListener(bossDied);
    }

    void playerDied()
    {
        GameObject.Find("Canvas").transform.Find("YouLose").gameObject.SetActive(true);
    }

    void bossDied()
    {
        GameObject.Find("Canvas").transform.Find("YouWin").gameObject.SetActive(true);
    }
}
