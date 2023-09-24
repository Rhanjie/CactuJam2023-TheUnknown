using UnityEngine;

public class Enemy : Character
{
    private void Start()
    {
        
    }
    
    private void Update()
    {
        
    }

    public override void Destroy()
    {
        //TODO: Effect
        
        gameObject.SetActive(false);
    }
}