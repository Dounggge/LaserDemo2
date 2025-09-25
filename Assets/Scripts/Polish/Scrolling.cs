using UnityEngine;

public class Scrolling : MonoBehaviour
{
    [SerializeField] Vector2 scrollSpeed;

    Vector2 offset;
    Material material;
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }
}
