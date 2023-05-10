using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int[] damageAmount;
    public float[] pushForce;

    public int weaponLevel = 0;
    private SpriteRenderer _spriteRenderer;

    private Animator _animator;
    private float _attackCooldown = 0.5f;
    private float _lastSwing;
    private static readonly int Swing1 = Animator.StringToHash("Swing");

    public Weapon()
    {
        damageAmount = new[] {1,2,4,5,7};
        pushForce = new[] {4.0f, 4.0f, 4.5f, 4.5f, 5.0f};
    }

    protected void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - _lastSwing > _attackCooldown)
            {
                _lastSwing = Time.time;
                Swing();
            }
        }
    }

    private void Swing()
    {
        _animator.SetTrigger(Swing1);
    }

    protected override void OnCollide(Collider2D col)
    {
        if (col.CompareTag("Fighter"))
        {
            if (col.name != "Player")
            {
                DamageDto damage = new DamageDto()
                {
                    DamageAmount = damageAmount[weaponLevel],
                    Origin = transform.position,
                    PushForce = pushForce[weaponLevel]
                };
                
                col.SendMessage("ReceiveDamage", damage);
            }
        }
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        _spriteRenderer.sprite = GameManager.Manager.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevelAndSprite(int level)
    {
        weaponLevel = level;
        _spriteRenderer.sprite = GameManager.Manager.weaponSprites[weaponLevel];
    }
}
