using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsComponent : MonoBehaviour
{
    [SerializeField] MyInputs controls = null;
    [SerializeField] InputAction move = null;
    [SerializeField] InputAction rotate = null;
    [SerializeField] InputAction rotatePlayer = null;

    public InputAction Move => move;
    public InputAction Rotate => rotate;
    public InputAction RotatePlayer => rotatePlayer;



    void Awake()
    {
        controls = new();
    }

    void OnEnable()
    {
        move = controls.Player.Move;
        rotate = controls.Player.Rotate;
        rotatePlayer = controls.Player.RotatePlayer;

        EnableAll();
    }

    void OnDisable()
    {
        move.Disable();
        rotate.Disable();
        rotatePlayer.Disable();
    }

    void EnableAll()
    {
        move.Enable();
        rotate.Enable();
        rotatePlayer.Enable();
    }
}
