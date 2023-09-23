using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    
    
    private void Start()
    {
        
    }
    
    private void Update()
    {
        
    }

    public void PerformMove(InputAction.CallbackContext context)
    {
        var delta = context.ReadValue<Vector2>();
            
        Move(delta);
    }

    public override void Destroy()
    {
        
    }
}