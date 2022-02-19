using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    int score;

    public void UpdateScore(int amountToIncrease)
    {
        score += amountToIncrease;
        Debug.Log($"Score : {score}");
    }
}
