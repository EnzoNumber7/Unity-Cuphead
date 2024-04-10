using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateKunai : CharacterState
{

    private bool endAttack;

    [SerializeField] public float kunaiRadius;
    [SerializeField] public float rangeRadius;

    private Vector2 mousePos;

    [SerializeField] public GameObject firePoint;

    [SerializeField] public GameObject currentKunai;
    [SerializeField] public GameObject Grab;
    //kunai
    [SerializeField] public GameObject Kunai;

    public CharacterStateKunai() : base() { }


    private void Start()
    {

    }

    public override void EnterState()
    {
        endAttack = false;
        Shoot();
    }


    public override void ExitState()
    {
        player.isAttaking = false;
        //player.animator.SetBool("IsAttacking", false);
    }

    public override void UpdateFrame()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endAttack = CheckKunai();

        if (endAttack) //Attaquer avec le kunai
        {
            GameObject target = currentKunai.GetComponent<Kunai>().transform.parent.gameObject;
            if (target.tag == "Enemy") {
                ((Enemy)target.GetComponent<Enemy>()).TakeDamage(3);
            }
        }

        base.UpdateFrame();

    }

    public override void OnChangeState()
    {
        if (endAttack)
        {
            if (player._rb.velocity.y > 0)
            {
                characterStateMachine.ChangeState(player.stateJumping);
            }
            else if (player._rb.velocity.y < 0)
            {
                characterStateMachine.ChangeState(player.stateJumping);
            }
            else if (player._rb.velocity.x != 0)
            {
                characterStateMachine.ChangeState(player.stateMoving);
            }
            else
            {
                characterStateMachine.ChangeState(player.stateIdle);
            }
        }
    }

    private void Shoot()
    {
        float angle = Mathf.Atan2(mousePos.y - firePoint.transform.position.y, mousePos.x - firePoint.transform.position.x) * Mathf.Rad2Deg - 90f;

        firePoint.transform.localRotation = Quaternion.Euler(0, 0, angle);
        currentKunai = Instantiate(Kunai, firePoint.transform.position, firePoint.transform.rotation);
        Grab.GetComponent<Grab>().GetCurrentKunai(currentKunai);
        currentKunai.GetComponent<Kunai>().destiny = mousePos;

    }
    public bool CheckKunai()
    {
        return currentKunai.GetComponent<Kunai>().isAttached;
    }

}
