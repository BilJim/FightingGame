﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class BinaryReaderExtension
{
    public static Color32 ReadColor32(this BinaryReader binaryReader)
    {
        return new Color32(binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte(),
            binaryReader.ReadByte());
    }

    public static Color ReadColor(this BinaryReader binaryReader)
    {
        return new Color(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(),
            binaryReader.ReadSingle());
    }

    public static DateTime ReadDateTime(this BinaryReader binaryReader)
    {
        return new DateTime(binaryReader.ReadInt64());
    }

    public static Quaternion ReadQuaternion(this BinaryReader binaryReader)
    {
        return new Quaternion(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(),
            binaryReader.ReadSingle());
    }

    public static Rect ReadRect(this BinaryReader binaryReader)
    {
        return new Rect(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(),
            binaryReader.ReadSingle());
    }

    public static Vector2 ReadVector2(this BinaryReader binaryReader)
    {
        return new Vector2(binaryReader.ReadSingle(), binaryReader.ReadSingle());
    }

    public static Vector3 ReadVector3(this BinaryReader binaryReader)
    {
        return new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
    }

    public static Vector4 ReadVector4(this BinaryReader binaryReader)
    {
        return new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(),
            binaryReader.ReadSingle());
    }

    public static List<Vector2> ReadVector2List(this BinaryReader binaryReader)
    {
        List<Vector2> resultList = new();
        //集合个数
        int count = binaryReader.ReadInt32();
        for (int i = 0; i < count; i++)
        {
            resultList.Add(new Vector2(binaryReader.ReadSingle(), binaryReader.ReadSingle()));
        }
        return resultList;
    }
}