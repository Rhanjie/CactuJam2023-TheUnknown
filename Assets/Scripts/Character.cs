using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour, IAttackable, IHittable, IDestroyable
{
    [SerializeField]
    private CharacterSettings settings;
    
    [SerializeField]
    protected MovementBehaviour movement;
    
    public float CurrentHealth { get; private set; }
    
    private void Start()
    {
        CurrentHealth = settings.health;
    }
    
    private void OnValidate()
    {
        movement.UpdateSettings(settings);
    }

    public void Attack()
    {
        //TODO: Not implemented
    }

    public void Hit()
    {
        //TODO: Not implemented
    }
    
    public abstract void Destroy();
}