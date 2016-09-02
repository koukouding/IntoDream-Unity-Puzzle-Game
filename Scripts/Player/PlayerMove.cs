 using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public float speed=6f;
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 10000f;
	
	void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
		playerRigidbody = GetComponent <Rigidbody> ();
	}
	
	
	void FixedUpdate()
	{
		Move ();
	}
	
	void Move()
	{

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		//creat a ray,floorMask is used to make the ray only hit the floor
		if(Input.GetMouseButton(0))
		{
		

			if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
			{
			
				//because the y axies of floorHit is lower than the charater,so
				Vector3 mouse=new Vector3(floorHit.point.x,transform.position.y,floorHit.point.z);
				transform.LookAt(mouse);
				transform.Translate(Vector3.forward*speed*Time.deltaTime);
		}
		}
	}



	//坐标法

	/*public float speed=20f;
	Rigidbody playerRigidbody;
	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move ();
	}
	/*下面的移动效果并不理想。
	void  Move()  
	{
		//将物体由世界坐标系转化为屏幕坐标系 ，由vector3 结构体变量ScreenSpace存储，以用来明确屏幕坐标系Z轴的位置  
		Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);  
		//完成了两个步骤，1由于鼠标的坐标系是2维的，需要转化成3维的世界坐标系，2只有三维的情况下才能来计算鼠标位置与物体的距离，offset即是距离  
		//Vector3 offset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,ScreenSpace.z))-transform.position;  
		//Vector3 direction = Vector3.Normalize (offset);
		
		//鼠标的位置.
		Vector3 mousePosition = Input.mousePosition;
		Vector3 curScreenSpace = new Vector3 (mousePosition.x, mousePosition.y, ScreenSpace.z);     
		//将当前鼠标的2维位置转化成三维的位置，再加上鼠标的移动量  
		Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenSpace) ;
		Vector3 originPosition = transform.position;
		Vector3 moveDirection = curPosition - originPosition;
		if(Input.GetMouseButton(0)) 
		{ 
			//Debug.Log (CurPosition);
			
			playerRigidbody.MovePosition (originPosition+moveDirection.normalized*Time.deltaTime*speed );
			
			//transform.LookAt(CurPosition);
			
			//transform.Translate(transform.forward *2);
			
			
		}
		
	}*/
}