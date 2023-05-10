using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Input = UnityEngine.Windows.Input;

public abstract class Moving : Fighter
{
    protected BoxCollider2D BoxCollider2D;
    
    protected RaycastHit2D Hit;

    private Vector3 _originalSize;

    public float xSpeed = 1.0f;
    public float ySpeed = 1.0f;
    
    protected virtual void Start()
    {
        _originalSize = transform.localScale;
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        input = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        
        transform.localScale = input.x switch
        {
            > 0 => _originalSize,
            < 0 => new Vector3(_originalSize.x * -1, _originalSize.y, _originalSize.z),
            _ => transform.localScale
        };

        input += PushDirection;
        PushDirection = Vector3.Lerp(PushDirection, Vector3.zero, pushRecoverySpeed);
        
        Hit = Physics2D.BoxCast(transform.position, BoxCollider2D.size, 0, new Vector2(input.x, 0),
            Mathf.Abs(input.x * Time.deltaTime), LayerMask.GetMask("Actors", "Blocks"));
        if (Hit.collider == null)
        {
            transform.Translate(input.x *Time.deltaTime, 0, 0);
        }

        Hit = Physics2D.BoxCast(transform.position, BoxCollider2D.size, 0, new Vector2(0, input.y),
            Mathf.Abs(input.y * Time.deltaTime), LayerMask.GetMask("Actors", "Blocks"));
        if (Hit.collider == null)
        {
            transform.Translate(0, input.y *Time.deltaTime, 0);
        }
    }
}
