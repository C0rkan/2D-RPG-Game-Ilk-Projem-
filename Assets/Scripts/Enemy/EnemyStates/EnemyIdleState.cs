using UnityEngine;

public class EnemyIdleState : EnemyGroundedState
{
    public EnemyIdleState(Enemy enemy, StateMachine stateMachine, string AnimBoolName) : base(enemy, stateMachine, AnimBoolName) {
    }

    public override void Enter() {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Update() {
        base.Update();
        if (stateTimer < 0) {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
