using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour, IMoveable, IJumpable, IAttackable, IHittable, IDestroyable
{
    [SerializeField]
    private CharacterSettings settings;
    
    [Header("References")]
    [SerializeField]
    private Transform body;
    
    [SerializeField]
    private Rigidbody2D physics;
    
    [SerializeField]
    private Transform handPoint;
    
    public float CurrentHealth { get; private set; }
    
    public Vector2 Position { get; private set; }
    public bool IsFacingRight { get; private set; }
    
    private float _horizontalMove;
    private float _verticalMove;
    
    private void Start()
    {
        CurrentHealth = settings.health;
    }
    
    private void FixedUpdate()
    {
        var movement = CalculateMovement();
        ApplyForce(movement, ForceMode2D.Force);

        //var friction = CalculateFriction();
        //ApplyForce(friction, ForceMode2D.Impulse);
    }
    
    public void Move(Vector2 delta)
    {
        _horizontalMove = delta.x;
        _verticalMove = delta.y;
        
        if (ShouldBeFlipped())
            Flip();
    }

    public void Jump()
    {
        //TODO: Not implemented
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
    
    private Vector2 CalculateMovement()
    {
        var targetSpeed = new Vector2(_horizontalMove, _verticalMove) * settings.speed;
        var speedDifference = targetSpeed - physics.velocity;

        //var direction = speedDifference.normalized;
        
        var movement = speedDifference * settings.acceleration;
        return movement;
    }

    private float CalculateFriction()
    {
        if (Mathf.Abs(_horizontalMove) >= 0.01f)
            return 0;

        //var amount = Mathf.Min(Mathf.Abs(physics.velocity.x), Mathf.Abs(settings.frictionAmount));
        //amount *= Mathf.Sign(physics.velocity.x);
        
        //return amount;
        return 0;
    }
    
    public void ApplyForce(Vector2 force, ForceMode2D mode)
    {
        if (force == Vector2.zero)
            return;
        
        physics.AddForce(force, mode);
    }
    
    private bool ShouldBeFlipped()
    {
        return IsFacingRight && _horizontalMove > 0f || !IsFacingRight && _horizontalMove < 0f;
    }
    
    private void Flip()
    {
        IsFacingRight = !IsFacingRight;

        var localScale = body.localScale;
        localScale.x *= -1f;

        body.localScale = localScale;
    }
}