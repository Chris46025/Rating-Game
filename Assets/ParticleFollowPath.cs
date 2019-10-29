using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPath : MonoBehaviour {

	public string pathName;
	public float time;

	public void Blast(){
		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath (pathName), "easeType", iTween.EaseType.easeInOutSine, "time", time));
	}

}
