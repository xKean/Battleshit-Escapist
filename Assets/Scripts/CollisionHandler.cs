using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem explosionParticles;
    PlayerControls playerControls;
    MeshRenderer playerMeshRenderer;
    BoxCollider playerBoxCollider;



    void Start()
    {
        playerControls = GetComponent<PlayerControls>();
        playerMeshRenderer = GetComponent<MeshRenderer>();
        playerBoxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        playerControls.enabled = false;
        explosionParticles.Play();
        playerMeshRenderer.enabled = false;
        playerBoxCollider.enabled = false;
        Invoke("ReloadScene", loadDelay);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
