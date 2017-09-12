using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSounds : MonoBehaviour
{
	// Use this for initialization
	void Awake () {
        PreloadSounds();
    }

    void PreloadSounds() {

        //SoundSystem.Instance.PlayClip(AUDIO_TYPE.BACKGROUND_MUSIC, AudioClipManager.GetInstance().GetAudioClip(""));
        //AudioClipManager.GetInstance().GenerateAudioClip("filename","filepath");
        //Keep Adding on The same line with the filename and filepath replaced respectively for new sounds

        AudioClipManager.GetInstance().GenerateAudioClip("ArcherShootSFX", "Attacks!/Archer_01");
        AudioClipManager.GetInstance().GenerateAudioClip("MageMagicSFX", "Attacks!/Mage_01");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorSlashSFX", "Attacks!/Warrior_01");
        AudioClipManager.GetInstance().GenerateAudioClip("ExtraSFX", "Extra/EXTRA_01");
        AudioClipManager.GetInstance().GenerateAudioClip("FailSFX", "Fail/Stone");
        AudioClipManager.GetInstance().GenerateAudioClip("MoveSFX_1", "Move/Slide_01");
        AudioClipManager.GetInstance().GenerateAudioClip("MoveSFX_2", "Move/Slide_02");
        AudioClipManager.GetInstance().GenerateAudioClip("MoveSFX_3", "Move/Slide_03");
        AudioClipManager.GetInstance().GenerateAudioClip("MoveSFX_4", "Move/Slide_04");
        AudioClipManager.GetInstance().GenerateAudioClip("MoveSFX_5", "Move/Slide_05");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_No_1", "No/Select_No_01");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_No_2", "No/Select_No_02");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_No_3", "No/Select_No_03");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_No_4", "No/Select_No_04");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_Fail", "Select/Select_Fail");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_Hover", "Select/Select_Hover");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_Yes", "Select/Select_Yes");
        AudioClipManager.GetInstance().GenerateAudioClip("Win_1", "Win/Win_01");
        AudioClipManager.GetInstance().GenerateAudioClip("Win_2", "Win/Win_02");
        AudioClipManager.GetInstance().GenerateAudioClip("Win_3", "Win/Win_03");
        AudioClipManager.GetInstance().GenerateAudioClip("Win_4", "Win/Win_04");
        AudioClipManager.GetInstance().GenerateAudioClip("Win_5", "Win/Win_05");
        AudioClipManager.GetInstance().GenerateAudioClip("Win_6", "Win/Win_06");
        AudioClipManager.GetInstance().GenerateAudioClip("Win_7", "Win/Win_07");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_Yes_1", "Yes/Select_Yes_01");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_Yes_2", "Yes/Select_Yes_02");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_Yes_3", "Yes/Select_Yes_03");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_Yes_4", "Yes/Select_Yes_04");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_Yes_5", "Yes/Select_Yes_05");
        AudioClipManager.GetInstance().GenerateAudioClip("Select_Yes_6", "Yes/Select_Yes_06");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherAtk_Voice_1", "Voice/Attack/Archer_Attack_01");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherAtk_Voice_2", "Voice/Attack/Archer_Attack_02");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherAtk_Voice_3", "Voice/Attack/Archer_Attack_03");
        AudioClipManager.GetInstance().GenerateAudioClip("Mage_Voice_1", "Voice/Attack/Mage_Attack_01");
        AudioClipManager.GetInstance().GenerateAudioClip("Mage_Voice_2", "Voice/Attack/Mage_Attack_02");
        AudioClipManager.GetInstance().GenerateAudioClip("Mage_Voice_3", "Voice/Attack/Mage_Attack_03");
        AudioClipManager.GetInstance().GenerateAudioClip("Mage_Voice_4", "Voice/Attack/Mage_Attack_04");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorAtk_Voice_1", "Voice/Attack/Warrior_Attack_01");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorAtk_Voice_2", "Voice/Attack/Warrior_Attack_02");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorAtk_Voice_3", "Voice/Attack/Warrior_Attack_03");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherDmg_Voice_1", "Voice/Damage/Archer_Damage_01");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherDmg_Voice_2", "Voice/Damage/Archer_Damage_02");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherDmg_Voice_3", "Voice/Damage/Archer_Damage_03");
        AudioClipManager.GetInstance().GenerateAudioClip("MageDmg_Voice_1", "Voice/Damage/Mage_Damage_01");
        AudioClipManager.GetInstance().GenerateAudioClip("MageDmg_Voice_2", "Voice/Damage/Mage_Damage_02");
        AudioClipManager.GetInstance().GenerateAudioClip("MageDmg_Voice_3", "Voice/Damage/Mage_Damage_03");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorDmg_Voice_1", "Voice/Damage/Warrior_Damage_01");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorDmg_Voice_2", "Voice/Damage/Warrior_Damage_02");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorDmg_Voice_3", "Voice/Damage/Warrior_Damage_03");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherFail_Voice_1", "Voice/Fail/Archer_Fail_01");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherFail_Voice_2", "Voice/Fail/Archer_Fail_02");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherFail_Voice_3", "Voice/Fail/Archer_Fail_03");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherFail_Voice_4", "Voice/Fail/Archer_Fail_04");
        AudioClipManager.GetInstance().GenerateAudioClip("MageFail_Voice_1", "Voice/Fail/Mage_Fail_01");
        AudioClipManager.GetInstance().GenerateAudioClip("MageFail_Voice_2", "Voice/Fail/Mage_Fail_02");
        AudioClipManager.GetInstance().GenerateAudioClip("MageFail_Voice_3", "Voice/Fail/Mage_Fail_03");
        AudioClipManager.GetInstance().GenerateAudioClip("MageFail_Voice_4", "Voice/Fail/Mage_Fail_04");
        AudioClipManager.GetInstance().GenerateAudioClip("MageFail_Voice_5", "Voice/Fail/Mage_Fail_05");
        AudioClipManager.GetInstance().GenerateAudioClip("MageFail_Voice_6", "Voice/Fail/Mage_Fail_06");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorFail_Voice_1", "Voice/Fail/Warrior_Fail_01");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorFail_Voice_2", "Voice/Fail/Warrior_Fail_02");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorFail_Voice_3", "Voice/Fail/Warrior_Fail_03");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorFail_Voice_4", "Voice/Fail/Warrior_Fail_04");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherWin_Voice_1", "Voice/Win!/Archer_Win_01");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherWin_Voice_2", "Voice/Win!/Archer_Win_02");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherWin_Voice_3", "Voice/Win!/Archer_Win_03");
        AudioClipManager.GetInstance().GenerateAudioClip("ArcherWin_Voice_4", "Voice/Win!/Archer_Win_04");
        AudioClipManager.GetInstance().GenerateAudioClip("MageWin_Voice_1", "Voice/Win!/Mage_Win_01");
        AudioClipManager.GetInstance().GenerateAudioClip("MageWin_Voice_2", "Voice/Win!/Mage_Win_02");
        AudioClipManager.GetInstance().GenerateAudioClip("MageWin_Voice_3", "Voice/Win!/Mage_Win_03");
        AudioClipManager.GetInstance().GenerateAudioClip("MageWin_Voice_4", "Voice/Win!/Mage_Win_04");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorWin_Voice_1", "Voice/Win!/Warrior_Win_01");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorWin_Voice_2", "Voice/Win!/Warrior_Win_02");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorWin_Voice_3", "Voice/Win!/Warrior_Win_03");
        AudioClipManager.GetInstance().GenerateAudioClip("WarriorWin_Voice_4", "Voice/Win!/Warrior_Win_04");
    }
}
