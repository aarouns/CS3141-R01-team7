using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntity : MonoBehaviour{
    // Update is called once per frame
    void Update(){
        if(!HasEnemy)
            findTarget();

        if(!HasEnemy)
            return;

        if(isInRange && !moving)
            Debug.Log("Attack!");
        else
            GetInRange();
    }
}
