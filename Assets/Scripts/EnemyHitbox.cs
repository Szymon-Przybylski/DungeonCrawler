using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damageAmount = 1;
    public float pushForce = 5.0f;

    protected override void OnCollide(Collider2D col)
    {
        if (col.CompareTag("Fighter") && col.name == "Player")
        {
            DamageDto damage = new DamageDto()
            {
                DamageAmount = damageAmount,
                Origin = transform.position,
                PushForce = pushForce
            };
                
            col.SendMessage("ReceiveDamage", damage);
        }
    }
}
