using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float _timeMultiplier;
    [SerializeField] private float _startHour;
    [SerializeField] private Light _sunLight;
    [SerializeField] private float _sunriseHour;
    [SerializeField] private float _sunsetHour;
    [SerializeField] private Color _dayAmbientLight;
    [SerializeField] private Color _nightAmbientLight;
    [SerializeField] private AnimationCurve _lightChangeCurve;
    [SerializeField] private float _maxLightIntensity;
    [SerializeField] private float _maxMoonIntensity;
    

    private DateTime _currentTime;
    private TimeSpan _sunriseTime;
    private TimeSpan _sunsetTime;
    
    void Start()
    {
        _currentTime = DateTime.Now.Date + TimeSpan.FromHours(_startHour);

        _sunriseTime = TimeSpan.FromHours(_sunriseHour);
        _sunsetTime = TimeSpan.FromHours(_sunsetHour);
    }


    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    private void UpdateTimeOfDay()
    {
        _currentTime = _currentTime.AddSeconds(Time.deltaTime * _timeMultiplier);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(_sunLight.transform.forward, Vector3.down);
        _sunLight.intensity = Mathf.Lerp(0, _maxLightIntensity, _lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight =
            Color.Lerp(_nightAmbientLight, _dayAmbientLight, _lightChangeCurve.Evaluate(dotProduct));
    }

    private void RotateSun()
    {
        float sunLightRotation;

        if (_currentTime.TimeOfDay > _sunriseTime && _currentTime.TimeOfDay < _sunsetTime)
        {
            TimeSpan sunriseToSunsetDurration = CalculateTimeDifference(_sunriseTime, _sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(_sunriseTime, _currentTime.TimeOfDay);

            double percantage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDurration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percantage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(_sunsetTime, _sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(_sunsetTime, _currentTime.TimeOfDay);
            
            double percantage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percantage);
        }
        
        _sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan diff = toTime - fromTime;

        if (diff.TotalSeconds < 0)
        {
            diff += TimeSpan.FromHours(24);
        }

        return diff;
    }
}
