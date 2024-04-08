using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateSlidingRight : CharacterStateSliding
{
    public CharacterStateSlidingRight(CharacterStateMachine stateMachine, Player p, int direction) : base(stateMachine, p, direction)
    {
        this.direction = direction;
    }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        player._rb.velocity -= new Vector2(0, player._rb.velocity.y / 2);

        
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
            player._rb.AddForce(new Vector2(player._jumpForce / 100 * direction, 0));
            characterStateMachine.ChangeState(player.stateJumping);
        }

    }

}
