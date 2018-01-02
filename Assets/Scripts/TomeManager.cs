﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tomes;

/** This is the "inventory"
 *	This script will be attatched to the player object
 *	This script handles tome selection using a list
 *
 ** Name conventions for a tome in UNITY:
 *	 - No spaces
 *   - Pascal Case!!!! (i.e. => FireTome)
 */
public class TomeManager : MonoBehaviour {
  private List<Tome> inventory;
  private int tomeIndex;

	public AudioClip cannotUse;
	public AudioClip collectedTome;
	public AudioClip switchTome;
	private AudioSource audioSource;
	private float timer = 0f;
	private float delay = 1.0f;
	private bool playSound = true;


  private Tome current;
	private bool inUse;
  // keeps track of current item - index
  // general tome object = Tome tome;
  // to call functions for each tome: compare the string value of the current index

  // Tome current;
  // ********int index;
  // ********Dictionary indexMap (this guy associates int index to strings to reference inventory)
  // ********some selection has to happen. if we're using Q and E to scroll, we can have an int
  // ********that increments and decrements (index), and we can reference the name of the tome associated
  // ********to that int using the dictionary indexMap
  // ********otherwise we dont need index or indexMap if we use the pausing system bc we can associate
  // ********the tomes themselves in the pause menu to strings (name)
  // name is a String like "fire"
  // inventory["fire"] is the bool associated to the key "fire" in inventory
  // (if inventory[name]) { current = new FireTome; }


  void Start() {
    inventory = new List<Tome>();
    tomeIndex = 0;
		audioSource = GetComponent<AudioSource>();
		//AddTome(gameObject.GetComponent<FireTome>());
		//AddTome(gameObject.GetComponent<StunTome>());
		//AddTome(gameObject.GetComponent<FloatTome>());

		/* current = gameObject.GetComponent<FireTome>(); *///<--- THIS IS HOW YOU CHANGE THE TOME OBJECT TYPE
		inUse = false;
  }

  void Update() {
    /* pressing 'q'/MouseWheel-DOWN decreases tomeIndex, if the index is the first tome it will warp to the last index */
    if(Input.GetKeyDown("q") || Input.GetAxis("Mouse ScrollWheel") < 0) {
      if(inventory.Count > 0) {
		    current.use(false);
			  if(tomeIndex == 0) {
  		    tomeIndex = inventory.Count - 1;
					audioSource.PlayOneShot(switchTome, 0.4f);
  		  }
  		  else {
  		    tomeIndex -= 1;
					audioSource.PlayOneShot(switchTome, 0.4f);
  		  }
			  current = inventory[tomeIndex];
			  /* IF YOU WANT IT SO THAT WAY YOU CAN SWAP BETWEEN TOMES AND CONTINUE USING THE TOMES UNCOMMENT THIS BLOCK BELOW */
			  /*
			  if(Input.GetMouseButton(0)) {
			  	 current.use(true);
			  }
			  */
      }
	  }

		/* pressing 'e'/MouseWheel-UP increases tomeIndex, if the index is the last tome it will warp to the first index */
	  if(Input.GetKeyDown("e") || Input.GetAxis("Mouse ScrollWheel") > 0) {
		  if(inventory.Count > 1) {
			  current.use(false);
  		  if(tomeIndex == inventory.Count - 1) {
  				tomeIndex = 0;
					audioSource.PlayOneShot(switchTome, 0.4f);
  		  }
  		  else {
          tomeIndex += 1;
					audioSource.PlayOneShot(switchTome, 0.4f);
  		  }
      }
			current = inventory[tomeIndex];
			/* IF YOU WANT IT SO THAT WAY YOU CAN SWAP BETWEEN TOMES AND CONTINUE USING THE TOMES UNCOMMENT THIS BLOCK BELOW */
			/*
			if(Input.GetMouseButton(0)) {
				current.use(true);
			}
			*/
	  }

	  /* Could use mouse click */
   /*if(Input.GetMouseButtonDown(0)) {
     // tome.use();
     // snapping for fire tome will go into that method, NOT here
     // this is bc each tome has different behaviour - for example jump tome
     // does not require aiming. no reason to execute that code when it's not needed
     Debug.Log("Used Tome!");
     } */

	   /* To use tomes */
		 if(inventory.Count > 0) {
	     if(Input.GetMouseButtonDown(0)) {
		     current.use(true);
				 current.playSound(true);
	     }
	     if(Input.GetMouseButtonUp(0)) {
		     current.use(false);
				 current.playSound(false);
			 }
		 }
		 else {
			 if(Input.GetMouseButtonDown(0)) {
				 if(playSound) {
				 	 audioSource.PlayOneShot(cannotUse, 0.4f);
					 playSound = false;
				 }
			 }
			 if(!playSound) {
				 timer += 1.0f * Time.deltaTime;
				 if(timer > delay) {
					 playSound = true;
					 timer = 0f;
				 }
			 }
		 }
	    //Debug.Log(tomeIndex);
  }

