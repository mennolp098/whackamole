using UnityEngine;
using System.Collections;

public class RemoveParticleSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,particleSystem.startLifetime);
	}

}
