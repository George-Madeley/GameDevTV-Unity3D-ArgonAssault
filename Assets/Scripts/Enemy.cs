using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Particle System to play when the enemy is destroyed")]
    [SerializeField] GameObject deathVFX;
    [Tooltip("Parent object for the particle systems")]
    [SerializeField] Transform parentObject;
    [Tooltip("Score per hit")]
    [SerializeField] int scorePerHit = 10;

    ScoreBoard scoreBoard;

    private void Start() {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemy();
    }

    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentObject;
        Destroy(gameObject);
    }

    private void ProcessHit()
    {
        scoreBoard.IncreaseScore(scorePerHit);
    }
}
