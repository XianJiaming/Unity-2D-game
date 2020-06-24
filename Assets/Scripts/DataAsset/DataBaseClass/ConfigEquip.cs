
using System;
using System.Collections;
using System.Collections.Generic;

namespace DataClass
{
    public class ConfigEquip{
        public int id;
        public string name;
        public Dictionary<int,int> attributes;
        public int equipType;
        public int level;
        public int[] effects;
    }
}