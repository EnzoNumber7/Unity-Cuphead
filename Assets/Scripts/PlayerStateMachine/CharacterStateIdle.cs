using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateIdle : CharacterState
{

    public CharacterStateIdle(CharacterStateMachine stateMachine, Player p) : base(stateMachine, p)
    {

    }

    public override void EnterState() { }
    public override void ExitState() { }
    public override void UpdateFrame() { }

}
