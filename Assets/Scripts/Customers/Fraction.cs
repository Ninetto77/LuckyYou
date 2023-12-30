using System;
using UnityEngine;

[Serializable]
public class Fraction
{
    [Tooltip("Имя фракции")]
    public string FractionName;

    [Tooltip("Время ожидания за прилавком")]
    [Range(1f, 120f)] public float WaitingTimeToOrder;

    [Tooltip("Время ожидания заказа")]
    [Range(1f, 120f)] public float OrderWaitingTime;

    [Tooltip("Вероятность мусора")]
    [Range(1f, 100f)] public float GarbageChance;

    [Tooltip("Вероятность появления фракции")]
    [Range(1f, 100f)] public int CreateChance;

    [Tooltip("Время обед")]
    [Range(1f, 60f)] public float EatingTime;

    [Tooltip("Время выполнения (WC)")]
    [Range(1f, 60f)] public float DoingTime;

    [Tooltip("Репутация")]
    public float Reputation;

    [Tooltip("Фракция")]
    public FromFraction IsFraction;

    [Tooltip("Босс фракции")]
    public GameObject Boss;
}
