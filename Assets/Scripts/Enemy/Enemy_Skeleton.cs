using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    protected override void Awake() {
        base.Awake();
        idleState = new EnemyIdleState(this, stateMachine, "idle");
        moveState= new EnemyMoveState(this, stateMachine, "move");
        attackState = new EnemyAttackState(this, stateMachine, "attack");
        battleState = new EnemyBattleState(this, stateMachine, "battle");
        deadState = new EnemyDeadState(this, stateMachine, "idle");
    }

    protected override void Start() {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    
}
