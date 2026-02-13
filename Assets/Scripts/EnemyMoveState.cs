using UnityEngine;

public class EnemyMoveState : EnemyGroundedState
{
    public EnemyMoveState(Enemy enemy, StateMachine stateMachine, string AnimBoolName) : base(enemy, stateMachine, AnimBoolName) {
    }

    public override void Enter() {
        base.Enter();

        if (enemy.groundDetected == false || enemy.wallDetected) {
            enemy.Flip();
        }
    }

    public override void Update() {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir,rb.linearVelocity.y);

        if (enemy.groundDetected == false || enemy.wallDetected) {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
