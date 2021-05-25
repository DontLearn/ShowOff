using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public GameObject bullet;
    public float visibleDistance = 10;
    public float visibleAngle = 10.0f;
    [HideInInspector]
    public float distance;

    private RaycastHit hitInfo;

    public GameObject GetPlayer()
    {
        return player;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));
        
        castRayFroward();
        fovDetection();
    }
    void Fire()
    {
        GameObject b = Instantiate(bullet, transform.position, transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
    }
    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }
    public void PlayerIsVisible()
    {
        anim.SetBool("IsPlayerVisible", true);
    }
    public void PlayerNotVisible()
    {
        anim.SetBool("IsPlayerVisible", false);
    }

    private void castRayFroward()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        
        if (Physics.Raycast(transform.position, fwd, out hitInfo, visibleDistance))
        {
            Debug.DrawRay(transform.position, fwd * visibleDistance, Color.yellow);
        }
        
    }
    private void fovDetection()
    {
       //if (hitInfo.transform.tag == "Player")
       //{
       //    Debug.Log("asfasgweukjnk");
       //}

        //ray from npc forward with a magnitude = visible distance
        //if hit info is player and angle between this forward and direction from npc o player is < visible angle == player has been spoted
        Vector3 direction = this.transform.forward;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (direction.magnitude < visibleDistance && angle < visibleAngle)
        {
            Debug.Log("asfasgweukjnk");
            direction.y = 0;
            PlayerIsVisible();//enter the chase state
        }
    }
    public RaycastHit GetHitInfo()
    {
        return hitInfo;
    }
    public float GetVisibleAngle()
    {
        return visibleAngle;
    }
}