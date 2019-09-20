using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Google.Protobuf;

public class ProtoSerialize : MonoBehaviour
{
    public static byte[] Serialize<T>(T obj) where T : IMessage
    {
        return obj.ToByteArray();
    }
    public static T Deserialize<T>(byte[] data) where T : class, IMessage, new()
    {
        T obj = new T();
        IMessage message = obj.Descriptor.Parser.ParseFrom(data);
        return message as T;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
