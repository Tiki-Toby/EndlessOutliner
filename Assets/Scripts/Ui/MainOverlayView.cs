using TMPro;
using UnityEngine;

namespace Ui
{
    public class MainOverlayView : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private GameObject startInfoPanel;

        public TMP_Text ScoreText => scoreText;
        public GameObject StartInfoPanel => startInfoPanel;
    }
}