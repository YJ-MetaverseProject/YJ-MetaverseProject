using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : Door
{
    Animator doubledoor_anim;

    private void Start()
    {
        doubledoor_anim = GetComponent<Animator>();
    }
    private void Update()
    {

    }
}
