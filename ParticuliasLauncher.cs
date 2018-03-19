using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuliasLauncher : MonoBehaviour {

    public ParticleSystem particleLauncher;
    public ParticleSystem splatterParticles;
    List<ParticleCollisionEvent> collisionEvents;
    public particulaDecalPool ManchaPool;
    public GameObject instanciar;
    //public GameObject gao;
	// Use this for initialization
	void Start () {
        collisionEvents = new List<ParticleCollisionEvent>();
       // StartCoroutine(DisparaPintura());
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher,other,collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++) {
            ManchaPool.ParticulaHit(collisionEvents[i]);
            EmitLocation(collisionEvents[i]);
        }
        
    }

    void EmitLocation(ParticleCollisionEvent particulaConColiison) {
        splatterParticles.transform.position = particulaConColiison.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(particulaConColiison.normal);
        splatterParticles.Emit(1);
        var gamo = Instantiate(instanciar,particulaConColiison.intersection,Quaternion.LookRotation(particulaConColiison.normal));
    }

    public IEnumerator DisparaPintura() {
        while (true == true)
        {
            if (Input.touchCount >= 1)  
            {
                yield return new WaitForSeconds(1);
                particleLauncher.Emit(1);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.touchCount >= 1)
        {
            //StartCoroutine(DisparaPintura());
            particleLauncher.Emit(1);
        }
    }
}
