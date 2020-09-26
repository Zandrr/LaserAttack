using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xPadding = .6f;
    [SerializeField] float yPadding = .55f;
    [SerializeField] float yPaddingTop = 9f;
    [SerializeField] float health = 200f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSFX;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.3f;


    Coroutine firingCoroutine;
    DamageDealer damageDealer;
    float deathSoundVolume = 0.7f;
    float explosionTime = 1f;


    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float laserOffset = .5f;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        GetHealth();
    }

    public float GetHealth()
    {
        return health;
    }


    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {

        while (true)
        {
            Vector2 lasPos = new Vector2(
                transform.position.x,
                transform.position.y + laserOffset);
            GameObject laser = Instantiate(
                laserPrefab,
                lasPos,
                Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>())
        {
            Die();
        }

        damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
        damageDealer.Hit();


    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetEnemyDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        var updatedTransformPosition = transform.position;
        updatedTransformPosition.z = -1;

        GameObject particleEffect = Instantiate(deathVFX, updatedTransformPosition, transform.rotation);
        Destroy(particleEffect, explosionTime);

        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
        FindObjectOfType<Level>().LoadGameOverScene();

    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);

    }

    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPaddingTop;

    }
}