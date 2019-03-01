﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // delegate signatures
    public delegate void EventSettingBlock(Vector3 v, int blockType);
    public static event EventSettingBlock OnEventBlockSet;

    public VoxelChunk voxelChunk;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Handling Block Placement & Removal
		if (Input.GetButtonDown("Fire1"))
        {
            Vector3 v;
            if (PickBlock(out v, 4, false))
            {
                OnEventBlockSet(v, 0);
            }
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Vector3 v;
            if (PickBlock(out v, 4, true))
            {
                Debug.Log(v);
                OnEventBlockSet(v, 1);
            }
        }

        //Player Respawn
        if (transform.position.y <= -10)
        {
            this.transform.position = new Vector3(0, 7, 0);
        }
	}

    bool PickThisBlock(out Vector3 v, float dist)
    {
        v = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(
        Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist))
        {
            // offset towards the centre of the block hit
            v = hit.point - hit.normal / 2;
            // round down to get the index of the block hit
            v.x = Mathf.Floor(v.x);
            v.y = Mathf.Floor(v.y);
            v.z = Mathf.Floor(v.z);
            return true;
        }

        return false;
    }

    bool PickEmptyBlock(out Vector3 v, float dist)
    {
        v = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(new
        Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist))
        {
            // offset towards centre of the neighbouring block
            v = hit.point + hit.normal / 2;
            // round down to get the index of the empty
            v.x = Mathf.Floor(v.x);
            v.y = Mathf.Floor(v.y);
            v.z = Mathf.Floor(v.z);
            return true;
        }
        return false;
    }

   bool PickBlock(out Vector3 v, float dist, bool placing)
   {
        v = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(new
        Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist))
        {
            if (placing)
            {
                // offset towards centre of the neighbouring block
                v = hit.point + hit.normal / 2;

                // round down to get the index of the empty
                v.x = Mathf.Floor(v.x);
                v.y = Mathf.Floor(v.y);
                v.z = Mathf.Floor(v.z);
            }
            else
            {
                // offset towards the centre of the block hit
                v = hit.point - hit.normal / 2;

                // round down to get the index of the block hit
                v.x = Mathf.Floor(v.x);
                v.y = Mathf.Floor(v.y);
                v.z = Mathf.Floor(v.z);
            }
            return true;
        }

        return false;
    }
}