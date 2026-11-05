using UnityEngine;

public class Hyperbolic : ARinteractableObject

{
    private Animator _animator;

    private void Onable()
    {
        _animator = GetComponent<Animator>();       
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
                _animator.SetTrigger("GoToIdLe");
                Debug.Log("TriggerDesactivado");
                break;
        }
    }
}
