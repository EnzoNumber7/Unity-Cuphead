using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMoving : CharacterState
{

    public CharacterStateMoving(CharacterStateMachine stateMachine, Player p) : base (stateMachine, p)
    {
        
    }
    public override void EnterState() { }
    public override void ExitState() { }
    public override void UpdateFrame() 
    {
        player._rb.velocity = new Vector2(Input.GetAxis("Horizontal") * player._speed * Time.deltaTime * 100, player._rb.velocity.y);
    }
}
