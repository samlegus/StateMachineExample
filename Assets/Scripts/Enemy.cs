using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public enum State
    {
        IDLE,
        CHASE,
        ATTACK
    }

    public State playerState;
    Transform player;
    Animator anim;
    SpriteRenderer sr;
    public float speed;
    public float attackDistance;
    public float aggroRange;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerState = State.IDLE;
    }

    void Update()
    {
        //Chase();
        FacePlayer();
        switch(playerState)
        {
            case State.IDLE:
                Idle();
                break;
            case State.CHASE:
                Chase();
                break;
            case State.ATTACK:
                Attack();
                break;
        }
    }

    void Idle()
    {
        anim.SetBool("attack", false);
        anim.SetBool("chase", false);

        if(Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            playerState = State.ATTACK;
            return;
        }
           
        if(Vector3.Distance(transform.position, player.position) <= aggroRange)
        {
            playerState = State.CHASE;
            return;
        }
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
        
        if(Vector3.Distance(transform.position, player.position) <= attackDistance)
            playerState = State.ATTACK;
        if(Vector3.Distance(transform.position, player.position) >= aggroRange)
            playerState = State.IDLE;
    }

    void Attack()
    {
        anim.SetBool("chase", false);
        anim.SetBool("attack", true);
        if(Vector3.Distance(transform.position, player.position) >= attackDistance)
            playerState = State.CHASE;
    }

    void FacePlayer()
    {
        sr.flipX = player.position.x < transform.position.x;
    }
}
