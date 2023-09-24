using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour, IHittable, IDestroyable
{
    [SerializeField]
    protected CharacterSettings settings;
    
    [SerializeField]
    protected MovementBehaviour movement;
    
    [SerializeField]
    protected AttackBehaviour attack;
    
    [SerializeField]
    protected SpriteRenderer body;
    
    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private Transform lookAt;

    public Transform LookAt
    {
        get => lookAt;
        set
        {
            lookAt = value;
            
            movement.SetTarget(lookAt);
            attack.SetTarget(lookAt);
        }
    }

    public Transform Handler { get; private set; }
    public float CurrentHealth { get; private set; }

    private bool _isInsensitive;
    
    private static readonly int Velocity = Animator.StringToHash("Velocity");

    private void Start()
    {
        Handler = transform;
        CurrentHealth = settings.health;
    }
    
    private void OnValidate()
    {
        movement.UpdateSettings(settings);
        movement.SetTarget(lookAt);
        
        attack.UpdateSettings(settings);
        attack.SetTarget(lookAt);
    }
    
    protected virtual void Update()
    {
        animator.SetFloat(Velocity, movement.Velocity);
    }

    public void Hit()
    {
        if (_isInsensitive)
            return;
        
        body.DOColor(Color.black, settings.insensitivityTime)
            .SetLoops(2, LoopType.Yoyo)
            .OnStart(() => _isInsensitive = true)
            .OnComplete(() => _isInsensitive = false);
    }

    public abstract void Destroy();
}