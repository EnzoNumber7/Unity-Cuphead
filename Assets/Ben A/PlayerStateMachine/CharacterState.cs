using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{

    protected CharacterStateMachine characterStateMachine;
    protected Player player;

    public void Awake()
    {
        characterStateMachine = gameObject.transform.parent.GetComponent<Player>().stateMachine;
        player = gameObject.transform.parent.GetComponent<Player>();
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void UpdateFrame() { OnChangeStateKunai();  OnChangeState(); }
    public virtual void OnChangeStateKunai() {

        if (Input.GetMouseButtonDown(0) && player.isUsed == false)
        {
            characterStateMachine.ChangeState(player.stateKunai);
            player.isUsed = true;
        }
        if (player.stateKunai.currentKunai != null)
        {
            if (Input.GetMouseButtonDown(1) && player.isUsed == true)
            {
                player.stateKunai.currentKunai.GetComponent<Kunai>().ReturnToPlayer(transform.position);
            }
            if (Input.GetKeyDown(KeyCode.Q) && player.stateKunai.currentKunai.GetComponent<Kunai>().isAttached)
            {
                player.stateKunai.currentKunai.GetComponent<Kunai>().Detach(transform.position);
            }
        }

    }

    public virtual void OnChangeState() { }

}
