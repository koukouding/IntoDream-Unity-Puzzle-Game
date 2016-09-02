using UnityEngine;
using System.Collections;

public class Level0_Door : MonoBehaviour {
	private float OpenSpeed = 10;
	private bool isOpen = true;
	public Light light;
	Rigidbody rigidbody;
	float dragTimes=0;
	AudioSource sound;

	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		sound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

void OnMouseDown()
	{			

		sound.Play();
			dragTimes++;

			if (dragTimes % 2 == 0)
				isOpen = false;
			else
				isOpen = true;
			
			
		light.enabled = !isOpen;
			
			if (isOpen == false) {

			rigidbody.Sleep();
			rigidbody.WakeUp();
			rigidbody.AddForce (Vector3.forward*-10,ForceMode.VelocityChange);

			}
			

			else{
			Debug.Log (dragTimes);
			rigidbody.AddForce (Vector3.forward*10,ForceMode.VelocityChange);
			}
	


	 
	}


}