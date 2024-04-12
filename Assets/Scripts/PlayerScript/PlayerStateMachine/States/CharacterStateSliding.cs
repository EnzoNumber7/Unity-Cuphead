using UnityEngine;

public class CharacterStateSliding : CharacterState
{

    public CharacterStateSliding() : base() { }
    public override void EnterState() { }
    public override void ExitState() { player._rb.velocity = new Vector2(0,0);}
    public override void UpdateFrame()
    {
        base.UpdateFrame();

        player._rb.velocity += new Vector2(0, player._rb.velocity.y / 2);
        
    }

    public override void OnChangeState()
    {
        if (player._feet.GetComponent<FeetPlayer>().isGrounded)
        {
            characterStateMachine.ChangeState(player.stateIdle);
        }

        //if (player._rightSide.GetComponent<LeftSide>().isTriggering == false)
        //{
        //    characterStateMachine.ChangeState(player.stateFalling);
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterStateMachine.ChangeState(player.stateWallJumping);
        }
        if (Input.GetMouseButtonUp(1))
        {
            player.stateMachine.ChangeState(player.stateBalancing);
        }
    }

}