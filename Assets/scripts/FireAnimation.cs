using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnimation : MonoBehaviour
{

    Player player;
    Vector2 playerToFireAnimationVector;
    SpriteRenderer fireAnimation;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        fireAnimation = GetComponent<SpriteRenderer>();
        playerToFireAnimationVector = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            LockFireToPlayer();
            TriggerFire();
        }

    }

    private void TriggerFire()
    {
        var upButton = Input.GetKey(KeyCode.UpArrow);
        var wButton = Input.GetKey(KeyCode.W);

        fireAnimation.enabled = false;

        if(upButton || wButton)
        {
            fireAnimation.enabled = true;
        }
    }

    private void LockFireToPlayer()
    {
        Vector2 playerPos = player.transform.position; // might not work
        transform.position = playerPos + playerToFireAnimationVector;
    }
 
}
