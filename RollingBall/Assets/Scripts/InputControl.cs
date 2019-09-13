using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoSingleton<InputControl>
{
    public event System.EventHandler PlayerSwiped;
    public event System.EventHandler PlayerTouched;

    private void Update()
    {
    }
}