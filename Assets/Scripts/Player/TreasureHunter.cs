﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureHunter : BasePirate
{
    #region Attributes
    private bool pickingUp; //If the treasure pirate is currently picking anything up
    #endregion
    
    #region Properties
    public bool PickingUp
    {
        get
        {
            return pickingUp;
        }
    }
    #endregion
    
    protected override void Start() //Use this for initialization
    {
        base.Start();
	}

    protected override void Update() //Update is called once per frame
    {
        base.Update();
	}

    protected override void FixedUpdate() //Physics updates
    {
        base.FixedUpdate();
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Treasure"  && Input.GetButton("Attack") && pirateActive) //If this pirate is within range of the treasure and tries to pick it up
            Pickup(); //Check for pickup
    }

    #region Methods
    protected override void Dead()
    {
        Debug.Log("Pirate: " + name + "has Died");
        Destroy(gameObject);
    }

    /// <summary>
    /// Let the treasure pirate pick up treasure
    /// </summary>
    private void Pickup()
    {
        pirateAnim.Play("PickupTreasure");
    }
    #endregion
}