using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingScript : MonoBehaviour
{
    [SerializeField]
    private PlayerWalkingSound _walkSound;

    private bool test;

    void Start()
    {
        _walkSound = GetComponent<PlayerWalkingSound>();
    }


    private void Update()
    {
        if(Input.GetAxis("Horizontal") >= 0.01f)
        {
            _walkSound.MovementDetection(true);
        }
    }
}
