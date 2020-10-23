using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private bool _isLoop = false;
    private int _execTimes = 1;
    private bool _isStop = false;
    private float count = 0.0f;
    private float _time = 0.0f;
    // private event done;
    private GameObject gameObject;
    Timer(int execTimes){
        _isLoop = execTimes == -1;
        _execTimes = execTimes;

    }

    void Enter(float time){
        _time = time;
        _isStop = false;
    }

    void Update()
    {
        if (_isStop == true)
        {
            return ;
        }

        count = count + Time.deltaTime;
        if (count >= _time)
        {
            count = 0;
            if (_isLoop == false)
            {
                _execTimes = _execTimes - 1;
                if (_execTimes == 0)
                {
                    _isStop = true;
                }
            }
        }
    }
}
