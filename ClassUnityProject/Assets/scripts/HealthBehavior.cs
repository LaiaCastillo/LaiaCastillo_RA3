using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private float health = 5f;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float flashDuration = 4f;
    [SerializeField] private float damageCooldown = 0.3f;
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private Image healthImg;

    private float lastDamageTime = -Mathf.Infinity;
    private Color originalColor;
    public event Action<bool> GameOverEvent = delegate { };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        try
        {
            GameObject obj = GameObject.FindGameObjectWithTag("HealthImg");
            Debug.Log(obj);
            if (obj != null)
            {
                Debug.Log(obj);
                healthImg = obj.GetComponent<UnityEngine.UI.Image>();
            }
            GetComponent<Player>();
            isPlayer = true;
        }
        catch
        {
            isPlayer = false;
        }
    }
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        originalColor = _sr.color;
        
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject obj = GameObject.FindGameObjectWithTag("HealthImg");
        Debug.Log(obj);
        if (obj != null)
        {
            Debug.Log(obj);
            healthImg = obj.GetComponentInChildren<UnityEngine.UI.Image>();
        }
    }



    // Update is called once per frame
    void Update()
    {
    }
    

    public void Heal(float amount)
    {
        health += amount;
        if (health > 5)
        {
            health = 5;
        }
        Debug.Log("You just heal your self: " + amount + " of health");
        if (isPlayer)
        {
            ChangeImg();
        }
        
    }
    public void Hurt(float amount)
    {
        Debug.Log("You just got hurt");
        
        
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            health -= amount;
            lastDamageTime = Time.time;

            FlashDamageColor();
        }
        if (health <= 0)
        {
            Die();
        }
        if (isPlayer)
        {
            ChangeImg();
        }
    }
    private void ChangeImg()
    {
        healthImg.sprite = Resources.Load<Sprite>("health" + health);

        
    }
    private void FlashDamageColor()
    {
        _sr.color = damageColor;
        Invoke(nameof(ResetColor), flashDuration);
    }
    private void ResetColor()
    {
        _sr.color = originalColor;
    }

    public void Die()
    {
        health = 0;
        ChangeImg();
        Destroy(gameObject);
        if (isPlayer)
        {
            GameOverEvent.Invoke(false);
        }
        
    }

}
