using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other) {
        Destroy(gameObject);
    }
}
