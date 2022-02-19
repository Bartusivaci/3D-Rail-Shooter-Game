using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 15;

    Scoreboard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<Scoreboard>();       
    }

    void OnParticleCollision(GameObject other)
    {
        IncreaseScore();
        KillEnemy();
    }

    void IncreaseScore()
    {
        scoreBoard.UpdateScore(scorePerHit);
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    
}
