//
//RandomWind.cs for unity-chan!
//
//Original Script is here:
//ricopin / RandomWind.cs
//Rocket Jump : http://rocketjump.skr.jp/unity3d/109/
//https://twitter.com/ricopin416
//
using UnityEngine;
using System.Collections;

namespace UnityChan
{
	public class RandomWind : MonoBehaviour
	{
		private SpringBone[] springBones;
		public bool isWindActive = true;
		private float startValue = 0;
		public float speed = 0.05f;
		// Use this for initialization
		void Start()
		{
			springBones = GetComponent<SpringManager>().springBones;
			startValue = Random.Range(0f, 1f);
		}

		// Update is called once per frame
		void Update()
		{
			Vector3 force = Vector3.zero;
			if (isWindActive)
			{
				force = new Vector3(Mathf.PerlinNoise(Time.time, startValue) * speed, 0, 0);
			}

			for (int i = 0; i < springBones.Length; i++)
			{
				springBones[i].springForce = force;
			}
		}

		void OnGUI()
		{
			Rect rect1 = new Rect(10, Screen.height - 40, 400, 30);
			isWindActive = GUI.Toggle(rect1, isWindActive, "Random Wind");
		}

	}
}