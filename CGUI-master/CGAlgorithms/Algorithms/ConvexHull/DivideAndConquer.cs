using CGUtilities;
using System.Collections.Generic;
using System.Linq;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class DivideAndConquer : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
              
        }

        public static List<Point> Divide(List<Point> divisiblePoints) {

            if (divisiblePoints.Count <= 1)
            {
                return divisiblePoints;
            }
            List<Point> rightPoints = new List<Point>();
            List<Point> leftPoints = new List<Point>();

            for (int i = 0; i < divisiblePoints.Count/2; i++)
            {
                rightPoints.Add(divisiblePoints[i]);
            }
            for (int i = divisiblePoints.Count / 2; i < divisiblePoints.Count; i++) { 

                leftPoints.Add(divisiblePoints[i]);
            }
            return Conquer(rightPoints, leftPoints);
        }
        public static List<Point> Conquer(List<Point> rightPoints, List<Point> leftPoints)
        { 
        
        
        }

        public override string ToString()
        {
            return "Convex Hull - Divide & Conquer";
        }

    }
}