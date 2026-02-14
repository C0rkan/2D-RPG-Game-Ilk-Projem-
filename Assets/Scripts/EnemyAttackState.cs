using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, StateMachine stateMachine, string AnimBoolName) : base(enemy, stateMachine, AnimBoolName) {
    }

    public override void Update() {
        base.Update();

        if (triggerCalled) {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
