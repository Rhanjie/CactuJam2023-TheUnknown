using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour, IAttackable, IHittable, IDestroyable
{
    [SerializeField]
    private CharacterSettings settings;
    
    [SerializeField]
    protected MovementBehaviour movement;
    
    [SerializeField]
    protected AttackBehaviour attack;
    
    public float CurrentHealth { get; private set; }
    
    private void Start()
    {
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
        //TODO: Not implemented
    }
    
    public abstract void Destroy();
}