using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : DontDestroy<SoundManager>
{
    public enum SE
    {
        foot,
        UI_Click, 
        UI_Select,
        Get_Item,
        Dial,
        Lock,
        open,
        Door_open,
        Paper_touch,
        paper_drop,
        Drop_Paper,
        Book_Pass_Sound,
        box,
        discriminator,
        FallingHand,
        Blood_item,
        stamp,
        Letter_Hole,
        open_box,
        Closet_close,
        Closet_open,
        Clothesincloset,
        Drop_Painting,
        candlestick_fire,
        portrait,
        pillow,
        glass_break,
        necklace,
        Drawer_Open,
        Drawer_Close,
        Clock,
        Ladder,
        Success_Music_Box,
        Fail_Music_Box,
        Painting_Stick,
        Curtain_open,
        paper_rip,
        safe,
        coffin,
        melting,
        Frame,
        Drop_Ladder,
        clockEvent,
        item_Click,
        Count
    }

    public enum BGM
    {
        Story_BGM,
        Main_BGM,
        Ending_BGM,
        Middle_BGM,
        Prologue_BGM,
        Count

    }
    public enum SoundType
    {
        BGM, SE
    }
    public Dictionary<string, AudioSource> sounds = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
            if(transform.GetChild(i).GetComponent<AudioSource>())
                sounds[transform.GetChild(i).name] = transform.GetChild(i).GetComponent<AudioSource>();
    }

    public static void SetSoundValue(float v, SoundType soundType)
    {
        DataManager.LoadData();
        var data = DataManager.playData;


        v = Mathf.Max(0, v);
        v = Mathf.Min(1, v);
        if (soundType == SoundType.BGM)
        {
            data.bgmValue = v;
            instance.SetBGMs(data.muteBGM, data.bgmValue);
        }
        else if (soundType == SoundType.SE)
            data.seValue = v;

        DataManager.SaveData();
    }
    public static float GetSoundValue(SoundType soundType)
    {
        DataManager.LoadData();
        var data = DataManager.playData;

        if (soundType == SoundType.BGM)
            return data.bgmValue;
        else if (soundType == SoundType.SE)
            return data.seValue;
        return 0;
    }
    public static void MuteSound(bool state, SoundType soundType)
    {
        DataManager.LoadData();
        var data = DataManager.playData;

        if (soundType == SoundType.BGM)
        {
            data.muteBGM = state;
            instance.SetBGMs(data.muteBGM, data.bgmValue);
        }
        else if (soundType == SoundType.SE)
            data.muteSE = state;

        DataManager.SaveData();
    }

    public static void PlaySE(SE se)
    {
        DataManager.LoadData();
        var data = DataManager.playData;

        instance.sounds[se.ToString()].mute = data.muteSE;
        instance.sounds[se.ToString()].volume = data.seValue;
        instance.sounds[se.ToString()].Play();
    }
    
    private void SetBGMs(bool mute,float volume)
    {
        for (int i = 0; i < (int)BGM.Count; i++)
        {
            instance.sounds[((BGM)(i)).ToString()].volume = volume;
            instance.sounds[((BGM)(i)).ToString()].mute = mute;
        }
    }

    public static void StopBGM()
    {
        for (int i = 0; i < (int)BGM.Count; i++)
            instance.sounds[((BGM)(i)).ToString()].Stop();
    }

    public static void PlayBGM(BGM bgm)
    {
        DataManager.LoadData();
        var data = DataManager.playData;
        StopBGM();
        instance.SetBGMs(data.muteBGM, data.bgmValue);
        instance.sounds[bgm.ToString()].Play();
    }
}
