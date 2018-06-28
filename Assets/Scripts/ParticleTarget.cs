using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleTarget : MonoBehaviour {
     [SerializeField]
     private Transform _attractorTransform;
 
     private ParticleSystem _particleSystem;
     private ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[1000];
 
     public void Start ()
     {
         _particleSystem = GetComponent<ParticleSystem> ();
     }
 
     public void LateUpdate()
     {
         if (_particleSystem.isPlaying) {
             int length = _particleSystem.GetParticles (_particles);
             Vector3 attractorPosition = _attractorTransform.position;
			
             for (int i=0; i < length; i++) {
                     _particles [i].position = _particles [i].position + (attractorPosition - _particles [i].position) / (_particles [i].remainingLifetime) * Time.deltaTime;
             }
             _particleSystem.SetParticles (_particles, length);
         }
 
     }
 }
