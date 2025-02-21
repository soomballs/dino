using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;

    public Text nameText;
    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;

    void Start()
    {
        updateCharacter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if(selectedOption >= characterDB.characterCount)
        {
            selectedOption = 0;
        }

        updateCharacter(selectedOption);
        PlayerPrefs.SetInt("selectedOption", selectedOption);
        PlayerPrefs.Save();
        Debug.Log("Selected Option from nextButton: " + selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;

        if(selectedOption < 0)
        {
            selectedOption = characterDB.characterCount - 1;
        }

        updateCharacter(selectedOption);
        PlayerPrefs.SetInt("selectedOption", selectedOption);
        PlayerPrefs.Save();
        Debug.Log("Selected Option from backButton: " + selectedOption);
    }

    public void updateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        nameText.text = character.characterName;
    }

    public int getOption()
    {
        return selectedOption;
    }

}
