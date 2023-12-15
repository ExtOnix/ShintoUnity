using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

[RequireComponent(typeof(LifeComponent), typeof(ThrowComponent))]
public class Ichigo : MonoBehaviour
{
    public event Action OnBombChange;
    public event Action<float> OnMove;
    public event Action<bool> OnShoot;
    public event Action<bool> OnThrow;
    public event Action<bool> OnDrop;

    [SerializeField] PlayerInputs controls = null;
    [SerializeField] SpringArm arm = null;
    [SerializeField] CharacterController controller = null;
    [SerializeField] MeshRenderer mesh = null;

    [SerializeField] ThrowComponent component = null;
    [SerializeField] LifeComponent life = null;

    [SerializeField] List<Bomb> inventory = null;
    [SerializeField] Bomb currentBomb = null;

    [SerializeField] LayerMask hitLayer;
    [SerializeField,Range(1,10)] int length = 5;

    Vector3 initLocation = Vector3.zero;

    bool isWalkingForward = false;

    bool canMove = true;
    bool hasBomb = false;
    int currentIndex = 0;


    public bool CanMove { get => canMove; set => canMove = value; }
    public SpringArm Arm { get { return arm; } set { arm = value; } }

    public bool HasBomb { get { return hasBomb; } }
    public Bomb CurrentBomb => currentBomb;


    #region inputs
    [SerializeField,HideInInspector] InputAction move = null;
    [SerializeField,HideInInspector] InputAction rotate = null;
    [SerializeField,HideInInspector] InputAction shootBomb = null;
    [SerializeField,HideInInspector] InputAction throwBomb = null;
    [SerializeField,HideInInspector] InputAction dropBomb = null;
    [SerializeField,HideInInspector] InputAction scrollUp = null;
    [SerializeField,HideInInspector] InputAction scrollDown = null;
    [SerializeField,HideInInspector] InputAction rotatePlayer = null;
    [SerializeField,HideInInspector] InputAction respawn = null;
    [SerializeField,HideInInspector] InputAction pause = null;
    #endregion

    private void Awake()
    {
        controls = new PlayerInputs();
        life.OnDie += UpdateInputState;
        initLocation = transform.position;
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
        scrollUp = controls.Player.ScrollUp;
        scrollUp.Enable();
        scrollDown = controls.Player.ScrollDown;
        scrollDown.Enable();
        rotatePlayer = controls.Player.RotatePlayer;
        rotatePlayer.Enable();
        respawn = controls.Player.Respawn;
        respawn.Enable();
        pause = controls.Player.Pause;
        pause.Enable();

        shootBomb.performed += ShootBomb;
        throwBomb.performed += ThrowBomb;
        dropBomb .performed += DropBomb;
        scrollDown.performed += ScrollDown;
        scrollUp.performed += ScrollUp;
       rotatePlayer.performed += WalkingForward;
        respawn.performed += RespawnPlayer;
        pause.performed += Pause;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Ray _r = new Ray(transform.position + transform.up, mesh.transform.forward);
        Gizmos.DrawRay(_r);
        Gizmos.color = Color.white;
    }
    private void OnDisable()
    {
        DisableMovements();
    }

    public void EnableMovements()
    {
        move.Enable();
        rotatePlayer.Enable();
    }
    public void DisableMovements()
    {
        move.Disable();
        rotatePlayer .Disable();
    }

    void UpdateInputState(bool _status)
    {
        if (!_status) EnableAllInputs(); else DisableAllInputs();
    }

    public void EnableAllInputs()
    {
        move.Enable();
        rotatePlayer.Enable();
        rotate.Enable();
        dropBomb.Enable();
        throwBomb.Enable();
        shootBomb.Enable();
        scrollDown.Enable();
        scrollUp.Enable();
        pause.Enable();
        canMove = true;
    }

    public void DisableAllInputs()
    {
        canMove = false;
        move.Disable();
        rotatePlayer .Disable();
        rotate.Disable();
        dropBomb.Disable();
        throwBomb.Disable();
        shootBomb .Disable();
        scrollDown.Disable();
        scrollUp.Disable();
        pause.Disable();
    }
    private void Start()
    {
        currentBomb = inventory[0];
        OnBombChange?.Invoke();
    }
    private void Update()
    {
        Move();
        DetectObject();
        SetPlayerRotationWithSpringArmRotation();

        if (!hasBomb)
            return;
        KeepBomb();
    }

    void LateUpdate()
    {
        Rotate();
    }

    void Pause(InputAction.CallbackContext _context)
    {
        
    }


    void SetHasBomb()
    {
        hasBomb = false;
    }
    #region Movements
    void Move()
    {
        Vector3 _movementDirection = move.ReadValue<Vector3>();
        OnMove.Invoke(_movementDirection.x);
        OnMove.Invoke(_movementDirection.z);
        RotateMesh(_movementDirection);
        if (!canMove)
            MoveWithoutGravity(_movementDirection);
        else
        MoveWithGravity(_movementDirection);
    }

