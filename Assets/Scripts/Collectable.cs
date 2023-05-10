using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    protected bool Collected;

    protected override void OnCollide(Collider2D col)
    {
        if (col.name == "Player")
        {
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        Collected = true;
    }
}
