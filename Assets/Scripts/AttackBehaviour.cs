using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField]
    private float attackTime = 0.3f;
    
    private Camera _mainCamera;
    private CharacterSettings _settings;

    private bool _isAnimation;
    private bool _reversedAttack;
    
    private void Start()
    {
        _mainCamera = GameObject
            .FindWithTag("MainCamera")
            .GetComponent<Camera>();
    }
    
    public void UpdateSettings(CharacterSettings settings)
    {
        _settings = settings;
    }

    private void Update()
    {
        if (!_isAnimation)
            CalculateHandDirection();
    }

    public void Attack()
    {
        if (_isAnimation)
            return;
        
        //TODO: Find targets in range

        SwordAnimation();
        
        _reversedAttack = !_reversedAttack;
    }
    
    private void CalculateHandDirection()
    {
        var direction = GetDirectionToMouse();
        handPoint.up = direction;
    }

    private Vector2 GetDirectionToMouse()
    {
        var mousePosition = Mouse.current.position;
        var convertedPosition = _mainCamera.ScreenToWorldPoint(mousePosition.value);
        
        var handPosition = transform.position;
        var direction = new Vector2(handPosition.x - convertedPosition.x, handPosition.y - convertedPosition.y);

        return direction;
    }

    private void SwordAnimation()
    {
        _isAnimation = true;

        var direction = _reversedAttack ? -1 : 1;
        var newRotation = new Vector3(0, 0, 180 * direction);

        StartCoroutine(StartHitting());

        handPoint.DOLocalRotate(newRotation, attackTime, RotateMode.FastBeyond360)
            .SetEase(Ease.InCubic)
            .SetRelative(true)
            .OnStart(() => slashEffect.emitting = true)
            .OnComplete(() =>
            {
                slashEffect.emitting = false;
                _isAnimation = false;
            });
        
        var newWeaponRotation = new Vector3(0, 0, 100 * direction);
        weapon.DOLocalRotate(newWeaponRotation, attackTime, RotateMode.FastBeyond360)
            .SetEase(Ease.InCubic)
            .SetRelative(true);
    }

    private IEnumerator StartHitting()
    {
        yield return new WaitForSeconds(attackTime / 2f);
        
        slashEffect.emitting = true;
        var results = Physics2D.OverlapCircleAll(hitPoint.position, _settings.range, layerMask);

        foreach (var hit in results)
        {
            var hittable = hit.transform.GetComponent<IHittable>();
            if (hittable == null || hittable.Handler == transform)
                continue;
            
            hittable.Hit();
        }
        
        yield return new WaitForSeconds(attackTime / 2f);
    }
}