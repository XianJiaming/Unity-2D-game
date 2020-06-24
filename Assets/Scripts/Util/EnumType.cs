public class EnumType
{
   

    //队列角色变化类型
    public enum RoleUpdateType
    {
        //有的角色变化是不需要处理个人属性的

        AddFight,// 直接在战斗队列中生成一个角色（不是从其他地方来的）
        RemoveFight,//直接删除（售卖）战斗队列的角色
        FightToPre, //两个队列变化（两个队列交换）

        Fight, //战斗队列内部变化（战斗队列之间交换）
       // Pre, // 准备队列内部变化（准备队列之间交换, 也用于直接在准备队列中增加、删除等)
    }



    public enum RoleListType
    {
        Pre,
        Fight
    }
    public enum FightListType
    {
        Player,
        Ai
    }
}


   

