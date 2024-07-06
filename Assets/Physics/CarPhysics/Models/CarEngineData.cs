using UnityEngine;

namespace Physics.CarPhysics.Models
{
    [CreateAssetMenu(fileName = "CarEngineData", menuName = "CarPhysics/CarEngineData", order = 0)]
    public class CarEngineData : ScriptableObject
    {

        public float strength;
        public float damping;

    }
}