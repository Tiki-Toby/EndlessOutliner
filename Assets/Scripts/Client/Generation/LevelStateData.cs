namespace Client.Generation
{
    public interface IReadOnlyLevelStateData
    {
        public float DeltaMove { get; }
        public float Distance { get; }
    }
    
    public class LevelStateData : IReadOnlyLevelStateData
    {
        private float _deltaMove;
        private float _distance;

        public float DeltaMove => _deltaMove;
        public float Distance => _distance;

        public LevelStateData()
        {
            Reset();
        }

        public void UpdateData(float deltaMove)
        {
            _deltaMove = deltaMove;
            _distance += deltaMove;
        }

        public void Reset()
        {
            _deltaMove = 0f;
            _distance = 0f;
        }
    }
}