using System;
using Physics.CarPhysics.Controller;
using Physics.CarPhysics.Models;
using UnityEngine;

namespace Physics.CarPhysics
{
    public class Engine : MonoBehaviour
    {
        private CarController _carController;
        [SerializeField] private Rigidbody carRb;
        [SerializeField] private CarEngineData carEngineData;
        private void Awake()
        {
            _carController = new CarController.Builder().WithData(carEngineData).Build();
        }

        public void FixedUpdate()
        {
            
            Vector3 velocity = carRb.velocity;
            float offset =0 ; // position at rest - position now 
            _carController.CalculateSuspensionForce(offset, velocity.y);
            
        }
    }
}