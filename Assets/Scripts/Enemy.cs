using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    [SerializeField] int Health = 10;
    [SerializeField] float Speed;
    [SerializeField] int Damage = 1;
    [SerializeField] Rigidbody2D Rigidbody;
    [SerializeField] PlayerController Player;
    [SerializeField] Animator Animator;
    bool Dead;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Player != null && !Dead)
        {
            Vector3 playerDir = Player.transform.position - transform.position;
            playerDir = playerDir.normalized;

            //Rigidbody.AddForce(playerDir * Time.deltaTime, ForceMode2D.Impulse);
            transform.position += (Vector3)playerDir * Time.deltaTime;

            Animator.SetFloat("MoveDirX", playerDir.x);
            Animator.SetFloat("MoveDirY", playerDir.y);
        }
    }

    // Applies x damage to enemy
    public void TakeDamage(int dmg)
    {
        Health -= dmg;

        if (Health <= 0)
        {
            Dead = true;
            Animator.SetTrigger("Death");
            Rigidbody.velocity = Vector2.zero;
            Destroy(gameObject, 2.24f);
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
