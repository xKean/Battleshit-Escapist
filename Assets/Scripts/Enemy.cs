using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int enemyHP = 45;
    ScoreBoard scoreBoard;
    PlayerControls playerControls;
    GameObject parentGameObject;


    void Start()
    {

        scoreBoard = FindObjectOfType<ScoreBoard>();
        playerControls = FindObjectOfType<PlayerControls>();
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");

    }
    void OnParticleCollision(GameObject other)
    {

        int playerdamage = playerControls.playerdamage;
        ProcessHit(playerdamage);
        if (this.enemyHP <= 0)
        {
            KillEnemy();
        }

    }

    void ProcessHit(int playerdamage)
    {
        this.enemyHP -= playerdamage;
        scoreBoard.IncreaseScore(playerdamage);
        GameObject vfx = Instantiate(hitVFX, this.transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;

    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, this.transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(this.gameObject);
    }


}
