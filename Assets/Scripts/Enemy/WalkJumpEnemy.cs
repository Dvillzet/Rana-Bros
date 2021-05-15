using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkJumpEnemy : Enemy
{

    private void FixedUpdate()
    {
        if (enemyAlive) return;
        if (!detectarPiso()){
            necesitaBrincar = true;
        }
        if (DetectarPared())
        {
            necesitaBrincar = true;
        }
    }
    void Update()
    {
        if (enemyAlive) return;
        Walk();
        if (necesitaBrincar)
        {
            Jump();
            necesitaBrincar = false;
        }
    }
}
