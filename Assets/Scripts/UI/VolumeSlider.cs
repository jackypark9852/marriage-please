using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
   public Button button;
   public Slider slider;
   public void SetVoice(){
      MusicManager.Instance.SetVolume(slider.value);
   }
}
