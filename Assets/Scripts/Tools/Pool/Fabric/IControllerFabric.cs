using UnityEngine;

namespace Tools.Ui.Fabric
{
    public interface IControllerFabric<TController, TKey, TView> 
        where TController : IPoolableElementController<TKey, TView>
        where TView : Component
    {
        public TController CreateController();
    }
}