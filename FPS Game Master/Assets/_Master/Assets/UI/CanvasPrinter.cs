using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasPrinter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoAmmount;
    [SerializeField] private TextMeshProUGUI maxAmmoAmmount;
    [SerializeField] private Image healthBar;
    [SerializeField] private bool isActive;
    [SerializeField] private Image damagePanel;

    private float currentHealthInBar;
    private void Start()
    {
        StartCoroutine(UpdateInfo());
    }

    private IEnumerator UpdateInfo()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(.1f);
            currentHealthInBar = (float)PlayerInfo.PlayerCurrentHealth/ (float)PlayerInfo.PlayerMaxHealth;
            healthBar.fillAmount = currentHealthInBar;

            ammoAmmount.text = PlayerInfo.PlayerAmmo.ToString();
            maxAmmoAmmount.text = PlayerInfo.PlayerMaxAmmo.ToString();
            var color = damagePanel.color;
            var alpha = 1f - (float)PlayerInfo.PlayerCurrentHealth / (float)PlayerInfo.PlayerMaxHealth;
            damagePanel.color = new Color(color.r, color.g, color.b, Mathf.Clamp(alpha,0,1));
        }

    }
}
