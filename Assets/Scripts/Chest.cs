using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChestSprite;
    public int treasureValue = 5;
    
    protected override void OnCollect()
    {
        if (!Collected)
        {
            base.OnCollect();

            GetComponent<SpriteRenderer>().sprite = emptyChestSprite;
            GameManager.Manager.coins += treasureValue;
            GameManager.Manager.ShowText($"Found {treasureValue} coins!", 25,
                Color.yellow, transform.position, Vector3.up * 25, 1.5f);
        }
    }
}
