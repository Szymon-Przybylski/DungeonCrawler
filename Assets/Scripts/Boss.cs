using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Boss : Enemy
{

    public float summonedEnemySpeed = 2.5f;
    public float distanceFromBoss = 1.0f;
    public Transform summonedEnemy;

    private void Update()
    {
        summonedEnemy.position = transform.position +
                                 new Vector3(-Mathf.Cos(Time.time * summonedEnemySpeed) * distanceFromBoss,
                                     Mathf.Sin(Time.time * summonedEnemySpeed) * distanceFromBoss, 0);
    }
}
