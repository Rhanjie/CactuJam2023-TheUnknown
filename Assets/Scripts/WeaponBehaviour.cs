using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    // Update is called once per frame
    private void Update()
    {
        var mousePosition = Mouse.current.position;

        var convertedPosition = _camera.ScreenToWorldPoint(mousePosition.value);

        var direction = new Vector2(convertedPosition.x - transform.position.x, convertedPosition.y - transform.position.y);

        transform.up = direction;
    }
}
