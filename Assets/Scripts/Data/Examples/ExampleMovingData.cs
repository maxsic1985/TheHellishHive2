using UnityEngine;



namespace HellishHive2
{
    [System.Serializable]
    public class ExampleMovingData
    {
        public float ShipSpeed;
        public float TimeToStayNearDock;

        public Vector2 ShipSpawnPosition;
        public Vector2 ShipParkingPosition;
        public Vector2 ShipReturnPosition;
    }
}