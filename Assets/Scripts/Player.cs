using System;
using System.Collections;
using System.Numerics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : Entity {

    public PlayerInputSet input { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }

    [Header("Attack Details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityTimer = .1f;
    public float comboResetTime = 1;
    private Coroutine queuedAttackCo;


    [Header("Movement Details")]
    public float moveSpeed;
    public float jumpForce = 5;
    public Vector2 wallJumpDir;
    [Range(0, 1)]
    public float inAirMoveMultipiler = .7f;
    [Range(0, 1)]
    public float wallSlideSlowMultipiler = .7f;
    [Space]
    public float dashDuration = .25f;
    public float dashSpeed = 20;

    public Vector2 moveInput { get; private set; }

    protected override void Awake() {
        base.Awake();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
    }

    protected override void Start() {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public void EnterAttackWithDelay() {
        if (queuedAttackCo != null) {
            StopCoroutine(queuedAttackCo);
        }

        queuedAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    private IEnumerator EnterAttackStateWithDelayCo() {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }

    private void OnEnable() {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        //input.Player.Movement.started - tuþa basýlma anýnda
        //input.Player.Movement.performed - tuþ basýlý tutulduðunda
        //input.Player.Movement.canceled - tuþ býrakýldýðýnda 

    }

    private void OnDisable() {
        input.Disable();
    }
}
