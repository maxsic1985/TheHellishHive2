using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Achievment
{
    private string _name;
    private string _description;
    private bool _unlocked;
    private int _points;
    private int _spriteIndex;
    private GameObject _achievmentRef;

    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }

        set
        {
            _description = value;
        }
    }

    public bool Unlocked
    {
        get
        {
            return _unlocked;
        }

        set
        {
            _unlocked = value;
        }
    }

    public int Points
    {
        get
        {
            return _points;
        }

        set
        {
            _points = value;
        }
    }

    public int SpriteIndex
    {
        get
        {
            return _spriteIndex;
        }

        set
        {
            _spriteIndex = value;
        }
    }

    public GameObject AchievmentRef
    {
        get
        {
            return _achievmentRef;
        }

        set
        {
            _achievmentRef = value;
        }
    }

    public Achievment(string name, string description, int points, GameObject  achievmnentRef, int spriteIndex)
    {
        this.Name = name;
        this.Description = description;
        this.Unlocked = false;
        this.SpriteIndex = spriteIndex;
        this.AchievmentRef = achievmnentRef;
        this._points = points;
        LoadAchievment();
    }

    public bool EarnAchievment()
    {
        if (!_unlocked)
        {
            _achievmentRef.GetComponent<Image>().sprite=AchievmentManager.Instance.unlockedSprite;
            SaveAchievment(true);
            return true;
        }
            return false;
    }

    public void SaveAchievment(bool value)
    {
        _unlocked = value;
        int tmpPoints = PlayerPrefs.GetInt("Points");
        if (_unlocked)
        {
            PlayerPrefs.SetInt("Points", tmpPoints + _points);

        }
        PlayerPrefs.SetInt(_name, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadAchievment()
    {
        _unlocked = PlayerPrefs.GetInt(_name) == 1 ? true : false;
        if (_unlocked)
        {
            Debug.Log("I unlock it");
            AchievmentManager.Instance.textPoint.text = "Очки достижений:" + PlayerPrefs.GetInt("Points");
            _achievmentRef.GetComponent<Image>().sprite = AchievmentManager.Instance.unlockedSprite;

        }
    }
}
