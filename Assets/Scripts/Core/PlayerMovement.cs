using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float moveSpeed;
    // Bounce limits
    Vector2 minBounce;
    Vector2 maxBounce;
    [SerializeField] float bouncePadding = 0.1f; // Padding to avoid sticking to edges

    void Start()
    {
        InitBounce();
    }
    void Update()
    {
        Move();
    }

    void InitBounce()
    {
        Camera mainCamera = Camera.main;
        minBounce = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        maxBounce = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
    }

    protected void Move()
    {
        Vector3 moving = moveInput * Time.deltaTime * moveSpeed;
        moving.x = Mathf.Clamp(transform.position.x + moving.x, minBounce.x + bouncePadding, maxBounce.x - bouncePadding);
        moving.y = Mathf.Clamp(transform.position.y + moving.y, minBounce.y + bouncePadding, maxBounce.y - bouncePadding);
        transform.position = moving ;
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Move Input: " + moveInput);
    }
}
