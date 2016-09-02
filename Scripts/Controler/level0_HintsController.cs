using UnityEngine;
using System.Collections;

public class level0_HintsController : MonoBehaviour {
	Camera cam;//用于发射射线的相机
	public Camera UIcam;//UI层的相机
	public GameObject Label;
	Vector3 mp;//鼠标位置
	LayerMask layer;//物体所在层
	public string layerName;
	Transform targetTransform;//点选的物体
	public Transform[] hintObject;//需要显示的物体
	int hintNum;//提示数
	 UILabel lab;
	TweenColor color;
	TweenAlpha objectAlpha;

	void Start ()
	{

	
		hintNum = hintObject.Length;
		//layer = LayerMask.GetMask ("srting");
		layer = 1 << 11;
		//Debug.Log (layer.value);
		cam =this.GetComponent<Camera>();
	    lab = Label.GetComponent<UILabel> ();
		color = Label.GetComponent<TweenColor>();
		objectAlpha=Label.GetComponent<TweenAlpha>();
		//color.enabled = false;
		//writerEff = Label.GetComponent<TypewriterEffect> ();
		//UIcam = GameObject.Find("Camera").GetComponent<Camera>();
		//Lab = GameObject.Find("Label").GetComponent<UILabel>();
		//EventDelegate.Add (writerEff.onFinished, ColorFade);//字体打完后，让文字渐隐；
	}
	
	
	void Update()
	{
		//Debug.Log (lab.color);
	
		if (targetTransform == null) 
		{
			//lab.enabled=false;


		}

		if (TarRaycast())  //判断鼠标是否指在某物体上
		{

			//这里转换坐标的时候我用的是“指定物体y轴方向向上0.3f处”，当然只是大体保证了匹配位置，最好的方式是手动指定，即提前手动拖一个空物体至“有需要点击的物体”下，固定其合适位置，然后坐标用这个空物体的~
			SetPosition();
			if(targetTransform==hintObject[0])
				{//灯.

				color.from=new Color(255,255,255,255);
			
				lab.text="吊灯好亮，\r\n拉旁边的线可以关掉";
				lab.enabled=true;
				ColorFade();
				}
			if(targetTransform==hintObject[1])
			{//门.
				color.from=new Color(255,255,255,255);
				lab.text="我的房间门，\r\n点击可以开关门";
				lab.enabled=true;
				ColorFade();
			}
			if(targetTransform==hintObject[2])
			{//桌子
				color.from=new Color(0,0,0,255);
				lab.text="实木桌，很结实。";
				lab.enabled=true;
				ColorFade();
			}
			if(targetTransform==hintObject[3])
			{//杯子
				color.from=new Color(255,255,255,255);
				lab.text="杯子里已经没有水了。";
				lab.enabled=true;
				ColorFade();
			}
			if(targetTransform==hintObject[4])
			{//药
				color.from=new Color(0,0,0,255);
				lab.text="百忧解，抗抑郁的药。今晚已经吃过了。";
				lab.enabled=true;
				ColorFade();
			}

			//Vector3 pos = cam.WorldToViewportPoint(targetTransform.position + new Vector3(0, targetTransform.localScale.y / 2 + 0.3f, 0));
			//Lab.transform.position = UIcam.ViewportToWorldPoint(pos);
			//Lab.text = targetTransform.name;
		}
	}
	//射线检测部分，不懂可看我之前的文章~
	public bool TarRaycast()
	{

		//Lab.text = null;
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

		targetTransform = null;
	    return false;


	}
	void ColorFade()
	{
		objectAlpha.ResetToBeginning ();
		objectAlpha.enabled = true;
		objectAlpha.Play ();
	}

	void SetPosition()
	{
		Vector3 pos = cam.WorldToViewportPoint (targetTransform.position);
		//Lab.transform.position = UIcam.ViewportToWorldPoint(pos);
		//Lab.text = targetTransform.name;

		if (targetTransform == hintObject [1]) {
			pos = cam.WorldToViewportPoint (targetTransform.position+new Vector3(0,4.5f, 0));
		}
		pos.z = 1;
		Label.transform.position = UIcam.ViewportToWorldPoint (pos);
	}


}

