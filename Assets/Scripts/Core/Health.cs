using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth;
    [Header("Camera Shake Settings")]
    [SerializeField] ParticleSystem hitEffect;
    // Camera Shaking when get hit
    [SerializeField] bool IsCameraShake;
    CameraShake cameraShake;
    GameObject tagObject;
    // Score Keeper
    [Header("Score Settings")]
    Score scoreKeeper;
    [SerializeField] public bool isPlayer;
    [SerializeField] public int scoreValue;
    DamageDealer damageDealer;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake == null)
        {
            Debug.LogWarning("CameraShake not found in the scene.");
        }
        scoreKeeper = FindAnyObjectByType<Score>();
    }

    void Start()
    {
        GetHealth();
    }

    private int GetHealth()
    {
        return maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            if(collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(collision.gameObject);
            }
            TakeDamage(damageDealer.GetDamageAmount());
            ShakeCamera();
            PlayHitEffect();

        }
    }

    private void TakeDamage(int damage)
    {
        maxHealth -= damage;
        if (maxHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(!isPlayer)
        {
            scoreKeeper.AddScore(scoreValue);
        }
        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }
    }

    void ShakeCamera()
    {
        if (IsCameraShake && cameraShake != null)
        {
            cameraShake.GetPlay();
        }
    }
}

