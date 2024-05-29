using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool doorState = false; //���� ���� true�� open false�� close
    private Animator door_anim;
    private void Start()
    {
        door_anim = GetComponent<Animator>();
    }
    public bool DoorState
    {
        get { return doorState; }
        set { doorState = value; }
    }
    public void Open()
    {
        doorState = true;
        door_anim.SetBool("DoorState", true);
    }

    public void Close()
    {
        doorState = false;
        door_anim.SetBool("DoorState", false);
    }
}
