using System;
using System.Collections.Generic;
using UnityEngine;
using RenderSettings = UnityEngine.RenderSettings;

[Serializable]
public class Weather
{
    public string name;
    public Vector3 lightAngle;
    public Color lightColor;
    public Material skyBox;
    public GameObject extra;

    public Weather(Light directionalLight)
    {
        lightColor = directionalLight.color;
        lightAngle = directionalLight.transform.eulerAngles;
        skyBox = RenderSettings.skybox;
    }

    public void SetLight(Light directionalLight)
    {
        directionalLight.transform.eulerAngles = lightAngle;
        directionalLight.color = lightColor;
        RenderSettings.skybox = skyBox;
        if(extra != null)
            extra.SetActive(true);
    }

    public void DisableExtra()
    {
        if(extra != null)
            extra.SetActive(false);
    }
   
}

public class WeatherController : MonoBehaviour
{
    private Light _directionalLight;
    public int currentWeather;
    public List<Weather> weatherList;
    private Weather _defaultWeather;

    private void Awake()
    {
        _directionalLight = GameObject.FindObjectOfType<Light>();
        _defaultWeather = new Weather(_directionalLight);
    }
    public void SetCurrentWeather()
    {
        SetWeather(currentWeather);
    }

    public void SetWeather(int index)
    {
        weatherList[currentWeather].DisableExtra();
        weatherList[index].SetLight(_directionalLight);
        currentWeather = index;
    }

    public void SetDefaultWeather()
    {
        weatherList[currentWeather].DisableExtra();
        _defaultWeather.SetLight(_directionalLight);
    }
}