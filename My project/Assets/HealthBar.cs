using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private Image background;
    [SerializeField] private float reduceSpeed = 2;
    private float target = 1;


    private void Awake()
    {
        background.enabled = false;
        _healthbarSprite.enabled = false;
    }

    public void UpdateHealthBar(float maxHealth, float HP)
    {
        background.enabled = true;
        _healthbarSprite.enabled = true;
        target = _healthbarSprite.fillAmount = HP / maxHealth;
    }

    private void Update()
    {
        _healthbarSprite.fillAmount = Mathf.MoveTowards(_healthbarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}
