using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Collision Settings")]
    [Tooltip("How long to wait after a collision has occured before restarting the level")]
    [SerializeField] float restartDelay = 1f;
    [Tooltip("The Particle System to play when a collision occurs")]
    [SerializeField] ParticleSystem crashParticles;

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"{this.name}**Triggered by**{other.gameObject.name}");
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        crashParticles.Play();
        turnOffRendererInChildren();
        GetComponent<movement>().enabled = false;
        Invoke("RestartLevel", restartDelay);
    }

    private void turnOffRendererInChildren()
    {
        foreach(Renderer childRenderer in GetComponentsInChildren<Renderer>()) {
            childRenderer.enabled = false;
            break;
        }
    }

    private void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
