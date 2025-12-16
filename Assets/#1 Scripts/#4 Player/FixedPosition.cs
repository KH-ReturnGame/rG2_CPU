using System;
using UnityEngine;

public class FixedPosition : MonoBehaviour
{
    
    private void Update()
    {
        transform.position = transform.parent.position;
    }
}