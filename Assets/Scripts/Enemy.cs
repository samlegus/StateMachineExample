using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    Animator anim;
    SpriteRenderer sr;
    public float speed;
    public float attackDistance;
    public float aggroRange;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Idle();
        //Chase();
        //Attack();
        FacePlayer();
    }

    void Idle()
    {
        anim.SetBool("attack", false);
        anim.SetBool("chase", false);
    }

    void Chase()
    {
        anim.SetBool("chase", true);
        anim.SetBool("attack", false);

        Vector3 direction;
        if(player.position.x > transform.position.x)
            direction = Vector3.right;
        else
            direction = Vector3.left;
        
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Attack()
    {
        anim.SetBool("chase", false);
        anim.SetBool("attack", true);
    }

    void FacePlayer()
    {
        sr.flipX = player.position.x < transform.position.x;
    }
}
