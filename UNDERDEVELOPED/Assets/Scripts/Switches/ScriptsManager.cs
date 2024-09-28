using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Properties;
using UnityEngine;

public class ScriptsManager : MonoBehaviour
{
    [SerializeField]
    private List<Behaviour> _components;

    public void EnableComponents()
    {
        foreach (Behaviour component in _components)
        {
            component.enabled = true;
        }
    }

    public void AddComponent(Behaviour component)
    {   
        _components.Add(component);
    }

    public void EnableOneComponent(Behaviour component)
    {
        component.enabled = true;
    }
}
