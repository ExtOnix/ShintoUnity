using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(InputsComponent))]
public class Player : MonoBehaviour
{
    [SerializeField] InputsComponent inputs = null;
    [SerializeField] SpringArm arm = null;

    bool isWalkingForward = false;

    private void Start()
    {
        inputs = GetComponent<InputsComponent>();
        inputs.RotatePlayer.performed += WalkingForward;
    }

    void Update()
    {
        Move();
        Rotate();
        SetPlayerRotationWithSpringArmRotation();
    }


    void Move()
    {
        Vector3 _axis = inputs.Move.ReadValue<Vector3>();
        transform.position += transform.forward * 5 * Time.deltaTime * _axis.z;
        transform.position += transform.right * 5 * Time.deltaTime * _axis.x;
    }

    void Rotate()
    {
        float _axis = inputs.Rotate.ReadValue<float>();
        arm.transform.localEulerAngles += new Vector3(0, _axis, 0);
    }

    void SetPlayerRotationWithSpringArmRotation()
    {
        if (!isWalkingForward) return;
        Vector3 _rot = arm.transform.eulerAngles;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _rot.y, transform.eulerAngles.z);
        arm.transform.localEulerAngles = new Vector3(_rot.x, 0, _rot.z);
    }


    void WalkingForward(InputAction.CallbackContext _context)
    {
        isWalkingForward = _context.ReadValueAsButton();
    }
}
