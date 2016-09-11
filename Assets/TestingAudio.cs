using UnityEngine;
using System.Collections;

public class TestingAudio : MonoBehaviour
{

	public AudioClip clip;
	public SpriteRenderer background;
	private AudioSource audioSouce;
	public Shader shader;
	public Transform timerGO;

	private float timer = 0;
	private float v;
	Vector3 start = new Vector3 ();
	Vector3 end = new Vector3 ();

	void Awake ()
	{
		// Get components
		this.audioSouce = GetComponent<AudioSource> ();
		this.audioSouce.clip = this.clip;
		this.audioSouce.Play ();
	}

	void Start ()
	{
		Debug.Log ("Chanels: " + clip.channels);
		Debug.Log ("Lengh: (Do dai tinh theo s):" + clip.length);
		Debug.Log ("W: " + background.bounds.size.x + " - " + background.bounds.size.y);
	
		// Xac dinh diem dau diem cuoi, background

		start = new Vector3(0,2) + new Vector3 (-background.bounds.size.x / 2, 0) + new Vector3 (0, -background.bounds.size.y / 2);
		end = new Vector3(0,2) + new Vector3 (background.bounds.size.x / 2, 0) + new Vector3 (0, -background.bounds.size.y / 2);
		float totalWidth = background.bounds.size.x;
		float totalHeigh = background.bounds.size.y;
			
		// Lay cac thong tin mp3
		float[] samples = new float[clip.channels * clip.samples];
		clip.GetData (samples, 0);
		float lineWidth = totalWidth / samples.Length;
		Vector3 down = new Vector3 ();
		Vector3 top = new Vector3 ();

		Debug.Log ("Length: " + clip.samples);
		for (int i = 1; i < samples.Length-1; i++) {
			down = start + new Vector3 (i * lineWidth, 0);
			top = down + new Vector3 (0, samples [i] * totalHeigh);
			if (i % 1000 == 0) {
				
				if (samples [i] > 0)
					CreateLine (down, top, Color.white, lineWidth);
				else
					CreateLine (down, top, Color.red, lineWidth);

				if (samples [i] > 0)
					Debug.Log (i);
			}
		
		}


		// Tinh toan thoi gian chay
		float numberOfSecond = clip.length;
		timerGO.position = new Vector3 (start.x, timerGO.position.y);
		// s = t * v; v = m/s
		v = totalWidth / numberOfSecond;
	}

	void Update()
	{
		timer += Time.deltaTime;
		timerGO.position = new Vector3 (start.x + timer * v,timerGO.position.y);
	}



	void CreateLine (Vector3 down, Vector3 top, Color color,float width = 0.1f)
	{
		GameObject lineGO = new GameObject ();
		lineGO.transform.parent = this.transform;

		LineRenderer line = lineGO.AddComponent<LineRenderer> ();
		line.material = new Material (Shader.Find ("Particles/Additive"));
		line.SetWidth (0.05f, 0.05f);
		line.SetPosition (0, down);
		line.SetPosition (1, top);
		line.SetColors (color, color);
	}


}
