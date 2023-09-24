using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : Character
{
    private Camera _mainCamera;
    
    private void Start()
    {
        _mainCamera = GameObject
            .FindWithTag("MainCamera")
            .GetComponent<Camera>();
    }

    private void Update()
    {
        UpdateTargetPosition();
    }

    public void PerformMove(InputAction.CallbackContext context)
    {
        var delta = context.ReadValue<Vector2>();
            
        movement.Move(delta);
    }

    public override void Destroy()
    {
        //TODO: Gameover
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateTargetPosition()
    {
        var mousePosition = Mouse.current.position;
        var convertedPosition = _mainCamera.ScreenToWorldPoint(mousePosition.value);

        lookAt.transform.position = convertedPosition;
    }
}