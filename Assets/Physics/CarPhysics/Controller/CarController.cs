using Physics.CarPhysics.Models;

namespace Physics.CarPhysics.Controller
{
    public class CarController
    {
        private CarEngineData _carEngineData;
        
        
        private CarController()
        {
            
        }

        public float CalculateSuspensionForce(float offset, float strength, float velocity, float damping)
        {
            return (offset * strength) - velocity * damping;
        }

        
        
        #region Builder

        

        class Builder
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