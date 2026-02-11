using UnityEngine;

public class Player_GroundedState : EntityState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }


    public override void Update() {
        base.Update();

        if (rb.linearVelocity.y < 0 && player.groundDetected == false) {
            stateMachine.ChangeState(player.fallState);
        }

        if (input.Player.Jump.WasPressedThisFrame()) {
            stateMachine.ChangeState(player.jumpState);
        }

        if (input.Player.BasicAttack.WasPressedThisFrame()) {
            stateMachine.ChangeState(player.basicAttackState);
        }
    }
}
