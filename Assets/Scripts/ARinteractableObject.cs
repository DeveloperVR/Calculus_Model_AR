using System.Collections.Generic;
using UnityEngine;

public abstract class ARinteractableObject : MonoBehaviour
{
    private List<ARinteractableObject> _interactables = new List<ARinteractableObject>();
    protected enum State
    {
        Idle, Active
    }

    protected State ARObjectState = State.Idle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ARinteractableObject>(out var interactable))
        {
            AddInteractable(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<ARinteractableObject>(out var interactable))
        {
            RemoveInteractable(interactable);
        }
    }

    protected void AddInteractable (ARinteractableObject interactuable)
    {
        _interactables.Add(interactuable);
        Debug.Log("Objeto" + interactuable.name + "añadido");
        SetState(State.Active);
    }

    protected void RemoveInteractable(ARinteractableObject interactuable)
    {
        _interactables.Remove(interactuable);
        SetState(State.Idle);
    }

    private void OnDisable()
    {
        foreach(var interactable in _interactables)
        {
            interactable.RemoveInteractable(this);
        }
        _interactables.Clear();
        SetState(State.Idle);
    }

    protected virtual void SetState(State state)
    {
        ARObjectState = state;
    }
}
