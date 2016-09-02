using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour {
	public Light light;
	Vector2 mouseChange;
	bool ifLight;
	float dragTimes;
	Rigidbody rigidbody;
	Vector3 originalPosition;
	
	void Start () {
		dragTimes = 0;
		rigidbody = GetComponent<Rigidbody> ();
		originalPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
	
	}

	
	IEnumerator OnMouseDrag()
	{
		Debug.Log ("mouse drag");
		if (Input.GetMouseButtonUp (0)) {
			dragTimes++;
			if (dragTimes % 2 == 0)
				ifLight = true;
			else
				ifLight = false;
			yield return new WaitForFixedUpdate ();
			light.enabled = ifLight;
		}
	}
}
