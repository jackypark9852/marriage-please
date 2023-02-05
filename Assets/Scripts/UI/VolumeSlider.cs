using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
   public static Button muteButton;
   public static Slider slider;
   public static bool isMute= false;
   void Awake(){
      foreach(var item in GetComponentsInChildren<Button>())
         if(item.name == "MuteButton")
            muteButton = item;

      slider = GetComponentInChildren<Slider>();
      slider.value = MusicManager.Instance.GetVolume();
   }
   public static void MuteVoice(){
      if(isMute){
         MusicManager.Instance.SetVolume(slider.value);
         muteButton.GetComponent<Image>().color= new Color(255, 255, 255);
         isMute = false;
      }
      else{
         MusicManager.Instance.SetVolume(0);
         muteButton.GetComponent<Image>().color= new Color(255, 0, 0);
         isMute = true;
      }
   }
   public static void SetVoice(){
      if(isMute){
         MuteVoice();
      }
      MusicManager.Instance.SetVolume(slider.value);

   }
}
