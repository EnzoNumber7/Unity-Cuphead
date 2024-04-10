using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateAttaking : CharacterState
{

    private bool endAttack = false;


    public CharacterStateAttaking() : base() {  }


    private void Start()
    {

    }

    public override void EnterState() 
    {
        print("debut att");

        StartCoroutine(Hein());

        print("vraiment debut att");

        StartCoroutine(WaitAndReturn(0.5f));
    }

    public override void ExitState()
    {
        player.isAttaking = false;
    }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        if (player._cac.GetComponent<Cac>().isEnemyForward)
        {
            ((Enemy)player._cac.GetComponent<Cac>().enemy.GetComponent<Enemy>()).TakeDamage(1);
        }
    }

    public override void OnChangeState()
    {
        if(endAttack)
        {
            print("fin att");

            if (player._rb.velocity.y > 0)
            {
                characterStateMachine.ChangeState(player.stateJumping);               
            }
            else if(player._rb.velocity.y < 0)
            {
                characterStateMachine.ChangeState(player.stateJumping);               
            }
            else if(player._rb.velocity.x != 0)
            {
                characterStateMachine.ChangeState(player.stateMoving);
            }
            else
            {
                characterStateMachine.ChangeState(player.stateIdle);
            }
        }
    }

    public IEnumerator WaitAndReturn(float attSpeed)
    {
        print("coroutine oue");
        yield return new WaitForSeconds(attSpeed);
        endAttack = true;
    }

    public IEnumerator Hein()
    {
        print("go");
        yield return new WaitForSeconds(1);
        print("ze");
    }

}
