using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour {
	const float k_Spring = 50.0f;
	const float k_Damper = 5.0f;
	const float k_Drag = 10.0f;
	const float k_AngularDrag = 5.0f;
	const float k_Distance = 0.2f;
	const bool k_AttachToCenterOfMass = false;
	LayerMask mask=1<<10;
	private SpringJoint m_SpringJoint;
	
	
	private void Update()
	{
		// Make sure the user pressed the mouse down
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		
		var mainCamera = FindCamera();
		
		// We need to actually hit an object
		RaycastHit hit = new RaycastHit();
		if (
			!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
		                 mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, 100,
		                 mask))
		{
			return;
		}
		// We need to hit a rigidbody that is not kinematic
		if (!hit.rigidbody || hit.rigidbody.isKinematic)
		{
			return;
		}
		
		if (!m_SpringJoint)
		{
			var go = new GameObject("Rigidbody dragger");
			Rigidbody body = go.AddComponent<Rigidbody>();
			m_SpringJoint = go.AddComponent<SpringJoint>();
			body.isKinematic = true;
		}
		
		m_SpringJoint.transform.position = hit.point;
		m_SpringJoint.anchor = Vector3.zero;
		
		m_SpringJoint.spring = k_Spring;
		m_SpringJoint.damper = k_Damper;
		m_SpringJoint.maxDistance = k_Distance;
		m_SpringJoint.connectedBody = hit.rigidbody;
		
		StartCoroutine("DragObject", hit.distance);
	}
	
	
	private IEnumerator DragObject(float distance)
	{
		var oldDrag = m_SpringJoint.connectedBody.drag;
		var oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
		m_SpringJoint.connectedBody.drag = k_Drag;
		m_SpringJoint.connectedBody.angularDrag = k_AngularDrag;
		var mainCamera = FindCamera();
		while (Input.GetMouseButton(0))
		{
			var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			m_SpringJoint.transform.position = ray.GetPoint(distance);
			yield return null;
		}
		if (m_SpringJoint.connectedBody)
		{
			m_SpringJoint.connectedBody.drag = oldDrag;
			m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
			m_SpringJoint.connectedBody = null;
		}
	}
	
	
	private Camera FindCamera()
	{
		if (GetComponent<Camera>())
		{
			return GetComponent<Camera>();
		}
		
		return Camera.main;
	}
}
//以下是自己写的代码1
//	Vector2 mouseChange;
//	
//	
//	Vector3 originalPosition;
//	
//	void Start () {
//		
//		originalPosition = transform.position;
//	}
//	
//	
//	void Update () {
//		
//		
//	}
//	
//	IEnumerator OnMouseDown()  
//	{  
//		
//		//将物体由世界坐标系转化为屏幕坐标系 ，由vector3 结构体变量ScreenSpace存储，以用来明确屏幕坐标系Z轴的位置  
//		Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);  
//		//完成了两个步骤，1由于鼠标的坐标系是2维的，需要转化成3维的世界坐标系，2只有三维的情况下才能来计算鼠标位置与物体的距离，offset即是距离  
//		Vector3 offset = transform.position-Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,ScreenSpace.z));  
//		
//		//得到现在鼠标的2维坐标系位置  
//		
//		
//		while(Input.GetMouseButton(0)) {  
//			Vector3 curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);     
//			//将当前鼠标的2维位置转化成三维的位置，再加上鼠标的移动量  
//			Vector3 CurPosition = Camera.main.ScreenToWorldPoint (curScreenSpace) + offset;   
//			//当鼠标左键按下时  
//			//CurPosition就是物体应该的移动向量赋给transform的position属性       
//			
//			
//			transform.position =CurPosition;
//			
//			
//			
//			
//			//这个很主要  
//			yield return 0;  
//			
//		}
//		
//		
//		
//	}
	//以下是物体贴地面移动代码.
	
//	//声明从鼠标发出一条射线clickRay
//	Ray clickRay;
//	
//	//声明clickRay与游戏物体的碰撞
//	RaycastHit clickPoint;
//	
//	//声明clickRay与地面的碰撞
//	RaycastHit posPoint;
//	//设置地面层，我的地面层是第8层，所以是8。不会设置层的话请看下边的Tips。
//	LayerMask mask=1<<8;
//	
//	void Start () {
//	}
//	
//	void Update(){
//		
//		clickRay=Camera.main.ScreenPointToRay(Input.mousePosition);
//	}
//	void OnMouseDown()
//	{
//		//如果射线与物体相碰，则调用OnMouseDrag()
//		if(Physics.Raycast (clickRay,out clickPoint))
//		{
//			OnMouseDrag();
//		}
//	}
//	void OnMouseDrag()
//	{
//		//取射线与地面相碰的坐标，赋给mouseMove,再把mouseMove的x坐标和z坐标赋给物体，y坐标不变（因为是贴在地面上移动）
//		Physics.Raycast (clickRay ,out posPoint,Mathf.Infinity,mask.value);
//		Vector3 mouseMove=posPoint.point;
//		transform.position = (new Vector3 (mouseMove.x, transform.position.y, mouseMove.z));
//		return;
//	}
//}


	//以下代码出现原因：因为想要拖动的物体的碰撞器被别的物体遮住了，导致onmousedown没法用，而我不知道原因，又自己参考unity自带的DragRigidbody写了一个。
    //但是这个代码仍然无法筛选不同的碰撞体

//	Vector2 mouseChange;
//	Vector3 originalPosition;
//	Transform  hittedTransform;
//
//	
//	private void Update()
//	{
//		// Make sure the user pressed the mouse down
//		if (!Input.GetMouseButtonDown(0))
//		{
//			return;
//		}
//		
//		var mainCamera = FindCamera();
//		
//		// We need to actually hit an object
//		RaycastHit hit = new RaycastHit();
//		if (
//			!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
//		                 mainCamera.ScreenPointToRay(Input.mousePosition).direction, out hit, 100,
//		                 Physics.DefaultRaycastLayers))
//		{
//			return;
//		}
//		// We need to hit a rigidbody that is not kinematic
//		if (!hit.rigidbody || hit.rigidbody.isKinematic)
//		{
//			return;
//		}
//
//		hittedTransform = hit.collider.transform;
//		StartCoroutine("DragObject", hit.distance);
//	}
//	
//	
//	private IEnumerator DragObject(float distance)
//	{
//
//		var mainCamera = FindCamera();
//		while (Input.GetMouseButton(0))
//		{
//			var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
//			hittedTransform.position = ray.GetPoint(distance);
//			yield return null;
//		}
//
//	}
//	
//	
//	private Camera FindCamera()
//	{
//		if (GetComponent<Camera>())
//		{
//			return GetComponent<Camera>();
//		}
//		
//		return Camera.main;
//	}
//
//
//	void Start () {
//		originalPosition = transform.position;
//	}
//	
//
//
//	}


