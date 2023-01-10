using UnityEngine;
using DG.Tweening;

public enum MoveType
{
    ToPosition,
    ByAmount
}

namespace Aezakmi.Tweens
{
    public class Move : TweenBase
    {
        [Header("Move Tween Settings")]
        [SerializeField] private MoveType _moveType;
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _moveAmount;

        private Vector3 _targetPosition;
        private bool _isRelative;

        protected override void SetTweener()
        {
            SetTargetPosition();

            Tweener = transform
                .DOLocalMove(_targetPosition, LoopDuration)
                .SetLoops(LoopCount, LoopType)
                .SetEase(LoopEase)
                .SetDelay(Delay)
                .SetRelative(_isRelative);
        }

        private void SetTargetPosition()
        {
            if (_moveType == MoveType.ToPosition)
            {
                _targetPosition = _position;
                _isRelative = false;
            }
            else if (_moveType == MoveType.ByAmount)
            {
                _targetPosition = _moveAmount;
                _isRelative = true;
            }
        }
    }
}