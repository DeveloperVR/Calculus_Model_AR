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

        Debug.Log($"{gameObject.name} cambia al estado: {state}");
        base.SetState(state);

        if (_animator == null)
        {
            Debug.LogError("Animator no encontrado");
            return;
        }
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
