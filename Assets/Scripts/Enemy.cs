using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Moving
{
    public int experience = 1;

    public float triggerLength = 0.3f;
    public float chaseLength = 1.0f;
    private bool _chasing;
    private bool _collidingWithPlayer;
    private Transform _playerTransform;
    private Vector3 _startingPosition;

    private BoxCollider2D _hitbox;
    private Collider2D[] _hits = new Collider2D[10];
    public ContactFilter2D filter2D;

    protected override void Start()
    {
        base.Start();
        _playerTransform = GameManager.Manager.player.transform;
        _startingPosition = transform.position;
        _hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(_playerTransform.position, _startingPosition) < chaseLength)
        {
            if (Vector3.Distance(_playerTransform.position, _startingPosition) < triggerLength)
            {
                _chasing = true;
            }
            
            if (_chasing)
            {
                if (!_collidingWithPlayer)
                {
                    UpdateMotor((_playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(_startingPosition - transform.position);
            }
        }
        else
        {
           UpdateMotor(_startingPosition - transform.position);
           _chasing = false;
        }

        _collidingWithPlayer = false;
        _hitbox.OverlapCollider(filter2D, _hits);
        for (int i = 0; i < _hits.Length; i++)
        {
            if (_hits[i] == null)
            {
                continue;
            }

            if (_hits[i].CompareTag("Fighter") && _hits[i].name == "Player")
            {
                _collidingWithPlayer = true;
            }

            _hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.Manager.GrantExperience(experience);
        GameManager.Manager.ShowText($"+{experience} experience", 30,
            Color.magenta, transform.position, Vector3.up, 1.0f);
    }
}
