using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateBalancing : CharacterState
{
    [SerializeField] private GameObject feet;
    [SerializeField] private GameObject kunai;
    public CharacterStateBalancing() : base()
    {

    }

    public override void EnterState() { }
    public override void ExitState() { }
    public override void UpdateFrame()
    {

        base.UpdateFrame();
    }

    public override void OnChangeState()
    {
        if (feet.GetComponent<FeetPlayer>().isGrounded == true && kunai.GetComponent<Kunai>().isAttached == false)
        {
            characterStateMachine.ChangeState(player.stateIdle);
        }
    }

}
