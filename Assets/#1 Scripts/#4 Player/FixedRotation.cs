using System;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    void FixedUpdate() {
        transform.localRotation = Quaternion.identity;
    }
}