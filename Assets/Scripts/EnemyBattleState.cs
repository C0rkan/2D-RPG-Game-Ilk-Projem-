using Unity.Mathematics;
using UnityEngine;

public class EnemyBattleState : EnemyState {
    private Transform player;
    private float lastTimeWasInBattle;
    public EnemyBattleState(Enemy enemy, StateMachine stateMachine, string AnimBoolName) : base(enemy, stateMachine, AnimBoolName) {
    }

    public override void Enter() {
        base.Enter();

        if (player == null) {
            player = enemy.PlayerDetected().transform;
        }

        if (ShouldRetreat()) {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), rb.linearVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }

    }

    public override void Update() {
        base.Update();

        if (enemy.PlayerDetected()) {
            UpdateBattleTimer();
        }

        if (BattleTimeIsOver()) {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (WithinAttackRange() && enemy.PlayerDetected()) {
            stateMachine.ChangeState(enemy.attackState);
        }
        else {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(),rb.linearVelocity.y);
        }
    }

    private void UpdateBattleTimer() {
        lastTimeWasInBattle = Time.time;
    }

    private bool BattleTimeIsOver() {
        return Time.time > lastTimeWasInBattle + enemy.battletimeDuration;
    }

    private bool WithinAttackRange() {
        return DistanceToPlayer() < enemy.attackDistance;
    }

    private bool ShouldRetreat() {
        return DistanceToPlayer() < enemy.minRetreatDistance;
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
