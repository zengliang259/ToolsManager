﻿/************************************************************
//     文件名      : ChildLuaUpdateExecutor.cs
//     功能描述    : 
//     负责人      : guoliang
//     参考文档    : 无
//     创建日期    : 03/15/2018
//     Copyright  : Copyright
**************************************************************/
public class ChildLuaUpdateExecutor:ChildUpdateExecutor
{
    public override string GetUpdateType()
    {
        return "Lua";
    }
}

