using UnityEngine;

public class EnemyDeadState : EnemyState {
    private Collider2D col;
    public EnemyDeadState(Enemy enemy, StateMachine stateMachine, string AnimBoolName) : base(enemy, stateMachine, AnimBoolName) {
        col = enemy.GetComponent<Collider2D>();
    }

    public override void Enter() {
        anim.enabled = false;
        col.enabled = false;
        
        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

        stateMachine.SwitchOffStateMachine();
    }
}
