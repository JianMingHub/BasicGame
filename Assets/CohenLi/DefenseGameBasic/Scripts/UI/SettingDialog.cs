using System.Collections;
using System.Collections.Generic;
using UDEV.DefenseGameBasic;
using UnityEngine;
using UnityEngine.UI;

namespace UDEV.DefenseGameBasic
{
    public class SettingDialog : Dialog, IComponentCheking
    {
        public Slider musicSlider;
        public Slider soundSlider;
        
        public bool IsComponentNull()
        {
            return AudioController.Ins == null || musicSlider == null || soundSlider == null;
        }
        public override void Show(bool isShow)
        {
            base.Show(isShow);

            if (IsComponentNull()) return;

            musicSlider.value = Pref.musicVol;
            soundSlider.value = Pref.soundVol;
        }
        public void OnMusicChange(float value)
        {
            if (IsComponentNull()) return;

            AudioController.Ins.musicVol = value;
            AudioController.Ins.musicAus.volume = value;
            Pref.musicVol = value;
        }
        public void OnSoundChange(float value)
        {
            if (IsComponentNull()) return;

            AudioController.Ins.soundVol = value;
            AudioController.Ins.soundAus.volume = value;
            Pref.soundVol = value;
        }
    }
}
