using UnityEngine;

public class EnemyState : EntityState {
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string AnimBoolName) : base(stateMachine, AnimBoolName) {
        this.enemy= enemy;

        rb =enemy.rb;
        anim = enemy.anim;
    }
    
    public override void UpdateAnimationParameters() {
        base.UpdateAnimationParameters();

        float battleAnimSpeedMultipiller = enemy.battleMoveSpeed / enemy.moveSpeed;

        anim.SetFloat("moveAnimSpeedMultipiller", enemy.moveAnimSpeedMultipiller);
        anim.SetFloat("battleAnimSpeedMultipiller", battleAnimSpeedMultipiller);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }
}
