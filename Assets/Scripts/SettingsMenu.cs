using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

  public class SettingsMenu : MonoBehaviour
  { 
	 TMP_Dropdown qualityDropDown;
 
	 void Start()
	 {
		qualityDropDown = GetComponent<TMP_Dropdown>();
		int Quality=PlayerPrefs.GetInt("_qualityIndex", 0);
		qualityDropDown.value = Quality;
	 }
 
	  public void SetQuality (int qualityIndex)
	  {
		QualitySettings.SetQualityLevel(qualityIndex);
		PlayerPrefs.SetInt("_qualityIndex", qualityIndex); 
	  }
  }