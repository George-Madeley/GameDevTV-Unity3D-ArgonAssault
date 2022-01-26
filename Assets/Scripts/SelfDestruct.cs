using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [Tooltip("Amount of time until destruction after instatiation")]
    [SerializeField] float timeTiillDestroy = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeTiillDestroy);
    }
}
