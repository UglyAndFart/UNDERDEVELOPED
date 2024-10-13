using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class SlideTutorial : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _images;
    [SerializeField]
    private Button _btnNext, _btnPrev;
    [SerializeField]
    private Image _imageDimension;
    private int _currentImageIndex;

    private void Start()
    {
        if (_images == null)
        {
            return;
        }

        _btnNext.onClick.AddListener(NextImage);
        _btnPrev.onClick.AddListener(PrevImage);
        _btnPrev.gameObject.SetActive(false);

        _imageDimension.sprite = _images[0];
    }

    private void NextImage()
    {
        if (_images.Length-1 <= _currentImageIndex)
        {
            return;
        }

        _imageDimension.sprite = _images[++_currentImageIndex];
        
        if (_currentImageIndex > 0)
        {
            _btnPrev.gameObject.SetActive(true);
        }

        if (_currentImageIndex == _images.Length-1)
        {
            _btnNext.gameObject.SetActive(false);
        }
    }

    private void PrevImage()
    {
        if (_currentImageIndex == 0)
        {
            return;
        }

        _imageDimension.sprite = _images[--_currentImageIndex];

        if (_currentImageIndex == 0)
        {
            _btnPrev.gameObject.SetActive(false);
        }

        if (_currentImageIndex < _images.Length)
        {
            _btnNext.gameObject.SetActive(true);
        }
    }
}
