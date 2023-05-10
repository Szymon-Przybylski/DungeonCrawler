using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Moving
{
    private SpriteRenderer _spriteRenderer;

    private bool _isAlive = true;
    
    private static readonly int Show = Animator.StringToHash("Show");

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ReceiveDamage(DamageDto dmg)
    {
        if (!_isAlive)
        {
            return;
        }
        base.ReceiveDamage(dmg);
        GameManager.Manager.OnHitPointChange();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (_isAlive)
        {
            UpdateMotor(new Vector3(x, y, 0));
        }
    }

    public void SwapSprite(int spriteId)
    {
        _spriteRenderer.sprite = GameManager.Manager.playerSprites[spriteId];
    }

    public void OnLevelUp()
    {
        maxHitPoints += 5;
        hitPoints = maxHitPoints;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

    public void Heal(int healingAmount)
    {
        if (hitPoints == maxHitPoints)
        {
            return;
        }

        hitPoints += healingAmount;
        if (hitPoints > maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }
        GameManager.Manager.ShowText($"+ {healingAmount} hp", 25, Color.green, transform.position, Vector3.up, 1.0f);
        GameManager.Manager.OnHitPointChange();
    }

    protected override void Death()
    {
        _isAlive = false;
        GameManager.Manager.deathMenuAnimator.SetTrigger(Show);
    }

    public void Respawn()
    {
        _isAlive = true;
        Heal(maxHitPoints);
        LastImmune = Time.time;
        PushDirection = Vector3.zero;
    }
}
