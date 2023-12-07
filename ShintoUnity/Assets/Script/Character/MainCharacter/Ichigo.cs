using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ichigo : MonoBehaviour
{
    [SerializeField] PlayerInputs controls = null;
    [SerializeField,HideInInspector] InputAction move = null;
    [SerializeField,HideInInspector] InputAction rotate = null;

    private void Awake()
    {
        controls = new PlayerInputs();
    }

    private void OnEnable()
    {
        move = controls.Player.Movement;
        move.Enable();
        rotate = controls.Player.Rotate;
        rotate.Enable();

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; 
        Ray _r = new Ray(transform.position + (Vector3.forward/2), transform.forward);
        Gizmos.DrawRay(_r);
        Gizmos.color = Color.white;
    }
    private void OnDisable()
    {
        rotate.Disable();
        move.Disable();
    }
    private void Update()
    {
        Move();
        Rotate();
    }
    void Move()
    {
        Vector3 _movementDirection = move.ReadValue<Vector3>();
        transform.position += transform.forward * 5f * Time.deltaTime * _movementDirection.z;
        transform.position += transform.right * 5f * Time.deltaTime * _movementDirection.x;
    }

    void Rotate()
    {
        float _rotValue = rotate.ReadValue<float>();
        transform.eulerAngles += transform.up * 50f * Time.deltaTime * _rotValue;
    }
}
