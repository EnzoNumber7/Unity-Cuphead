using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateAttaking : CharacterState
{

    private bool endAttack;


    public CharacterStateAttaking() : base() {  }


    private void Start()
    {

    }

    public override void EnterState() 
    {
        endAttack = false;
        StartCoroutine(WaitAndReturn(0.5f));
    }

    public override void ExitState()
    {
        player.isAttaking = false;
        player.animator.SetBool("IsAttacking", false);
    }

    public override void UpdateFrame()
    {

        if (player._cac.GetComponent<Cac>().isEnemyForward && endAttack)
        {
            ((Enemy)player._cac.GetComponent<Cac>().enemy.GetComponent<Enemy>()).TakeDamage(1,player.gameObject);
        }

        player._rb.velocity = new Vector2(Input.GetAxis("Horizontal") * player._speed * Time.deltaTime * 100,player._rb.velocity.y);

        base.UpdateFrame();
    }

    public override void OnChangeState()
    {
        if(endAttack)
        {

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
        yield return new WaitForSeconds(attSpeed);
        endAttack = true;
    }
}
