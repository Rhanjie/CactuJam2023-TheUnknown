using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    private void Update()
    {
        FindTarget();
        
        if (lookAt != null)
            FollowTarget();
        
        else movement.Stop();
    }

    private void FollowTarget()
    {
        var position = transform.position;
        var targetPosition = lookAt.transform.position;
        var direction = new Vector2(targetPosition.x - position.x, targetPosition.y - position.y).normalized;
        
        Debug.LogError(direction);
        movement.Move(direction);
        
        if (IsTargetInRange())
            attack.Attack();
    }

    private bool IsTargetInRange()
    {
        var position = transform.position;
        var targetPosition = lookAt.transform.position;

        var distance = new Vector2(targetPosition.x - position.x, targetPosition.y - position.y).magnitude;

        return distance <= settings.range;
    }

    private void FindTarget()
    {
        var position = body.transform.position;
        var size = new Vector2(20, 13);
        var layerMask = LayerMask.GetMask("Player");

        var result = Physics2D.OverlapBox(position, size, 0, layerMask);
        
        var player = result != null ? result.GetComponent<Player>() : null;
        if (player == null)
        {
            ResetTarget();
            
            return;
        }
        
        lookAt = player.transform;
    }

    private void ResetTarget()
    {
        lookAt = null;
    }

    public override void Destroy()
    {
        //TODO: Effect
        
        gameObject.SetActive(false);
    }
    
    private void OnDrawGizmos()
    {
        var position = body.transform.position;
        var size = new Vector2(20, 13);
        
        Gizmos.DrawWireCube(position, size);
    }
}