using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem deathVFX;

    bool isDead = false;

    void OnTriggerEnter(Collider other)
    {
        ProcessDeath();
    }

    void ProcessDeath()
    {
        if(isDead) { return; }
        GetComponent<PlayerController>().enabled = false;
        deathVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        isDead = true;
        Invoke("ReloadLevel", loadDelay);

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
