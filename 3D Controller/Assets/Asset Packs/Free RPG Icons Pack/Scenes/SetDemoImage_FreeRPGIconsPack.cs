using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace DemoImage_WeaponBundle1
{
    public class SetDemoImage : MonoBehaviour
    {
        public Image[] DemoImage = new Image[501];

        // Start is called before the first frame update
        void Start()
        {
            DemoImage = GetComponentsInChildren<Image>();
            int j = 1;
            
            for (int i = 1; i < 51; i++) // DemoImage[0] is background
            {
                
                if (((i - 1) % 5) == 0) { 
                    j = 1;
                }

                switch ((i - 1) / 5)
                {
                    case 0:
                        DemoImage[i].sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Free RPG Icons Pack/Icons/Magic Spell/Free RPG Icons Pack_Buff_" + j + ".png"); j++;
                        break;
                    case 1:
                        DemoImage[i].sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Free RPG Icons Pack/Icons/Magic Spell/Free RPG Icons Pack_Cold_" + j + ".png"); j++;
                        break;
                    case 2:
                        DemoImage[i].sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Free RPG Icons Pack/Icons/Magic Spell/Free RPG Icons Pack_Dark_" + j + ".png"); j++;
                        break;
                    case 3:
                        DemoImage[i].sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Free RPG Icons Pack/Icons/Magic Spell/Free RPG Icons Pack_Debuff_" + j + ".png"); j++;
                        break;
                    case 4:
                        DemoImage[i].sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Free RPG Icons Pack/Icons/Magic Spell/Free RPG Icons Pack_Earth_" + j + ".png"); j++;
                        break;
                    case 5:
                        DemoImage[i].sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Free RPG Icons Pack/Icons/Magic Spell/Free RPG Icons Pack_Fire_" + j + ".png"); j++;
                        break;
                    case 6:
                        DemoImage[i].sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Free RPG Icons Pack/Icons/Magic Spell/Free RPG Icons Pack_Holy_" + j + ".png"); j++;
                        break;
                    case 7:
                        DemoImage[i].sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Free RPG Icons Pack/Icons/Magic Spell/Free RPG Icons Pack_Nature_" + j + ".png"); j++;
                        break;
                    case 8:
                        DemoImage[i].sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Free RPG Icons Pack/Icons/Magic Spell/Free RPG Icons Pack_Water_" + j + ".png"); j++;
                        break;
                    case 9:
                        DemoImage[i].sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Free RPG Icons Pack/Icons/Magic Spell/Free RPG Icons Pack_Wind_" + j + ".png"); j++;
                        break;
                }
            }
        }
    }
}