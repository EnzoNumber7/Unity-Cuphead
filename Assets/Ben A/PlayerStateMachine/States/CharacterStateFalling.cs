using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateFalling : CharacterState
{

    public float fallMultiplier;

    public CharacterStateFalling(CharacterStateMachine stateMachine, Player p, float fallMulti) : base(stateMachine, p) { fallMultiplier = fallMulti; }
    
    public override void EnterState() { }

    public override void ExitState() {
        //Potientielement ajouter des particules sur le player � l'aterissage
    }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        player._rb.velocity = new Vector2(Input.GetAxis("Horizontal") * player._speed * Time.deltaTime * 100, player._rb.velocity.y);

        player._rb.velocity += Vector2.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;

    }

    public override void OnChangeState()
    {
        if (player._feet.GetComponent<FeetPlayer>().isGrounded)
        {
            characterStateMachine.ChangeState(player.stateIdle);
        }
    }
}