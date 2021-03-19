//
//SpringBone.cs for unity-chan!
//
//Original Script is here:
//ricopin / SpringBone.cs
//Rocket Jump : http://rocketjump.skr.jp/unity3d/109/
//https://twitter.com/ricopin416
//
//Revised by N.Kobayashi 2014/06/20
//
using UnityEngine;
using System.Collections;

namespace UnityChan
{
	public class SpringBone : MonoBehaviour
	{
		// next bone
		public Transform child;

		//bone's direction in bone's coord
		public Vector3 boneAxis = new Vector3(-1.0f, 0.0f, 0.0f);
		//for collider
		public float radius = 0.05f;

		//using param in bone or manager
		public bool isUseEachBoneForceSettings = false;

		//The force that the spring returns
		public float stiffnessForce = 0.01f;

		//damping force
		public float dragForce = 0.4f;
		public Vector3 springForce = new Vector3(0.0f, -0.0001f, 0.0f);
		public SpringCollider[] colliders;
		public bool debug = true;
		//Kobayashi:Thredshold Starting to activate activeRatio
		public float threshold = 0.01f;
		private float springLength;
		private Quaternion localRotation;
		private Transform trs;
		private Vector3 currTipPos;
		private Vector3 prevTipPos;
		private Transform org;

		private SpringManager managerRef;

		private void Awake()
		{
			trs = transform;
			localRotation = transform.localRotation;
			managerRef = GetParentSpringManager(transform);
		}

		private SpringManager GetParentSpringManager(Transform t)
		{
			var springManager = t.GetComponent<SpringManager>();

			if (springManager != null)
				return springManager;

			if (t.parent != null)
			{
				return GetParentSpringManager(t.parent);
			}

			return null;
		}

		private void Start()
		{
            if (!child && transform.childCount>0)
            {
				child = transform.GetChild(0);
            }
            if (!child)
            {
				return; 
            }
			springLength = Vector3.Distance(trs.position, child.position);
			currTipPos = child.position;
			prevTipPos = child.position;
		}

		public void UpdateSpring()
		{
			if (!child)
			{
				return;
			}
			org = trs;
			// Reset rotation 
			Debug.Log("reset rotation " + trs.localRotation + Quaternion.identity * localRotation);
			//trs.localRotation = Quaternion.identity * localRotation;

			float sqrDt = Time.deltaTime * Time.deltaTime;

			//stiffness
			Vector3 force = trs.rotation * (boneAxis * stiffnessForce) / sqrDt;

			//drag
			force += (prevTipPos - currTipPos) * dragForce / sqrDt;

			force += springForce / sqrDt;

			Vector3 temp = currTipPos;

			//verlet https://en.wikipedia.org/wiki/Verlet_integration
			currTipPos = (currTipPos - prevTipPos) + currTipPos + (force * sqrDt);

			//restore length
			currTipPos = ((currTipPos - trs.position).normalized * springLength) + trs.position;

			//衝突判定
			for (int i = 0; i < colliders.Length; i++)
			{
				if (Vector3.Distance(currTipPos, colliders[i].transform.position) <= (radius + colliders[i].radius))
				{
					Vector3 normal = (currTipPos - colliders[i].transform.position).normalized;
					currTipPos = colliders[i].transform.position + (normal * (radius + colliders[i].radius));
					currTipPos = ((currTipPos - trs.position).normalized * springLength) + trs.position;
				}


			}

			prevTipPos = temp;

			//Apply rotation
			Vector3 aimVector = trs.TransformDirection(boneAxis);
			Quaternion aimRotation = Quaternion.FromToRotation(aimVector, currTipPos - trs.position);
			//original
			//trs.rotation = aimRotation * trs.rotation;
			//Kobayahsi:Lerp with mixWeight
			Quaternion secondaryRotation = aimRotation * trs.rotation;
			trs.rotation = Quaternion.Lerp(org.rotation, secondaryRotation, managerRef.dynamicRatio);
			//Vector3 view = trs.rotation.eulerAngles;
			//Debug.Log("rotation " + view);
			//trs.rotation =  Quaternion.Euler(0,0, view.z);
		}

		private void OnDrawGizmos()
		{
			if (debug)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(currTipPos, radius);
			}
		}
	}
}
