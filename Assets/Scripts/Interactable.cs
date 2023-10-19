using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;
    public string promptMessage; // ��������Ϣ
    
    public void BaseInteract()
    {
        if (useEvents) 
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }


    protected virtual void Interact()
    {

    }
    
}
