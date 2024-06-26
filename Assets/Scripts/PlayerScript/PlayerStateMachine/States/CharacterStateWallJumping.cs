using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateWallJumping : CharacterState
{

    public float jumpForce;

    public CharacterStateWallJumping(float JumpForce) : base() { jumpForce = JumpForce; }

    public override void EnterState()
    {
        player._rb.AddForce(new Vector2(jumpForce, 5));
    }
    public override void ExitState() { }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        //player._rb.velocity += Vector2.right * Input.GetAxis("Horizontal") * player._speed * Time.deltaTime * 100;

    }

    public override void OnChangeState()
    {
        if (player._rb.velocity.y < 0)
        {
            characterStateMachine.ChangeState(player.stateFalling);
        }

        //if (player._rightSide.GetComponent<LeftSide>().isTriggering)
        //{
        //    player.stateMachine.ChangeState(player.stateSliding);
        //}

    }
}