﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region Attributes
    private bool active; //If the item is active
    private CaptainPirate pirateScript;
    [SerializeField] private int damage;
    private int explosionDamage;
    [SerializeField]
    private GameObject firePrefab;
    #endregion

    #region Properties
    public bool Active
    {
        get
        {
            return active;
        }
        set
        {
            active = value;
        }
    }
    #endregion

    #region InBuiltMethods
    void Start() //Use this for initialization
    {
        //active = true;

        explosionDamage = -1000;
    }

    private void OnCollisionEnter(Collision coll)
    {
        //Dropped items
        if (active && coll.gameObject.tag == "Terrain") //Colliding with the ground
        {
            if (gameObject.name.Contains("Bomb")) //If this is a bomb
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4);

                //For every object in the explosion radius apply damage
                foreach (Collider c in hitColliders)
                {
                    if (c.gameObject.name.Contains("Captain"))
                    {
                        pirateScript = c.gameObject.GetComponent<CaptainPirate>();
                        StartCoroutine(pirateScript.Stun(3));
                    }
                }

                StartCoroutine(ExplosionTimer(.017f));
            }
            else if (gameObject.name.Contains("Lantern") && active == true) // if gameObject is lantern
            {
                // TODO: add stuff for spawning fire on the main island
                GameObject fire = GameObject.Instantiate(firePrefab, this.transform.position, Quaternion.identity);
                fire.GetComponent<Fire>().Ignite();
                Destroy(gameObject);
            }
            else //If this is any other object
            {
                Destroy(gameObject);
            }
        }
        else if (active && coll.gameObject.name.Contains("Captain")) //Colliding with a pirate
        {
            if (gameObject.name.Contains("Bomb")) //If this is a bomb
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4);

                //For every object in the explosion radius apply damage
                foreach (Collider c in hitColliders)
                {
                    if (c.gameObject.name.Contains("Captain"))
                    {
                        pirateScript = c.gameObject.GetComponent<CaptainPirate>();
                        StartCoroutine(pirateScript.Stun(3));
                    }
                }

                StartCoroutine(ExplosionTimer(.017f));
            }
            else if (gameObject.name.Contains("Coconut"))
            {
                pirateScript = coll.gameObject.GetComponent<CaptainPirate>();
                StartCoroutine(pirateScript.Stun(2));

                StartCoroutine(ExplosionTimer(0));
            }
            else //If this is any other object
            {
                Destroy(gameObject);
            }
        }
        else if (active && (coll.gameObject.tag == "IslandPlatform" || coll.gameObject.tag == "MovingPlatform" || coll.gameObject.tag == "RotatingPlatform"))
        {
            if (gameObject.name.Contains("Lantern") && active == true)
            {
                GameObject fire = GameObject.Instantiate(firePrefab, this.transform.position, Quaternion.identity);
                fire.GetComponent<Fire>().Ignite();
                Destroy(gameObject);
            }
            else if (gameObject.name.Contains("Bomb")) //If this is a bomb
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4);

                //For every object in the explosion radius apply damage
                foreach (Collider c in hitColliders)
                {
                    if (c.gameObject.name.Contains("Captain"))
                    {
                        pirateScript = c.gameObject.GetComponent<CaptainPirate>();
                        StartCoroutine(pirateScript.Stun(3));
                    }
                }

                StartCoroutine(ExplosionTimer(.017f));
            }
            else //If this is any other object
            {
                Destroy(gameObject);
            }
        }
    }

    //Make the bomb explode
    internal IEnumerator ExplosionTimer(float time)
    {
        yield return new WaitForSeconds(time);

        transform.position = new Vector3(0, -1000, 0); //Move the object off screen, don't destroy it yet because it's still needed for reference

        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }
    #endregion
}