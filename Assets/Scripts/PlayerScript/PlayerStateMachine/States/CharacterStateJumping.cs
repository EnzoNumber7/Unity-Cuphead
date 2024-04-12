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

        player._rb.AddForce(new Vector2(player._rb.velocity.x, jumpForce));
    }
    public override void ExitState() { }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        player._rb.velocity = new Vector2(Input.GetAxis("Horizontal") * player._speed * Time.deltaTime * 100, player._rb.velocity.y);

    }

    public override void OnChangeState()
    {
        if(player._rb.velocity.y < 0 || Input.GetKeyUp(KeyCode.Space))
        {
            player._rb.velocity = new Vector2(0, 0);
            characterStateMachine.ChangeState(player.stateFalling);
        }

        //if (Input.GetAxis("Horizontal") != 0)
        //{
        //    characterStateMachine.ChangeState(player.stateMoving);
        //}
        //if (player._rightSide.GetComponent<LeftSide>().isTriggering)
        //{
        //    player.stateMachine.ChangeState(player.stateSliding);
        //}
        if (Input.GetMouseButtonUp(1))
        {
            characterStateMachine.ChangeState(player.stateBalancing);
        }

    }
}
