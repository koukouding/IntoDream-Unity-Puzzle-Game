using UnityEngine;
using System.Collections;

public class JointTest : MonoBehaviour {
	
	//链接关节游戏对象  
	GameObject connectedObj = null;  
	//当前链接的关节组件  
	Component jointComponent = null;  
	
	void Start()  
	{     
		//获得链接关节的游戏对象  
		connectedObj = GameObject.Find("Cube1");  
	}  
	
	void OnGUI()  
	{  
		if(GUILayout.Button("添加链条关节"))  
		{  
			
			ResetJoint();  
			jointComponent = gameObject.AddComponent<HingeJoint>();  
			HingeJoint hjoint = (HingeJoint)jointComponent;  
			connectedObj.GetComponent<Rigidbody>().useGravity = true;  
			hjoint.connectedBody = connectedObj.GetComponent<Rigidbody>();  
		}  
		
		if(GUILayout.Button("添加固定关节"))  
		{  
			ResetJoint();  
			jointComponent =gameObject.AddComponent<FixedJoint>();  
			FixedJoint fjoint = (FixedJoint)jointComponent;  
			connectedObj.GetComponent<Rigidbody>().useGravity = true;  
			fjoint.connectedBody = connectedObj.GetComponent<Rigidbody>();  
		}  
		
		if(GUILayout.Button("添加弹簧关节"))  
		{  
			ResetJoint();  
			jointComponent =gameObject.AddComponent<SpringJoint>();  
			SpringJoint sjoint = (SpringJoint)jointComponent;  
			connectedObj.GetComponent<Rigidbody>().useGravity = true;  
			sjoint.connectedBody = connectedObj.GetComponent<Rigidbody>();  
		}  
		
		if(GUILayout.Button("添加角色关节"))  
		{  
			ResetJoint();  
			jointComponent =gameObject.AddComponent<CharacterJoint>();  
			CharacterJoint cjoint = (CharacterJoint)jointComponent;  
			connectedObj.GetComponent<Rigidbody>().useGravity = true;  
			cjoint.connectedBody = connectedObj.GetComponent<Rigidbody>();  
		}  
		
		if(GUILayout.Button("添加可配置关节"))  
		{  
			ResetJoint();  
			jointComponent =gameObject.AddComponent<ConfigurableJoint>();  
			ConfigurableJoint cojoint = (ConfigurableJoint)jointComponent;  
			connectedObj.GetComponent<Rigidbody>().useGravity = true;  
			cojoint.connectedBody = connectedObj.GetComponent<Rigidbody>();  
		}  
	}  
	
	//重置关节  
	void ResetJoint(){  
		//销毁之前添加的关节组件  
		Destroy (jointComponent);  
		//重置对象位置  
		this.transform.position = new Vector3(821.0f,72.0f,660.0f);  
		connectedObj.gameObject.transform.position = new Vector3(805.0f,48.0f,660.0f);  
		//不敢应重力  
		connectedObj.GetComponent<Rigidbody>().useGravity = false;  
	}  
}
