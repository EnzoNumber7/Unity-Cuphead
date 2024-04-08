using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateSlidingLeft : CharacterStateSliding
{
    public CharacterStateSlidingLeft(CharacterStateMachine stateMachine, Player p, int direction) : base(stateMachine, p, direction)
    {
        this.direction = direction;
    }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        player._rb.velocity -= new Vector2(0, player._rb.velocity.y);

        if (Input.GetAxis("Horizontal") < 0)
        {
            player._rb.velocity += new Vector2(Input.GetAxis("Horizontal") / 2,0);
        }
    }

    public override void OnChangeState()
    {

        if (player._feet.GetComponent<FeetPlayer>().isGrounded)
        {
            characterStateMachine.ChangeState(player.stateIdle);
        }

        if (player._rightSide.GetComponent<LeftSide>().isTriggering == false)
        {
            characterStateMachine.ChangeState(player.stateFalling);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            player._rb.AddForce(new Vector2(player._jumpForce * direction, 0));
            characterStateMachine.ChangeState(player.stateJumping);
            print("Jump");
        }

    }

}
