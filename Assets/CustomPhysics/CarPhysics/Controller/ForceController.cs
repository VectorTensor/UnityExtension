using CustomPhysics.CarPhysics.Models;
using UnityEngine;

namespace CustomPhysics.CarPhysics.Controller
{
    public class ForceController
    {
        private CarEngineData _carEngineData;
        private Ray _ray;
        private Transform _tireObject;
        private float _restDistance;
        
        
        
        private ForceController()
        {
            
        }

        /// <summary>
        ///  Calculate suspension force
        /// F = offset * strength - velocity * damping
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="strength"></param>
        /// <param name="velocity"></param>
        /// <param name="damping"></param>
        /// <returns></returns>
        public float CalculateSuspensionForce(float offset, float velocity)
        {
            return (offset * _carEngineData.strength) - velocity * _carEngineData.damping;
        }

        public void UpdateForce(Rigidbody rb)
        {
            if (Physics.Raycast(_ray, out var hit))
            {

                float offset = _restDistance - hit.distance;
                rb.AddForce(CalculateSuspensionForce(offset,rb.velocity.y)*Vector3.up,ForceMode.Force);
                Debug.DrawRay(_tireObject.position, Vector3.down * hit.distance, Color.red );
                


            }
            
            
        }

        
        
        #region Builder
        

        public class Builder
        {
            
            private CarEngineData _carEngineData;
            private Transform _tireObject;

            public Builder WithServices()
            {
                
                return this;
            }

            public Builder WithData(CarEngineData c)
            {

                _carEngineData = c;
                return this;
                
            }

            public Builder WithObject(Transform t)
            {

                _tireObject = t;

                return this;
            }
            public ForceController Build()
            {
                var c = new ForceController();
                if (_carEngineData == null || _tireObject == null)
                {
                    throw new System.Exception("CarEngineData or tireobject is null");
                }
                c._carEngineData = _carEngineData;
                c._tireObject = _tireObject;
                Ray r = new Ray(_tireObject.position, Vector3.down);
                c._ray = r;
                UnityEngine.Physics.Raycast(r,out var hit);
                c._restDistance = hit.distance;
                return c;

            }
            
        }
        
        #endregion
        
    }
}