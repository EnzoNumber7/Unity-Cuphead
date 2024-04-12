using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMoving : CharacterState
{

    public CharacterStateMoving() : base()
    {

    }
    public override void EnterState() { }
    public override void ExitState() { }
    public override void UpdateFrame()
    {
        base.UpdateFrame();
            
        if (player.blockedDirection == BlockedDirection.None)
        {
            player._rb.velocity = new Vector2(Input.GetAxis("Horizontal") * player._speed * Time.deltaTime * 100, player._rb.velocity.y);
        }
        else if(player.blockedDirection == BlockedDirection.Left && Input.GetAxis("Horizontal") > 0)
        {
            player._rb.velocity = new Vector2(Input.GetAxis("Horizontal") * player._speed * Time.deltaTime * 100, player._rb.velocity.y);
            player.blockedDirection = BlockedDirection.None;
        }
        else if (player.blockedDirection == BlockedDirection.Right && Input.GetAxis("Horizontal") < 0)
        {
            player._rb.velocity = new Vector2(Input.GetAxis("Horizontal") * player._speed * Time.deltaTime * 100, player._rb.velocity.y);
            player.blockedDirection = BlockedDirection.None;
        }
    }

    public override void OnChangeState()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            if (player._feet.GetComponent<FeetPlayer>().isGrounded)
            {
                characterStateMachine.ChangeState(player.stateIdle);
            } 
            else
            {
                characterStateMachine.ChangeState(player.stateFalling);
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterStateMachine.ChangeState(player.stateJumping);
        }

        if (Input.GetMouseButtonUp(1))
        {
            characterStateMachine.ChangeState(player.stateBalancing);
        }

    }

}
