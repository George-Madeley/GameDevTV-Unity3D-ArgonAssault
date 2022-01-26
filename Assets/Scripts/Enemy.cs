using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Particle System to play when the enemy is destroyed")]
    [SerializeField] GameObject deathVFX;
    [Tooltip("Parent object for the particle systems")]
    [SerializeField] Transform parentObject;

    private void OnParticleCollision(GameObject other) {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentObject;
        Destroy(gameObject);
    }
}
