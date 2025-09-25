using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damageAmount;

    public int GetDamageAmount()
    {
        return damageAmount;
    }

}
