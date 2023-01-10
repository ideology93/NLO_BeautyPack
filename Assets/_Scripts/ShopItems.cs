using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems
{
    public enum TableType
    {
        yellow,
        red
    }
     public enum BoxType
    {
        yellow,
        pink
    }
    public static int GetTablePrice(TableType tableType)
    {
        switch (tableType)
        {
            default:
            case(TableType.yellow): return 0;
            case(TableType.red): return 50;
        }
    }
        public static int GetBoxPrice(BoxType boxType)
    {
        switch (boxType)
        {
            default:
            case(BoxType.yellow): return 0;
            case(BoxType.pink): return 50;
        }
    }
    
}
