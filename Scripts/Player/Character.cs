using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
public class Character : MonoBehaviour {
	 public GameObject  thisHeart;
	public GameObject otherHeart;
	public bool isHeartFallCalled = false;//只调用一次HeartFall函数.
	public float distanceWithHeart;
	public bool isHeartChanged = false;
	Vector3 offset;
	GameObject otherCharacter;
	void Update()
	{
		Vector3 positionChange = transform.position - otherHeart.transform.position;
		Vector3 position2 = transform.position - thisHeart.transform.position;//用于检测心脏是否被自己遮住.

		//当交换心脏函数被启动，检测是否有心脏没物归原位.
		if (isHeartChanged) 
		{
			if(positionChange.magnitude!=0)
			otherHeart.transform.position=transform.position;
			//双方心脏归位后，才可以行走.
			if(thisHeart.transform.position==otherCharacter.transform.position)
				StartCoroutine("StartMove");
//			if(GetComponent<Collider>()!=null)
//			{
//			GetComponent<Collider>().isTrigger=false;
//			}
//			if(otherHeart.GetComponent<Collider>()!=null)
//			{
//			otherHeart.GetComponent<Collider>().isTrigger=true;
//			}
		}
		//Debug.Log (positionChange.magnitude);
		//遮挡检测.
		if (isHeartFallCalled&!isHeartChanged)
		{
		
			if(positionChange.magnitude<1.95)
			{
				ExchangeHeart();
			}
//			if(thisHeart.transform.position.z>transform.position.z)
//			{
//				IfCovered ();
//			}
			//Debug.Log(position2.magnitude);
			if (position2.magnitude < 1.95) {
				IfCovered ();
			}
		}


	}

	void OnTriggerEnter(Collider otherObject)
	{

		if (otherObject.tag == "Player"&&isHeartFallCalled==false) 
		{
			otherCharacter=otherObject.gameObject;
			//Destroy (GetComponent<BoxCollider>());
			Destroy(otherObject);//去除初次碰撞检测器.
			IsMoving(false);

			//StartCoroutine("Turn",otherCharacter);

			StartCoroutine(HeartFall(thisHeart,otherHeart));

		}


		if (isHeartFallCalled&&otherObject.tag == "Object"&&isHeartChanged==false) 
		{
			if(otherObject.gameObject==otherHeart)
			{
			ExchangeHeart();
			
			}

		}



	}
	/// <summary>
	/// 心脏掉落后，解除父子关系，并实现从人体中跳出的效果.
	/// </summary>
	/// <returns>The fall.</returns>
	/// <param name="Heart">Heart.</param>
	/// <param name="other">Other.</param>
	IEnumerator HeartFall(GameObject Heart,GameObject other)
	{
		if (Heart.GetComponent <Rigidbody > () == null)
		{
			Heart.AddComponent<Rigidbody> ();
		}
		Rigidbody rigidbody=Heart.GetComponent <Rigidbody >();

		Heart.transform.SetParent(null) ;
		offset =other.transform .position-Heart .transform .position;

		Turn (otherCharacter);//面向对象.
		yield return new WaitForSeconds (2);
		float moveamt= 80;
	

		rigidbody.AddForce (offset.normalized *moveamt);
		yield return 0;
		rigidbody .useGravity = true;

		yield return new WaitForSeconds (2);




		rigidbody.Sleep();
		 
		yield return new WaitForSeconds (1);
		Heart.AddComponent<MouseDrag>();
		isHeartFallCalled=true;

	}
	void ExchangeHeart()
	{

//		Vector3 offset = otherHeart.transform .position - transform.position;
//		Debug.Log (offset.magnitude);
//		if (offset.magnitude < distanceWithHeart) 
		//		{
		otherHeart.transform.position=transform.position;
			Rigidbody rigidbody=otherHeart.GetComponent <Rigidbody >();
			rigidbody.useGravity=false;
		rigidbody.Sleep ();
		isHeartChanged=true;
		//}
	}



	public void IsMoving(bool isMoving)
	{
		if (GetComponent<PlayerMove> () != null) 
		{

			GetComponent<PlayerMove> ().enabled = isMoving;
		}
	}

	void IfCovered()
	{
		Rigidbody rigidbody=thisHeart.GetComponent <Rigidbody >();
		
		thisHeart.transform.SetParent(null) ;
		offset =otherHeart.transform .position-thisHeart.transform .position;
		
		
		float moveamt= 100;
		
		
		rigidbody.AddForce (offset.normalized *moveamt);
	}

	IEnumerator StartMove()
	{
		yield return new WaitForSeconds (1);
		IsMoving (true);
	}

	void Turn(GameObject other)
	{
		float time = 0;
		float smooth=3;
		Vector3 direction = other.transform.position - transform.position;
//		while (time<=smooth)
//		{
//			time++;
//			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime);
//			yield return 0;
//		}
		iTween.LookTo (other,transform.position,smooth);
	}


}
	