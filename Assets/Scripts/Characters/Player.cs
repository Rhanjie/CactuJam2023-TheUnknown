using System;
using System.Linq;
using Characters.Interfaces;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [SerializeField]
    private HUD hud;
    
    private Camera _mainCamera;
    private IInteractable _target = null;
    
    private void Start()
    {
        _mainCamera = GameObject
            .FindWithTag("MainCamera")
            .GetComponent<Camera>();
    }

    private void Update()
    {
        UpdateTargetPosition();

        InteractionChecker();
        InteractionListener();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(body.transform.position, new Vector2(3, 5));
    }

    private void InteractionChecker()
    {
        var position = body.transform.position;
        var size = new Vector2(3, 5);
        var layerMask = LayerMask.GetMask("Interactable");

        var results = Physics2D.OverlapBoxAll(position, size, layerMask).ToList();
        
        var interactables = results
            .Select(result => result.GetComponent<IInteractable>())
            .Where(interactable => interactable != null)
            .ToArray();
        
        if (interactables.Length == 0)
        {
            ClearInteraction();
            
            return;
        }

        var foundTarget = interactables.First();
        if (foundTarget == _target)
            return;

        _target = foundTarget;
        
        hud.ToggleInteractionText(true);
    }

    private void ClearInteraction()
    {
        hud.ToggleInteractionText(false);
        _target = null;
    }

    private void InteractionListener()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (_target == null)
                return;
            
            _target.Interact(this);
        }
    }

    public void PerformMove(InputAction.CallbackContext context)
    {
        var delta = context.ReadValue<Vector2>();
            
        movement.Move(delta);
    }
    
    public void PerformAttack()
    {
        attack.Attack();
    }

    public override void Destroy()
    {
        //TODO: Gameover
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenDictionary(string title, string content)
    {
        hud.OpenDictionary(title, content);
    }

    private void UpdateTargetPosition()
    {
        var mousePosition = Mouse.current.position;
        var convertedPosition = _mainCamera.ScreenToWorldPoint(mousePosition.value);

        lookAt.transform.position = convertedPosition;
    }
}