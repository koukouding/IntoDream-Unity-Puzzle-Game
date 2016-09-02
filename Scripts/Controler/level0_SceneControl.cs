using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class level0_SceneControl : MonoBehaviour {
	public Light houseLight;
	public GameObject lighter;
	public Light doorLight;
	public GameObject medicine;
	public GameObject cup;
	public GameObject hints;
	public Image seceneOverImage;
	public float colorDecay;
	public Color overColor = new Color(0f, 0f, 0f, 255f);
	public Color startColor = new Color(0f, 0f, 0f, 0f);
	AudioSource drink;
	// Use this for initialization
	void Start () {
		drink = GetComponent<AudioSource> ();
		seceneOverImage.color = overColor;
		StartCoroutine ("StartLevel01");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (doorLight.enabled);
		
		if (!doorLight.enabled && !houseLight.enabled)
		{
			
			//seceneOverImage.color = Color.Lerp ( seceneOverImage.color,overColour, colorDecay*Time.deltaTime );
			seceneOverImage.color = Color.Lerp ( seceneOverImage.color,overColor, 0.0015f*Time.deltaTime );
			if(seceneOverImage.color.a>1f)
				Application.LoadLevel("level1");
		}
		//UIEventListener.Get(lighter).onClick = ObjectClick;
		
		
		
	}
	

	
	void ImageColorDecay(Image image,Color toColor)
	{
		image.color = Color.Lerp ( image.color,toColor, colorDecay );
	}
	
	IEnumerator StartLevel01()
	{
		while(seceneOverImage.color.a>0.1)
		{
			
			yield return null;
			seceneOverImage.color = Color.Lerp ( seceneOverImage.color,startColor, colorDecay );
		}
		yield return new WaitForSeconds (1);
		hints.SetActive(true);
		//yield return new WaitForSeconds (2);
		yield return new WaitForSeconds (3f);
		medicine.SetActive(true);//显示物体
		drink.Play ();
		yield return new WaitForSeconds (2f);
		cup.SetActive(true);

		
		if (doorLight.enabled ||houseLight.enabled)
		{
			yield return new WaitForSeconds (2);
			hints.GetComponent<UILabel>().text="还有灯亮着..\r\n睡不着..";			
		}
	}

}
