using UnityEngine;

public class CharacterStateSliding : CharacterState
{
    public int direction;

    public CharacterStateSliding(CharacterStateMachine stateMachine, Player p, int direction) : base(stateMachine, p)
    {
        this.direction = direction;
    }
    public override void EnterState() { }
    public override void ExitState() { player._rb.velocity = new Vector2(0,0); }
    


}