using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class SlideTutorial : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _images;
    [SerializeField]
    private Button _next, _prev;
    [SerializeField]
    private Image _imageDimension;
    private int _currentImageIndex;
    private SpriteLibrary _tutorialUI;

    private void Start()
    {
        // if (_images == null)
        // {
        //     return;
        // }

        _next.onClick.AddListener(NextImage);
        _prev.onClick.AddListener(PrevImage);

        _imageDimension.sprite = _images[0];
    }

    private void NextImage()
    {
        if (_images.Length-1 <= _currentImageIndex)
        {
            return;
        }

        _imageDimension.sprite = _images[++_currentImageIndex];
    }

    private void PrevImage()
    {
        if (_currentImageIndex == 0)
        {
            return;
        }

        _imageDimension.sprite = _images[--_currentImageIndex];
    }

    // private void EndSlide()
    // {

    // }
}
