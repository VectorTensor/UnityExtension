using CustomPhysics.CarPhysics.Controller;
using CustomPhysics.CarPhysics.Models;
using UnityEngine;

namespace CustomPhysics.CarPhysics
{
    public class Engine : MonoBehaviour
    {
        private ForceController[] _forceControllers;
        [SerializeField] private Rigidbody carRb;
        [SerializeField] private CarEngineData carEngineData;
        [SerializeField] private Transform[] forcePoints;
        
        
        private void Awake()
        {
            _forceControllers = new ForceController[4];
            for (int i =0 ; i< _forceControllers.Length; i++) // foreach (var x in _forceControllers)
            {
                _forceControllers[i] = new ForceController.Builder().WithData(carEngineData)
                                                                    .WithObject(forcePoints[i])
                                                                    .Build();

            }
        }

        public void FixedUpdate()
        {
            
            // Vector3 velocity = carRb.velocity;
            // float offset =0 ; // position at rest - position now 
            // _forceControllers.CalculateSuspensionForce(offset, velocity.y);
            
            foreach (var forceController in _forceControllers)
            {
                forceController.UpdateForce(carRb);
            }
        }

        [ContextMenu("Down")]
        public void Down()
        {
            var temp = carRb.position;
            temp.y -= 1;
            carRb.position = temp;

        }
    }
}