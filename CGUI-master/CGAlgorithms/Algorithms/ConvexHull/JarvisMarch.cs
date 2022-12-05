using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class JarvisMarch : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            int numOfPoints = points.Count;

            // If Number of points is less than three return points as a Extreme Point.
            if (numOfPoints < 3)
            {
                outPoints = points;
                return;
            }


            double minY = 10000000, minX = 0;
            for (int i = 0; i < points.Count; i++)
                if (points[i].Y < minY) {
                    minY = points[i].Y;
                    minX = points[i].X;
                }
            

            Point minPoint = new Point(minX, minY);
            outPoints.Add(minPoint);
            Point start = minPoint;

            //Extra point adjacent to the first minimum point
            Point extraPoint = new Point(minX - 20, minY);


            while (true)
            {
                double largestTheta = 0;
                double distance = 0;
                double largestDistance = 0;
                Point nextPoint = minPoint;

                for (int i = 0; i < points.Count; i++)
                {
                    Point minPoint_extraPoint = new Point(minPoint.X - extraPoint.X, minPoint.Y - extraPoint.Y);
                    Point minPoint_nextPoint = new Point(points[i].X - minPoint.X, points[i].Y - minPoint.Y);

                    // Calculate Dot and Cross Product
                    double dotProduct = DotProduct(minPoint_extraPoint, minPoint_nextPoint);
                    double crossProdict = HelperMethods.CrossProduct(minPoint_extraPoint, minPoint_nextPoint);
                   
                    // Calculate The Angle and the Distance between minPoint_extraPoint and minPoint_nextPoint
                    double Theta = Math.Atan2(crossProdict, dotProduct);
                    distance = Distance(minPoint, points[i]);

                    // if theta is (-ve) -> Convert Theta to (+ve)
                    if (Theta < 0)
                        Theta = Theta + (2 * Math.PI);
                    // Get Largest Theta
                    if (Theta > largestTheta)
                    {
                        largestTheta = Theta;
                        largestDistance = distance;
                        nextPoint = points[i];
                    }
                    // if We find two point in the same line we shoud take the largest point based on distance
                    else if (Theta == largestTheta && distance > largestDistance)
                    {
                        largestDistance = distance;
                        nextPoint = points[i];
                    }
                }

                if (start.X == nextPoint.X && start.Y == nextPoint.Y)
                    break;

                outPoints.Add(nextPoint);
                extraPoint = minPoint;
                minPoint = nextPoint;
            }
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) + (p1.Y - p2.Y));
        }

        private double DotProduct(Point p1, Point p2)
        {
            return (p1.X * p2.X) + (p1.Y * p2.Y);
        }

        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}

