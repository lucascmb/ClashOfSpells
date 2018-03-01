using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    private ParticleSystem ps;
    private ParticleSystem.EmissionModule pe;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        pe = ps.emission;
	}
	
	// Update is called once per frame
	void Update () {
        pe.rateOverTimeMultiplier = pe.rateOverTimeMultiplier + 5f;
	}
}
