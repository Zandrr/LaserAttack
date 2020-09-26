using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyHealth = 500;
    [SerializeField] GameObject evilLaserPrefab;
    [SerializeField] float MinTimeBetweenShots = 0.7f;
    [SerializeField] float MaxTimeBetweenShots = 3f;
    [SerializeField] float shotCounter;
    [SerializeField] float evilLaserSpeed = 10f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] [Range(0, 1)] float fireSFXVolume = 0.7f;
    [SerializeField] float explosionTime = 1f;
    [SerializeField] int points = 50;

    DamageDealer damageDealer;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(MinTimeBetweenShots, MaxTimeBetweenShots);
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(MinTimeBetweenShots, MaxTimeBetweenShots);

        }
    }

    private void Fire()
    {
        Vector2 evilLaserPos = new Vector2(
            transform.position.x,
            transform.position.y - 1f);

        GameObject laser = Instantiate(
            evilLaserPrefab,
            evilLaserPos,
            Quaternion.identity) as GameObject;

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -evilLaserSpeed);
        AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, fireSFXVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
        damageDealer.Hit();

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        enemyHealth -= damageDealer.GetPlayerDamage();
        if (enemyHealth <= 0)
        {
            
            Die();
        }
    }

    private void Die()
    {
        //not so good way to change position of explosion so it shows up...
        var updatedTransformPosition = transform.position;
        updatedTransformPosition.z = -1;

        gameSession.AddToScore(points);
        Destroy(gameObject);
        GameObject particleEffect = Instantiate(deathVFX, updatedTransformPosition, transform.rotation);
        Destroy(particleEffect, explosionTime);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);

    }
}