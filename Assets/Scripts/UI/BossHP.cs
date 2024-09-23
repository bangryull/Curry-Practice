using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHP : MonoBehaviour
{
    [SerializeField]
    private Image HealthImage;
    [SerializeField]
    private TextMeshProUGUI HPText;
    [SerializeField]
    private Boss boss;

    private void Start()
    {
        boss = GameObject.FindWithTag("Boss").GetComponent<Boss>();
        boss.onTakeDamage.AddListener(BossDamaged);
        UpdateHPText();
    }

    public void BossDamaged(float damage)
    {
        HealthImage.fillAmount -= damage / boss.MaxHP;
        UpdateHPText();
    }

    private void UpdateHPText()
    {
        HPText.text = " " + boss.HP.ToString() + "/" + boss.MaxHP.ToString();
    }
}
