 using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level01_SceneControl : MonoBehaviour {
	Camera cam;//用于发射射线的相机
	public Camera UIcam;//UI层的相机
	public GameObject mainHints;
	public GameObject objectHints;
	public GameObject Player;

	Vector3 mp;//鼠标位置
	LayerMask layer;//物体所在层
	public string layerName;
	Transform targetTransform;//点选的物体
	public Transform[] hintObject;//需要显示的物体
	int hintNum;//提示数
	UILabel mainLab;
	UILabel objectLab;
	TweenColor color;
	TweenAlpha mainAlpha;
	TweenAlpha objectAlpha;
	bool ifCall=false;
	Vector3 labPos;
	// Use this for initialization
	void Start () {
	
	
		hintNum = hintObject.Length;
	
		layer = 1 << 11;

		cam =this.GetComponent<Camera>();
		mainLab = mainHints.GetComponent<UILabel> ();
		objectLab = objectHints.GetComponent<UILabel> ();

		mainAlpha=mainHints.GetComponent<TweenAlpha>();
		objectAlpha=objectHints.GetComponent<TweenAlpha>();

		Player.GetComponent<Character> ().IsMoving (false);
		string words = "是她！\r\n她怎么会在这里？..";
		StartCoroutine ("MainHints",words);
		Player.GetComponent<Character> ().IsMoving (true);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (doorLight.enabled);
		if (targetTransform == null) 
		{
			objectHints.transform.position = UIcam.ViewportToWorldPoint (labPos);
		}
		if (TarRaycast())  //判断鼠标是否指在某物体上
		{
			labPos=SetPosition ();

			//这里转换坐标的时候我用的是“指定物体y轴方向向上0.3f处”，当然只是大体保证了匹配位置，最好的方式是手动指定，即提前手动拖一个空物体至“有需要点击的物体”下，固定其合适位置，然后坐标用这个空物体的~

			if(targetTransform==hintObject[0])
			{//Boy
				if(!Player.GetComponent<Character>().isHeartChanged)
				{
				
				objectLab.text="我愿意把心给她";

				
				}
				else
				{
					objectLab.text="我爱你";
				}
				//objectLab.color=new Color(0,0,0,255);
				objectLab.enabled=true;
				ColorFade(objectAlpha);
			}
			if(targetTransform==hintObject[1])
			{//Girl
				if(!Player.GetComponent<Character>().isHeartChanged)
				{
				
				objectLab.text="我的前女友，\r\n她以为我不爱她";
				}
				else
				{
					objectLab.text="我爱你";
				}
				//objectLab.color=new Color(255,255,255,255);
					objectLab.enabled=true;
				ColorFade(objectAlpha);
				}

			if(targetTransform==hintObject[2])
			{//门
				//objectLab.color=new Color(0,0,0,255);
				objectLab.text="点击门离开";
				objectLab.enabled=true;
				ColorFade(objectAlpha);
				if(Input.GetMouseButton(0))
				{
					Application.LoadLevel("ToBeContinued");
				}
				
			}

			//当心脏交换，出现提示；
			if(Player.GetComponent<Character>().isHeartChanged&&ifCall)
			{
				string s="继续往前走吧，\r\n我的爱人。";
				StartCoroutine("MainHints",s);
				ifCall=false;
			}

		}
	

		
	}

	public bool TarRaycast()
	{
		
	
		mp = Input.mousePosition;
		mp.z = 1;
		targetTransform = null;
		if (cam != null) {
			RaycastHit hitInfo; 
			Ray ray = cam.ScreenPointToRay (new Vector3 (mp.x, mp.y, 1f));
			if (Physics.Raycast (ray.origin, ray.direction, out hitInfo, 1000, layer)) {
				//,200,layer))
				//			if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition).origin,
				//			                cam.ScreenPointToRay(Input.mousePosition).direction, out hitInfo, 100,
				//			                layer))
				
				targetTransform = hitInfo.collider.transform;
				return true;
				
			}
		} 
		

		return false;
		
		
	}

	Vector3 SetPosition()
	{

		Vector3 pos = cam.WorldToViewportPoint (targetTransform.position+new Vector3(0,2f, 0));
	
		pos.z = 1;
		objectHints.transform.position = UIcam.ViewportToWorldPoint (pos);
//		Vector3 viewPos=UIcam.ViewportToWorldPoint (pos);

		return pos;
	}
//	void ImageColorDecay(Image image,Color toColor)
//	{
//		image.color = Color.Lerp ( image.color,toColor, colorDecay );
//	}
//	
	IEnumerator MainHints(string words)
	{

			yield return new WaitForSeconds (1);
		mainAlpha.ResetToBeginning ();
		mainLab.text = words;	
		mainAlpha.enabled = true;
		mainAlpha.Play();
		yield return new WaitForSeconds (mainAlpha.duration);

		}
	void ColorFade(TweenAlpha objectAlpha)
	{
		objectAlpha.enabled = false;
		objectAlpha.ResetToBeginning ();
		objectAlpha.enabled = true;
		objectAlpha.Play ();
	}

	TweenAlpha SetHint(string text)
	{
		GameObject hint;

//		hint=Instantiate (objectHints, labPos, Quaternion.identity)as GameObject;
//		hint.transform.position = labPos;
//
//		TweenAlpha objectAlpha;
//		objectAlpha=objectHints.GetComponent<TweenAlpha>();
//		UILabel objectlab;
//		objectLab = objectHints.GetComponent<UILabel> ();
		objectLab.text = text;

		return objectAlpha;

	}


	}

