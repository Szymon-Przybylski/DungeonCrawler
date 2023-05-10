using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;

    private void Awake()
    {
        if (GameManager.Manager != null)
        {
            Destroy(floatingTextManager.gameObject);
            Destroy(hud.gameObject);
            Destroy(menu.gameObject);
            Destroy(player.gameObject);
            Destroy(gameObject);
            return;
        }
        
        // wipe save
        PlayerPrefs.DeleteAll();
        
        Manager = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> experienceTable;

    public PlayerController player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitPointsBar;
    public Animator deathMenuAnimator;
    public GameObject hud;
    public GameObject menu;

    public int coins;
    public int experience;
    private static readonly int Hide = Animator.StringToHash("Hide");

    public void SaveState()
    {
        string saveData = "";

        // players skin
        saveData += "0" + "|";
        // players coins
        saveData += coins.ToString() + "|";
        // players experience
        saveData += experience.ToString() + "|";
        // players weapon level
        saveData += weapon.weaponLevel.ToString();
        
        PlayerPrefs.SetString("SaveState", saveData);
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
        
        if (!PlayerPrefs.HasKey("SaveState"))
        {
           return; 
        }
        
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        
        // players skin
        
        // players coins
        coins = int.Parse(data[1]);
        // players experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
        {
            player.SetLevel(GetCurrentLevel());
        }
        // players weapon
        weapon.SetWeaponLevelAndSprite(int.Parse(data[3]));
    }
    
    public void ShowText(string message, int fontsize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(message, fontsize, color, position, motion, duration);
    }

    public bool TryUpgradeWeapon()
    {
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if (coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    public int GetCurrentLevel()
    {
        int currentLevel = 0;
        int add = 0;

        while (experience >= add)
        {
            add += experienceTable[currentLevel];
            currentLevel++;

            if (currentLevel == experienceTable.Count)
            {
                return currentLevel;
            }
        }

        return currentLevel;
    }

    public int GetExperienceToLevel(int level)
    {
        int i = 0;
        int exp = 0;

        while (i < level)
        {
            exp += experienceTable[i];
            i++;
        }

        return exp;
    }

    public void GrantExperience(int exp)
    {
        int currentLevel = GetCurrentLevel();
        experience += exp;
        if (currentLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        OnHitPointChange();
        player.OnLevelUp();
    }

    public void OnHitPointChange()
    {
        float ratio = (float)player.hitPoints / (float)player.maxHitPoints;
        hitPointsBar.localScale = new Vector3(1, ratio, 1);
    }

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    public void Respawn()
    {
        deathMenuAnimator.SetTrigger(Hide);
        SceneManager.LoadScene("Main");
        player.Respawn();
    }
}
