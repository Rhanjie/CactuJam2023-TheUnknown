using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackBehaviour : MonoBehaviour, IAttackable
{
    [SerializeField]
    private Transform handPoint;
    
    [SerializeField]
    private Transform weapon;

    public float attackTime = 0.3f;
    
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

        handPoint.DOLocalRotate(newRotation, attackTime, RotateMode.FastBeyond360)
            .SetEase(Ease.InCubic)
            .SetRelative(true)
            .OnComplete(() => { _isAnimation = false; });
        
        var newWeaponRotation = new Vector3(0, 0, 100 * direction);
        weapon.DOLocalRotate(newWeaponRotation, attackTime, RotateMode.FastBeyond360)
            .SetEase(Ease.InCubic)
            .SetRelative(true);
    }
}