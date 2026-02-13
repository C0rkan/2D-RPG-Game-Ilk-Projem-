using UnityEngine;

public class EnemyGroundedState : EnemyState
{
    public EnemyGroundedState(Enemy enemy, StateMachine stateMachine, string AnimBoolName) : base(enemy, stateMachine, AnimBoolName) {
    }

    public override void Update() {
        base.Update();

        if (enemy.PlayerDetection() == true) {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
