using Client.ReactiveModels;

namespace Ui
{
    public class MainOverlayController
    {
        private readonly PlayerReactiveModel _playerReactiveModel;
        private readonly MainOverlayView _view;

        public MainOverlayController(PlayerReactiveModel playerReactiveModel, MainOverlayView view)
        {
            _playerReactiveModel = playerReactiveModel;
            _view = view;
            
            _playerReactiveModel.SubscribeOnTakeCoin(UpdateCoinCount);
        }

        public void SetPreviewPanelActive(bool isPreviewPanelActive)
        {
            _view.StartInfoPanel.SetActive(isPreviewPanelActive);
        }

        public void UpdateCoinCount(int count)
        {
            _view.ScoreText.text = count.ToString();
        }
    }
}