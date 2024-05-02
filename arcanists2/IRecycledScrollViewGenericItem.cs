

using UnityEngine;

#nullable disable
public interface IRecycledScrollViewGenericItem
{
  void Init(object obj, int index);

  GameObject gameObject { get; }
}
