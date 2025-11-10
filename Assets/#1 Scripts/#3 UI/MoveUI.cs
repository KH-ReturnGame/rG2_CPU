using DependencyInjection;
using DG.Tweening;
using UnityEngine;

class MoveUI : MonoBehaviour, IDependencyProvider
{
    [Provide]
    public MoveUI ProvideMoveUI()
    {
        return this;
    }

    public void Move(Transform target, GameObject obj, float duration, Ease ease)
    {
        obj.transform.DOMove(target.position, duration)
            .SetEase(ease);
    }
}