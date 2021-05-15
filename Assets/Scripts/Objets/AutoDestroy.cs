using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AutoDestroy : MonoBehaviour
{
    public void autoDestruction()
    {
        Destroy(this.gameObject);
    }
}
