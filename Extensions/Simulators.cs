using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Extensions
{
	public static class Simulators
	{
		public static IEnumerable<double[]> BrownianMotion(int stepsN, int MaxStepLenght, double[] StartingPointXY)
		{
			RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
			double num = 0;
			double maxStepLenght = 0;
			double startingPointXY = StartingPointXY[0];
			double startingPointXY1 = StartingPointXY[1];
			double num1 = 0;
			double num2 = 0;
			byte[] numArray = new byte[stepsN * 2];
			rNGCryptoServiceProvider.GetBytes(numArray);
			for (int i = 0; i < stepsN; i++)
			{
				num = (double)numArray[i] * 360 / 255;
				maxStepLenght = (double)(numArray[stepsN + i] * MaxStepLenght) / 255;
				num1 = startingPointXY;
				num2 = startingPointXY1;
				startingPointXY = startingPointXY + maxStepLenght * Math.Cos(num);
				startingPointXY1 = startingPointXY1 + maxStepLenght * Math.Sin(num);
				yield return new double[] { num1, num2, startingPointXY, startingPointXY1 };
			}
		}

		public static IEnumerable<double[]> FastBrownianMotion(int stepsN, int MaxStepLenght, double[] StartingPointXY)
		{
			Random random = new Random(DateTime.Now.Millisecond);
			double num = 0;
			double startingPointXY = StartingPointXY[0];
			double startingPointXY1 = StartingPointXY[1];
			double num1 = 0;
			double num2 = 0;
			for (int i = 0; i < stepsN; i++)
			{
				num = (double)random.Next(360);
				num1 = startingPointXY;
				num2 = startingPointXY1;
				startingPointXY = startingPointXY + (double)(random.Next(MaxStepLenght) + 1) * Math.Cos(num);
				startingPointXY1 = startingPointXY1 + (double)(random.Next(MaxStepLenght) + 1) * Math.Sin(num);
				yield return new double[] { num1, num2, startingPointXY, startingPointXY1 };
			}
		}

		public static IEnumerable<double[]> TurtleWalk(this IEnumerable<int> numbers, short stepLenght, double[] StartingPointXY)
		{
			double num = 0;
			double startingPointXY = StartingPointXY[0];
			double startingPointXY1 = StartingPointXY[1];
			double num1 = 0;
			double num2 = 0;
			foreach (int num3 in numbers)
			{
				num = (double)(num3 % 360);
				num1 = startingPointXY;
				num2 = startingPointXY1;
				startingPointXY = startingPointXY + (double)stepLenght * Math.Cos(num);
				startingPointXY1 = startingPointXY1 + (double)stepLenght * Math.Sin(num);
				yield return new double[] { num1, num2, startingPointXY, startingPointXY1 };
			}
		}

		public static IEnumerable<double[]> TurtleWalkOnDifferences(this IEnumerable<int> numbers, short stepLenght, double[] StartingPointXY)
		{
			double num = 0;
			double startingPointXY = StartingPointXY[0];
			double startingPointXY1 = StartingPointXY[1];
			double num1 = 0;
			double num2 = 0;
			int num3 = 0;
			int num4 = numbers.First<int>();
			int num5 = 0;
			foreach (int num6 in numbers.Skip<int>(1))
			{
				num5 = num6 - num4;
				num4 = num6;
				Math.DivRem(num5, 360, out num3);
				num = (double)num3;
				num1 = startingPointXY;
				num2 = startingPointXY1;
				startingPointXY = startingPointXY + (double)stepLenght * Math.Cos(num);
				startingPointXY1 = startingPointXY1 + (double)stepLenght * Math.Sin(num);
				yield return new double[] { num1, num2, startingPointXY, startingPointXY1 };
			}
		}
	}
}