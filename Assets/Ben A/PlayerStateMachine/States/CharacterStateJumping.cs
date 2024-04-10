using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateJumping : CharacterState
{

    public float jumpForce;

    public CharacterStateJumping( float JumpForce) : base() { jumpForce = JumpForce; }

    public override void EnterState() {

        if (!player._feet.GetComponent<FeetPlayer>().isGrounded)
            return;

        player._rb.AddForce(new Vector2(player._rb.velocity.x, player._jumpForce));
    }
    public override void ExitState() { }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        //player._rb.velocity += Vector2.right * Input.GetAxis("Horizontal") * player._speed * Time.deltaTime * 100;

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

        if (player._leftSide.GetComponent<LeftSide>().isTriggering)
        {
            player.stateMachine.ChangeState(player.stateLeftSliding);
        }

        if (player._rightSide.GetComponent<LeftSide>().isTriggering)
        {
            player.stateMachine.ChangeState(player.stateRightSliding);
        }

    }
}
