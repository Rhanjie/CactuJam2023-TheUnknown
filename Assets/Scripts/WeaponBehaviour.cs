using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    private Transform _cachedTransform;

    private void Start()
    {
        _cachedTransform = transform;
    }
    
    private void Update()
    {
        var mousePosition = Mouse.current.position;
        var convertedPosition = mainCamera.ScreenToWorldPoint(mousePosition.value);
        
        var handPosition = _cachedTransform.position;
        var direction = new Vector2(convertedPosition.x - handPosition.x, convertedPosition.y - handPosition.y);

        _cachedTransform.up = direction;
    }
}
