using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMovement movement;
    private PlayerCamera look;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerCamera>();

        //kdykoliv je onFoot.jump uskutečněno použijeme callback context abychom zavolali funkci movement.Jump
        //stejné platí pro onFoot.Crouch a onFoot.Sprint
        //callback context = informace, která se předává action callbacku, ohledně toho, co vyvolalo akci
        //callback = funkce, která je uložena jako data a poté je využíváná jinačí funkcí
        onFoot.Jump.performed += ctx => movement.Jump();
        onFoot.Crouch.performed += ctx => movement.Crouch();
        onFoot.Sprint.performed += ctx => movement.Sprint();
    }

    void FixedUpdate()
    {
        // Řekneme playermovement aby se hýbal z hodnoty z naší pohybové akce
        movement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        look.ProcessLook(onFoot.Camera.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
