using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Stats : Character
{
    public void UpdateLevel()
    {
        int experienceMax = 10 * Level * Level;
        if (experience >= experienceMax)
        {
            Level++;
            experience -= experienceMax;
        }
    }


    public void UpdateHpMax()
    {
        HpMax = Con * 2;
        if (Hp > HpMax)
        {
            Hp = HpMax;
        }
    }

    public void UpdateMpMax()
    {
        MpMax = Wis * 2;
        if (Mp > MpMax)
        {
            Mp = MpMax;
        }
    }

    public void UpdateStr()
    {
        int weaponBonus = 0;
        if (weaponEquip[3])
        {
            weaponBonus = weaponStats[3];
        }

        Str = startStr + weaponBonus + Level;
    }

    public void UpdateDex()
    {
        int weaponBonus = 0;
        if (weaponEquip[4])
        {
            weaponBonus += weaponStats[4];
        }

        Dex = startDex + weaponBonus + Level;
    }

    public void UpdateInt()
    {
        int weaponBonus = 0;
        if (weaponEquip[5])
        {
            weaponBonus += weaponStats[5];
        }

        Int = startInt + weaponBonus + Level;
    }

    public void UpdateCon()
    {
        int assistBonus = 0;
        if (assistEquip[5])
        {
            assistBonus += assistStats[5];
        }

        Con = startCon + assistBonus + Level;
    }

    public void UpdateWis()
    {
        int assistBonus = 0;
        if (assistEquip[4])
        {
            assistBonus += assistStats[4];
        }
        Wis = startWis + assistBonus + Level;
    }

    public void UpdateLuk()
    {
        int assistBonus = 0;
        if (assistEquip[3])
        {
            assistBonus += assistStats[3];
        }
        Luk = startLuk + assistBonus;
    }

    public void UpdateAtk()
    {
        int weaponBonus = 0;

        for (int i = 0; i < weaponEquip.Length; i++)
        {
            if (weaponEquip[i])
            {
                weaponBonus += weaponAtk[i];
            }
        }

        Atk = 1 + Str + weaponBonus;
    }

    public void UpdateDef()
    {
        int assistBonus = 0;
        int armorBonus = 0;

        for (int i = 0; i < assistEquip.Length; i++)
        {
            if (assistEquip[i])
            {
                assistBonus += assistDef[i];
            }
        }
        for (int i = 0; i < armorEquip.Length; i++)
        {
            if (armorEquip[i])
            {
                armorBonus += armorDef[i];
            }
        }
        Def = Con / 3 + armorBonus + assistBonus;
    }
    public void UpdateStats()
    {
        UpdateLevel();

        UpdateStr();
        UpdateDex();
        UpdateInt();
        UpdateCon();
        UpdateWis();
        UpdateLuk();

        UpdateHpMax();
        UpdateMpMax();
        UpdateAtk();
        UpdateDef();
    }
    //====================던전 시스템====================
    public int dungeonDay = 3;
    public int dungeonHour = 0;
    //====================던전 맵====================
    public int playerX = -1; //포탈좌표x
    public int playerY = -1; //포탈좌표x
    public int portalX = -1; //포탈좌표x
    public int portalY = -1; //포탈좌표y
    public int floor = 1; //층수
    public char[,] map; //

    //====================주사위====================
    public Random random = new Random(); //랜덤
    public int dice20() //20면 주사위
    {
        return random.Next(1, 21) + Luk;
    }
    public int dice6() //6면 주사위
    {
        return random.Next(1, 7);
    }
}
