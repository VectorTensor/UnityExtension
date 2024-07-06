using Physics.CarPhysics.Models;

namespace Physics.CarPhysics.Controller
{
    public class CarController
    {
        private CarEngineData _carEngineData;
        
        
        private CarController()
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

        
        
        #region Builder
        

        public class Builder
        {
            
            private CarEngineData _carEngineData;

            public Builder WithServices()
            {
                
                return this;
            }

            public Builder WithData(CarEngineData c)
            {

                _carEngineData = c;
                return this;
                
            }
            public CarController Build()
            {
                var c = new CarController();
                if (_carEngineData == null)
                {
                    throw new System.Exception("CarEngineData is null");
                }
                c._carEngineData = _carEngineData;
                return c;

            }
            
        }
        
        #endregion
        
    }
}