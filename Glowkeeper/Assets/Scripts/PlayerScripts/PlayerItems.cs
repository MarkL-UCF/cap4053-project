using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItems : ScriptableObject 
{
    // Start is called before the first frame update
    public string Name;
    public string StatDescription;
    public Boolean shopItem;

    public virtual void Info() { }
    public virtual void Activate(GameObject parent) { }

}
