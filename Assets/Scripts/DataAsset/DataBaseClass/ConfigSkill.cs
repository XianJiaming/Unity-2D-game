
using System;
using System.Collections;
using System.Collections.Generic;

namespace DataClass
{
    public class ConfigSkill{
        public int id;
        public string name;
        public int[] targetType;
        public int targetQueue;
        public Dictionary<int,int[]> level1;
        public Dictionary<int,int[]> level2;
        public Dictionary<int,int[]> level3;
        public string selfEffect;
        public string wholeEffect;
        public string targetEffect;
        public string desc;
    }
}