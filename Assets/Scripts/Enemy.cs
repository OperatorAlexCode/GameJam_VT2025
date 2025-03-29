using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int Health = 10;
    [SerializeField] float Speed;
    [SerializeField] int Damage = 1;
    [SerializeField] Rigidbody2D Rigidbody;
    [SerializeField] PlayerController Player;
    [SerializeField] Animator Animator;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Player != null)
        {
            Vector3 playerDir = Player.transform.position - transform.position;
            playerDir = playerDir.normalized;

            Rigidbody.AddForce(playerDir*Time.deltaTime, ForceMode2D.Impulse);

            Animator.SetFloat("MoveDirX", playerDir.x);
            Animator.SetFloat("MoveDirY", playerDir.y);
        }
    }

    // Applies x damage to enemy
    public void TakeDamage(int dmg)
    {
        Health -= dmg;

        if (Health <= 0)
            Destroy(gameObject);
    }
}
