using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public TextMeshProUGUI levelText, healthText, moneyText, upgradeCostText, experienceText;

    private int _currentSelectedCharacter = 0;
    public Image characterSelectedSprite;
    public Image weaponSelectedSprite;
    public RectTransform experienceBar;

    public void OnArrowClick(bool right)
    {
        switch (right)
        {
            case true:
            {
                _currentSelectedCharacter++;

                if (_currentSelectedCharacter == GameManager.Manager.playerSprites.Count)
                {
                    _currentSelectedCharacter = 0;
                }

                OnSelectionChanged();
                break;
            }
            default:
            {
                _currentSelectedCharacter--;

                if (_currentSelectedCharacter < 0)
                {
                    _currentSelectedCharacter = GameManager.Manager.playerSprites.Count - 1;
                }

                OnSelectionChanged();
                break;
            }
        }
    }

    private void OnSelectionChanged()
    {
        characterSelectedSprite.sprite = GameManager.Manager.playerSprites[_currentSelectedCharacter];
        GameManager.Manager.player.SwapSprite(_currentSelectedCharacter);
    }

    public void OnUpgradeClick()
    {
        if (GameManager.Manager.TryUpgradeWeapon())
        {
            UpdateMenu();
        } 
    }

    public void UpdateMenu()
    {
        weaponSelectedSprite.sprite = GameManager.Manager.weaponSprites[GameManager.Manager.weapon.weaponLevel];
        upgradeCostText.text = GameManager.Manager.weapon.weaponLevel == GameManager.Manager.weaponPrices.Count
            ? "MAX"
            : GameManager.Manager.weaponPrices[GameManager.Manager.weapon.weaponLevel].ToString();

        healthText.text = GameManager.Manager.player.hitPoints.ToString();
        moneyText.text = GameManager.Manager.coins.ToString();
        levelText.text = GameManager.Manager.GetCurrentLevel().ToString();


        int currentLevel = GameManager.Manager.GetCurrentLevel();
        if (currentLevel == GameManager.Manager.experienceTable.Count)
        {
            experienceText.text = $"{GameManager.Manager.experience.ToString()} total experience";
            experienceBar.localScale = Vector3.one;
        }
        else
        {
            int previousLevelExperience = GameManager.Manager.GetExperienceToLevel(currentLevel - 1);
            int currentLevelExperience = GameManager.Manager.GetExperienceToLevel(currentLevel);

            int experienceDifference = currentLevelExperience - previousLevelExperience;
            int alreadyAccumulatedExperience = GameManager.Manager.experience - previousLevelExperience;

            float completionRatio = (float)alreadyAccumulatedExperience / (float)experienceDifference;

            experienceText.text = $"{alreadyAccumulatedExperience} / {experienceDifference}";
            experienceBar.localScale = new Vector3(completionRatio, 1, 1);
        }
    }
}
