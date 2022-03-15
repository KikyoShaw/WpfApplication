using System;

namespace EasingFunction
{
    public class AnimateBase
    {
        public static double Back(double x, LEaseMode easeMode)
        {
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    return 2.70158 * x * x * x - 1.70158 * x * x;
                case LEaseMode.EaseOut:
                    return 1 + 2.70158 * Math.Pow(x - 1, 3) + 1.70158 * Math.Pow(x - 1, 2);
                case LEaseMode.EaseInOut:
                    return x < 0.5 ? (Math.Pow(2 * x, 2) * (7.189819 * x - 2.5949095)) / 2 : (Math.Pow(2 * x - 2, 2) * (7.189819 * x - 4.5949095) + 2) / 2;
                default:
                    return 0;
            }
        }

        private static double BounceOut(double x)
        {
            if (x < 1d / 2.75d) return 7.5625 * x * x;
            else if (x < 2d / 2.75d) return 7.5625 * (x -= 1.5d / 2.75d) * x + 0.75;
            else if(x<2.5d/2.75d) return 7.5625 * (x -= 2.25d / 2.75d) * x + 0.9375;
            else return 7.5625 * (x -= 2.625d / 2.75d) * x + 0.984375;
        }

        public static double Bounce(double x, LEaseMode easeMode)
        {
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    return 1 - BounceOut(1 - x);
                case LEaseMode.EaseOut:
                    return BounceOut(x);
                case LEaseMode.EaseInOut:
                    return x < 0.5 ? (1 - BounceOut(1 - 2 * x)) / 2 : (1 + BounceOut(2 * x - 1)) / 2;
                default:
                    return 0;
            }
        }

        public static double Circle(double x, LEaseMode easeMode)
        {
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    return 1 - Math.Sqrt(1 - Math.Pow(x, 2));
                case LEaseMode.EaseOut:
                    return Math.Sqrt(1 - Math.Pow(x - 1, 2));
                case LEaseMode.EaseInOut:
                    return x < 0.5 ? (1 - Math.Sqrt(1 - Math.Pow(2 * x, 2))) / 2 : (Math.Sqrt(1 - Math.Pow(-2 * x + 2, 2)) + 1) / 2;
                default:
                    return 0;
            }
        }

        public static double Power(double x,LEaseMode easeMode,int power)
        {
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    return Math.Pow(x, power);
                case LEaseMode.EaseOut:
                    return 1-Math.Pow(1-x,power);
                case LEaseMode.EaseInOut:
                    return x < 0.5 ? Math.Pow(2,power-1)*Math.Pow(x,power) : 1 - Math.Pow(-2 * x + 2, power) / 2;
                default:
                    return 0;
            }
        }

        public static double Quadratic(double x, LEaseMode easeMode)
        {
            return Power(x, easeMode, 2);
        }

        public static double Cubic(double x, LEaseMode easeMode)
        {
            return Power(x, easeMode, 3);
        }

        public static double Quartic(double x, LEaseMode easeMode)
        {
            return Power(x, easeMode,4);
        }

        public static double Quintic(double x, LEaseMode easeMode)
        {
            return Power(x, easeMode, 5);
        }

        public static double Elastic(double x, LEaseMode easeMode)
        {
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    return x == 0 ? 0 : x == 1 ? 1 : -Math.Pow(2, 10 * x - 10) * Math.Sin((x * 10 - 10.75) * 2 * Math.PI / 3);
                case LEaseMode.EaseOut:
                    return x == 0 ? 0 : x == 1 ? 1 : Math.Pow(2, -10 * x) * Math.Sin((x * 10 - 0.75) * 2 * Math.PI / 3)+1;
                case LEaseMode.EaseInOut:
                    return x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? -(Math.Pow(2, 20 * x - 10) * Math.Sin((x * 20 - 11.125) * 2 * Math.PI / 4.5)) / 2 : (Math.Pow(2, -20 * x + 10) * Math.Sin((x * 20 - 11.125) * 2 * Math.PI / 4.5)) / 2 + 1;
                default:
                    return 0;
            }
        }

        public static double Exponential(double x, LEaseMode easeMode)
        {
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    return x == 0 ? 0 : Math.Pow(2, 10 * x - 10); 
                case LEaseMode.EaseOut:
                    return x == 1 ? 1 : 1 - Math.Pow(2, -10 * x);
                case LEaseMode.EaseInOut:
                    return x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? Math.Pow(2, 20 * x - 10) / 2 : (2 - Math.Pow(2, -20 * x + 10)) / 2;
                default:
                    return 0;
            }
        }

        public static double Sine(double x, LEaseMode easeMode)
        {
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    return 1 - Math.Cos((x * Math.PI) / 2);
                case LEaseMode.EaseOut:
                    return Math.Sin((x * Math.PI) / 2);
                case LEaseMode.EaseInOut:
                    return -(Math.Cos(Math.PI * x) - 1) / 2;
                default:
                    return 0;
            }
        }

    }
}
