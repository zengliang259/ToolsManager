//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Text;


[InitializeOnLoad]
public class BuildDll  {
    private static int m_lastVersion = 1;
    private static string myDllName = "dwGame";
    static string dllName = "";
	[MenuItem ("EZFun/build/build xgame dll &%#d")] // & - alt, %-ctrl, # - shift
	public static void BuildXgameDll ()
	{	
		AssetDatabase.Refresh ();

		string datapath = Application.dataPath;
		string[] strArray = datapath.Split('/');
		string parentPath = "";
		for(int i = 0; i < strArray.Length - 1; i ++)
		{
			parentPath += strArray[i] + "/";
		}

		string projectCofig = parentPath + "Assembly-CSharp.csproj";
		ReplaceCofig(projectCofig);

		dllName = myDllName;
		string path = parentPath + "/buildDll.bat";
		System.Diagnostics.Process.Start(path, dllName); 
    }


	static void ForeachFile(string path, Action<FileInfo> genFile)
	{
		if (!Directory.Exists(path))
			return;
		
		foreach (var fileStr in (new DirectoryInfo(path)).GetFiles())
		{
			if(fileStr.Name.EndsWith("meta"))
			{
				continue;
			}
			genFile(fileStr);
		}
		foreach (var dirStr in Directory.GetDirectories(path))
		{
			if (Directory.Exists(dirStr))
			{
				ForeachFile(dirStr, genFile);
			}
		}
	}

	static void ReplaceCofig(string path)
	{
		string text = string.Empty;
		using (StreamReader reader = new StreamReader(path, Encoding.Default))
		{
			text = reader.ReadToEnd();
			reader.Close();
		}

		using (StreamWriter writer = new StreamWriter(path, false))
		{
            dllName = myDllName;
			ReplaceNode(ref text, "RootNamespace", dllName);
			ReplaceNode(ref text, "AssemblyName", dllName);

			text = text.Replace("<NoWarn>0169</NoWarn>", "<NoWarn>0169</NoWarn> \r\n <AllowUnsafeBlocks>true</AllowUnsafeBlocks>");
			text = text.Replace("<Compile Include=\"Assets\\XGame\\GameStartScrpits\\GameStart.cs\" />", "");

			string defineConstants = GetNoddeContext (text, "DefineConstants");
			defineConstants = defineConstants.Replace ("DEBUG;", "");
			defineConstants = defineConstants.Replace ("UNITY_EDITOR;", "");
            defineConstants += ";FEAT_COMPILER";

            text = Replace (text, "<DebugType>pdbonly</DebugType>", "<DebugType>pdbonly</DebugType>\r\n <DefineConstants>" + defineConstants + "</DefineConstants>");
			writer.Write(text);
			writer.Close();
		}
	}

	static void ReplaceNode(ref string text, string nodeName, string content)
	{
		string leftStr = "<" + nodeName + ">";
		string rightStr = "</" + nodeName + ">";
		int left = text.IndexOf(leftStr) + nodeName.Length + 2;
		int right = text.IndexOf(rightStr);

		string nodeCotent = text.Substring(left, right - left);
		string oldStr = leftStr + nodeCotent + rightStr;
		string newStr = leftStr + content + rightStr;
		text = text.Replace(oldStr, newStr);
	}	

	static string Replace(string text, string oldcontent, string newContent)
	{

		int startIndex = text.IndexOf (oldcontent);

		int secStartIndex = startIndex + oldcontent.Length;

		string fStr = text.Substring (0, startIndex);

		string tStr = text.Substring (secStartIndex);

		return fStr + newContent + tStr;
	}

	static string GetNoddeContext(string text, string nodeName)
	{
		string leftStr = "<" + nodeName + ">";
		string rightStr = "</" + nodeName + ">";

		int left = text.IndexOf(leftStr) + nodeName.Length + 2;
		int right = text.IndexOf (rightStr, left);
		return text.Substring (left, right - left);
	}
    
    
    public static void BuildGameDll()
	{
		UnityEngine.Debug.Log ("Sync C# Project");
		EditorApplication.ExecuteMenuItem ("Assets/Open C# Project");

		AssetDatabase.Refresh ();

		AutoBuildDll ();
	}

	private static void AutoBuildDll()
	{
		string datapath = Application.dataPath;
		string[] strArray = datapath.Split('/');
		string parentPath = "";
		for(int i = 0; i < strArray.Length - 1; i ++)
		{
			parentPath += strArray[i] + "/";
		}

		string projectCofig = parentPath + "Assembly-CSharp.csproj";
		var dllName = ReplaceCofigStr(projectCofig);

		string path = parentPath + "/buildDll.bat";
		System.Diagnostics.Process.Start(path, dllName); 
	}

	static string ReplaceCofigStr(string path)
	{
		string dllName = null;

		string text = string.Empty;
		using (StreamReader reader = new StreamReader(path, Encoding.Default))
		{
			text = reader.ReadToEnd();
			reader.Close();
		}

		using (StreamWriter writer = new StreamWriter(path, false))
		{
            dllName = myDllName;
            ReplaceNodeStr(ref text, "RootNamespace", dllName);
            ReplaceNodeStr(ref text, "AssemblyName", dllName);

			text = text.Replace("<NoWarn>0169</NoWarn>", "<NoWarn>0169</NoWarn> \r\n <AllowUnsafeBlocks>true</AllowUnsafeBlocks>");
			text = text.Replace("<Compile Include=\"Assets\\XGame\\GameStartScrpits\\GameStart.cs\" />", "");

			string defineConstants = GetNoddeContextStr(text, "DefineConstants");
			defineConstants = defineConstants.Replace ("DEBUG;", "");
			defineConstants = defineConstants.Replace ("UNITY_EDITOR;", "");
			defineConstants += ";FEAT_COMPILER";

			text = ReplaceStr(text, "<DebugType>pdbonly</DebugType>", "<DebugType>pdbonly</DebugType>\r\n <DefineConstants>" + defineConstants + "</DefineConstants>");
			writer.Write(text);
			writer.Close();
		}

		return dllName;
	}

	static string GetNoddeContextStr(string text, string nodeName)
	{
		string leftStr = "<" + nodeName + ">";
		string rightStr = "</" + nodeName + ">";

		int left = text.IndexOf(leftStr) + nodeName.Length + 2;
		int right = text.IndexOf (rightStr, left);
		return text.Substring (left, right - left);
	}

	static void ReplaceNodeStr(ref string text, string nodeName, string content)
	{
		string leftStr = "<" + nodeName + ">";
		string rightStr = "</" + nodeName + ">";
		int left = text.IndexOf(leftStr) + nodeName.Length + 2;
		int right = text.IndexOf(rightStr);

		string nodeCotent = text.Substring(left, right - left);
		string oldStr = leftStr + nodeCotent + rightStr;
		string newStr = leftStr + content + rightStr;
		text = text.Replace(oldStr, newStr);
	}	

	static string ReplaceStr(string text, string oldcontent, string newContent)
	{

		int startIndex = text.IndexOf (oldcontent);

		int secStartIndex = startIndex + oldcontent.Length;

		string fStr = text.Substring (0, startIndex);

		string tStr = text.Substring (secStartIndex);

		return fStr + newContent + tStr;
	}

}
