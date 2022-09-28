using UnityEngine;

    [CreateAssetMenu(fileName = "Game Setting", menuName = "Config/Game Setting")]
    public class GameSetteng : ScriptableObject
    {
        [Header("Setting")]
        public float speed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float maxRoadCount;
        [SerializeField] private float maxSpikesCount;
        [SerializeField] private float maxGoldsCount;

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public float MaxSpeed => maxSpeed;

        public float MaxRoadCount => maxRoadCount;

        public float MaxSpikesCount => maxSpikesCount;

        public float MaxGoldsCount => maxGoldsCount;
    }
