using UnityEngine;

public class Eliptic : ARinteractableObject

{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("No se encontró animator en" + gameObject.name);
        } 
    }

    protected override void SetState(State state)
    {
        base.SetState(state);
        switch(state)
        {
            case State.Active:
                _animator.SetTrigger("StartInteraction");
                Debug.Log("TriggerActivado");
                break;
            case State.Idle:
                _animator.SetTrigger("GoToIdle"); 
                Debug.Log("TriggerDesactivado");
                break;
        }
    }
}
