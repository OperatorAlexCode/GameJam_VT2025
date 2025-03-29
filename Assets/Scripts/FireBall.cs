using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Spell
{
    [SerializeField] int Damage = 1;
    [SerializeField] float Velocity = 1;

    protected override void SpellLogicStart()
    {
        this.RigidBody.AddForce(transform.up * Velocity,ForceMode2D.Impulse);
    }

    protected override void OnCollision(GameObject collidedObject)
    {
        if (collidedObject.tag == "Enemy")
            collidedObject.GetComponent<Enemy>().TakeDamage(Damage);

        Destroy(gameObject);
    }
}
