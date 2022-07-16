using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;

    public void UpdateHealthBar(float maxHealth, float HP)
    {
        _healthbarSprite.fillAmount = HP / maxHealth;
    }


}
