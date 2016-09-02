using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StartGameButton : MonoBehaviour {
	//获取需要监听的按钮对象
	public GameObject startButton;
	public GameObject levelButton;
	public GameObject settingButton;
	public GameObject aboutButton;
	public Image image;
	public float colorDecay;
	public Color overColor = new Color(0f, 0f, 0f, 255f);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake () 
	{	

		//设置这个按钮的监听，指向本类的ButtonClick方法中。
		UIEventListener.Get(startButton).onClick = ButtonClick;
	}
	
	//计算按钮的点击事件
	void ButtonClick(GameObject button)
	{
		if (button == startButton) 
		{
			StartCoroutine("StartLevel00");
		}
		
	}

	void ImageColorDecay(Image image)
	{
		image.color = Color.Lerp ( image.color,overColor, colorDecay*Time.deltaTime );
	}

	IEnumerator StartLevel00()
	{
		while(image.color.a<1.5f)
		{
			ImageColorDecay(image);
			Debug.Log (image.color);
				yield return  null;
			}
		//yield return new  WaitForSeconds (1); 

		Application.LoadLevel("level0");
	}
}
