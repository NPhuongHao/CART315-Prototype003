using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    public static string WALLG = "wallgreen";
    public static string WALLO = "wallorange";
    public static string WALLB = "wallblue";
    public static string FRUITG = "fruitgreen";
    public static string FRUITB = "fruitblue";
    public static string FRUITO = "fruitorange";
    public static string BOMB = "bomb";
    public static string TAIL = "TAIL";
  
} // tags

public class Metrics
{
    public static float NODE = 0.2f;
} //metrics

public enum PlayerDirection
{
    LEFT = 0,
    UP = 1,
    RIGHT = 2,
    DOWN = 3,
    COUNT = 4
} //player direction
