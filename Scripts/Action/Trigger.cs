using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_TriggerType
{
    Trigger_Bool,
    Trigger_Object,
}
public class Trigger : MonoBehaviour
{
    [SerializeField]
    public ENUM_TriggerType ENUM_TriggerType = ENUM_TriggerType.Trigger_Bool;

    public bool triggerBool = false;
    public GameObject triggerObject = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        switch (ENUM_TriggerType)
        {
            case ENUM_TriggerType.Trigger_Bool:
                if(triggerBool == false)
                    triggerBool = true;
                break;
            case ENUM_TriggerType.Trigger_Object:
                triggerObject.SetActive(true);
                break;
            default:
                break;
        }
            
    }
}
