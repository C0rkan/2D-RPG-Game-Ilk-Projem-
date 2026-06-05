using UnityEngine;

public class EnemyStunnedState : EnemyState {

    private Enemy_VFX vfx;
    public EnemyStunnedState(Enemy enemy, StateMachine stateMachine, string AnimBoolName) : base(enemy, stateMachine, AnimBoolName) {
        vfx = enemy.GetComponent<Enemy_VFX>();
    }


    public override void Enter() {
        base.Enter();

        vfx.EnableAttackAlert(false);
        enemy.EnableCounterWindow(false);
        stateTimer = enemy.stunnedDuration;
        rb.linearVelocity = new Vector2(enemy.stunnedVelocity.x * -enemy.facingDir, enemy.stunnedVelocity.y );

    }

    public override void Update() {
        base.Update();

        if (stateTimer < 0) {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
