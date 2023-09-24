using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour, IHittable, IDestroyable
{
    [SerializeField]
    private CharacterSettings settings;
    
    [SerializeField]
    protected MovementBehaviour movement;
    
    [SerializeField]
    protected AttackBehaviour attack;
    
    [SerializeField]
    protected SpriteRenderer body;
    
    public Transform Handler { get; private set; }
    public float CurrentHealth { get; private set; }

    private bool _isInsensitive;

    private void Start()
    {
        Handler = transform;
        CurrentHealth = settings.health;
    }
    
    private void OnValidate()
    {
        movement.UpdateSettings(settings);
        attack.UpdateSettings(settings);
    }

    public void Attack()
    {
        attack.Attack();
    }

    public void Hit()
    {
        if (_isInsensitive)
            return;
        
        body.DOColor(Color.red, settings.insensitivityTime)
            .SetLoops(2, LoopType.Yoyo)
            .OnStart(() => _isInsensitive = true)
            .OnComplete(() => _isInsensitive = false);
    }
    
    public abstract void Destroy();
}