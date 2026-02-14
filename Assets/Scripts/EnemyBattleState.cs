using UnityEngine;

public class EnemyBattleState : EnemyState {
    private Transform player;
    
    public EnemyBattleState(Enemy enemy, StateMachine stateMachine, string AnimBoolName) : base(enemy, stateMachine, AnimBoolName) {
    }

    public override void Enter() {
        base.Enter();

        if (player == null) {
            player = enemy.PlayerDetection().transform;
        }
    }

    public override void Update() {
        base.Update();

        if (WithinAttackRange()) {
            stateMachine.ChangeState(enemy.attackState);
        }
        else {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(),rb.linearVelocity.y);
        }
    }

    private bool WithinAttackRange() {
        return DistanceToPlayer() < enemy.attackDistance;
    }

    private float DistanceToPlayer() {

        if (player == null) {
            return float.MaxValue;
        }

        return Mathf.Abs(enemy.transform.position.x - player.transform.position.x);
    }

    private int DirectionToPlayer() {
        if (player == null) {
            return 0;
        }

        return player.position.x > enemy.transform.position.x ? 1 : -1; // if else sorgusunun baþka bir yazým þekli. 
    }
}
