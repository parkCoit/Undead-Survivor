using System;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    enum Achieve { unlockPotato, unlockBean };
    Achieve[] achieves;

    private void Awake() {
        achieves = (Achieve[])Enum.GetValues(typeof(Achieve));

        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();   
        }
    }

    private void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achieve achieve in achieves)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    private void Start() {
        UnLockCharacter();
    }

    private void UnLockCharacter()
    {
        for (int index=0; index < lockCharacter.Length; index++)
        {
            string achieveName = achieves[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achieveName) == 1;
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

}
