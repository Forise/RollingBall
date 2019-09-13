using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Fields
    public System.Action playerDied;
    public System.Action playerFinished;
    #endregion Fields

    #region Mono Methods
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            playerFinished?.Invoke();
        }
        else if(collision.gameObject.tag == "Deadly")
        {
            playerDied?.Invoke();
        }
    }
    #endregion Mono Methods
}
