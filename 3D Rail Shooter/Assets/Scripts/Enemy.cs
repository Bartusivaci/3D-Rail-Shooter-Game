using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFXAndSFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 5;

    Scoreboard scoreBoard;
    GameObject parent;

    void Start()
    {
        AddRigidbody();
        scoreBoard = FindObjectOfType<Scoreboard>();
        parent = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if(hitPoints < 1)
        {
            KillEnemy();
        }
        
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        hitPoints--;
        scoreBoard.UpdateScore(scorePerHit);
    }

    void KillEnemy()
    {
        GameObject fx = Instantiate(deathVFXAndSFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent.transform;
        Destroy(gameObject);
    }

    
}
