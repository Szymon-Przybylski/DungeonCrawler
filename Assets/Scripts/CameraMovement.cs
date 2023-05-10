using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform _focusPoint;
    
    public float boundaryX = 0.15f;
    public float boundaryY = 0.05f;

    private void Start()
    {
        _focusPoint = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;
        
        float deltaX = _focusPoint.position.x - transform.position.x;
        if (deltaX > boundaryX || deltaX < -boundaryX)
        {
            delta.x = transform.position.x < _focusPoint.position.x ? deltaX - boundaryX : deltaX + boundaryX;
        }
        float deltaY = _focusPoint.position.y - transform.position.y;
        if (deltaY > boundaryY || deltaY < -boundaryY)
        {
            delta.y = transform.position.y < _focusPoint.position.y ? deltaY - boundaryY : deltaY + boundaryY;
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
