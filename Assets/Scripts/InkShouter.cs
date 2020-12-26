using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;

public class InkShouter : MonoBehaviour
{

	public bool shoot = false;

	[SerializeField]
	private Brush brush;

	private ParticleSystem mParticleSystem;

	private List<ParticleCollisionEvent> collisionEvents;

	ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];
	//収束先
	Vector3 targetPosition;

	
	void Start() 
	{
		SetActive(false);
		mParticleSystem = GetComponent<ParticleSystem>();
		collisionEvents = new List<ParticleCollisionEvent>();
		targetPosition = new Vector3 (- 100, -100, -100);
		this.enabled = false;
	}

	public void SetActive(bool state)
	{
		
		var ps = GetComponent<ParticleSystem>();
		var e = ps.emission;
		e.enabled = state;
		if (state) e.rateOverTime = 150.0f;
		else e.rateOverTime = 0.0f;
	}

	void OnParticleCollision(GameObject other)
	{
		int numEvents = mParticleSystem.GetCollisionEvents(other, collisionEvents);
		for (int i = 0; i < numEvents; ++i)
		{
			// var target = other.GetComponent<DepthMeshCollider>();
			// if (target != null)
			// {
			// 	target.InitializeComponent();
			// }
			var paintObject = other.GetComponent<InkCanvas>();
			if (paintObject != null)
			{
				Debug.Log("Collide !!!");
				try
				{
					// paintObject.InitializeCacheMeshData();
					Debug.Log(collisionEvents[i].intersection);
					paintObject.Paint(brush, collisionEvents[i].intersection);
				}
				catch(NullReferenceException e)
				{
					Debug.Log(e.TargetSite.Name);
				}
			}
		}
	}
	
    // Update is called once per frame
    void Update()
    {
		// transform.rotation = camera.transform.rotation;
		// transform.position.z = transform.position.z + 0.83f;
		// LateUpdate();
        // this.enabled = shoot;
		// transform.position = ScreenToCanvas(camera.transform.position);
    }
}
