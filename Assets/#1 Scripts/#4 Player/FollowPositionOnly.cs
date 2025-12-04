using UnityEngine;
public class FollowPositionOnly : MonoBehaviour
{
    private Transform parentTransform;
    private Quaternion originalRotation;

    void Start()
    {
        parentTransform = transform.parent;
        originalRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // 부모의 위치만 따라가고 회전은 원래대로 유지
        if (parentTransform != null)
        {
            transform.position = parentTransform.position;
            transform.rotation = originalRotation;
        }
    }
}