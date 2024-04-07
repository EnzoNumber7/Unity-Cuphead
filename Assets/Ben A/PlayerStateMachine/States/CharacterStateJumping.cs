using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateJumping : CharacterState
{

    public float jumpForce;

    public CharacterStateJumping(CharacterStateMachine stateMachine, Player p, float JumpForce) : base(stateMachine, p) { jumpForce = JumpForce; }

    public override void EnterState() {

        if (!player._feet.GetComponent<FeetPlayer>().isGrounded)
            return;

        //player._rb.velocity = new Vector2(player._rb.velocity.x, 0);
        //player._rb.velocity += Vector2.up * player._jumpForce;
        player._rb.AddForce(new Vector2(player._rb.velocity.x, player._jumpForce));
    }
    public override void ExitState() { }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        player._rb.velocity += Vector2.right * Input.GetAxis("Horizontal") * player._speed * Time.deltaTime * 100;

    }

    public override void OnChangeState()
    {
        if(player._rb.velocity.y < 0 || Input.GetKeyUp(KeyCode.Space))
        {
            characterStateMachine.ChangeState(player.stateFalling);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            characterStateMachine.ChangeState(player.stateMoving);
        }


    }
}
