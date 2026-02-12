using UnityEngine;

public class EnemyState : EntityState {
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string AnimBoolName) : base(stateMachine, AnimBoolName) {
        this.enemy= enemy;

        rb =enemy.rb;
        anim = enemy.anim;
    }

    public override void Update() {
        base.Update();

        if(Input.GetKeyDown(KeyCode.F))
            stateMachine.ChangeState(enemy.attackState);

        anim.SetFloat("moveAnimSpeedMultipiller",enemy.moveAnimSpeedMultipiller);
    }
}
