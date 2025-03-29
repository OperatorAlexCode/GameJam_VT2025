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
    bool Stunned;

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
            if (Player.IsDead)
            {
                Destroy(gameObject);
            }

            else if (!Dead && !Stunned)
            {
                Vector3 playerDir = Player.transform.position - transform.position;
                playerDir = playerDir.normalized;

                //Rigidbody.AddForce(playerDir * Time.deltaTime, ForceMode2D.Impulse);
                transform.position += playerDir * Time.deltaTime;

                Animator.SetFloat("MoveDirX", playerDir.x);
                Animator.SetFloat("MoveDirY", playerDir.y);
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        Health -= dmg;

        if (Health <= 0)
        {
            Dead = true;
            Animator.SetTrigger("Death");
            Rigidbody.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
        }

        else
        {
            StartCoroutine(SpriteFlicker());
        }
    }

    IEnumerator SpriteFlicker()
    {
        Color startingColor = GetComponent<SpriteRenderer>().color;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.2f);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().color = startingColor;

    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Player.TakeDamage(Damage);
    }
}
