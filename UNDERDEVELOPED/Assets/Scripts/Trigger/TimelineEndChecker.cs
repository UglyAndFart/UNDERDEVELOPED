using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineEndChecker : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _playableDirector;
    private bool _timelineOver = false;
    void Start()
    {
        if (_playableDirector != null)
        {
            _playableDirector.stopped += OnTimelineStop;
        }
    }

    private void OnTimelineStop(PlayableDirector director)
    {
        _timelineOver = true;
        
        if (director.name == "ForestIntro")
        {
            Destroy(director.transform.parent.gameObject);
        }
    }

    public bool GetTimelineOver()
    {
        return _timelineOver;
    }

    public void SetCutscene(PlayableDirector playableDirector)
    {
        _playableDirector = playableDirector;
        _timelineOver = false;
    }

    private void OnDestroy()
    {
        if (_playableDirector != null)
        {
            _playableDirector.stopped -= OnTimelineStop;
        }
    }
}
