using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitPoints = 10;
    public int maxHitPoints = 10;
    public float pushRecoverySpeed = 0.1f;

    protected float ImmunityTime = 1.0f;
    protected float LastImmune;

    protected Vector3 PushDirection;

    protected virtual void ReceiveDamage(DamageDto dmg)
    {
        if (Time.time - LastImmune > ImmunityTime)
        {
            LastImmune = Time.time;
            hitPoints -= dmg.DamageAmount;
            PushDirection = (transform.position - dmg.Origin).normalized * dmg.PushForce;
            
            GameManager.Manager.ShowText(dmg.DamageAmount.ToString(), 25, Color.red,
                transform.position, Vector3.zero, 0.5f);

            if (hitPoints <= 0)
            {
                hitPoints = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {
        
    }
}
