using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Mathf.Clamp(score, 0, int.MaxValue); // Ensure score does not go below 0
        Debug.Log("Score: " + score);
    }
}
