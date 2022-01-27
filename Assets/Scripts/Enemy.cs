using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Particle System to play when the enemy is destroyed")]
    [SerializeField] GameObject deathVFX;
    [Tooltip("Particle System fto play when the enemy has been hit")]
    [SerializeField] GameObject hitVFX;
    [Tooltip("Score per hit")]
    [SerializeField] int scorePerHit = 10;
    [Tooltip("Enemy HP")]
    [SerializeField] int enemyHP = 3;

    ScoreBoard scoreBoard;
    GameObject parentObject;

    private void Start() {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentObject = GameObject.FindWithTag("RuntimeInstantiation");
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (IsEnemyDead())
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        IncreaseScore(1);
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        if(vfx == null) {
            Debug.Log("VFX Null)");
        }
        if(parentObject == null) {
            Debug.Log(("Parent Null"));
        }
        vfx.transform.parent = parentObject.transform;
        DecreaseHealth();
    }

    private void KillEnemy()
    {
        IncreaseScore(5);
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentObject.transform;
        Destroy(gameObject);
    }

    private void IncreaseScore(int multiplier)
    {
        scoreBoard.IncreaseScore(scorePerHit * multiplier);
    }

    private void DecreaseHealth()
    {
        enemyHP--;
    }

    private bool IsEnemyDead()
    {
        return enemyHP <= 0;
    }
}
