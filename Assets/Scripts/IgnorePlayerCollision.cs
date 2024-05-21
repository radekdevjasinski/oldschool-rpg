using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayerCollision : MonoBehaviour
{
    public void setIgnorePlayer(bool state)
    {
        if (state)
        {
            gameObject.layer = LayerMask.NameToLayer("ignorePlayer");
        }
    }
}
