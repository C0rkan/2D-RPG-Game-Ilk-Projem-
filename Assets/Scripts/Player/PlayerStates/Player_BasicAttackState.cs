using Unity.VisualScripting;
using UnityEngine;

public class Player_BasicAttackState : PlayerState {
    
    private float attackVelocityTimer;
    private const int FirstComboIndex = 1;  // Ilk attack deðeri için sabit bir sayý kullanmak yerine onu deðiþtirilemez bir deðiþken kullanarak daha okunaklý hale getirdik. 
    private int attackDir;
    private int comboIndex = 1;
    private int comboLimit = 3;


    private float lastTimeAttacked;
    private bool comboAttackQueued;


    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) {

        if (comboLimit != player.attackVelocity.Length) {
            comboLimit= player.attackVelocity.Length;
        }
    }

    public override void Enter() {
        base.Enter();

        comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        if (player.moveInput.x != 0) {
            attackDir = ((int)player.moveInput.x);
        }
        else {
            attackDir = player.facingDir;
        }

        //attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;  Bu komut bir if else sorgusu soru iþareti koþul ilk kýsým true " : " sonrasý da false deðeri için cevap. 
        
        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
        
    }

    

    public override void Update() {
        base.Update();
        HandleAttackVelocity();

        if (input.Player.BasicAttack.WasPressedThisFrame()) {
            QueueNextAttack();
        }

        if (triggerCalled) {
               HandleStateExit();
        }   
    }
    public override void Exit() {
        base.Exit();

        comboIndex++;
        lastTimeAttacked = Time.time;

    }

    private void QueueNextAttack() {
        if (comboIndex < comboLimit) {
            comboAttackQueued = true;
        }
    }

    private void HandleStateExit() {
        if (comboAttackQueued) {

            anim.SetBool(animBoolName, false);
            player.EnterAttackWithDelay();
        }
        else {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void HandleAttackVelocity() {

        attackVelocityTimer -=Time.deltaTime;

        if (attackVelocityTimer < 0 )
            player.SetVelocity(0,rb.linearVelocity.y);
    }

    private void ApplyAttackVelocity() {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];

        attackVelocityTimer = player.attackVelocityTimer;
        
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);

    }

    private void ResetComboIndexIfNeeded() {

        if (Time.time  > lastTimeAttacked + player.comboResetTime) {
            comboIndex = FirstComboIndex;
        }

        if (comboIndex > comboLimit) {
            comboIndex = FirstComboIndex;
        }
    }   
}
