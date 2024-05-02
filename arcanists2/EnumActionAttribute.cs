

using System;
using UnityEngine;

#nullable disable
[AttributeUsage(AttributeTargets.Method)]
public class EnumActionAttribute : PropertyAttribute
{
  public System.Type enumType;

  public EnumActionAttribute(System.Type enumType) => this.enumType = enumType;
}
