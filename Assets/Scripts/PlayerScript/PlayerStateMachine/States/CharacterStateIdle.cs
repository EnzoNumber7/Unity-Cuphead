using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateIdle : CharacterState
{

    public CharacterStateIdle() : base()
    {

    }

    public override void EnterState() { }
    public override void ExitState() { }
    public override void UpdateFrame() {

        base.UpdateFrame();
    }

    public override void OnChangeState()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            characterStateMachine.ChangeState(player.stateMoving);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterStateMachine.ChangeState(player.stateJumping);
        }

    }

}