  void AddTome(Tome newTome) {
		if(inventory.Count > 0) {
			current.use(false);
		}
    inventory.Add(newTome);
    current = newTome; //<----- This makes your new tome the newly acquired tome
		tomeIndex = inventory.Count - 1;
  }

	void OnTriggerEnter2D(Collider2D other) {
		// This first check is here so that way I can just destroy the other.gameObject with one line of code at the bottom of this block o code
		if(other.CompareTag("Tome")) {
			/* We have collided with a tome object so lets add the tome based on the game objects name */
			Debug.Log("Collided with the: " + other.name);
			if(other.name == "FireTome") {
				AddTome(gameObject.GetComponent<FireTome>());
			}
			else if(other.name == "StunTome") {
				AddTome(gameObject.GetComponent<StunTome>());
			}
			else if(other.name == "FloatTome") {
				AddTome(gameObject.GetComponent<FloatTome>());
			}
			/* Combat Tomes:
			else if(other.name == "LazerTome") {
				AddTome(gameObject.GetComponent<LazerTome>());
			}
			else if(other.name == "PunchTome") {
				AddTome(gameObject.GetComponent<PunchTome>());
			}
			else if(other.name == "IceTome") {
				AddTome(gameObject.GetComponent<IceTome>());
			}
			else if(other.name == "ShieldTome") {
				AddTome(gameObject.GetComponent<ShieldTome>());
			}
			else if(other.name == "SuplexTome") {
				AddTome(gameObject.GetComponent<SuplexTome>());
			}
			else if(other.name == "JumpAttackTome") {
				AddTome(gameObject.GetComponent<JumpAttackTome>());
			}
			***Non-Combat Tomes:
			else if(other.name == "StealthTome") {
				AddTome(gameObject.GetComponent<StealthTome>());
			}
			else if(other.name == "FlyTome") {
				AddTome(gameObject.GetComponent<FlyTome>());
			}
			else if(other.name == "BargainingTome") {
				AddTome(gameObject.GetComponent<BargainingTome>());
			}
			else if(other.name == "SpeedBoostTome") {
				AddTome(gameObject.GetComponent<SpeedBoostTome>());
			}
			else if(other.name == "IntimidationTome") {
				AddTome(gameObject.GetComponent<IntimidationTome>());
			}
			else if(other.name == "DisguiseTome") {
				AddTome(gameObject.GetComponent<DisguiseTome>());
			}
			else if(other.name == "HealTome") {
				AddTome(gameObject.GetComponent<HealTome>());
			}
			else if(other.name == "GoofyTome") {
				AddTome(gameObject.GetComponent<GoofyTome>());
			}
			else if(other.name == "InvestigationTome") {
				AddTome(gameObject.GetComponent<InvestigationTome>());
			}
			else if(other.name == "TallTome") {
				AddTome(gameObject.GetComponent<TallTome>());
			}
			else if(other.name == "TinyTome") {
				AddTome(gameObject.GetComponent<TinyTome>());
			}
			else if(other.name == "TimeTome") {
				AddTome(gameObject.GetComponent<TimeTome>());
			}
			***Summoning Tomes:
			else if(other.name == "DeadEnemiesTome") {
				AddTome(gameObject.GetComponent<DeadEnemiesTome>());
			}
			else if(other.name == "GodTome") {
				AddTome(gameObject.GetComponent<GodTome>());
			}

			*/
			Destroy(other.gameObject);
			audioSource.PlayOneShot(collectedTome, 0.4f);
		}
	}
  /* We need something for selection */
	/* Here we will keep track of all the tomes the player has */
}
