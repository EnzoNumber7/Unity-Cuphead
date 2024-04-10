using UnityEngine;

public class CharacterStateSliding : CharacterState
{

    public CharacterStateSliding() : base() { }
    public override void EnterState() { }
    public override void ExitState() { player._rb.velocity = new Vector2(0,0); }
    


}