using System;
using UnityEngine;

[Serializable]
public class Fraction
{
    [Tooltip("��� �������")]
    public string FractionName;

    [Tooltip("����� �������� �� ���������")]
    [Range(1f, 120f)] public float WaitingTimeToOrder;

    [Tooltip("����� �������� ������")]
    [Range(1f, 120f)] public float OrderWaitingTime;

    [Tooltip("����������� ������")]
    [Range(1f, 100f)] public float GarbageChance;

    [Tooltip("����������� ��������� �������")]
    [Range(1f, 100f)] public int CreateChance;

    [Tooltip("����� ����")]
    [Range(1f, 60f)] public float EatingTime;

    [Tooltip("����� ���������� (WC)")]
    [Range(1f, 60f)] public float DoingTime;

    [Tooltip("���������")]
    public float Reputation;

    [Tooltip("�������")]
    public FromFraction IsFraction;

    [Tooltip("���� �������")]
    public GameObject Boss;
}
