using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ichigo : MonoBehaviour
{
    [SerializeField] PlayerInputs controls = null;
    [SerializeField] SpringArm arm = null;
    [SerializeField,Range(1,10)] int maxLife = 5;

    [SerializeField] List<Bomb> inventory = null;
    [SerializeField] Bomb currentBomb = null;

    int life = 10;
    bool isDead = false;
    bool inInvincibility = false;

    bool canTakeBomb = false;
    bool canShootBomb = false;
    bool canDropBomb = false;
    bool canThrowBomb = false;


    #region inputs
    [SerializeField,HideInInspector] InputAction move = null;
    [SerializeField,HideInInspector] InputAction rotate = null;
    [SerializeField,HideInInspector] InputAction takeBomb = null;
    [SerializeField,HideInInspector] InputAction shootBomb = null;
    [SerializeField,HideInInspector] InputAction throwBomb = null;
    [SerializeField,HideInInspector] InputAction dropBomb = null;
    #endregion

    private void Awake()
    {
        controls = new PlayerInputs();
    }
    private void Start()
    {
        life = maxLife;
    }

    private void OnEnable()
    {
        move = controls.Player.Movement;
        move.Enable();
        rotate = controls.Player.Rotate;
        rotate.Enable();
        takeBomb = controls.Player.TakeBomb;
        takeBomb.Enable();
        shootBomb = controls.Player.ShootBomb;
        shootBomb.Enable();
        throwBomb = controls.Player.ThrowBomb;
        throwBomb .Enable();
        dropBomb = controls.Player.DropBomb;
        dropBomb.Enable();

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
    #region Movements
    void Move()
    {
        Vector3 _movementDirection = move.ReadValue<Vector3>();
        if(_movementDirection.z > 0)
            transform.rotation = new Quaternion(transform.rotation.x,arm.AttachedCamera.transform.rotation.y,transform.rotation.z, transform.rotation.w);
        transform.position += transform.forward * 5f * Time.deltaTime * _movementDirection.z;
        transform.position += transform.right * 5f * Time.deltaTime * _movementDirection.x;

    }

    void Rotate()
    {
        float _rotValue = rotate.ReadValue<float>();
        arm.Angle += _rotValue;
    }
    #endregion

    void TakeDamages(int _damage)
    {
        if (isDead || inInvincibility)
            return;
        life -= _damage;
        //onLifeChange.Broadcast(life);
        //inInvincibility = true;
        //onInvinsibilityStart.Broadcast();
        if (life == 0)
        {
            isDead = true;
            //onDie.Broadcast();
            return;
        }

    }


}
