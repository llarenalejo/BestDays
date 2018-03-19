using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particulaDecalPool : MonoBehaviour {

     private int particulaDecalDataIndice;
    public int maximoDecals=100;
    private ParticulaDecalDelta[] particulaData;

    public float SizeMinimo = .5f;//size de la mancha de pintura
    public float SizeMAx = 1.5f;

    private ParticleSystem.Particle[] particulas;
    private ParticleSystem ManchaParicula;
	// Use this for initialization
	void Start () {
        ManchaParicula = GetComponent<ParticleSystem>();
        particulas = new ParticleSystem.Particle[maximoDecals];
        particulaData = new ParticulaDecalDelta[maximoDecals];
        for (int x=0;x<maximoDecals;x++) {
            particulaData[x] = new ParticulaDecalDelta();
        }
	}

    public void ParticulaHit(ParticleCollisionEvent particulaColisionada ) { setParticleData(particulaColisionada); }


    void setParticleData(ParticleCollisionEvent particulaColisionEvento) {
        if (particulaDecalDataIndice>=maximoDecals) {
            particulaDecalDataIndice = 0;
        }
        //guarada la colision , rotacion , size y el color

        particulaData[particulaDecalDataIndice].position = particulaColisionEvento.intersection;
        Vector3 particulaRotacionEuler = Quaternion.LookRotation(particulaColisionEvento.normal).eulerAngles;
        particulaRotacionEuler.z = Random.Range(0, 360);// conversion de la rotacion de la particula colsionada y random eje z
        particulaData[particulaDecalDataIndice].rotation = particulaRotacionEuler;
        //particulaData[particulaDecalDataIndice].rotation = Quaternion.LookRotation(particulaColisionEvento.normal).eulerAngles;
        //particulaData[particulaDecalDataIndice].rotation.z = Random.Range(0, 360);
        particulaData[particulaDecalDataIndice].size = Random.Range(SizeMinimo,SizeMAx);//asigno rango de sizes

        particulaDecalDataIndice++;

    }

    void DisplayParticulas() {
        for(int y=0;y<=particulaData.Length;y++){   //asignamos todos los datos a la particula 
            particulas[y].position = particulaData[y].position;
            particulas[y].rotation3D = particulaData[y].rotation;
            particulas[y].startSize = particulaData[y].size;
        }
        ManchaParicula.SetParticles(particulas,particulas.Length);
    }

}
