/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShooting : Shooting
{
    [SerializeField] Animator animator; // Animator for the enemy shooting animation

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Firing() // Override the Firing method to include animation
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
            animator.SetBool("isShooting", true); // Set the shooting animation to true
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
            animator.SetBool("isShooting", false); // Set the shooting animation to false
        }
    }
}*/
// when i done on tutorial -> will re-code all of this to SOLID and OOP principles