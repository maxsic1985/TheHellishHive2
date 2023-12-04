using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace HalfDiggers.Runner
{
    public struct PostProcessingComponent
    {
      
        public Vignette VignetteValue;
        public Color ColorValue;
        public float InitialSmoothnessValue;
        public float InitialIntensivityValue;
        public float TwoLifeIntensivityValue; 
        public float OneLifeIntensivityValue;
        public float IntensivityValue;
        public float FadeSpeed;
        public bool IsRoundedValue;
        
    }
}
