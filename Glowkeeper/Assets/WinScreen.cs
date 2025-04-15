using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
 [SerializeField] GameObject winCanvas;

void Start()
{
    winCanvas.SetActive(false);
}
}
