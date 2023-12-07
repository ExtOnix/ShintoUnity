using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ichigo : MonoBehaviour
{
    public event Action<int> OnTakeDamages;
    [SerializeField] PlayerInputs controls = null;
    [SerializeField] SpringArm arm = null;
    [SerializeField,Range(1,10)] int maxLife = 5;

    [SerializeField] ThrowComponent component = null;

    [SerializeField] List<Bomb> inventory = null;
    [SerializeField] Bomb currentBomb = null;

    int life = 10;
    bool isDead = false;
    bool inInvincibility = false;

    bool hasBomb = false;
    int currentIndex = 0;


    #region inputs
    [SerializeField,HideInInspector] InputAction move = null;
    [SerializeField,HideInInspector] InputAction rotate = null;
    [SerializeField,HideInInspector] InputAction shootBomb = null;
    [SerializeField,HideInInspector] InputAction throwBomb = null;
    [SerializeField,HideInInspector] InputAction dropBomb = null;
    #endregion

    private void Awake()
    {
        controls = new PlayerInputs();
        OnTakeDamages += TakeDamages;
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
        shootBomb = controls.Player.ShootBomb;
        shootBomb.Enable();
        throwBomb = controls.Player.ThrowBomb;
        throwBomb .Enable();
        dropBomb = controls.Player.DropBomb;
        dropBomb.Enable();

        shootBomb.performed += ShootBomb;
        throwBomb.performed += ThrowBomb;
        dropBomb .performed += DropBomb;
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

        if (!hasBomb)
            return;
        KeepBomb();
    }


    void SetHasBomb()
    {
        hasBomb = false;
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

    void KeepBomb()
    {
        Bomb _bomb = component.CurrentBomb;
        if(!_bomb) return;
        _bomb.transform.position = transform.position;
    }
    void SelectBomb()
    {
        Bomb _bomb = Instantiate<Bomb>(inventory[currentIndex],transform.position + (Vector3.up * 10),transform.rotation);
        //onBombSpawn.Broadcast(true);
        //_bomb->AttachToActor();
        if (!component)
            return;
        component.CurrentBomb = _bomb;
        if (!_bomb)
            return;
        hasBomb = true;
        component.CurrentBomb.OnExplode += SetHasBomb;
    }

    void DropBomb(InputAction.CallbackContext _context)
    {
        if (!hasBomb) return;
        Bomb _bomb = component.CurrentBomb;
        if (!component || !_bomb)
            return;
        component.Throw(Vector3.zero, Vector3.zero);
        component.CurrentBomb.OnExplode -= SetHasBomb;
        hasBomb = false;
        //onBombSpawn.Broadcast(false);
    }

    void ShootBomb(InputAction.CallbackContext _context)
    {
            if (!hasBomb)
            {
                SelectBomb();
            }
            else if (hasBomb)
            {
                Bomb _bomb = component.CurrentBomb;
                if (!component || !_bomb)
                    return;
                component.Throw(transform.forward,Vector3.zero);
                component.CurrentBomb.OnExplode -= SetHasBomb;
                hasBomb = false;
                //onBombSpawn.Broadcast(false);
            }
    }
    void ThrowBomb(InputAction.CallbackContext _context)
        {
            Bomb _bomb = component.CurrentBomb;
            if (!hasBomb)
            {
                SelectBomb();
            }
            else if (hasBomb)
            {
                if (!component || !_bomb)
                    return;
                component.Throw(transform.forward, transform.up);
                component.CurrentBomb.OnExplode -= SetHasBomb;
                hasBomb = false;
                //onBombSpawn.Broadcast(false);
            }
    }
}
