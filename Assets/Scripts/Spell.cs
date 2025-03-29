using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] protected float LifeTime;
    [SerializeField] protected PlayerController Player;
    [SerializeField] protected Rigidbody2D RigidBody;
    [SerializeField] protected LayerMask CollisionLayers;

    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        RigidBody = GetComponent<Rigidbody2D>();
        SpellLogicStart();
        StartCoroutine(Destructor());
    }

    void Update()
    {
        SpellLogic();
    }

    virtual protected void SpellLogicStart()
    {

    }

    virtual protected void SpellLogic()
    {

    }

    virtual protected void OnCollision(GameObject collidedObject)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((CollisionLayers & (1 << collision.gameObject.layer)) > 0)
            OnCollision(collision.gameObject);
    }

    IEnumerator Destructor()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
