using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private Image HealthImage;
    [SerializeField]
    private TextMeshProUGUI HPText;
    [SerializeField]
    private Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.onTakeDamage.AddListener(PlayerDamaged);
        UpdateHPText();
    }

    public void PlayerDamaged(float damage)
    {
        HealthImage.fillAmount -= damage / player.MaxHP;
        UpdateHPText();
    }

    private void UpdateHPText()
    {
        HPText.text = " " + player.HP.ToString() + "/" + player.MaxHP.ToString();
    }
}
