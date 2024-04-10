using UnityEngine;

public class CharacterStateSliding : CharacterState
{
    public int direction;

    public CharacterStateSliding(int direction) : base()
    {
        this.direction = direction;
    }
    public override void EnterState() { }
    public override void ExitState() { player._rb.velocity = new Vector2(0,0); }
    


}