using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : ScriptableObject
{
    public string Name;
    public float cooldownTime;
    public float activeTime;
    public string StatDescription;
    public bool shopItem;

    public virtual void Info() { }

    public virtual void Activate(GameObject parent){}

    public virtual void BeginCooldown(GameObject parent){}
}
