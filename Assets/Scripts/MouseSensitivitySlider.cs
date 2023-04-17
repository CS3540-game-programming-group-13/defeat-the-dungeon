using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivitySlider : MonoBehaviour
{
	private Slider mainSlider;

	public void Start()
	{
		mainSlider = GetComponent<Slider>();
		mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
		mainSlider.value = PlayerSaveDataManager.instance.playerData.MouseSensitivity;
	}

	public void ValueChangeCheck()
	{
		PlayerSaveDataManager.instance.SetMouseSensitivity(mainSlider.value);
	}
}
