using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter2D;
    protected BoxCollider2D BoxCollider2D;
    private Collider2D[] _hits = new Collider2D[10];
 
    protected virtual void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    protected virtual void Update()
    {
        BoxCollider2D.OverlapCollider(filter2D, _hits);

        for (int i = 0; i < _hits.Length; i++)
        {
            if (_hits[i] == null)
            {
                continue;
            }
            
            OnCollide(_hits[i]);

            _hits[i] = null;
        }
        
    }

    protected virtual void OnCollide(Collider2D col)
    {
        Debug.Log($"OnCollide was not implemented in {this.name}");
    }
}
