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
    public virtual void UpdateFrame() { OnChangeState(); }
    public virtual void OnChangeState() { }

}