    void MoveWithGravity(Vector3 _movementDirection)
    {
        Vector3 _movement = transform.forward * _movementDirection.z + transform.right * _movementDirection.x;
        controller.SimpleMove(_movement * 10);
    }

    void MoveWithoutGravity(Vector3 _movementDirection)
    {
        transform.position += transform.forward * 5f * Time.deltaTime * _movementDirection.z;
        transform.position += transform.right * 5f * Time.deltaTime * _movementDirection.x;
        return;
    }

    void RotateMesh(Vector3 _movementDirection)
    {
        if (_movementDirection.z < 0)
            mesh.transform.localEulerAngles = new Vector3(0, 180, 0);
        else if (_movementDirection.z > 0)
            mesh.transform.localEulerAngles = new Vector3(0, 0, 0);
        else if (_movementDirection.x > 0)
            mesh.transform.localEulerAngles = new Vector3(0, 90, 0);
        else if (_movementDirection.x < 0)
            mesh.transform.localEulerAngles = new Vector3(0, -90, 0);
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

    void Rotate()
    {
        float _axis = controls.Player.Rotate.ReadValue<float>();
        arm.transform.localEulerAngles += new Vector3(0, _axis, 0);
    }
    #endregion

    void RespawnPlayer(InputAction.CallbackContext _context)
    {
        Respawn();
    }
    public void Respawn()
    {
        Debug.Log("respawn");
        life.IsDead = false;
        life.ResetLife();
        transform.position = initLocation;
    }
    void KeepBomb()
    {
        Bomb _bomb = component.CurrentBomb;
        if(!_bomb) return;
        _bomb.transform.position = transform.position + (Vector3.up * 2);
    }
    void SelectBomb()
    {
        Bomb _bomb = Instantiate<Bomb>(inventory[currentIndex],transform.position + (Vector3.up * 2),transform.rotation);
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
        OnDrop.Invoke(true);
        Bomb _bomb = component.CurrentBomb;
        if (!component || !_bomb)
            return;
        component.Throw(Vector3.zero, Vector3.zero);
        component.CurrentBomb.OnExplode -= SetHasBomb;
        hasBomb = false;
        OnDrop.Invoke(false);
        //onBombSpawn.Broadcast(false);
    }

    void ShootBomb(InputAction.CallbackContext _context)
    {
        bool _test = _context.ReadValueAsButton();
        if (!_test) return;
        Debug.Log("test");
            if (!hasBomb)
            {
                SelectBomb();
                return;
            }
            else if (hasBomb)
            {
                OnShoot.Invoke(true);
                Bomb _bomb = component.CurrentBomb;
                if (!component || !_bomb)
                    return;
                component.Throw(mesh.transform.forward,Vector3.zero);
                component.CurrentBomb.OnExplode -= SetHasBomb;
                hasBomb = false;
                //onBombSpawn.Broadcast(false);
                OnShoot.Invoke(false);
            }
    }
    void ThrowBomb(InputAction.CallbackContext _context)
    {
        Bomb _bomb = component.CurrentBomb;
        if (hasBomb)
        {
            OnThrow.Invoke(true);
            if (!component || !_bomb)
                return;
            component.Throw(mesh.transform.forward, mesh.transform.up);
            component.CurrentBomb.OnExplode -= SetHasBomb;
            hasBomb = false;
        //onBombSpawn.Broadcast(false);
            OnThrow.Invoke(false );
        }
    }

    void ScrollUp(InputAction.CallbackContext _context)
    {
        if (inventory.Count == 0)
            return;
        if (currentIndex == inventory.Count - 1)
            currentIndex = 0;
        else
            currentIndex++;
        currentBomb = inventory[currentIndex];
        OnBombChange.Invoke();
        Debug.Log(currentBomb.BombName);
    }

    void ScrollDown(InputAction.CallbackContext _context)
    {
        if (inventory.Count == 0)
            return;
        if (currentIndex == 0)
            currentIndex = inventory.Count - 1;
        else
            currentIndex--;
        currentBomb = inventory[currentIndex];
        OnBombChange.Invoke();
        Debug.Log(currentBomb.BombName);
    }

    public void AddBomb(Bomb _bomb)
    {
        foreach (Bomb _bombElement in inventory)
        {
            if (_bomb.BombName == _bombElement.BombName)
                return;
        }
        inventory.Add(_bomb);
    }

    void DetectObject()
    {
        bool _hitFwd = Physics.Raycast(new Ray(transform.position + transform.up, mesh.transform.forward), out RaycastHit _resultFwd, length, hitLayer);
        if (_hitFwd)
        {
            Debug.Log("bonjour");
            Block _block = _resultFwd.collider.GetComponent<Block>();
            if (!_block)
                return;
            _resultFwd.collider.GetComponent<Block>().Move(-(_resultFwd.normal));
        }
    }
}
