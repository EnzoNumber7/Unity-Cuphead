using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateSlidingRight : CharacterStateSliding
{
    public CharacterStateSlidingRight() : base()   
    {
    }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        player._rb.velocity -= new Vector2(0, player._rb.velocity.y);

        if (Input.GetAxis("Horizontal") > 0)
        {
            player._rb.velocity -= new Vector2(Input.GetAxis("Horizontal") / 2, 0);
        }

    }

    public override void OnChangeState()
    {

        if (player._rightSide.GetComponent<LeftSide>().isTriggering == false) 
        {
            characterStateMachine.ChangeState(player.stateFalling);
        }

        if (player._feet.GetComponent<FeetPlayer>().isGrounded)
        {
            characterStateMachine.ChangeState(player.stateIdle);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterStateMachine.ChangeState(player.stateWallJumping);
        }

    }

}
