using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpaceProject.ECS;

[AttributeUsage(AttributeTargets.Class)]
public class SystemsOrderAttribute : Attribute
{
    public SystemsOrderEnum orderType = SystemsOrderEnum.Standart;
    public byte priority = 0;

    public SystemsOrderAttribute(SystemsOrderEnum _orderType, byte p) 
    {
        orderType = _orderType;
        priority = p;
    }

    public SystemsOrderAttribute(string _orderType, byte p)
    {
        Enum.TryParse(_orderType, out SystemsOrderEnum temp);
        orderType = temp;
        priority = p;
    }

    public SystemsOrderAttribute(SystemsOrderEnum _orderType)
    {
        orderType = _orderType;
    }

    public SystemsOrderAttribute(string _orderType)
    {
        Enum.TryParse(_orderType, out SystemsOrderEnum temp);
        orderType = temp;
    }
}

public enum SystemsOrderEnum
{
    PrePhysics,
    Physics,
    PostPhysics,
    Standart
}