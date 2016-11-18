/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 04.11.2016
 * Time: 17:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace DataTypes
{
	/// <summary>
	/// Описание аналогового сигнала для архивации
	/// </summary>
	public class AnalogSignal : Signal
	{
		public float Value { get; set; }
		public int Size { get; set; }
	}
}
