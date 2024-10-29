using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Upgrade : ScriptableObject
{
    public virtual void ActivateUpgrade()
    {
    }
}
public abstract class InputButton : ScriptableObject
{

}

//upgrades
// - weapon unlock
//      * weapon index to unlock
//      *
//      * reference to player stats