using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Pool bulletPool;
    [SerializeField] public bool isEnemy;
    [Header("Firing State")]
    [SerializeField] public float fireRate = 0.5f; // Time in seconds between shots
    [SerializeField] public float fireLifeTime = 2f; // Time in seconds before the bullet is destroyed
    [SerializeField] public float bulletSpeed = 10f; // Speed of the bullet
    [SerializeField] public float fireVariation = 0.1f; // Variation in bullet direction for enemies
    [SerializeField] public float fireRateMin = 0.1f; // Minimum fire rate for enemies
    //Sound Effect - i should make a sound manager later
    AudioSource audioSource;
    //Checking if the player is firing and countinuous firing
    public bool isFiring;
    Coroutine firingCoroutine;

    private void Awake() // Initialize the SoundEffect component
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("SoundEffect component not found on " + gameObject.name);
        }
    }

    public void Start()
    {

        if (isEnemy)
        {
            isFiring = true; // Enemies start firing immediately
        }
    }

    void Update()
    {
        Firing();
    }

    public void OnFire(InputValue value) // This method is called when the fire button is pressed or released
    {
        isFiring = value.isPressed;
        Firing();
    }


public void Firing() // This method starts or stops the firing coroutine based on the isFiring state
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    public IEnumerator FireContinuously() // This coroutine handles the continuous firing of bullets
    {
        while (isFiring)
        {
            GameObject bullet = bulletPool.GetObject();

            bullet.transform.position = transform.position;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            rb.linearVelocity = transform.up * bulletSpeed;

            audioSource.Play();

            StartCoroutine(ReturnObjectAfterTime(bullet, fireLifeTime));

            float randomFireRate = Random.Range(fireRate - fireVariation, fireRate + fireVariation);

            randomFireRate = Mathf.Clamp(randomFireRate, fireRateMin, float.MaxValue);

            yield return new WaitForSeconds(randomFireRate);
        }
    }

    public IEnumerator ReturnObjectAfterTime(GameObject obj, float delay) // This coroutine returns the bullet to the pool after a delay
    {
        yield return new WaitForSeconds(delay);
        bulletPool.ReturnObject(obj);
    }

}
