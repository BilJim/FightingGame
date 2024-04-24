//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2024-04-24 00:14:30.999
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace FightingGame
{
    /// <summary>
    /// 敌人表。
    /// </summary>
    public class DATAMonster : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取敌人编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

		/// <summary>
		/// 获取攻击力。
		/// </summary>
		public int Attack
		{
			get;
			private set;
		}

		/// <summary>
		/// 获取最大血量。
		/// </summary>
		public int MaxHP
		{
			get;
			private set;
		}

		/// <summary>
		/// 获取移动速度。
		/// </summary>
		public float MoveSpeed
		{
			get;
			private set;
		}

		/// <summary>
		/// 获取移动前等待时间。
		/// </summary>
		public float MoveWaitTime
		{
			get;
			private set;
		}

		/// <summary>
		/// 获取跳跃速度。
		/// </summary>
		public float JumpSpeed
		{
			get;
			private set;
		}

		/// <summary>
		/// 获取重力加速度。
		/// </summary>
		public int GSpeed
		{
			get;
			private set;
		}

		/// <summary>
		/// 获取出生点。
		/// </summary>
		public Vector2 BornPos
		{
			get;
			private set;
		}

		/// <summary>
		/// 获取巡逻目标点。
		/// </summary>
		public List<Vector2> PatrolPosList
		{
			get;
			private set;
		}

		/// <summary>
		/// 获取触发攻击状态的范围。
		/// </summary>
		public float AtkRange
		{
			get;
			private set;
		}

		public override bool ParseDataRow(string dataRowString, object userData)
		{
			string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
			for (int i = 0; i < columnStrings.Length; i++)
			{
				columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
			}

			int index = 0;
			index++;
			m_Id = int.Parse(columnStrings[index++]);
			index++;
			Attack = int.Parse(columnStrings[index++]);
			MaxHP = int.Parse(columnStrings[index++]);
			MoveSpeed = float.Parse(columnStrings[index++]);
			MoveWaitTime = float.Parse(columnStrings[index++]);
			JumpSpeed = float.Parse(columnStrings[index++]);
			GSpeed = int.Parse(columnStrings[index++]);
			BornPos = DataTableExtension.ParseVector2(columnStrings[index++]);
			PatrolPosList = DataTableExtension.ParseVector2List(columnStrings[index++]);
			AtkRange = float.Parse(columnStrings[index++]);

			GeneratePropertyArray();
			return true;
		}

		public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
		{
			using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
				{
					m_Id = binaryReader.Read7BitEncodedInt32();
					Attack = binaryReader.Read7BitEncodedInt32();
					MaxHP = binaryReader.Read7BitEncodedInt32();
					MoveSpeed = binaryReader.ReadSingle();
					MoveWaitTime = binaryReader.ReadSingle();
					JumpSpeed = binaryReader.ReadSingle();
					GSpeed = binaryReader.Read7BitEncodedInt32();
					BornPos = binaryReader.ReadVector2();
					PatrolPosList = binaryReader.ReadVector2List();
					AtkRange = binaryReader.ReadSingle();
				}
			}

			GeneratePropertyArray();
			return true;
		}

		private void GeneratePropertyArray()
		{

		}
    }
}
