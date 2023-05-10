using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{
    // Start is called before the first frame update
    public int healingAmount = 1;
    private float _healingCooldown = 1.0f;
    private float _lastHealed;

    protected override void OnCollide(Collider2D col)
    {
        if (col.name != "Player")
        {
            return;
        }
        
        if (Time.time - _lastHealed > _healingCooldown)
        {
            _lastHealed = Time.time;
            GameManager.Manager.player.Heal(healingAmount);
        }
    }
}
