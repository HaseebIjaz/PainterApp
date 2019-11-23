using System;
using System.Windows;

namespace PainterApp.BusinessLogic.Algorithms
{
    /// <summary>
    /// PlaneAlgoriths Class contains Algorithms related to 2D Plane Geometry
    /// </summary>
    public static class PlaneAlgorithms
    {
        /// <summary>
        /// Returns Signed Difference between Y coordinates
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <returns></returns>
        public static double YDisplacement(Point P1, Point P2)
        {
            return P2.Y - P1.Y;
        }
        /// <summary>
        /// Returns Signed Difference between X coordinates
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <returns></returns>
        public static double XDisplacement(Point P1, Point P2)
        {
            return P2.X - P1.X;
        }
        /// <summary>
        /// Returns UnSigned Difference between Y coordinates
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <returns></returns>
        public static double YDistance(Point P1, Point P2)
        {
            return Math.Abs(YDisplacement(P1, P2));
        }
        /// <summary>
        /// Returns UnSigned Difference between X coordinates
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <returns></returns>
        public static double XDistance(Point P1, Point P2)
        {
            return Math.Abs(XDisplacement(P1, P2));
        }
        /// <summary>
        /// Calculates maxY and minX for calculating Top Left Point Using Left Diagonal Points
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <returns></returns>
        public static Point CalculateTopLeftPoint(Point P1, Point P2)
        {
            var offsetX = XDisplacement(P1, P2);
            var offsetY = YDisplacement(P1, P2);

            var maxY = offsetY > 0 ? P1.Y : P2.Y;
            var minX = offsetX < 0 ? P2.X : P1.X;

            Point topLeftPoint = new Point(minX, maxY);
            return topLeftPoint;
        }
    }


}
