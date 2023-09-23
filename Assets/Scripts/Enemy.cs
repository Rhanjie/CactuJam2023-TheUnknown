using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    private EnemySettings settings;
    
    private void Start()
    {
        
    }
    
    private void Update()
    {
        
    }

    public override void Destroy()
    {
        throw new System.NotImplementedException();
    }
}