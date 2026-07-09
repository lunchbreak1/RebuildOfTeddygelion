using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SaveManager
{
    private static readonly byte[] Key =
        Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");

    private static readonly byte[] IV =
        Encoding.UTF8.GetBytes("0123456789ABCDEF");

    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, "save.dat");

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);

        byte[] encrypted = Encrypt(json);

        File.WriteAllBytes(SavePath, encrypted);
    }

    public static SaveData Load()
    {
        if (!File.Exists(SavePath))
            return null;

        byte[] encrypted = File.ReadAllBytes(SavePath);

        string json = Decrypt(encrypted);

        return JsonUtility.FromJson<SaveData>(json);
    }

    private static byte[] Encrypt(string plainText)
    {
        using Aes aes = Aes.Create();

        aes.Key = Key;
        aes.IV = IV;

        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using StreamWriter sw = new(cs);

        sw.Write(plainText);

        sw.Close();

        return ms.ToArray();
    }

    private static string Decrypt(byte[] encryptedBytes)
    {
        using Aes aes = Aes.Create();

        aes.Key = Key;
        aes.IV = IV;

        using MemoryStream ms = new(encryptedBytes);
        using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader sr = new(cs);

        return sr.ReadToEnd();
    }

    public static void SaveAll(List<SaveData> saveList)
    {
        SaveCollection collection = new SaveCollection();
        collection.saves = saveList;

        string json = JsonUtility.ToJson(collection);

        byte[] encrypted = Encrypt(json);

        File.WriteAllBytes(SavePath, encrypted);
    }

    public static List<SaveData> LoadAll()
    {
        if (!File.Exists(SavePath))
            return new List<SaveData>();

        byte[] encrypted = File.ReadAllBytes(SavePath);

        string json = Decrypt(encrypted);

        SaveCollection collection = JsonUtility.FromJson<SaveCollection>(json);

        return collection.saves;
    }
}