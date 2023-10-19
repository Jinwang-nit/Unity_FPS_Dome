using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTime;
    public float maxHealth = 100f;
    [Header("Health")]
    public float chipspeed = 2f; 
    public Image frontHealth;
    public Image backHealth;

    [Header("Damage")]
    public Image overlay;
    public float durtion; // 不完全透明时间
    public float fadespeed; // 计时器
    private float durationTime;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            if (health < 30) return;

            durationTime += Time.deltaTime;
            if (durationTime > durtion)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadespeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        // 填充度 是上一帧的
        float fillF = frontHealth.fillAmount;
        float fillB = backHealth.fillAmount; 
        // health 是当前帧的
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealth.fillAmount = hFraction;
            backHealth.color = Color.red;
            lerpTime += Time.deltaTime;
            float percentComplete = lerpTime / chipspeed;
            percentComplete = percentComplete * percentComplete;
            backHealth.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealth.color = Color.green;
            backHealth.fillAmount = hFraction;
            lerpTime += Time.deltaTime;
            float percentComplete = lerpTime / chipspeed;
            percentComplete = percentComplete * percentComplete;
            frontHealth.fillAmount = Mathf.Lerp(fillF, backHealth.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTime = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.2f);
        durationTime = 0;
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTime = 0f;
    }
}
