using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : Collidable
{
    public string message;
    private float _messageCooldown = 3.0f;
    private float _lastMessage;

    private Vector3 _position;

    protected override void Start()
    {
        base.Start();
        _lastMessage = -_messageCooldown;
    }

    protected override void OnCollide(Collider2D col)
    {
        if (Time.time - _lastMessage > _messageCooldown)
        {
            _position = new Vector3(transform.position.x,
                transform.position.y + (float)BoxCollider2D.bounds.extents.y, transform.position.z);
            
            GameManager.Manager.ShowText(message, 25, Color.white, _position, Vector3.zero, _messageCooldown - 2.0f);
            _lastMessage = Time.time;
        }
    }
}
