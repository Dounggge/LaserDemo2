using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// This script is for a camera shake effect in Unity.

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeMagnitude = 0.5f;

    Camera mainCamera;
    Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    public void GetPlay()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = originalPosition; // Reset to original position
    }

}
