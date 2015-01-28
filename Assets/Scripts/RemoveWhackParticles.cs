using UnityEngine;
using System.Collections;

public class RemoveWhackParticles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,1f);
	}
}
