﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClassCreater
{
    public static void Creat()
    {
        new PlayerModel();
        new AIModel();
        new GameLevelModel();
        new CooperationModel();
        new RoleShopModel();
        new EquipModel();
        new TalentModel();



        new WndGameStart();
        new WndFight();
        new WndTips();
        new WndCooper();
        new WndRoleShop();
        new WndPrepare();
        new WndFightOver();
        new WndEquip();
        new WndTalent();
    }
}

