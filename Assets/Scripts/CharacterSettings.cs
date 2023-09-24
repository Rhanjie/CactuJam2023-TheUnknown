using UnityEngine;

public class CharacterSettings : ScriptableObject
{
    [Header("Main")]
    public string title;
    
    [Header("Attack")]
    public float health;
    public float damage;
    public float range;
    public float insensitivityTime;
    
    [Header("Movement")]
    public float speed;
    public float acceleration;
    public float friction;
}