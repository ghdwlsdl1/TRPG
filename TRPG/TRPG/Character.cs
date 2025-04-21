using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Character
{
    public string Name = ""; // 이름
    public int JobNames = 0; // 직업 선택 번호
    public string[] Job = { "없음", "전사", "도적", "마법사", "야만인" };// 직업이름
    public int Hp = 0; // 현재 HP
    public int HpMax = 0;// 최대 HP
    public int Mp = 0; // 현재 MP
    public int MpMax = 0; //최대 MP
    public int Level = 1; //레벨 기초 값1
    public int experience = 0; //경험치

    public int Str = 0;  // 힘
    public int Dex = 0;  // 민첩
    public int Int = 0;  // 지능
    public int Con = 0;  // 건강
    public int Wis = 0;  // 지혜
    public int Luk = 0;  // 운
    public int startStr = 0;  // 힘
    public int startDex = 0;  // 민첩
    public int startInt = 0;  // 지능
    public int startCon = 0;  // 건강
    public int startWis = 0;  // 지혜
    public int startLuk = 0;  // 운

    public int Atk = 0; //공격력
    public int Def = 0; // 방어력

    public int Money = 2000;

    //====================마을 시스템====================
    public bool duty = false;
    public int Day = 1;
    //====================아이탬====================
    public string[] weapon = { "없음", "무딘 검", "강철 검", "전투용 망치", "단검", "지팡이" };// 아이탬 이름
    public bool[] weaponTf = { false, false, false, false, false, false }; // 소지 여부
    public bool[] weaponEquip = { false, false, false, false, false, false }; // 장착 여부
    public int[] weaponAtk = { 0, 2, 5, 7, 3, 2 }; // 추가 공격력
    public int[] weaponStats = { 0, 0, 0, 5, 5, 7 }; // 추가 스텟
    public int[] weaponDeal = { 0, 1000, 3000, 5000, 5000, 5000 }; // 금액

    public string[] assist = { "없음", "철제 장화", "철제 투구", "행운의 부적", "빛나는 반지", "태양 목걸이" };
    public bool[] assistTf = { false, false, false, false, false, false };
    public bool[] assistEquip = { false, false, false, false, false, false };
    public int[] assistDef = { 0, 1, 2, 0, 0, 0 }; // 추가 방어력
    public int[] assistStats = { 0, 0, 0, 3, 5, 5 }; // 추가 스텟
    public int[] assistDeal = { 0, 2000, 4000, 5000, 5000, 5000 }; // 금액

    public string[] armor = { "없음", "낡은 갑옷", "가죽 갑옷", "사슬 갑옷", "강철 갑옷", "전사의 판금 갑옷" };
    public bool[] armorTf = { false, false, false, false, false, false };
    public bool[] armorEquip = { false, false, false, false, false, false };
    public int[] armorDef = { 0, 1, 3, 5, 7, 10 }; // 추가 방어력
    public int[] armorDeal = { 0, 1000, 2000, 3000, 4000, 5000 }; // 금액

}
