using UnityEngine;

public class EnemyBattleState : EnemyState {
    public EnemyBattleState(Enemy enemy, StateMachine stateMachine, string AnimBoolName) : base(enemy, stateMachine, AnimBoolName) {
    }

    public override void Enter() {
        base.Enter();
        Debug.Log("I Enter Battle State");
    }
}
