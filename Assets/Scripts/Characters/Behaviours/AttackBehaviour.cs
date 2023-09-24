using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Characters.Behaviours
{
    public class AttackBehaviour : MonoBehaviour, IAttackable
    {
        [SerializeField]
        private Transform handPoint;
    
        [SerializeField]
        private Transform weapon;
    
        [SerializeField]
        private TrailRenderer slashEffect;
    
        [SerializeField]
        private Transform hitPoint;
    
        [SerializeField]
        private LayerMask layerMask;
    
        private CharacterSettings _settings;
        private Transform _lookAt;

        private bool _isAnimation;
        private bool _reversedAttack;

        public void UpdateSettings(CharacterSettings settings)
        {
            _settings = settings;
        }

        private void Update()
        {
            if (!_isAnimation && _lookAt != null)
                CalculateHandDirection();
        }

        public void Attack()
        {
            if (_isAnimation)
                return;
        
            SwordAnimation();
        
            _reversedAttack = !_reversedAttack;
        }

        public void SetTarget(Transform target)
        {
            _lookAt = target;
        }
    
        private void CalculateHandDirection()
        {
            var direction = GetDirectionToTarget();
            handPoint.up = direction;
        }

        private Vector2 GetDirectionToTarget()
        {
            var targetPosition = _lookAt.position;
            var handPosition = transform.position;
            var direction = new Vector2(handPosition.x - targetPosition.x, handPosition.y - targetPosition.y);

            return direction;
        }

        private void SwordAnimation()
        {
            _isAnimation = true;

            var direction = _reversedAttack ? -1 : 1;
            var newRotation = new Vector3(0, 0, 180 * direction);

            StartCoroutine(StartHitting());

            handPoint.DOLocalRotate(newRotation, _settings.attackTime, RotateMode.FastBeyond360)
                .SetEase(Ease.InCubic)
                .SetRelative(true)
                .OnStart(() => slashEffect.emitting = true)
                .OnComplete(() =>
                {
                    slashEffect.emitting = false;
                    _isAnimation = false;
                });
        
            var newWeaponRotation = new Vector3(0, 0, 100 * direction);
            weapon.DOLocalRotate(newWeaponRotation, _settings.attackTime, RotateMode.FastBeyond360)
                .SetEase(Ease.InCubic)
                .SetRelative(true);
        }

        private IEnumerator StartHitting()
        {
            yield return new WaitForSeconds(_settings.attackTime / 1.5f);
        
            slashEffect.emitting = true;
            var results = Physics2D.OverlapCircleAll(hitPoint.position, _settings.range, layerMask);

            foreach (var result in results)
            {
                var hittable = result.transform.GetComponent<IHittable>();
                if (hittable == null || hittable.Handler == transform)
                    continue;
            
                hittable.Hit(_settings.damage);
            }

            yield return new WaitForSeconds(_settings.attackTime / 3f);
        }
    }
}