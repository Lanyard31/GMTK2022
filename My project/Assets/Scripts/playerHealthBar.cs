using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private Image background;
    [SerializeField] private float reduceSpeed = 2;
    private float target = 1;


    public void UpdateHealthBar(float maxHealth, float playerHP)
    {
        background.enabled = true;
        _healthbarSprite.enabled = true;
        target = _healthbarSprite.fillAmount = playerHP / maxHealth;
        Debug.Log(target);
    }
    
    private void Update()
    {

        _healthbarSprite.fillAmount = Mathf.MoveTowards(_healthbarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}