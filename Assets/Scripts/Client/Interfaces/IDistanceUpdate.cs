using Client.Generation;

namespace Client.Interfaces
{
    public interface IDistanceUpdate
    {
        public void UpdateDistance(IReadOnlyLevelStateData levelStateData);
    }
}